﻿using System;
using System.Xml.Linq;

namespace RPGPractice
{
    public class Battle
    {
        private Mob[] villians;
        private Mob[] heroes;
        private int experience;


        public event System.EventHandler BattleEnd;
        public event System.EventHandler Death;
        public event EventHandler<BattleEventArgs>? BattleEvent;

        //Setters/Getters
        public Mob[] Villians { get => villians; set => villians = value; }
        public Mob[] Heroes { get => heroes; set => heroes = value; }

        public Battle(Mob[] heroes)
        {
            //Populate hero and villians for battle
            this.heroes = heroes;
            this.villians = GenerateEncounter();

            //Load sprites onto BAttlefield EDIT

            //Subscribe to all Mob events for all Mobs
            SubscribeMobEvents(this.heroes);
            SubscribeMobEvents(this.villians);
        }

        //Subscribes all Mob events for a single Mob or an array of Mobs
        private void SubscribeMobEvents(Mob[] mobArr)
        {
            foreach (Mob mob in mobArr)
            {
                SubscribeMobEvents(mob);
            }
        }
        private void SubscribeMobEvents(Mob mob)
        {
            mob.BattleEvent += OnBattleEvent_Aggregator;
            mob.Death += OnDeath_Aggregator;
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

        public void OnDeath()
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
        /// When any Battle Event is raised, this re-raises the event for GUI
        ///     This way we dont have to keep subscribing/unsubscribing the GUI to events every new battle/character
        /// </summary>
        /// <param name="output"></param>
        private void OnBattleEvent_Aggregator(object sender, BattleEventArgs e)
        {
            //Relay the event
            BattleEvent?.Invoke(sender, e);
        }

        /// <summary>
        /// Aggregates Death events, and checks for end game state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeath_Aggregator(object sender, BattleEventArgs e)
        {
            //Relay event
            Death?.Invoke(sender, e);
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