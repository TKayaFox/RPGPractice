using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice
{
    public class Dragon : Mob
    {

        public Dragon(Random random) : base("Dragon", random) { }

        /// <summary>
        /// Sets All stats for Mob
        /// Meant to be overridden
        /// </summary>
        public override void Initialize()
        {
            UserControlled = false;
            MaxHitPoints = 50;
            MaxMana = 0; //Breath Attacks dont use Mana
            Initiative = 0; //Always last in initiative
            Intelligence = 4;
            Strength = 4;
            AttackMod = 4;
            Defense = 17; 
            MagicDefense = 17;
        }

        //Edit: Add Breath attack (Targets one Hero, does magic damage)
        //Edit: Add Swipe Attack (Targets all heroes)
    }
}