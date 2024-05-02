using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses
{
    public class Cleric : Mob
    {
        public Cleric(Random random) : base("Cleric", random) { }
        public Cleric(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Cleric;
            MaxHitPoints = 25;
            MaxMana = 5; //Only casters get Mana
            Initiative = 0;
            Intelligence = 2;
            Strength = -1;
            AttackMod = -1;
            Defense = 12;
            MagicDefense = 12;
        }

        /// <summary>
        /// Special is called when a Mob makes a special attack.
        ///     Not all Mob types have a special attack
        ///     In this case Special is a heal spell
        /// </summary>
        /// <param name="target"></param>
        protected override void UseSpecialAbility(Mob target)
        {
            //Make sure theres enough Mana
            if (Mana > 0)
            {
                //Reduce Mana
                Mana--;

                //Roll for heal
                int healValue = random.Next(1, 9) + Intelligence;

                target.Heal(healValue, Name);
            }
            else
            {
                //TODO: Figure out what to do if no Mana
            }
        }
    }
}