using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.MobClasses
{
    public class Ogre : Mob, NPC
    {
        public Ogre(Random random) : base("Ogre", random) { }
        public Ogre(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for Mob
        /// Meant to be overridden
        /// </summary>
        public override void Initialize()
        {
            UserControlled = false;
            MaxHitPoints = 30;
            MaxMana = 0; //Only casters get Mana
            Initiative = random.Next(20) - 5;  //roll 1d20+2
            Intelligence = 0;
            Strength = 3;
            AttackMod = 3;
            Defense = 18;
            MagicDefense = 8;
        }


        public void TakeTurn()
        {
            //Edit: Implement
        }

        //EDIT: Add damage multiplier for magic attacks
    }
}