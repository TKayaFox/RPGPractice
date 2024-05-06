using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses.EnemyMobs
{
    public class Bandit : EnemyMob
    {
        public Bandit(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for MobID
        /// </summary>
        protected override void Initialize()
        {
            MaxHitPoints = 10;
            Initiative = 2;
            Sprite = Properties.Resources.Bandit;
            Intelligence = 0;
            Strength = 0;
            AttackMod = 0;
            Defense = 9;
            MagicDefense = 9;
        }
        
        // Static Methods
        /// <summary>
        /// Returns a minimum and maximum number of this Enemy type for encounter generation
        ///     required for all Enemy types, but cannot be made abstract due to static behavior
        /// </summary>
        /// <param name="combatLevel"></param>
        /// <returns></returns>
        public static (int min, int max) EncounterData(int combatLevel)
        {
            int min = 1;
            int max = 1;

            //use combatLevel to determine max. min is always 1 since this is easiest mob
            switch (combatLevel)
            {
                //Dragon introduction
                case 4: 
                    max = 0;
                    min = 0;
                    break;
                //Ogre introduction
                case 2:
                    max = 0;
                    min = 0;
                    break;
                //4 Bandits only
                case 3:
                    max = 4;
                    min = 4;
                    break;
                //3 Bandits only
                case 1:
                    max = 3;
                    min = 3;
                    break;
                case > 3:
                    max = 5;
                    min = 0;
                    break;
            }

            return (min, max);
        }
    }
}