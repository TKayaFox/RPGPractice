using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RPGPractice.Engine.MobClasses.HeroMobs
{
    /// <summary>
    /// A Magic User
    ///    Casts Magic Damaging spells
    ///    High Magic Defense
    ///    Low Physical Defense
    /// </summary>
    public class Mage : CasterMob
    {
        public Mage(string name) : base(name) { }

        /// <summary>
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Mage;
            MaxHitPoints = 16;
            MaxMana = 5; //Only casters get ManaString
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
        /// Special is called when a Mob makes a special attack.
        ///     Not all Mob types have a special attack
        ///     In this case, Special is a damaging spell
        /// </summary>
        /// <param name="target"></param>
        protected override void CastSpell(MobData target)
        {
            //Reduce ManaString
            Mana--;

            //Determine damage and Attack Rolls (attackMod + 1d20)
            (int attackRoll, int damage) = Dice.RollAttack(Intelligence, Intelligence);

            //add attack roll to turn summary
            AppendTurnSummary($"{Name} throws a Fireball at {target.Name}. \t[Attack roll: {attackRoll} Damage {damage}]");

            //Build a new TargetedAction object and add to Queue
            TargetedAbility attack = new TargetedAbility();
            attack.Attacker = Data;
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
