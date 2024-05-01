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
        protected NPC(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Attack a random target
        /// </summary>
        /// <param name="heroes"></param>
        public void TakeTurn(Mob[] heroes)
        {
            Mob target = SetTarget(heroes);

            //BAndit only knows to attack
            Attack(target);
        }

        private Mob SetTarget(Mob[] heroes)
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
