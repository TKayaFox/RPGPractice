using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RPGPractice.Engine.MobClasses;
using RPGPractice.Events;

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
            random= new Random();
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

            // Build heroes EDIT: Modify with actual hero layout
            for (int i = 0; i < heroes.Length; i++)
            {
                //Initialize Hero and give a uniqueID
                Mob newHero= new Warrior($"Warrior {i}", random);
                newHero.UniqueID = i;

                //Subscribe and add to array
                newHero.ManageEvents(eventManager);
                heroes[i] = newHero;
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
            //stop publishing battle and unsubscribe it from all events
            battle.UnManageEvents(eventManager);

            //IF result of battle was player victory, keep looping
            if (victory)
            {
                //Increment victory count

                //EDIT: heal heroes;
            }
            else
            {
                //Edit: End game logic, save result to leaderboard, etc
            }

            //Edit: Save game data
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

            //Actually Start Battle logic
            battle.Start(eventManager);
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
        /// Publishes MobData and subscribes to all events
        /// Refactor: Remove if not in use
        /// </summary>
        /// <param name="eventManager"></param>
        public void ManageEvents()
        {
            //publish events to eventManager
            eventManager.Publish(this);

            //edit: Subscribe to any needed events
            eventManager.BattleEnd += OnBattleEnd_Handler;
            eventManager.NewGame += OnNewGame_Handler;
            eventManager.PlayerAction += OnPlayerAction_handler;
        }

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

        public void OnPlayerAction_handler(object sender, PlayerActionEventArgs playerAction)
        {
            //edit:Unpack Event
            //playerAction.Attacker
            //playerAction.Target
            ActionEnum action = playerAction.Action;
        }

        #endregion
    }
}