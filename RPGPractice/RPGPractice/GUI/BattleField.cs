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

        //=========================================
        //              Main Methods
        //=========================================
        #region Main Methods
        public BattleField(Mob[] heroes, Mob[] villians)
        {
            InitializeComponent();

            //Add all mobs into battlefield display
            UpdateMob(heroes);
            UpdateMob(villians);

            //edit:Add all mob names to TargetComboBox

            //Make arrays for PictureBoxes for easier Sprite Handling
            PictureBox[] heroSprites = new PictureBox[Max_Sprites];
            PictureBox[] villianSprites = new PictureBox[Max_Sprites];

            // Assign PictureBoxes to arrays using a loop
            for (int i = 0; i < Max_Sprites; i++)
            {
                heroSprites[i] = Controls.Find($"heroSprite{i + 1}", true).FirstOrDefault() as PictureBox;
                villianSprites[i] = Controls.Find($"villainSprite{i + 1}", true).FirstOrDefault() as PictureBox;
            }
        }

        /// <summary>
        /// Updates a Mob's status on display
        /// </summary>
        /// <param name="mob"></param>
        public void UpdateMob(Mob mob)
        {
            //edit: Display hero and villian information/sprites
        }
        public void UpdateMob(Mob[] mobs)
        {
            foreach (Mob mob in mobs)
            {
                UpdateMob(mob);
            }
        }

        public void ShowActionMenu()
        {
            //Show and enable ActionGroupBox
            ActionGroupBox.Enabled = true;
            ActionGroupBox.Visible = true;
        }

        //Hides action menu so player doesnt try to make a move when not their turn
        public void HideActionMenu()
        {
            ActionGroupBox.Enabled = false;
            ActionGroupBox.Visible = false;
        }

        #endregion

        //=========================================
        //                Events
        //=========================================
        //public event System.EventHandler Attack;

        #region Events

        #endregion

        //=========================================
        //             Event Handlers
        //=========================================
        #region Event Handlers
        /// <summary>
        /// Publishes Class and subscribes to all events
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
        /// UnPublishes Class and unsubscribes from all events
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

        private void OnTurnEnd_Handler(object? sender, EventArgs e)
        {
            HideActionMenu();
        }

        private void OnPlayerTurn_Handler(object? sender, PlayerTurnEventArgs e)
        {
            ShowActionMenu();
        }

        private void OnBattleEvent_Handler(object? sender, BattleEventArgs e)
        {
            //Edit: add new line to BattleEvent display
        }

        public void OnPlayerTurn_Handler()
        {
            ShowActionMenu();
        }

        public void OnDeath_Handler()
        {
            //Edit: Find Mobs sprite

            //Edit: change sprite to show Mob as dead
            heroSprite1.Visible = false;
        }
        #endregion

    }
}
