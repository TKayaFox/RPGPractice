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
            MaxHitPoints = 18;
            Initiative = random.Next(20) - 5;  //roll 1d20+2
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
            damage = damage + (damage / 2); //Add half damage rounded down (int dataloss does this)

            return base.DefendMagic(attackRoll, damage);
        }
    }
}