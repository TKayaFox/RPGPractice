using RPGPractice.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RPGPractice.Engine.MobClasses.HeroMobs
{
    public class Cleric : CasterMob
    {

        public Cleric(string name) : base(name) { }

        /// <summary>
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Cleric;
            MaxHitPoints = 25;
            MaxMana = 5; //Only casters get ManaString
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
        /// Special is called when a Mob makes a special heal.
        ///     Not all Mob types have a special heal
        ///     In this case Special is a heal spell
        /// </summary>
        /// <param name="target"></param>
        protected override void CastSpell(MobData target)
        {
            //Reduce ManaString
            Mana--;

            //Roll for heal
            int healValue = Dice.RollDamage(base.Intelligence);

            //Build a new TargetedAction object and add to Queue
            TargetedAbility heal = new TargetedAbility();
            heal.Attacker = Data;
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