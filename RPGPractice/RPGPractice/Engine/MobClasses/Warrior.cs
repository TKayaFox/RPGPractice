using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses
{
    public class Warrior : PlayerMob
    {
        public Warrior(Random random) : base("Warrior", random) { }
        public Warrior(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Fighter;
            MaxHitPoints = 30;
            Initiative = 2;  //roll 1d20+2
            Intelligence = -2;
            Strength = 2;
            AttackMod = 2;
            Defense = 15;
            MagicDefense = 8;
            BlockingBonus = 3;
        }
    }
}