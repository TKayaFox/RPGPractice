using RPGPractice.Core;
using RPGPractice.Core.Events;
using RPGPractice.Core.Events.Args;
using RPGPractice.Engine;
using RPGPractice.Engine.MobClasses;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RPGPractice
{

    /// <summary>
    /// GameForm Form
    /// Developer: Taylor Fox
    /// Works as the Main GUI Form for the RPG Game, with menu components and loading screens as needed
    /// </summary>
    public partial class GameForm : Form
    {
        //=========================================
        //              Variables
        //=========================================
        #region Variables
        private BattleScreen battlefield;
        #endregion

        #region Invokable Events
        public event EventHandler<EventManagerEventArgs> ManageObject;
        public event EventHandler NewGame;
        #endregion

        //=========================================
        //              Main Methods
        //=========================================
        #region Public Methods
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventManager"></param>
        public GameForm(EventManager eventManager)
        {
            InitializeComponent();

            //Subscribe to eventManager (handles relaying and subscribing to events)
            eventManager.ManageObjectSort(this, true);

            //start Game
            OnNewGame(this, EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void NewBattle(List<MobData> mobDataList)
        {
            //if battlefield has already been initialized unregister it from eventmanager
            if (battlefield != null)
            {
                OnManageObject(battlefield, false);
                battlefield.Unload();
                Controls.Remove(battlefield);
                battlefield = null;
            }

            //Initialize battleField then add it to eventManager
            battlefield = new BattleScreen();
            OnManageObject(battlefield, true);
            battlefield.Populate(mobDataList);
            Controls.Add(battlefield); battlefield.Visible = true;
        }

        #endregion

        //=========================================
        //                Events
        //=========================================

        #region Events
        /// <summary>
        /// Starts a new Game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNewGame(object sender, EventArgs e)
        {
            NewGame.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Displays contents of About.txt for user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get About txt as a string
            string aboutTxt = FileManager.GetAbout;

            //Display information in messagebox
            MessageBox.Show(aboutTxt);
        }
        #endregion

        #region Event Invokers
        public void OnManageObject(object target, bool isActive)
        {
            EventManagerEventArgs args = new EventManagerEventArgs();
            args.AddTarget = target;
            args.IsActive = isActive;
            ManageObject.Invoke(this, args);
        }
        #endregion

        #region Event Handlers
        public void OnNewBattle_Handler(object? sender, NewBattleEventArgs args)
        {
            //unpack relevent data from NewBattleEventArgs
            List<MobData> mobDataList = args.MobDataList;

            //get rid of old battle information
            UnloadBattleField();

            //Send to appropriate Method
            NewBattle(mobDataList);
        }

        private void UnloadBattleField()
        {
            if (battlefield != null)
            {
                //unload BattleScreen
                OnManageObject(battlefield, false);
                battlefield.Unload();
                Controls.Remove(battlefield);
                battlefield = null;
            }
        }
        #endregion
    }
}
