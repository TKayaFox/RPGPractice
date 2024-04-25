using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses
{
    public class Dragon : NPC
    {

        public Dragon(Random random) : base("Dragon", random) { }
        public Dragon(string name, Random random) : base(name, random) { }

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

        public void TakeTurn()
        {
            //EDIT: Insert turn decision logic
            //      IF really crazy, have dragon keep track of who last attacked and who is currently at most health and actually set decision tree
        }

        //Edit: Add Breath attack (Targets one Hero, does magic damage)
        //Edit: Add Swipe Attack (Targets all heroes)
    }
}