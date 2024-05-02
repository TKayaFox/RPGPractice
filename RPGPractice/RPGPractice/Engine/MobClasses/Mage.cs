using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RPGPractice.Engine.MobClasses
{
    public class Mage : Mob
    {

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
        protected override void UseSpecialAbility(Mob target)
        {
            //Make sure theres enough Mana
            if (Mana > 0)
            {
                //Reduce Mana
                Mana--;

                //Determine damage and Attack Rolls (attackMod + 1d20)
                int damage = RollDamage(Intelligence);
                int attackRoll = RollAttack(ref damage, Intelligence);

                //add attack roll to turn summary
                AppendTurnSummary($"{Name} throws a Fireball at {target.Name}. \t[Attack roll: {attackRoll} Damage {damage}]");

                //Tell targetedAbilityQueue they are being attacked
                //  add return to TurnSummary
                string targetTurnSummary = target.Hit(attackRoll, damage, Name, DamageType.Physical);
                AppendTurnSummary(targetTurnSummary);

                //Raise TurnEnd event
                OnTurnEnd();
            }
            else
            {
                //Restart Turn and display an error message "No Mana!"
                //TODO: Give all Mobs (Hero or villain) a StartTurn method. 
                //      Player controlled mobs will raise an event.
            }
        }
    }
}