using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPGPractice.Engine.MobClasses;
using RPGPractice.Events;
using RPGPractice.GUI;

namespace RPGPractice
{
    public partial class BattleField : UserControl
    {
        //=========================================
        //               Variables
        //=========================================
        private const int Max_Sprites = 5;

        PictureBox[] heroSprites;
        PictureBox[] villianSprites;
        Dictionary<int, MobData> mobDictionary;

        //=========================================
        //              Main Methods
        //=========================================
        #region Public Methods
        public BattleField(List<MobData> mobDataList)
        {
            InitializeComponent();
            mobDictionary = new Dictionary<int, MobData>();

            //Make arrays for PictureBoxes for easier Sprite Handling
            PictureBox[] heroSprites = new PictureBox[Max_Sprites];
            PictureBox[] villianSprites = new PictureBox[Max_Sprites];

            // Assign PictureBoxes to arrays using a loop
            for (int i = 0; i < Max_Sprites; i++)
            {
                heroSprites[i] = Controls.Find($"heroSprite{i + 1}", true).FirstOrDefault() as PictureBox;
                villianSprites[i] = Controls.Find($"villainSprite{i + 1}", true).FirstOrDefault() as PictureBox;
            }

            //Add all mobs into battlefield display and assign to PictureBoxes
            UpdateMobs(mobDataList);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// gets all initial data needed from mob array
        /// </summary>
        /// <param name="mobs"></param>
        private void UpdateMobs(List<MobData> mobDataList)
        {
            foreach (MobData data in mobDataList)
            {
                //Assign a PictureBox for the pixtureBox
                //determine if mob is a Hero or Villain(NPC) and assign to Sprite
                if (data.IsNPC)
                {
                    AssignSprite(data, villianSprites);
                }
                else
                {
                    AssignSprite(data, heroSprites);
                }

                //Add mob data to dictionary
                mobDictionary.Add(data.UniqueID, data);
            }
        }


        /// <summary>
        /// Assigns a pixtureBox to a picturebox in the picturebox array (Only if there is room)
        /// </summary>
        /// <param name="mob"></param>
        /// <param name="sprites"></param>
        private void AssignSprite(MobData data, PictureBox[] sprites)
        {
            int i = 0;
            bool identified = false;
            while (!identified && i < sprites.Length)
            {
                PictureBox pixtureBox = sprites[i];
                if (pixtureBox.Tag == null)
                {
                    identified = true;

                    //add PictureBox to MobData (automatically changes Sprite)
                    data.PictureBox = pixtureBox;
                }
                i++;
            }
        }

        private void ShowActionMenu()
        {
            //Show and enable ActionGroupBox
            ActionButtonBox.Enabled = true;
            ActionButtonBox.Visible = true;
        }

        //Hides action menu so player doesnt try to make a move when not their turn
        private void HideActionMenu()
        {
            ActionButtonBox.Enabled = false;
            ActionButtonBox.Visible = false;
        }

        #endregion

        //=========================================
        //                Events
        //=========================================
        public event EventHandler<PlayerActionEventArgs> PlayerAction;

        #region Events


        /// <summary>
        /// Any time user selects an Action Button
        ///     if targeted action, show Target selection
        ///     else Invoke PlayerAction event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActionButton_Click(object sender, EventArgs e)
        {
            //Edit: Determine action taken depending on sender and ActionEnum
            //edit: if targeted action, display ActionTargetBox 
            //      (Just hide ActionButtonBox)
            //  else invoke event
        }

        /// <summary>
        /// Handles Target Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetButt_Click(object sender, EventArgs e)
        {
            //edit: Get Target
            //edit: If target is NOT selected, provide error telling user to select a target
        }

        private void OnPlayerAction(Mob target, ActionEnum action)
        {
            //Edit: Actually Store PlayerActionEventArgs so it can be modified
            //edit: Package PlayerAction event with target and action
            //edit: Raise PlayerAction event
            //edit: reset PlayerActionEventArgs
        }

        #endregion

        //=========================================
        //             Event Handlers
        //=========================================
        #region Event Handlers
        /// <summary>
        /// Publishes MobData and subscribes to all events
        /// </summary>
        /// <param name="eventManager"></param>
        public void ManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            eventManager.Publish(this);

            //Subscribe to any needed events
            eventManager.BattleEvent += OnBattleEvent_Handler;
            eventManager.PlayerTurn += OnPlayerTurn_Handler;
            eventManager.TurnEnd += OnTurnEnd_Handler;
        }

        /// <summary>
        /// UnPublishes MobData and unsubscribes from all events
        /// </summary>
        /// <param name="eventManager"></param>
        public void UnManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            eventManager.Unpublish(this);

            //unSubscribe to any needed events
            eventManager.BattleEvent -= OnBattleEvent_Handler;
            eventManager.PlayerTurn -= OnPlayerTurn_Handler;
            eventManager.TurnEnd -= OnTurnEnd_Handler;
        }

        private void OnTurnEnd_Handler(object? sender, TurnEndEventArgs turnData)
        {
            HideActionMenu();
            //Unpack turnSummary and append to battleSummaryTBox
            battleSummaryTBox.Text += $"\n" +
                $"{ turnData.TurnSummary}\n" +
                $"----------------------------";
        }

        private void OnPlayerTurn_Handler(object? sender, PlayerTurnEventArgs e)
        {
            //Edit: Highlight the Hero whose turn it is
            //Edit: Show Hero name
            //Edit: Either modify Special Attack button (heal, spell etc) or 
            //Edit: Populate target selection with all viable targets
            //Show Action Menut
            ShowActionMenu();
        }

        private void OnBattleEvent_Handler(object? sender, TurnEndEventArgs e)
        {
            //Edit: add new line to BattleEvent display
        }

        public void OnPlayerTurn_Handler()
        {
            ShowActionMenu();
        }

        public void OnDeath_Handler()
        {
            //Edit: Decipher which Mob died
            //Edit: Find Mobs pixtureBox

            //Edit: change pixtureBox to show Mob as dead
            heroSprite1.Visible = false;
        }
        #endregion
    }
}
