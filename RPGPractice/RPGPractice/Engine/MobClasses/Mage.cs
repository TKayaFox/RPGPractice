using RPGPractice.Core.Events;
using RPGPractice.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RPGPractice.Engine.MobClasses
{
    public class Mage : PlayerMob
    {
        private int maxMana; //Only casters get Mana
        private int mana;
        protected virtual int MaxMana { get => maxMana; set => maxMana = value; }
        protected virtual int Mana { get => mana; set => mana = value; }

        public Mage(Random random) : base("Mage", random) { }
        public Mage(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Mage;
            MaxHitPoints = 16;
            MaxMana = 5; //Only casters get Mana
            Mana = MaxMana;
            Initiative = 0;  //roll 1d20
            Intelligence = 3;
            Strength = -2;
            AttackMod = -2;
            Defense = 10;
            MagicDefense = 15;
        }

        /// <summary>
        /// Called to determine if Mob can actually use their special attack
        /// Only allow Special Attack if current mana greater than 0
        /// </summary>
        protected override bool CanUseSpecial 
        { get
            {
                return (Mana > 0);
            }
        }

        /// <summary>
        /// Special is called when a Mob makes a special attack.
        ///     Not all Mob types have a special attack
        ///     In this case, Special is a damaging spell
        /// </summary>
        /// <param name="target"></param>
        protected override void UseSpecialAbility(MobData target)
        {
            //Reduce Mana
            Mana--;

            //Determine damage and Attack Rolls (attackMod + 1d20)
            int damage = RollDamage(Intelligence);
            int attackRoll = RollAttack(ref damage, Intelligence);

            //add attack roll to turn summary
            AppendTurnSummary($"{Name} throws a Fireball at {target.Name}. \t[Attack roll: {attackRoll} Damage {damage}]");
                
            //Build a new TargetedAction object and add to Queue
            TargetedAbility attack = new TargetedAbility();
            attack.Attacker = MobData;
            attack.Target = target;
            attack.AttackRoll = attackRoll;
            attack.Damage = damage;
            attack.DamageType = DamageType.Magic;
            TargetedAbilityQueue.Enqueue(attack);

            //end turn
            OnTurnEnd();
            }
        }
    }
