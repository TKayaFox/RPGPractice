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
        }

        /// <summary>
        /// Updates a Mob's status on display
        /// </summary>
        /// <param name="mob"></param>
        public void UpdateMob(Mob mob)
        {
            //edit: Display hero and villian information/sprites on display
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
        }

        public void OnPlayerTurn_Handler()
        {
            ShowActionMenu();
        }

        public void OnDeath_Handler()
        {
            //Edit: change sprite to show Mob as dead
            throw new System.NotImplementedException();
        }
        #endregion

    }
}
