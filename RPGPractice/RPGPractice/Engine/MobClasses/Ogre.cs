using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses
{
    public class Ogre : NPC
    {
        public Ogre(Random random) : base("Ogre", random) { }
        public Ogre(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Ogre;
            MaxHitPoints = 30;
            MaxMana = 0; //Only casters get Mana
            Initiative = random.Next(20) - 5;  //roll 1d20+2
            Intelligence = 0;
            Strength = 2;
            AttackMod = 2;
            Defense = 13;
            MagicDefense = 8;
        }


        public void TakeTurn()
        {
            //Edit: Implement
        }

        //EDIT: Add damage multiplier for magic attacks
    }
}