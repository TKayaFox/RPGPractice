using System;
using System.Xml.Linq;
using RPGPractice.Events;
using RPGPractice.MobClasses;

namespace RPGPractice.Engine
{
    public class Battle
    {
        //=========================================
        //                Variables
        //=========================================
        private Mob[] villians;
        private Mob[] heroes;
        Random random;
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

            villians = GenerateEncounter();

            //EDIT: Load sprites onto Battlefield 
        }

        public void RollInitiative()
        {
            throw new NotImplementedException();
        }

        public void NextTurn()
        {
            throw new NotImplementedException();
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

        /// <summary>
        /// Initializes an array of Mobs for a combat encounter
        /// </summary>
        /// <returns>Mob[] array of villians/NPCs</returns>
        public Mob[] GenerateEncounter()
        {
            //EDIT: Implement actuall encounter variation depending on Combat Level
            Mob[] villians = new Mob[3];
            
            for (int i = 0; i < villians.Length; i++)
            {
                villians[i] = new Bandit(random);
            }
            return villians;
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
        public event EventHandler<BattleEventArgs>? BattleEvent;
        public event EventHandler<BattleStartEventArgs> BattleStart;
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

        public void OnAttack_Handler()
        {
            throw new NotImplementedException();
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
            else if (AreMobsDead(villians))
            {
                bool victory = true;
                OnBattleEnd(victory);
            }
        }
    }
}