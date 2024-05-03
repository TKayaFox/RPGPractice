﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RPGPractice.Core;
using RPGPractice.Core.Events;
using RPGPractice.Engine.MobClasses;
using RPGPractice.Engine.MobClasses.EnemyMobs;
using RPGPractice.Engine.MobClasses.HeroMobs;
using RPGPractice.GUI;

namespace RPGPractice.Engine
{
    public class GameEngine
    {
        //=========================================
        //                Variables
        //=========================================
        #region Variables
        private const int NUM_HEROES = 3;
        private const int MAX_ENEMIES = 5;

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
            random = new Random();
            this.eventManager = eventManager;

            //subscribe to events
            ManageEvents();
        }

        /// <summary>
        /// Returns an array of Hero Mobs for player control
        /// </summary>
        private Mob[] BuildHeroes()
        {
            Mob[] heroes = new Mob[NUM_HEROES];

            heroes[0] = new Cleric("Cleric");
            heroes[1] = new Warrior("Warrior");
            heroes[2] = new Mage("Mage");

            // give each hero a uniqueId and publish them to eventmanager
            for (int i = 0; i < 3; i++)
            {
                heroes[i].UniqueID = i + 1;

                //Subscribe and add to array
                heroes[i].ManageEvents(eventManager);
            }

            return heroes;
        }

        private void NewGame()
        {
            //Setup player's Party of heroes
            heroes = BuildHeroes();

            //Start a new Battle
            NewBattle();
        }

        /// <summary>
        /// When a battle has ended, stop event managing for battle and save game data
        /// </summary>
        private void EndGame(bool victory)
        {
            //IF result of battle was player victory, keep looping
            if (victory)
            {
                //Increment victory count
                numWins++;
                MessageBox.Show("Victory!");

                //Start a new battle
                NewBattle();
            }
            else
            {
                //TODO: End game logic
                //  save result to leaderboard, etc
                MessageBox.Show("Game Over");
            }

            //TODO: Save game data
        }

        /// <summary>
        /// Starts a new Battle encounter
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public async Task NewBattle()
        {
            //TODO: Revive all heroes

            //Initialize a new encounter and Battle object
            Encounter encounter = new Encounter(MAX_ENEMIES, random);
            encounter.GenerateEncounter(eventManager, numWins);
            encounter.Heroes = heroes;

            //Reinitialize Battle
            if (battle != null)
            {
                battle.UnManageEvents(eventManager); // Ensure to unsubscribe from all events
            }
            battle = new Battle(encounter);

            //Actually Start Battle logic
            await battle.Start(eventManager);

            //let GUI catch up and dispaly
            await Task.Delay(2000);

            //Start turns
            battle.NextTurn();
        }

        #endregion


        //=========================================
        //              Events
        //=========================================
        #region Events
        #endregion

        #region Event Manager
        /// <summary>
        /// Publishes MobData and subscribes to all events
        /// TODO: refactor: Remove if not in use
        /// </summary>
        /// <param name="eventManager"></param>
        public void ManageEvents()
        {
            //Subscribe to any needed events
            eventManager.BattleEnd += OnBattleEnd_Handler;
            eventManager.NewGame += OnNewGame_Handler;
        }
        #endregion

        #region Event Handlers


        /// <summary>
        /// When a battle has ended, stop managing for battle and save game data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnBattleEnd_Handler(object sender, BattleEndEventArgs args)
        {
            EndGame(args.Victory);
        }

        /// <summary>
        /// Start a new Game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnNewGame_Handler(object sender, EventArgs args)
        {
            NewGame();
        }


        #endregion
    }
}