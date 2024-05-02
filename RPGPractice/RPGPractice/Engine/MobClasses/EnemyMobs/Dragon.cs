using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses.EnemyMobs
{
    public class Dragon : Enemy
    {
        public Dragon(string name, Dice dice) : base(name, dice) { }

        /// <summary>
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Dragon;
            MaxHitPoints = 40;
            Initiative = -1;
            Intelligence = 3;
            Strength = 3;
            AttackMod = 3;
            Defense = 16;
            MagicDefense = 16;
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