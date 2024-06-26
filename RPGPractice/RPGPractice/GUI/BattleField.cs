﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RPGPractice.Core;
using RPGPractice.Core.Enumerations;
using RPGPractice.Core.Events;
using RPGPractice.Engine;
using RPGPractice.Engine.MobClasses;

namespace RPGPractice
{
    public partial class BattleField : UserControl
    {
        //=========================================
        //               Variables
        //=========================================
        private const int Max_Sprites = 5;

        private PictureBox[] heroSprites;
        private PictureBox[] enemySprites;
        private Dictionary<int, MobData> mobDictionary;
        private List<MobData> attackTargetList;
        private List<MobData> specialTargetList;
        private MobActions action;
        private int currentTurnID;


        public event EventHandler<PlayerActionEventArgs> PlayerAction;
        public event EventHandler BattleStart;
        //TODO: Add a "Cancel" button in case user changes mind.

        //=========================================
        //              Main Methods
        //=========================================
        #region Public Methods
        public BattleField()
        {
            InitializeComponent();

            //Tag ActionButtons
            AttackButt.Tag = MobActions.Attack;
            DefendButt.Tag = MobActions.Block;
            SpecialButt.Tag = MobActions.Special;


        }

        public void Populate(List<MobData> mobDataList)
        {
            //show user that a new battle has started
            BattleUpdate("==================================\r\n" +
                             "\t\tNew Battle!" +
                     "\r\n==================================\r\n");

            mobDictionary = new Dictionary<int, MobData>();

            //Assign PictureBoxes to arrays for easier Sprite Handling
            BuildPictureArrays();

            //Add all mobs into battlefield display and assign to PictureBoxes
            SetupMobs(mobDataList);

            //Hide Action Menu until player has a turn
            HideActionMenu();
        }

        private void BuildPictureArrays()
        {
            heroSprites = new PictureBox[Max_Sprites];
            enemySprites = new PictureBox[Max_Sprites];

            for (int i = 0; i < Max_Sprites; i++)
            {
                heroSprites[i] = (PictureBox)Controls.Find($"heroSprite{i + 1}", true)[0];
                enemySprites[i] = (PictureBox)Controls.Find($"enemySprite{i + 1}", true)[0];
                heroSprites[i].Image = null;
                enemySprites[i].Image = null;
            }
        }

        /// <summary>
        /// Called to ensure BattleField is properly cleared for garbage collection
        /// </summary>
        public void Unload()
        {
            //Clear dictionary
            mobDictionary?.Clear();
            ClearTargetCBox();
            attackTargetList?.Clear();
            specialTargetList?.Clear();
            mobDictionary?.Clear();

            //clear all pictureboxes
            enemySprites = null;
            heroSprites = null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// gets all initial args needed from mobID array
        /// </summary>
        /// <param name="mobs"></param>
        private void SetupMobs(List<MobData> mobDataList)
        {
            foreach (MobData data in mobDataList)
            {
                //Assign a PictureBox for the pictureBox
                //determine if mobID is a Hero or Villain(NPC) and assign to Sprite
                if (data.IsNPC)
                {
                    AssignSprite(data, enemySprites);
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

                    //add PictureBox to Data (automatically changes Sprite)
                    data.PictureBox = pictureBox;
                }
                i++;
            }
            if (!identified)
            {
                throw new Exception("Unable to assign Mob a PictureBox!");
            }
        }

        private void ShowActionMenu()
        {
            //Show and enable ActionGroupBox
            ActionMenuGroup.Visible = true;
            ActionButtBox.Enabled = true;
            ActionButtBox.Visible = true;
        }

        //Hides action menu so player doesnt try to make a move when not their turn
        private void HideActionMenu()
        {
            ActionMenuGroup.Visible = false;
        }


        private void InitializeTargetSelectMenu()
        {
            //Populate targetCBox with all potential targets
            //add Data to ComboBox.
            //  Note: non-Strings added to Combobox will display their toString()
            //  This makes it easy to know what targetedAbilityQueue the user selects.
            switch (action)
            {
                //Attackable targets
                case MobActions.Attack:
                    targetCBox.DataSource = attackTargetList;
                    break;

                //Special Attack targets (may vary depending on type of Special Attack)
                case MobActions.Special:
                    targetCBox.DataSource = specialTargetList;
                    break;
            }
            //TargetBox is behind ActionButtBox so hide ActionButtBox
            ActionButtBox.Visible = false;

            //change button text to reflect current Action
            TargetButt.Text = action.ToString();
        }
        private MobData GetTargetData()
        {
            //get Data
            MobData data = (MobData)targetCBox.SelectedItem;

            //Empty Targets to prevent redundant items
            ClearTargetCBox();
            return data;
        }

        private void ClearTargetCBox()
        {
            targetCBox.SelectedIndex = -1;
            targetCBox.SelectedItem = null;
            targetCBox.DataSource = null;
        }

        /// <summary>
        /// Appends a string as a new line to battleSummaryTBox and keeps textbox scrolled
        /// </summary>
        /// <param name="data"></param>
        private void BattleUpdate(string data)
        {

            battleSummaryTBox.Text += $"\r\n{data}";

            // Scroll to the end of the textbox
            battleSummaryTBox.SelectionStart = battleSummaryTBox.Text.Length;
            battleSummaryTBox.ScrollToCaret();
        }

        #endregion

        //=========================================
        //                Events
        //=========================================

        #region Events


        /// <summary>
        /// Any time user selects an Action Button
        ///     if targeted action, show TargetList selection
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
                action = (MobActions)tag;
            }

            //If defending, skip targetedAbilityQueue selection
            if (action == MobActions.Block)
            {
                OnPlayerAction(0, action);
            }
            //else show ActionTargetBox for targetedAbilityQueue selection
            else
            {
                InitializeTargetSelectMenu();
            }
        }

        /// <summary>
        /// Handles TargetList Selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetButt_Click(object sender, EventArgs e)
        {
            //Get TargetList from ComboBox. interestingly comboBox is populated with Data objects
            MobData data;

            if (targetCBox.SelectedItem is MobData)
            {
                //get selected targetedAbilityQueue data then send PlayerAction event
                data = GetTargetData();
                OnPlayerAction(data.UniqueID, action);
            }
            else
            {
                //provide error telling user to select a target
                MessageBox.Show("You must select a target!");
            }

            //re-show ActionButtBox
            ActionButtBox.Visible = true;
        }
        private void BattleStartButt_Click(object sender, EventArgs e)
        {
            //Hide button and raise NewBattle event
            BattleStartButt.Visible = false;
            OnBattleStart();
        }

        private void TargetCancelButt_Click(object sender, EventArgs e)
        {
            //re-show ActionButtBox
            ActionButtBox.Visible = true;
        }

        /// <summary>
        /// raise the PlayerAction event
        /// </summary>
        /// <param name="targetID"></param>
        /// <param name="action"></param>
        private void OnPlayerAction(int targetID, MobActions action)
        {
            //Package PlayerAction event with targetedAbilityQueue and action
            PlayerActionEventArgs actionData = new PlayerActionEventArgs();
            actionData.TargetID = targetID;
            actionData.Action = action;
            actionData.AttackerID = currentTurnID;

            //Unhighlight player
            MobData attacker = mobDictionary[currentTurnID];
            attacker.Selected = false;

            //Hide action menu until next player turn
            HideActionMenu();
            PlayerAction.Invoke(this, actionData);
        }

        /// <summary>
        /// When Form is initialized and user has selected Start button, raise event
        /// </summary>
        private void OnBattleStart()
        {
            BattleStart.Invoke(this, EventArgs.Empty);
        }
        #endregion

        //=========================================
        //             Event Handlers
        //=========================================
        #region Event Handlers

        public void OnTurnEnd_Handler(object sender, TurnEndEventArgs turnData)
        {
            //Unpack turnSummary and append to battleSummaryTBox
            BattleUpdate(turnData.TurnSummary);
        }

        public void OnPlayerTurn_Handler(object sender, PlayerTurnEventArgs args)
        {
            //find mobData using MobID
            int mobID = args.MobID;
            currentTurnID = mobID;
            MobData mobData = mobDictionary[currentTurnID];

            //highlight the Hero whose turn it is
            mobDictionary[mobID].Selected = true;

            //Show Hero name in Action Menu
            TurnLabel.Text = mobData.ToString();

            //Setup Special Button to show what SpecialActionString ability current mob has.
            UpdateSpecialAction(mobData);

            //update targetedAbilityQueue Lists
            attackTargetList = args.AttackTargetList;
            specialTargetList = args.SpecialTargetList;

            //Show Action Menu
            ShowActionMenu();
        }

        private void UpdateSpecialAction(MobData mobData)
        {
            string action = mobData.SpecialActionString;

            if (!action.Equals(""))
            {
                SpecialButt.Visible = true;
                SpecialButt.Text = mobData.SpecialActionString;
            }
            //Else hide SpecialActionString Button because no specialAction ability
            else
            {
                SpecialButt.Visible = false;
            }
        }

        #endregion

    }
}
