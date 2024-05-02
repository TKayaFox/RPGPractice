using RPGPractice.Core.Events;
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
        protected Enemy(string name, Dice dice) : base(name, dice) { }

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

            //randomly choose a targetedAbilityQueue, but make sure that it is actualy alive (stop beating the dead horse!)
            do
            {
                int targetIndex = random.Next(heroes.Count);
                target = heroes[targetIndex];
            } while (!target.IsAlive);

            return target;
        }
    }
}
