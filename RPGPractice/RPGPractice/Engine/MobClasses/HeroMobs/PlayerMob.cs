using RPGPractice.Core;
using RPGPractice.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Engine.MobClasses.HeroMobs
{
    /// <summary>
    /// PlayerMob Abstract Mob Subclass
    /// Developer: Taylor Fox
    /// Abstract subclass that adds logic for PlayerControlled Mobs mainly raising PlayerTurn events
    /// </summary>
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
    }
}
