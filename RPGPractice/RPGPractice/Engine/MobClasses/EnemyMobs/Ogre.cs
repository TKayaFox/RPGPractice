using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses.EnemyMobs
{
    public class Ogre : EnemyMob
    {
        public Ogre(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Ogre;
            MaxHitPoints = 18;
            Initiative = 1;  //roll 1d20+2
            Intelligence = 0;
            Strength = 2;
            AttackMod = 2;
            Defense = 13;
            MagicDefense = 8;
        }

        /// <summary>
        /// Called when hit by a Magical offensive ability
        ///     Ogre Doubles all Magic Damage
        /// </summary>
        /// <param name="attackRoll"></param>
        /// <param name="damage"></param>
        /// <param name="attacker"></param>
        /// <returns>string describing what occurred during this turn</returns>
        public override string DefendMagic(int attackRoll, int damage)
        {
            damage = damage + damage / 2; //Add half damage rounded down (int dataloss does this)

            return base.DefendMagic(attackRoll, damage);
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
            int min = 0;
            int max = 0;

            //use combatLevel to determine max.
            switch (combatLevel)
            {
                //Multiple bandits only
                case 5:
                    max = 0;
                    break;
                //Dragon introduction
                case 4:
                    max = 0;
                    break;
                //Ogre introduction
                case 2:
                    max = 1;
                    min = 1;
                    break;

                //if not situational, then scale up by combatLevel
                case > 9:
                    max = 5;
                    break;
                case > 7:
                    max = 4;
                    break;
                case > 5:
                    max = 3;
                    break;
                case > 2:
                    max = 1;
                    break;
            }
            return (min, max);
        }
    }
}