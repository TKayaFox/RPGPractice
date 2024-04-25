﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.MobClasses
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
            //randomly decide target
            int targetIndex = random.Next((heroes.Length));
            Mob target = heroes[targetIndex];

            //BAndit only knows to attack
            Attack(target);
        }
    }
}
