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
        ActionEnum action;
        int currentTurnID;

        //=========================================
        //              Main Methods
        //=========================================
        #region Public Methods
        public BattleField(List<MobData> mobDataList)
        {
            InitializeComponent();
            mobDictionary = new Dictionary<int, MobData>();

            //Make arrays for PictureBoxes for easier Sprite Handling
            BuildPictureArrays();

            //Tag ActionButtons
            AttackButt.Tag = ActionEnum.Attack;
            DefendButt.Tag = ActionEnum.Defend;
            SpecialButt.Tag = ActionEnum.Special;

            // Assign PictureBoxes to arrays
            //  Refactor: This should be doable with a loop, but for some reason it didnt work. Revisit later.

            //Add all mobs into battlefield display and assign to PictureBoxes
            UpdateMobs(mobDataList);

            //Hide Action Menu until player has a turn
            HideActionMenu();
        }

        private void BuildPictureArrays()
        {
            heroSprites = new PictureBox[Max_Sprites];
            villianSprites = new PictureBox[Max_Sprites];

            heroSprites[0] = heroSprite1;
            heroSprites[1] = heroSprite2;
            heroSprites[2] = heroSprite3;
            heroSprites[3] = heroSprite4;
            heroSprites[4] = heroSprite5;
            villianSprites[0] = villianSprite1;
            villianSprites[1] = villianSprite2;
            villianSprites[2] = villianSprite3;
            villianSprites[3] = villianSprite4;
            villianSprites[4] = villianSprite5;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// gets all initial args needed from mobID array
        /// </summary>
        /// <param name="mobs"></param>
        private void UpdateMobs(List<MobData> mobDataList)
        {
            foreach (MobData data in mobDataList)
            {
                //Assign a PictureBox for the pictureBox
                //determine if mobID is a Hero or Villain(NPC) and assign to Sprite
                if (data.IsNPC)
                {
                    AssignSprite(data, villianSprites);
                }
                else
                {
                    AssignSprite(data, heroSprites);
                }

                //Add mobID args to dictionary
                mobDictionary.Add(data.UniqueID, data);
            }
        }


        /// <summary>
        /// Assigns a pictureBox to a picturebox in the picturebox array (Only if there is room)
        /// </summary>
        /// <param name="mob"></param>
        /// <param name="sprites"></param>
        private void AssignSprite(MobData data, PictureBox[] sprites)
        {
            int i = 0;
            bool identified = false;
            while (!identified && i < sprites.Length)
            {
                PictureBox pictureBox = sprites[i];
                if (pictureBox.Image == null)
                {
                    identified = true;

                    //add PictureBox to MobData (automatically changes Sprite)
                    data.PictureBox = pictureBox;
                }
                i++;
            }
        }

        private void ShowActionMenu()
        {
            //Show and enable ActionGroupBox
            ActionMenuGrou.Visible = true;
            ActionButtBox.Enabled = true;
            ActionButtBox.Visible = true;
        }

        //Hides action menu so player doesnt try to make a move when not their turn
        private void HideActionMenu()
        {
            ActionMenuGrou.Visible = false;
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
            //Determine action taken depending on sender and ActionEnum
            if (sender is Button && ((Button)sender).Tag != null)
            {
                var tag = ((Button)sender).Tag;
                action = (ActionEnum)tag;
            }

            //If defending, skip target selection
            if (action == ActionEnum.Defend)
            {
                OnPlayerAction(0, action);
            }
            //else show ActionTargetBox for target selection
            else
            {
                //TargetBox is behind ActionButtBox so hide ActionButtBox
                ActionButtBox.Visible = false;

                //change TurnLabel to reflect current Action
                TurnLabel.Text = action.ToString();
            }
        }

        /// <summary>
        /// Handles Target Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetButt_Click(object sender, EventArgs e)
        {
            //edit: Add a "Cancel" button in case user changes mind.
            //Get Target from ComboBox. interestingly comboBox is populated with MobData objects
            MobData data;
            if (TargetCBox.SelectedItem is MobData)
            {
                //get Data
                data = (MobData)TargetCBox.SelectedItem;

                //send PlayerAction event
                OnPlayerAction(data.UniqueID, action);
            }
            else
            {
                //edit: If target is NOT selected, provide error telling user to select a target
            }

            //re-show ActionButtBox
            ActionButtBox.Visible = true;
        }

        /// <summary>
        /// raise the PlayerAction event
        /// </summary>
        /// <param name="targetID"></param>
        /// <param name="action"></param>
        private void OnPlayerAction(int targetID, ActionEnum action)
        {
            //Package PlayerAction event with target and action
            PlayerActionEventArgs actionData = new PlayerActionEventArgs();
            actionData.TargetID = targetID;
            actionData.Action = action;

            //Raise PlayerAction event
            PlayerAction?.Invoke(this, actionData);
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
            PlayerAction += eventManager.OnPlayerAction_Aggregator;

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
            PlayerAction -= eventManager.OnPlayerAction_Aggregator;

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

        private void OnPlayerTurn_Handler(object? sender, PlayerTurnEventArgs args)
        {
            //find mobData using MobID
            currentTurnID = args.MobID;
            MobData mobData = mobDictionary[currentTurnID];

            //Highlight the Hero whose turn it is
            mobData.PictureBox.BorderStyle = BorderStyle.FixedSingle;

            //Show Hero name in Action Menu
            TurnLabel.Text = mobData.Name;

            //Setup SpecialAttack Button to show what Special ability current mob has.
            if (!mobData.Special.Equals(""))
            {
                SpecialButt.Visible = true;
                SpecialButt.Text = mobData.Special;
            }
            //Else hide Special Button because no special ability
            else
            {
                SpecialButt.Visible = false;
            }

            //Populate TargetCBox with all potential targets
            foreach (MobData data in mobDictionary.Values)
            {
                //Make sure Mob is alive before adding.
                if (data.IsAlive)
                {
                    //add MobData to ComboBox.
                    //  Note: non-Strings added to Combobox will be default display their toString()
                    //  This makes it easy to know what target the user selects.
                    TargetCBox.Items.Add(data);
                }
            }

            //Show Action Menu
            ShowActionMenu();
        }

        private void OnBattleEvent_Handler(object? sender, TurnEndEventArgs e)
        {
            //Edit: add new line to BattleEvent display
        }
        #endregion
    }
}
