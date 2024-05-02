﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Engine.MobClasses
{
    /// <summary>
    /// Dice handles randomized values for Mobs to standardize roll behavior
    /// </summary>
    public class Dice
    {
        Random random;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="random">Optional: If not provided will make it's own</param>
        public Dice(Random random)
        {
            this.random = random;
        }
        public Dice()
        {
            this.random = new Random();
        }


        /// <summary>
        /// Rolls attack and damage.
        ///     Automatically handles crits
        /// </summary>
        /// <param name="attackMod">value added to Attack roll</param>
        /// <param name="damageMod">value added to Damage roll</param>
        /// <returns>a Tuple with int attack, int damageResult</returns>
        public (int,int) RollAttack(int attackMod, int damageMod)
        {
            //Determine Attack Roll (attackMod + 1d20)
            int attack = random.Next(1, 21);
            int damage = 0;

            //Roll damage
            damage += RollDamage(damageMod);

            //Critical Hit: If ability roll was a 20, then slightly boost hit chance (attack) and boost damage
            if (attack >= 20)
            {
                attack += 5; //boost chance to hit
                damage += RollDamage(damageMod); //roll damage a second time for increased damage
            }

            attack += attackMod;

            //return as Tuple
            return (attack,damage);
        }

        /// <summary>
        /// rolls a damage dice and adds a modifier
        /// TODO: Setup Dice class to do this instead
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public int RollDamage(int modifier)
        {
            //Determine damage Roll (strength + 1d8)
            int damage = Roll(8); //roll 1d8
            damage += modifier;
            return damage;
        }

        /// <summary>
        /// Generic roll command for rolling a specific dice
        /// </summary>
        /// <param name="numsides">Required: specifies number of sides on the dice</param>
        /// <param name="numDice">Optional: specifies number of dice to roll</param>
        /// <returns>int total result of roll</returns>
        public int Roll(int numsides, int numDice = 1)
        {
            int roll = 0;
            for (int i = 0;i< numDice;i++)
            {
                //Possible rolls should be between 1 and numSides INCLUSIVE so increment numSides by 1
                roll += random.Next(1, (numsides + 1));
            }
            return roll;
        }
    }
}