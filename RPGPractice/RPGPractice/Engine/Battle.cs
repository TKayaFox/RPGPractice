using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using RPGPractice.Core;
using RPGPractice.Core.Enumerations;
using RPGPractice.Core.Events;
using RPGPractice.Engine.MobClasses;
using RPGPractice.Engine.MobClasses.EnemyMobs;

namespace RPGPractice.Engine
{
    /// <summary>
     /// FileManager class
     /// Developer: Taylor Fox
     /// Handles the Logic during 
     /// </summary>
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
        private Mob turnHolder;
        private Initiative initiative;
        private Dictionary<int, Mob> mobDictionary;

        #region Events
        public event EventHandler<NewBattleEventArgs> NewBattle;
        public event EventHandler<BattleResultEventArgs> BattleResult;
        public event EventHandler<TurnEndEventArgs> TurnEnd;
        public event EventHandler<EventManagerEventArgs> ManageObject;
        #endregion

        //=========================================
        //              Main Methods
        //=========================================

        public Battle(CombatEncounter encounter)
        {
            //Populate hero and enemies for battle
            this.heroes = encounter.Heroes;
            this.enemies = encounter.Enemies;
        }

        public async Task Start()
        {
            initiative = new Initiative(heroes, enemies);

            //setup MobID dictionary for easy reference
            mobDictionary = new Dictionary<int, Mob>();

            //add heroes and villains to dictionary
            AddToDictionary(heroes, mobDictionary);
            AddToDictionary(enemies, mobDictionary);

            //Start Gui battleForm logic
            OnNewBattle();

            //Register all mobs with EventManager
            OnManageMobs(true);
        }

        /// <summary>
        /// Adds an array of mobs to dictionary using uniqueID
        ///     Also sets mob to be managed by eventManager
        /// </summary>
        /// <param name="mobArr"></param>
        /// <param name="dictionary"></param>
        private void AddToDictionary(Mob[] mobArr, Dictionary<int, Mob> dictionary)
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
            //Check if all heroes or villains are dead.
            //  Victory is only assured if at least one hero lives
            bool loss = AreMobsDead(heroes);
            bool battleEnd = loss || AreMobsDead(enemies);
            if (battleEnd)
            {
                OnBattleResult(!loss); //OnBattleResult uses victory not loss, so reverse the boolean
            }
            else
            {
                AssignTurnHolder();
                TakeTurn();
            }
        }

        private void AssignTurnHolder()
        {
            //Determine who is next in initiative, but skip dead mobs.
            bool isAlive = false;
            while (!isAlive)
            {
                //get next MobID in initiative
                int uniqueID = initiative.NextTurn();
                turnHolder = mobDictionary[uniqueID];
                isAlive = turnHolder.IsAlive;
            }
        }

        /// <summary>
        /// Compile list of attackable targets and tell Mob to take its turn
        /// </summary>
        private void TakeTurn()
        {
            //Make lists of all targetable mobs on both sides
            List<MobData> heroTargetList = new List<MobData>();
            List<MobData> enemyTargetList = new List<MobData>();
            GetTargetableMobs(heroTargetList, enemyTargetList);

            //take turn
            turnHolder.StartTurn(heroTargetList, enemyTargetList);
        }

        private void GetTargetableMobs(List<MobData> heroTargetList, List<MobData> enemyTargetList)
        {
            //Only add living Mobs to Lists
            foreach (Mob mob in mobDictionary.Values)
            {
                if (mob.IsAlive)
                {
                    //Add to enemyTargetList if an NPC
                    if (mob is EnemyMob)
                    {
                        enemyTargetList.Add(mob.Data);
                    }
                    //Otherwise add to heroTargetList
                    else
                    {
                        heroTargetList.Add(mob.Data);
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
        /// 
        /// </summary>
        /// <param name="mobs"></param>
        /// <param name="mobDataList"></param>
        private static void CompileMobData(Mob[] mobs, List<MobData> mobDataList)
        {
            foreach (Mob mob in mobs)
            {
                MobData data = mob.Data;
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

        #region Event Invokers
        public void OnNewBattle()
        {
            NewBattleEventArgs args = new NewBattleEventArgs();

            //Package only needed MobID data into Data object for sending to Gui
            Mob[] mobs;
            List<MobData> mobDataList = new List<MobData>();
            CompileMobData(heroes, mobDataList);
            CompileMobData(enemies, mobDataList);

            args.MobDataList = mobDataList;

            NewBattle?.Invoke(this, args);
        }

        /// <summary>
        /// Called when either all heroes, or all villains have died.
        /// </summary>
        /// <param name="victory"></param>
        public void OnBattleResult(bool victory)
        {
            //UnManage Mobs (Cannot unmanage self from within the class without interfering with BattleResult
            OnManageMobs(false);

            //Invoke event
            BattleResultEventArgs args = new BattleResultEventArgs();
            args.Victory = victory;
            BattleResult?.Invoke(this, args);
        }

        public void OnTurnEnd(TurnEndEventArgs turnData)
        {
            //raise event then start next turn
            TurnEnd?.Invoke(this, turnData);
        }

        /// <summary>
        /// Unsubscribes all target and eventManager relationships for Mobs
        ///     If isActive is true, then re-subscribe to all relationships
        ///     Automatically prevents dual subscription
        /// </summary>
        /// <param name="target">Object that is to be Managed</param>
        /// <param name="isActive">
        /// - If true, subscription relationships between target and EventManager will be added/live/active
        /// - If false, the relationship will be severed/unsubscribed
        ///</param>
        public void OnManageMobs(bool isActive)
        {
            EventManagerEventArgs args = new EventManagerEventArgs();

            //Add Enemies and Heroes
            args.AddTargetArr = Enemies;
            args.AddTargetArr = Heroes;

            //Direct subscription/unsubscription to TurnEnd
            foreach (Mob mob in heroes)
            {
                mob.TurnEnd -= OnTurnEnd_Handler;
                if (isActive)
                {
                    mob.TurnEnd += OnTurnEnd_Handler;
                }
            }
            //Direct subscription/unsubscription to TurnEnd
            foreach (Mob mob in enemies)
            {
                mob.TurnEnd -= OnTurnEnd_Handler;
                if (isActive)
                {
                    mob.TurnEnd += OnTurnEnd_Handler;
                }
            }

            //Store whether values are being subscribed or unsubscribed
            args.IsActive = isActive;
            ManageObject?.Invoke(this, args);
        }
        /// <summary>
        /// Unsubscribes all target and eventManager relationships for self
        ///     If isActive is true, then re-subscribe to all relationships
        ///     Automatically prevents dual subscription
        /// </summary>
        /// <param name="target">Object that is to be Managed</param>
        /// <param name="isActive">
        /// - If true, subscription relationships between target and EventManager will be added/live/active
        /// - If false, the relationship will be severed/unsubscribed
        ///</param>
        public void OnManageMe(bool isActive)
        {
            EventManagerEventArgs args = new EventManagerEventArgs();

            //Store whether values are being subscrubed or unsubscribed
            args.AddTarget = this;
            args.IsActive = isActive;
            ManageObject.Invoke(this, args);
        }
        #endregion

        #region Event Handlers
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
                        result += $"{target.Heal(damage)} ";
                        break;
                }

                turnSummary += result;
            }

            turnEndData.TurnSummary += turnSummary;

            //relay event then start next turn
            OnTurnEnd(turnEndData);
            NextTurn();
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

            //If NotSupportedException is thrown, an invalid selection has been made
            try
            {
                PlayerAction(playerAction, action);
            }
            catch (NotSupportedException e)
            {
                //Tell user about the error, then allow them to try again
                MessageBox.Show(e.Message + "\r\nPlease choose another action!");
                TakeTurn();
            }

        }

        /// <summary>
        /// Called when Gui is ready and user selects NextTurn() button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnBattleStart_Handler(object sender, EventArgs e)
        {
            NextTurn();
        }

        private void PlayerAction(PlayerActionEventArgs playerAction, MobActions action)
        {
            //Check for valid target (see of dictionary contains the key
            bool validTarget = mobDictionary.ContainsKey(playerAction.TargetID);

            Mob target = null;
            if (validTarget)
            {
                target = mobDictionary[playerAction.TargetID];
            }

            //determine type of action was selected and send the appropriate command
            switch (action)
            {
                case MobActions.Block:
                    turnHolder.Block();
                    break;
                case MobActions.Attack:
                    //Ensure theres a target
                    if (validTarget)
                    {
                        turnHolder.Attack(target.Data);
                    }
                    else //throw exception telling user to re-try theit action
                    {
                        throw new NotSupportedException("Invalid Target");
                    }
                    break;
                case MobActions.Special:
                    //Ensure theres a target
                    if (validTarget)
                    {
                        turnHolder.Special(target.Data);
                    }
                    else //throw exception telling user to re-try theit action
                    {
                        throw new NotSupportedException("Invalid Target");
                    }
                    break;
            }
        }
        #endregion
    }
}