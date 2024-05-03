using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses.EnemyMobs
{
    public class Dragon : Enemy
    {
        public Dragon(string name) : base(name) { }

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

        //public void TakeTurn()
        //{
        //    //TODO: Insert turn decision logic
        //    //      IF really crazy, have dragon keep track of who last attacked and who is currently at most health and actually set decision tree
        //}

        //TODO: Add Breath attack (Targets one Hero, does magic damage)
        //TODO: Add Swipe Attack (Targets all heroes)

        // Static Methods
        /// <summary>
        /// Returns a minimum and maximum number of this Enemy type for encounter generation
        ///     required for all Enemy types, but cannot be made abstract due to static behavior
        /// </summary>
        /// <param name="combatLevel"></param>
        /// <returns></returns>
        public static (int min, int max) EncounterData(int combatLevel)
        {
            int min = 0;
            int max = 0;

            //use combatLevel to determine max. min is always 1 since this is easiest mob
            switch (combatLevel)
            {
                case > 15:
                    max = 5;
                    break;
                case > 12:
                    max = 4;
                    break;
                case > 9:
                    max = 3;
                    break;
                case > 6:
                    max = 2;
                    break;
                case > 4:
                    max = 1;
                    break;
            }

            return (min, max);
        }
    }
}