using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.MobClasses
{
    public class Bandit : NPC
    {
        public Bandit(Random random) : base("Bandit", random) { }
        public Bandit(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for Mob
        /// </summary>
        public override void Initialize()
        {
            MaxHitPoints = random.Next(11); //roll 1d10 
            MaxMana = 0; //Only casters get Mana
            Initiative = random.Next(21) + 3; //roll 1d20 + 3
            UserControlled = false;
            Intelligence = 0;
            Strength = 0;
            AttackMod = 0;
            Defense = 10;
            MagicDefense = 10;
        }
    }
}