using RPGPractice.Engine;
using RPGPractice.Engine.MobClasses;
using RPGPractice.Events;
using RPGPractice.GUI;

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
        #region Public Methods
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventManager"></param>
        public GameForm(EventManager eventManager)
        {
            InitializeComponent();
            this.eventManager = eventManager;

            ManageEvents();

            //start Game
            OnNewGame(this, EventArgs.Empty);
        }

        #endregion

        #region Private Methods

        private void NewBattle(List<MobData> mobDataList)
        {
            //Initialize battleField then add it to eventManager
            battlefield = new BattleField(mobDataList);
            battlefield.ManageEvents(eventManager);
            Controls.Add(battlefield);
        }

        /// <summary>
        /// Read in txt file and displays it as a MessageBox
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private void txtToMessageBox(string fileName, string header)
        {

            String data = "";

            try
            {
                StreamReader reader = new StreamReader(fileName);

                while (!reader.EndOfStream)
                {
                    data += reader.ReadLine() + "\n";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Display information in messagebox
            MessageBox.Show(data, header, MessageBoxButtons.OK);
        }

        #endregion

        //=========================================
        //                Events
        //=========================================
        public event System.EventHandler NewGame;

        #region Events
        /// <summary>
        /// Starts a new Game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNewGame(object sender, EventArgs e)
        {
            NewGame?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Displays contents of About.txt for user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //read data from about and display in message box
            txtToMessageBox("About.txt", "About");
        }
        #endregion

        //=========================================
        //             Event Handlers
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
            eventManager.BattleStart += OnBattleStart_Handler;
        }

        public void OnBattleStart_Handler(object sender, BattleStartEventArgs args)
        {
            //unpack relevent data from BattleStartEventArgs
            List<MobData> mobDataList = args.MobDataList;

            //Send to appropriate Method
            NewBattle(mobDataList);
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
