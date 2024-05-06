using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RPGPractice.Core.Events;
using RPGPractice.Engine.MobClasses;
using RPGPractice.Engine.MobClasses.EnemyMobs;
using RPGPractice.Engine.MobClasses.HeroMobs;
using RPGPractice;
using RPGPractice.Core;

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

        #region Invokable Events
        public event EventHandler<EventManagerEventArgs> ManageObject;
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
            MessageBox.Show("Please enter your name.")

            numWins = 4;

            //Subscribe to eventManager (handles relaying and subscribing to events)
            eventManager.ManageObjectSort(this,true);
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
                //Note: Heroes not subscribed into eventManager until Battle
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
        private void BattleEnd(bool victory)
        {
            //IF result of battle was player victory, keep looping
            if (victory)
            {
                Victory();
            }
            else
            {
                //Display Game over and start wrap up process (ClearGameData handles all Game over logic)
                MessageBox.Show("Game Over");
                UpdateLeaderBoard();
                ClearGameData();
            }
        }

        private void UpdateLeaderBoard()
        {
            //Get todays date
            string today = DateTime.Today.ToString("MM-dd-yyyy");

            //Get all game data together and save to leaderboard
            string gameResult;
            gameResult = $"{today} | Rounds won: {numWins}";

            FileManager.AddToLeaderBoard(gameResult);
        }

        private void Victory()
        {
            //Slightly heal party (but not fully)
            foreach (Mob hero in heroes)
            {
                hero.Heal(5);
            }

            //Increment victory count
            numWins++;
            MessageBox.Show("Victory!");

            //Start a new battle
            NewBattle();
        }

        /// <summary>
        /// Resets GameEngine data for reinitialization Doesnt actually
        /// </summary>
        private void ClearGameData()
        {
            //TODO: save game status to file
            //TODO: Update leaderboard if game finished (Game over)

            //If battle is instantiated, make sure to unmanage it
            UnManageBattle();
        }

        /// <summary>
        /// Starts a new Battle encounter
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public async Task NewBattle()
        {
            //TODO: Revive all heroes

            //Initialize a new encounter and Battle object
            CombatEncounter encounter = new CombatEncounter(MAX_ENEMIES, numWins, random);
            encounter.Heroes = heroes;

            //initialize Battle
            battle = new Battle(encounter);

            OnManageObject(battle, true);

            //TODO: Revisit task logic. Is it necessary/can it be improved

            //Actually Start Battle logic
            await battle.Start();
        }

        private void UnManageBattle()
        {
            if (battle != null)
            {
                //unload all mobs
                battle.OnManageMobs(false);
                
                //unload self
                OnManageObject(battle, false);
            }
        }

        #endregion


        //=========================================
        //              Events
        //=========================================
        #region Events


        /// <summary>
        /// Raises event telling EventManager to setup all subscriptions for this class
        /// </summary>
        /// <param name="subscriber"></param>
        public void OnManageObject(Object target, bool isActive)
        {
            EventManagerEventArgs args = new EventManagerEventArgs();
            args.IsActive = isActive;
            args.AddTarget = target;
            ManageObject.Invoke(target, args);
        }
        #endregion

        #region Event Handlers


        /// <summary>
        /// When a battle has ended, stop managing for battle and save game data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnBattleResult_Handler(object sender, BattleResultEventArgs args)
        {
            //unload Battle
            UnManageBattle();

            //run game over logic
            BattleEnd(args.Victory);
        }

        /// <summary>
        /// Start a new Game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnNewGame_Handler(object sender, EventArgs args)
        {
            //Reset GameEngine so it is ready for new game then start new game
            NewGame();
        }


        #endregion
    }
}