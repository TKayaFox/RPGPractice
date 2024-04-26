using System;
using System.Collections.Generic;
using System.Xml.Linq;
using RPGPractice.Engine.MobClasses;
using RPGPractice.Events;

namespace RPGPractice.Engine
{
    public class Battle
    {
        //=========================================
        //                Variables
        //=========================================
        private Mob[] villians;
        private Mob[] heroes;
        private Mob currentTurn;
        private Initiative initiative; 
        private Random random;
        private int combatLevel;

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
            //Populate hero and villians for battle
            this.heroes = heroes;
            this.combatLevel = combatLevel;
            this.random = random;
        }

        public void Start(EventManager eventManager)
        {
            //EDIT: Load sprites onto Battlefield 

            //EDIT: Load background to battlefield

            //subscribe to Events
            ManageEvents(eventManager);


            //setup initiative order and villians
            villians = GenerateEncounter(eventManager);
            initiative = new Initiative(heroes, villians);

            //Start Game
            OnBattleStart();
            NextTurn();
        }

        /// <summary>
        /// Either tell NPCs to take their turn OR get Player input
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void NextTurn()
        {

            currentTurn = initiative.NextTurn();

            //If currentMob is Not player controlled tell it to take it's turn
            if (currentTurn is NPC npc)
            {
                // Tell the NPC to take its turn
                npc.TakeTurn(heroes);
            }
            //Else display Action buttons for user and wait for players move
            else
            {
                OnPlayerTurn();
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
        /// <returns>Mob[] array of villians/NPCs</returns>
        public Mob[] GenerateEncounter(EventManager eventManager)
        {
            //EDIT: Implement actuall encounter variation depending on Combat Level
            Mob[] villians = new Mob[3];
            
            for (int i = 0; i < villians.Length; i++)
            {
                Mob villain = new Warrior($"Warrior {i}", random);
                villain.ManageEvents(eventManager);
                villians[i] = villain;
            }
            return villians;
        }

        /// <summary>
        /// Checks for Battle end state every time a Mob dies 
        /// (If all villians or all heroes are dead)
        /// </summary>
        private void EndGame()
        {

            //Check if all villians are dead
            if (AreMobsDead(heroes))
            {
                bool victory = false;
                OnBattleEnd(victory);
            }

            //Check if all heroes are dead
            else if (AreMobsDead(villians))
            {
                bool victory = true;
                OnBattleEnd(victory);
            }
        }

        /// <summary>
        /// Tell attacker Mob to attack target
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="target"></param>
        private void Attack(Mob attacker, Mob target)
        {
            attacker.Attack(target);
        }

        //=========================================
        //             Getters/Setters
        //=========================================

        public Mob[] Villians { get => villians; set => villians = value; }
        public Mob[] Heroes { get => heroes; set => heroes = value; }

        //=========================================
        //                Events
        //=========================================
        public event EventHandler<BattleStartEventArgs> BattleStart;
        public event EventHandler<PlayerTurnEventArgs> PlayerTurn;
        public event EventHandler<BattleEndEventArgs> BattleEnd;

        public void OnBattleStart()
        {
            BattleStartEventArgs args = new BattleStartEventArgs();
            args.Villians = villians;
            args.Heroes = heroes;

            BattleStart?.Invoke(this, args);
        }

        public void OnBattleEnd(bool victory)
        {
            //EDIT: Unsubscribe from all Mob events

            //EDIT:Raise event telling GUI and Game that battle ended and win or loss
        }

        public void OnPlayerTurn()
        {
            PlayerTurnEventArgs args = new PlayerTurnEventArgs();
            args.Mob = currentTurn;

            PlayerTurn?.Invoke(this, args);
        }

        //=========================================
        //                Event Handlers
        //=========================================

        /// <summary>
        /// Publishes Class and subscribes to all events
        /// </summary>
        /// <param name="eventManager"></param>
        public void ManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            eventManager.Publish(this);

            //Subscribe to events from eventManager
            eventManager.Death += OnDeath_Handler;
        }

        /// <summary>
        /// UnPublishes Class and unsubscribes from all events
        /// </summary>
        /// <param name="eventManager"></param>
        public void UnManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            eventManager.Unpublish(this);

            //unSubscribe to any needed events
            eventManager.Death -= OnDeath_Handler;


            //edit: Unpublish all villian Mobs
            foreach (Mob villian in villians)
            {
                villian.UnManageEvents(eventManager);
            }
        }

        /// <summary>
        /// Check for EndGame when someone dies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeath_Handler(object sender, EventArgs e)
        {
            EndGame();
        }
    }
}