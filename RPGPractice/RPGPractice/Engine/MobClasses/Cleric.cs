using RPGPractice.Core.Events;
using RPGPractice.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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
            Mana = MaxMana;
            Initiative = 0;
            Intelligence = 2;
            Strength = -1;
            AttackMod = -1;
            Defense = 12;
            MagicDefense = 12;
            SpecialActionString = "Heal";
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
        /// Special is called when a Mob makes a special heal.
        ///     Not all Mob types have a special heal
        ///     In this case Special is a heal spell
        /// </summary>
        /// <param name="target"></param>
        protected void UseSpecialAbility(MobData target)
        {
            //Reduce Mana
            Mana--;

            //Roll for heal
            int healValue = random.Next(1, 9) + Intelligence;

            //Build a new TargetedAction object and add to Queue
            TargetedAbility heal = new TargetedAbility();
            heal.Attacker = MobData;
            heal.Target = target;
            heal.Damage = healValue; ;
            heal.Damage = healValue; ;
            heal.DamageType = DamageType.Heal;
            TargetedAbilityQueue.Enqueue(heal);

            AppendTurnSummary($"{Name} Heals {target.Name}. \t[+{healValue} HP]");

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

            args.SpecialTargetList = allyTargetList;
        }
    }
}