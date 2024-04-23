using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice
{
    /// <summary>
    /// Mob represents any creature (Player Character or Non Player Character)
    /// All mobs should have the same basic functioning and Attributes, these attributes should be limited to 
    /// </summary>
    public abstract class Mob
    {
        private bool userControlled;
        private int maxHitPoints;
        private int hitPoints;
        private int maxMana;
        private int mana;
        private int initiative;
        private int intelligence;
        private int strength;
        private int attackMod;
        private int defense;
        private int magicDefense;
        private string name = "";

        /// <summary>
        /// Public Getters are used throughout the game
        /// </summary>
        public string Name { get => name; }
        public int Defense { get => defense; }

        /// <summary>
        /// Protected Setters are only for subclasses!
        /// Refactor: Most of these should only be set once, so there may be a way to optimize
        /// </summary>
        protected int setDefense { set => defense = value; }
        protected bool UserControlled { get => userControlled; set => userControlled = value; }
        protected int MaxHitPoints { get => maxHitPoints; set => maxHitPoints = value; }
        protected int HitPoints { get => hitPoints; set => hitPoints = value; }
        protected int MaxMana { get => maxMana; set => maxMana = value; }
        protected int Mana { get => mana; set => mana = value; }
        protected int Initiative { get => initiative; set => initiative = value; }
        protected int Intelligence { get => intelligence; set => intelligence = value; }
        protected int Strength { get => strength; set => strength = value; }
        protected int MagicDefense { get => magicDefense; set => magicDefense = value; }
        protected int AttackMod { get => attackMod; set => attackMod = value; }

        /// <summary>
        /// Called from Game when a Mob attacks another mob.
        /// Returns an initial Attack Roll to see if hits
        /// </summary>
        /// <returns></returns>
        public int RollAttack()
        {
            //Determine Attack Roll
            //EDIT: Make some sort of dice roll or something to slightly randomize results.
            int roll = attackMod;

            //return result
            return roll;
        }

        /// <summary>
        /// Rolls for damage
        /// </summary>
        /// <returns></returns>
        public int RollDmg()
        {
            //EDIT: Make some sort of dice roll, perhaps use damage modifier instead of strength
            int roll = Strength;
            return roll;
        }

        /// <summary>
        /// Called when an attack (or heal) hits Mob
        /// Modifies HP as needed
        /// </summary>
        public void Hit(int damage)
        {
            hitPoints += damage;
        }
    }
}