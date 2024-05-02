using RPGPractice.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Engine.MobClasses
{
    public abstract class NPC : Mob
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="random"></param>
        protected NPC(string name, Random random) :base(name, random) { }

        /// <summary>
        /// Automate a turn for the Mob
        /// by default, attacks a random target
        /// </summary>
        /// <param name="heroTargetList"></param>
        /// <param name="enemyTargetList"></param>
        public void TakeTurn(List<MobData> heroTargetList, List<MobData> enemyTargetList)
        {
            //set a target from available targets
            Mob target = SetTarget(heroTargetList);
            Attack(target);
        }

        private Mob SetTarget(List<MobData> heroTargetList)
        {
            Mob target;

            //randomly choose a target, but make sure that it is actualy alive (stop beating the dead horse!)
            do
            {
                int targetIndex = random.Next(heroes.Length);
                System.Diagnostics.Debug.WriteLine($"Rolling Random Target. Heroes Length = {heroes.Length}. Index = {targetIndex}");
                target = heroes[targetIndex];
            } while (!target.IsAlive);

            return target;
        }
    }
}
