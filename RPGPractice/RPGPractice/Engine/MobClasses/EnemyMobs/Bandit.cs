﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses.EnemyMobs
{
    public class Bandit : Enemy
    {
        public Bandit(string name, Dice dice) : base(name, dice) { }

        /// <summary>
        /// Sets All stats for MobID
        /// </summary>
        protected override void Initialize()
        {
            MaxHitPoints = 10;
            Initiative = random.Next(21) + 3;
            Sprite = Properties.Resources.Bandit;
            Intelligence = 0;
            Strength = 0;
            AttackMod = 0;
            Defense = 9;
            MagicDefense = 9;
        }
    }
}