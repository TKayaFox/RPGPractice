using RPGPractice.Core.Enumerations;
using RPGPractice.Core.Events;
using RPGPractice.Engine.MobClasses.EnemyMobs;
using RPGPractice.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Engine.MobClasses.EnemyMobs
{
    public abstract class Enemy : Mob
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="random"></param>
        protected Enemy(string name) : base(name) { }

        /// <summary>
        /// Automate a turn for the Mob
        /// by default, attacks a random targetedAbilityQueue
        /// </summary>
        /// <param name="heroTargetList"></param>
        /// <param name="enemyTargetList"></param>
        protected override void TakeTurn(List<MobData> heroTargetList, List<MobData> enemyTargetList)
        {
            //set a targetedAbilityQueue from available targets
            MobData target = SetTarget(heroTargetList);
            Attack(target);
        }

        private MobData SetTarget(List<MobData> heroes)
        {
            MobData target;

            //cycle through targetedAbilityQueue to find a target, skipping dead targets
            int targetIndex = 0;
            do
            {
                target = heroes[targetIndex];
                targetIndex++;

                //reset index if outside of bounds
                if (targetIndex >= heroes.Count)
                {
                    targetIndex = 0;
                }
            } while (!target.IsAlive);

            return target;
        }
    }
}
