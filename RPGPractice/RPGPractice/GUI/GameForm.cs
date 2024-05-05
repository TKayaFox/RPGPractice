using RPGPractice.Core;
using RPGPractice.Core.Events;
using RPGPractice.Engine;
using RPGPractice.Engine.MobClasses;
using RPGPractice.GUI;

namespace RPGPractice
{
    public partial class GameForm : Form
    {
        //=========================================
        //              Variables
        //=========================================
        #region Variables
        private const string ABOUT = "About.txt";

        private BattleField battlefield;
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
            }

            //Initialize battleField then add it to eventManager
            battlefield = new BattleField();
            OnManageObject(battlefield, true);
            battlefield.Populate(mobDataList);
            Controls.Add(battlefield); battlefield.Visible = true;
        }

        /// <summary>
        /// Read in txt file and displays it as a MessageBox
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private void TxtToMessageBox(string fileName, string header)
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
                System.Diagnostics.Debug.WriteLine("MessageBox error: "+ex.Message);
            }

            //Display information in messagebox
            MessageBox.Show(data, header, MessageBoxButtons.OK);
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
            //read data from about and display in message box
            TxtToMessageBox(ABOUT, "About");
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
        public void OnBattleInitialize_Handler(object? sender, BattleInitializeEventArgs args)
        {
            //unpack relevent data from BattleInitializeEventArgs
            List<MobData> mobDataList = args.MobDataList;

            //Send to appropriate Method
            NewBattle(mobDataList);
        }
        #endregion
    }
}
