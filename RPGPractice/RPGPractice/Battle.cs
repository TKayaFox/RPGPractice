using System;
using System.Xml.Linq;

namespace RPGPractice
{
    public class Battle
    {
        //=========================================
        //                Variables
        //=========================================
        private Mob[] villians;
        private Mob[] heroes;
        EventManager eventManager;
        private int combatLevel;

        //=========================================
        //              Main Methods
        //=========================================
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="heroes"></param>
        /// <param name="combatLevel"></param>
        public Battle(Mob[] heroes, int combatLevel)
        {
            //Populate hero and villians for battle
            this.heroes = heroes;
            this.villians = GenerateEncounter();
            this.combatLevel = combatLevel;
            this.eventManager = eventManager;

            //EDIT: Load sprites onto BAttlefield 
        }

        public void RollInitiative()
        {
            throw new System.NotImplementedException();
        }

        public void NextTurn()
        {
            throw new System.NotImplementedException();
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
            return mobArr.All(mob => !mob.IsAlive());
        }


        public Mob[] GenerateEncounter()
        {
            //EDIT: Implement
        }

        public void NewMob()
        {
            throw new System.NotImplementedException();
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
        public event EventHandler<BattleEndEventArgs> BattleEnd;
        public event System.EventHandler Death;
        public event EventHandler<BattleEventArgs>? BattleEvent;

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

        public void OnHit()
        {
            throw new System.NotImplementedException();
        }

        public void OnBattleEnd(bool victory)
        {
            //EDIT: Unsubscribe from all Mob events

            //EDIT:Raise event telling GUI and Game that battle ended and win or loss
        }

        //=========================================
        //                Event Handlers
        //=========================================

        public void OnAttack_Handler()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Checks for Battle end state every time a Mob dies 
        /// (If all villians or all heroes are dead)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeath_Handler(object sender, EventArgs e)
        {
            //Check if all villians are dead
            if (AreMobsDead(heroes))
            {
                bool victory = false;
                OnBattleEnd(victory);
            }

            //Check if all heroes are dead
            else if (AreMobsDead(villians) )
            {
                bool victory = true;
                OnBattleEnd(victory);
            }           
        }
    }
}