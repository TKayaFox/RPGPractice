using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses
{
    public class Mage : Mob
    {

        public Mage(Random random) : base("Mage", random) { }
        public Mage(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for Mob
        /// Meant to be overridden
        /// </summary>
        public override void Initialize()
        {
            UserControlled = true;
            MaxHitPoints = 16;
            MaxMana = 6; //Only casters get Mana
            Initiative = random.Next(20);  //roll 1d20
            Intelligence = 4;
            Strength = -2;
            AttackMod = -2;
            Defense = 15;
            MagicDefense = 18;
        }

        //EDIT: Add spells that do magic damage and reduce Mana
    }
}