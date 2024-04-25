using RPGPractice.Events;
using RPGPractice.MobClasses;

namespace RPGPractice
{
    public partial class GameForm : Form
    {
        //=========================================
        //              Variables
        //=========================================
        #region Variables
        private BattleField battlefield;
        private EventManager eventManager;

        #endregion

        //=========================================
        //              Main Methods
        //=========================================
        #region Main Methods
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventManager"></param>
        public GameForm(EventManager eventManager)
        {
            InitializeComponent();
            this.eventManager = eventManager;
        }

        /// <summary>
        /// Starts a new Game
        /// </summary>
        public void NewGame()
        {
            //EDIT: Get a name for the game save
            //String saveName = 

            //Call NewGame event
            //OnNewGame(saveName);
        }
        #endregion

        //=========================================
        //                Events
        //=========================================
        public event System.EventHandler Attack;

        #region Events
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
        }

        public void OnNewGame(String saveName)
        {
            //edit: package saveName into eventargs

            //edit: raise newGame event to tell engine to start a new game
        }
#endregion

        //=========================================
        //             Event Handlers
        //=========================================
        #region Event Handlers

        public void OnBattleStart_Handler(object sender, BattleStartEventArgs args)
        {
            //edit: unpack relevent data from BattleStartEventArgs

            Mob[] heroes = args.Heroes;
            Mob[] villians = args.Villians;

            //Initialize battleField then add it to eventManager
            battlefield = new BattleField(heroes, villians);
            battlefield.ManageEvents(eventManager);
        }

        /// <summary>
        /// On battle end, unsubscribe and close the existing battlefield
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnBattleEnd_Handler(object sender, BattleEndEventArgs e)
        {
            //edit: Unpublish and Unsubscribe from all subscribers for Battle
            battlefield.UnManageEvents(eventManager);

            //Edit: hide battlefield from form
        }

#endregion

    }
}
