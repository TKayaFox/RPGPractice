using RPGPractice.Core.Enumerations;
using RPGPractice.Core.Events;
using RPGPractice.Engine.MobClasses.EnemyMobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Engine.MobClasses.EnemyMobs
{
    /// <summary>
    /// EnemyMob Abstract Object Mob subclass
    /// Developer: Taylor Fox
    /// Abstract Subclass that adds Logic for Non Player Controlled Mobs (Enemies)
    /// </summary>
    public abstract class EnemyMob : Mob
    {
        protected Random random;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="random"></param>
        protected EnemyMob(string name, Random random) : base(name) 
        {
            this.random = random;
        }

        /// <summary>
        /// Automate a turn for the Mob
        /// by default, attacks a random targetedAbilityQueue
        /// </summary>
        /// <param name="heroTargetList"></param>
        /// <param name="enemyTargetList"></param>
        protected override void TakeTurn(List<MobData> heroTargetList, List<MobData> enemyTargetList)
        {
            //set a target from available targets
            MobData target = SetTarget(heroTargetList);
            Attack(target);
        }

        protected virtual MobData SetTarget(List<MobData> heroes)
        {
            MobData target;

            //Randomly choose a target skipping dead targets
            do
            {
                int index = random.Next(heroes.Count);
                target = heroes[index];
            } while (!target.IsAlive);

            return target;
        }
    }
}
