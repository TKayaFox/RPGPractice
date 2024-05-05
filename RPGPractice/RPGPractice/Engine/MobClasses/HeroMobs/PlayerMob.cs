using RPGPractice.Core;
using RPGPractice.Core.Events;
using RPGPractice.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Engine.MobClasses.HeroMobs
{
    public abstract class PlayerMob : Mob
    {
        /// <summary>
        /// Constructor
        /// Does nothing at this level
        /// </summary>
        /// <param name="name"></param>
        /// <param name="random"></param>
        protected PlayerMob(string name) : base(name) { }

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
    }
}
