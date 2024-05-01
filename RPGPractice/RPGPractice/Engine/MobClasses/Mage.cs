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
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Mage;
            MaxHitPoints = 16;
            MaxMana = 5; //Only casters get Mana
            Initiative = random.Next(20);  //roll 1d20
            Intelligence = 3;
            Strength = -2;
            AttackMod = -2;
            Defense = 10;
            MagicDefense = 15;
        }

        //EDIT: Add spells that do magic damage and reduce Mana
    }
}