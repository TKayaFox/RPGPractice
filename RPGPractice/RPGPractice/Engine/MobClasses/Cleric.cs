using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses
{
    public class Cleric : Mob
    {
        public Cleric(Random random) : base("Cleric", random) { }
        public Cleric(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Cleric;
            MaxHitPoints = 25;
            MaxMana = 5; //Only casters get Mana
            Initiative = random.Next(20);  //roll 1d20
            Intelligence = 2;
            Strength = -1;
            AttackMod = -1;
            Defense = 12;
            MagicDefense = 12;
        }

        /// <summary>
        /// Heals targeted creature
        /// </summary>
        /// <param name="target"></param>
        public void Heal(Mob target)
        {
            //Roll for heal
            int healValue = random.Next(9) + Intelligence;

            target.Heal(healValue, Name);

            //Reduce Mana
            Mana--;
        }
    }
}