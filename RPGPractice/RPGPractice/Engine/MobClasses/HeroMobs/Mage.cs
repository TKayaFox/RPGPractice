using RPGPractice.Core.Events;
using RPGPractice.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RPGPractice.Engine.MobClasses.HeroMobs
{
    public class Mage : PlayerMob
    {
        private int maxMana; //Only casters get Mana
        private int mana;
        protected virtual int MaxMana { get => maxMana; set => maxMana = value; }
        protected virtual int Mana { get => mana; set => mana = value; }

        public Mage(string name) : base(name) { }

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
            Initiative = 2;
            Intelligence = 3;
            Strength = -2;
            AttackMod = -2;
            Defense = 10;
            MagicDefense = 15;
            SpecialActionString = "Fireball";
        }

        /// <summary>
        /// Special is called when a Mob makes a special ability.
        ///     Not all Mob types have a special ability
        /// Only tries to use Special if CanUseSpecial returns true
        /// </summary>
        /// <param name="target"></param>
        public override void Special(MobData target)
        {
            //Only attempt Special if CanUseSpecial returns true
            //  Logic for CanUseSpecial is determined by subclass
            if (Mana > 0)
            {
                UseSpecialAbility(target);
            }
            else
            {
                //throw exception telling caller to try again
                throw new NotSupportedException("Out of Mana!");
            }
        }

        /// <summary>
        /// Special is called when a Mob makes a special attack.
        ///     Not all Mob types have a special attack
        ///     In this case, Special is a damaging spell
        /// </summary>
        /// <param name="target"></param>
        protected void UseSpecialAbility(MobData target)
        {
            //Reduce Mana
            Mana--;

            //Determine damage and Attack Rolls (attackMod + 1d20)
            (int attackRoll, int damage) = base.Dice.RollAttack(base.Intelligence, base.Intelligence);

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



        /// <summary>
        /// Compiles TargetList Lists for OnPlayerTurn
        ///     Override to alter Special Action behavior
        /// </summary>
        /// <param name="allyTargetList"></param>
        /// <param name="enemyTargetList"></param>
        /// <param name="args"></param>
        protected override void CompileTargetLists(List<MobData> allyTargetList, List<MobData> enemyTargetList, PlayerTurnEventArgs args)
        {
            //Make lists of viable targets
            args.AttackTargetList = enemyTargetList;

            args.SpecialTargetList = enemyTargetList;
        }
    }
}
