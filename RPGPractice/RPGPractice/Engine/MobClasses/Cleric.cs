using RPGPractice.Core.Events;
using RPGPractice.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses
{
    public class Cleric : PlayerMob
    {
        private int maxMana; //Only casters get Mana
        private int mana;
        protected virtual int MaxMana { get => maxMana; set => maxMana = value; }
        protected virtual int Mana { get => mana; set => mana = value; }

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
        /// Called to determine if Mob can actually use their special attack
        /// Only allow Special Attack if current mana greater than 0
        /// </summary>
        protected override bool CanUseSpecial
        {
            get
            {
                return (Mana > 0);
            }
        }

        /// <summary>
        /// Special is called when a Mob makes a special heal.
        ///     Not all Mob types have a special heal
        ///     In this case Special is a heal spell
        /// </summary>
        /// <param name="target"></param>
        protected override void UseSpecialAbility(MobData target)
        {
            //Reduce Mana
            Mana--;

            //Roll for heal
            int healValue = random.Next(1, 9) + Intelligence;

            //Build a new TargetedAction object and add to Queue
            TargetedAbility heal = new TargetedAbility();
            heal.Attacker = MobData;
            heal.Target = target;
            heal.DamageType = DamageType.Heal;
            TargetedAbilityQueue.Enqueue(heal);

            //end turn
            OnTurnEnd();
        }
    }
}