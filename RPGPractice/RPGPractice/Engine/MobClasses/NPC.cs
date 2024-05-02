﻿using RPGPractice.GUI;
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
        /// by default, attacks a random targetQueue
        /// </summary>
        /// <param name="heroTargetList"></param>
        /// <param name="enemyTargetList"></param>
        public override void TakeTurn(List<MobData> heroTargetList, List<MobData> enemyTargetList)
        {
            //set a targetQueue from available targets
            MobData target = SetTarget(heroTargetList);
            Attack(target);
        }

        private MobData SetTarget(List<MobData> heroes)
        {
            MobData target;

            //randomly choose a targetQueue, but make sure that it is actualy alive (stop beating the dead horse!)
            do
            {
                int targetIndex = random.Next(heroes.Count);
                target = heroes[targetIndex];
            } while (!target.IsAlive);

            return target;
        }
    }
}
