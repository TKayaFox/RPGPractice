using System;
using System.Xml.Linq;

namespace RPGPractice
{
    public class Battle
    {
        private Mob[] villians;
        private Mob[] heroes;
        EventManager eventManager;
        private int experience;
        private int combatLevel;


        public event System.EventHandler BattleEnd;
        public event System.EventHandler Death;
        public event EventHandler<BattleEventArgs>? BattleEvent;

        //Setters/Getters
        public Mob[] Villians { get => villians; set => villians = value; }
        public Mob[] Heroes { get => heroes; set => heroes = value; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="heroes"></param>
        /// <param name="combatLevel"></param>
        public Battle(Mob[] heroes, int combatLevel, EventManager eventManager)
        {
            //Populate hero and villians for battle
            this.heroes = heroes;
            this.villians = GenerateEncounter();
            this.combatLevel = combatLevel;
            this.eventManager = eventManager;

            //Load sprites onto BAttlefield EDIT

            //publish all mobs and battle to eventManager so events are relayed
            eventManager.Publish(this.heroes);
            eventManager.Publish(this.villians);
            eventManager.Publish(this);

            //Subscribe to events
            eventManager.Death += OnDeath_Handler;
        }

        public void RollInitiative()
        {
            throw new System.NotImplementedException();
        }

        public void NextTurn()
        {
            throw new System.NotImplementedException();
        }

        public void OnAttack_Handler()
        {
            throw new System.NotImplementedException();
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


        //Events
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
    }
}