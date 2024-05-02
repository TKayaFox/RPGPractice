using RPGPractice.Core.Events;
using RPGPractice.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Engine.MobClasses
{
    public abstract class PlayerMob : Mob
    {
        /// <summary>
        /// Constructor
        /// Does nothing at this level
        /// </summary>
        /// <param name="name"></param>
        /// <param name="random"></param>
        protected PlayerMob(string name, Random random) : base(name, random) { }

        protected override void TakeTurn(List<MobData> allyTargetList, List<MobData> enemyTargetList)
        {
            //raise PlayerTurn event to capture user's action
            OnPlayerTurn(allyTargetList, enemyTargetList);
        }


        /// <summary>
        /// Compiles TargetList Lists for OnPlayerTurn
        ///     Override to alter Special Action behavior
        /// </summary>
        /// <param name="allyTargetList"></param>
        /// <param name="enemyTargetList"></param>
        /// <param name="args"></param>
        protected virtual void CompileTargetLists(List<MobData> allyTargetList, List<MobData> enemyTargetList, PlayerTurnEventArgs args)
        {
            //Make lists of viable targets
            args.AttackTargetList = allyTargetList;

            //Unless overriden provide a list of all possible targets for Special Action
            //  combine both lists
            List<MobData> FullTargetList = new List<MobData>();
            FullTargetList.AddRange(allyTargetList);
            FullTargetList.AddRange(enemyTargetList);

            args.SpecialTargetList = FullTargetList;
        }

        //=========================================
        //                Events
        //=========================================
        public event EventHandler<PlayerTurnEventArgs> PlayerTurn;


        #region Event Managers
        /// <summary>
        /// Publishes self and subscribes to all events
        /// </summary>
        /// <param name="eventManager"></param>
        public override void ManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            PlayerTurn += eventManager.OnPlayerTurn_Aggregator;

            //also run for parent(Mob) class
            base.ManageEvents(eventManager);
        }


        /// <summary>
        /// UnPublishes self and unsubscribes from all events
        /// </summary>
        /// <param name="eventManager"></param>
        public override void UnManageEvents(EventManager eventManager)
        {
            //unpublish events to eventManager
            PlayerTurn -= eventManager.OnPlayerTurn_Aggregator;

            //also run for parent(Mob) class
            base.UnManageEvents(eventManager);
        }
        #endregion

        #region Event Invokers
        /// <summary>
        /// Tells the GUI to setup for players turn.
        /// </summary>
        public void OnPlayerTurn(List<MobData> allyTargetList, List<MobData> enemyTargetList)
        {
            //setup event arguments
            PlayerTurnEventArgs args = new PlayerTurnEventArgs();
            args.MobID = UniqueID;

            CompileTargetLists(allyTargetList, enemyTargetList, args);

            PlayerTurn.Invoke(this, args);
        }
        #endregion
    }
}
