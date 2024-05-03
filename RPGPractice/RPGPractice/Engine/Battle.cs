using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using RPGPractice.Core.Enumerations;
using RPGPractice.Core.Events;
using RPGPractice.Engine.MobClasses;
using RPGPractice.GUI;

namespace RPGPractice.Engine
{
    public class Battle 
    {
        //=========================================
        //                Variables
        //=========================================
        
        //Some items throw exceptions on purpose and attempt to loop until solved
        //  MAX_EXCEPTIONS prevents infinite loops
        private const int MAX_EXCEPTIONS = 5;

        private Mob[] enemies;
        private Mob[] heroes;
        private Mob currentTurn;
        private Initiative initiative;
        private Random random;
        private int combatLevel;
        private Dictionary<int, Mob> mobDictionary;

        //TODO: Implement Block Logic
        //TODO: Implement Special Attack Logic

        //=========================================
        //              Main Methods
        //=========================================
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="heroes"></param>
        /// <param name="combatLevel"></param>
        public Battle(Mob[] heroes, int combatLevel, Random random)
        {
            //Populate hero and enemies for battle
            this.heroes = heroes;
            this.combatLevel = combatLevel;
            this.random = random;
        }

        public async Task Start(EventManager eventManager)
        {

            //setup initiative order and enemies
            enemies = GenerateEncounter(eventManager);
            initiative = new Initiative(heroes, enemies);

            //setup MobID dictionary for easy reference
            mobDictionary = new Dictionary<int, Mob>();

            //add heroes and villains to dictionary
            AddToDictionary(heroes, mobDictionary);
            AddToDictionary(enemies, mobDictionary);

            //subscribe to Events
            ManageEvents(eventManager);

            //Start Gui logic
            OnBattleStart();
        }

        /// <summary>
        /// Adds an array of mobs to dictionary using uniqueID 
        /// </summary>
        /// <param name="mobArr"></param>
        /// <param name="dictionary"></param>
        private static void AddToDictionary(Mob[] mobArr, Dictionary<int, Mob> dictionary)
        {
            //add each mobID to dictionary using uniqueID for index
            foreach (Mob mob in mobArr)
            {
                dictionary.Add(mob.UniqueID, mob);
            }
        }

        /// <summary>
        /// Either tell NPCs to take their turn OR get Player input
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void NextTurn()
        {
            //Determine who is next in initiative, but skip dead mobs.
            bool isAlive = false;
            while (!isAlive)
            {
                //get next MobID in initiative
                int uniqueID = initiative.NextTurn();
                currentTurn = mobDictionary[uniqueID];
                isAlive = currentTurn.IsAlive;
                System.Diagnostics.Debug.WriteLine($"Current Turn: {currentTurn.Name} [{isAlive}]");
            }

            //Make lists of all targetable mobs on both sides
            List<MobData> heroTargetList = new List<MobData>();
            List<MobData> enemyTargetList = new List<MobData>();
            GetTargetableMobs(heroTargetList, enemyTargetList);

            //Tell mob to take it's turn
            //If NotSupportedException is thrown, an invalid selection has been made
            bool turnCompleted = false;
            int attempt = 0;
            do
            {
                try
                {
                    currentTurn.StartTurn(heroTargetList, enemyTargetList);
                }
                catch (NotSupportedException)
                {
                    //TODO: implement an error box stating whats wrong
                }
                turnCompleted = true;
                attempt++;
            } while (!turnCompleted && attempt < MAX_EXCEPTIONS);

            //TODO: If somehow the turn could not be completed notify user
        }

        private void GetTargetableMobs(List<MobData> heroTargetList, List<MobData> enemyTargetList)
        {
            //Only add living Mobs to Lists
            foreach (Mob mob in mobDictionary.Values)
            {
                if (mob.IsAlive)
                {
                    //Add to enemyTargetList if an NPC
                    if (mob is NPC)
                    {
                        enemyTargetList.Add(mob.MobData);
                    }
                    //Otherwise add to heroTargetList
                    else
                    {
                        heroTargetList.Add(mob.MobData);
                    }
                }
            }
        }

        /// <summary>
        /// Checks all mobs in an array if they are ALL dead
        /// returns true if zero Mobs are still alive
        /// </summary>
        /// <param name="mobArr"></param>
        /// <returns></returns>
        private static bool AreMobsDead(Mob[] mobArr)
        {
            // Simplified with LINQ for readability and performance
            return mobArr.All(mob => !mob.IsAlive);
        }

        /// <summary>
        /// Initializes an array of Mobs for a combat encounter
        /// </summary>
        /// <returns>MobID[] array of enemies/NPCs</returns>
        public Mob[] GenerateEncounter(EventManager eventManager)
        {
            //TODO: Implement actuall encounter scaling
            //      depending on Combat Level
            Mob[] enemies = new Mob[1];

            for (int i = 0; i < enemies.Length; i++)
            {
                Mob enemy = new Bandit($"Bandit {i}", random);
                enemy.ManageEvents(eventManager);
                enemy.UniqueID = i + 100;
                enemies[i] = enemy;
            }
            return enemies;
        }

        /// <summary>
        /// Checks for Battle end state every time a MobID dies 
        /// (If all enemies or all heroes are dead)
        /// </summary>
        private bool IsBattleEnd()
        {
            //Check if all heroes or villains are dead.
            //  Victory is only assured if at least one hero lives
            bool loss = AreMobsDead(heroes);
            bool battleEnd = loss || AreMobsDead(enemies);
            if (battleEnd)
            {
                OnBattleEnd(!loss); //OnBattleEnd uses victory not loss, so reverse the boolean
            }

            return battleEnd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobs"></param>
        /// <param name="mobDataList"></param>
        private static void CompileMobData(Mob[] mobs, List<MobData> mobDataList)
        {
            foreach (Mob mob in mobs)
            {
                MobData data = mob.MobData;
                mobDataList.Add(data);
            }
        }

        //=========================================
        //             Getters/Setters
        //=========================================

        public Mob[] Enemies { get => enemies; set => enemies = value; }
        public Mob[] Heroes { get => heroes; set => heroes = value; }

        //=========================================
        //                Events
        //=========================================
        public event EventHandler<BattleStartEventArgs> BattleStart;
        public event EventHandler<BattleEndEventArgs> BattleEnd;
        public event EventHandler<TurnEndEventArgs> TurnEnd;
        #region Event Managers

        /// <summary>
        /// Publishes MobData and subscribes to all events
        /// </summary>
        /// <param name="eventManager"></param>
        public void ManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            BattleStart += eventManager.OnBattleStart_Aggregator;
            BattleEnd += eventManager.OnBattleEnd_Aggregator;
            TurnEnd += eventManager.OnTurnEnd_Aggregator;

            //Subscribe to events from eventManager
            eventManager.PlayerAction += OnPlayerAction_handler;

            //subscribe to all mob TurnEnd events
            foreach (Mob mob in mobDictionary.Values)
            {
                mob.TurnEnd += OnTurnEnd_Handler;
            }
        }

        /// <summary>
        /// UnPublishes MobData and unsubscribes from all events
        /// </summary>
        /// <param name="eventManager"></param>
        public void UnManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            BattleStart -= eventManager.OnBattleStart_Aggregator;
            BattleEnd -= eventManager.OnBattleEnd_Aggregator;
            TurnEnd -= eventManager.OnTurnEnd_Aggregator;

            //unsubscribe events from eventManager
            eventManager.PlayerAction -= OnPlayerAction_handler;


            //unsubscribe all mob TurnEnd events
            foreach (Mob mob in mobDictionary.Values)
            {
                mob.TurnEnd -= OnTurnEnd_Handler;
            }
        }

        #endregion

        #region Event Invokers
        public void OnBattleStart()
        {
            BattleStartEventArgs args = new BattleStartEventArgs();

            //Package only needed MobID data into MobData object for sending to Gui
            Mob[] mobs;
            List<MobData> mobDataList = new List<MobData>();
            CompileMobData(heroes, mobDataList);
            CompileMobData(enemies, mobDataList);

            args.MobDataList = mobDataList;

            BattleStart.Invoke(this, args);
        }
        #endregion


        /// <summary>
        /// Called when either all heroes, or all villains have died.
        /// </summary>
        /// <param name="victory"></param>
        public void OnBattleEnd(bool victory)
        {
            BattleEndEventArgs args = new BattleEndEventArgs();
            args.Victory = victory;
            BattleEnd.Invoke(this, args);
        }

        //=========================================
        //                Event Handlers
        //=========================================

        /// <summary>
        /// At end of each turn, start the next turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTurnEnd_Handler(object? sender, TurnEndEventArgs turnEndData)
        {
            //Run through any pending TargetedActions
            string turnSummary = "";
            while (turnEndData.HasTargetedActions)
            {
                TargetedAbility ability = turnEndData.DequeueAction();

                string result = "";

                //Unpack detailsof ability and what it does
                MobData attackerData = ability.Attacker;
                MobData targetData = ability.Target;
                Mob target = mobDictionary[targetData.UniqueID];

                //
                int attackRoll = ability.AttackRoll;
                int damage = ability.Damage;
                DamageType damageType = ability.DamageType;

                //Determine what logic to follow
                switch (damageType)
                {
                    case DamageType.Physical:
                        result += $"\r\n{target.DefendPhysical(attackRoll, damage)}";
                        break;
                    case DamageType.Magic:
                        result += $"\r\n{target.DefendMagic(attackRoll, damage)}";
                        break;
                    case DamageType.Heal:
                        result += $"{ target.Heal(damage)} ";
                        break;
                }

                turnSummary += result;
            }

            turnEndData.TurnSummary += turnSummary;

            TurnEnd?.Invoke(sender, turnEndData);

            //check for game over
            if (IsBattleEnd())
            {

            }
            else
            {
                NextTurn();
            }
        }

        /// <summary>
        /// When a player chooses an action, implement it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="playerAction"></param>
        public void OnPlayerAction_handler(object sender, PlayerActionEventArgs playerAction)
        {
            //Unpack Args
            MobActions action = playerAction.Action;

            //determine type of action was selected and send the appropriate command
            switch (action)
            {
                case MobActions.Block:
                    currentTurn.Block();
                    break;
                case MobActions.Attack:
                    if (playerAction.TargetID != -1)
                    {
                        Mob target = mobDictionary[playerAction.TargetID];
                        currentTurn.Attack(target.MobData);
                    }
                    break;
                case MobActions.Special:
                    if (playerAction.TargetID != -1)
                    {
                        Mob target = mobDictionary[playerAction.TargetID];
                        currentTurn.Special(target.MobData);
                    }
                    break;
            }

        }
    }
}