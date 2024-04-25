using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGPractice.Events;
using RPGPractice.MobClasses;

namespace RPGPractice.Engine
{
    public class GameEngine
    {
        //=========================================
        //                Variables
        //=========================================
        #region Variables
        private const int NUM_HEROES = 3;

        private Mob[] heroes;
        private Battle battle;
        private EventManager eventManager;
        private Random random;

        private string name;
        private int numWins;

        #endregion

        //=========================================
        //              Main Methods
        //=========================================
        #region Main Methods
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventManager"></param>
        public GameEngine(EventManager eventManager)
        {

            this.eventManager = eventManager;

            //subscribe to events
            ManageEvents();

            //Setup player's Party of heroes
            heroes = BuildHeroes();

            //Start a new Battle
            NewBattle();
        }

        /// <summary>
        /// Returns an array of Hero Mobs for player control
        /// </summary>
        private Mob[] BuildHeroes()
        {
            Mob[] heroes = new Mob[NUM_HEROES];

            // Build heroes EDIT: Modify with actual hero layout
            for (int i = 0; i < heroes.Length; i++)
            {
                heroes[i] = new Warrior($"Warrior {i}", random);
            }

            return heroes;
        }

        /// <summary>
        /// Starts a new Battle encounter
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void NewBattle()
        {
            //edit: Revive all party members to full hp

            //Initialize a new Battle object
            Battle battle = new Battle(heroes, numWins, random);

            //Add new Battle object to eventManager
            eventManager.Publish(battle);
        }

        #endregion


        //=========================================
        //              Events
        //=========================================
        #region Events
        #endregion

        //=========================================
        //                Event Handlers
        //=========================================
        #region Event Handlers

        /// <summary>
        /// Publishes Class and subscribes to all events
        /// Refactor: Remove if not in use
        /// </summary>
        /// <param name="eventManager"></param>
        public void ManageEvents()
        {
            //publish events to eventManager
            eventManager.Publish(this);

            //edit: Subscribe to any needed events
            eventManager.BattleEnd += OnBattleEnd_Handler;
        }

        /// <summary>
        /// When a battle has ended, stop managing for battle and save game data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnBattleEnd_Handler(object sender, BattleEndEventArgs e)
        {
            //stop publishing battle and unsubscribe it from all events
            battle.UnManageEvents(eventManager);

            //IF result of battle was player victory, keep looping
            if (e.Victory == true)
            {
                //Increment victory count

                //EDIT: heal heroes;

                //Edit: Start new Battle
            }
            else
            {
                //Edit: End game logic, save result to leaderboard, etc
            }
        }

        #endregion
    }
}