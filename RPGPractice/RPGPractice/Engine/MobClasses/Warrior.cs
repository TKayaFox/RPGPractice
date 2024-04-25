using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses
{
    public class Warrior : Mob
    {
        public Warrior(Random random) : base("Warrior", random) { }
        public Warrior(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for Mob
        /// Meant to be overridden
        /// </summary>
        public override void Initialize()
        {
            UserControlled = true;
            MaxHitPoints = 30;
            MaxMana = 0; //Only casters get Mana
            Initiative = random.Next(20) + 2;  //roll 1d20+2
            Intelligence = -2;
            Strength = 4;
            AttackMod = 4;
            Defense = 18;
            MagicDefense = 8;
        }
    }
}