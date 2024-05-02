using RPGPractice.Core.Events;
using RPGPractice.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Engine.MobClasses
{
    internal abstract class PlayerMob : Mob
    {
        /// <summary>
        /// Constructor
        /// Does nothing at this level
        /// </summary>
        /// <param name="name"></param>
        /// <param name="random"></param>
        protected PlayerMob(string name, Random random) : base(name, random) { }

        public override void TakeTurn()
        {

        }

        /// <summary>
        /// Override the base EventManager to add new Events
        /// </summary>
        /// <param name="eventManager"></param>
        public override void ManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            PlayerTurn += eventManager.OnPlayerTurn_Aggregator;
        }

        /// <summary>
        /// Tells the GUI to setup for players turn.
        /// </summary>
        public void OnPlayerTurn(List<MobData> allyTargetList, List<MobData> enemyTargetList)
        {
            //setup event arguments
            PlayerTurnEventArgs args = new PlayerTurnEventArgs();
            args.MobID = currentTurn.UniqueID;

            //Make lists of viable targets
            args.AttackTargetList = attackTargetList;
            args.SpecialTargetList = specialTargetList;

            PlayerTurn?.Invoke(this, args);
        }
    }
}
