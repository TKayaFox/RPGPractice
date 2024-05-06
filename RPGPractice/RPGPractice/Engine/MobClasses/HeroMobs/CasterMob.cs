﻿using RPGPractice.Core.Events;
using RPGPractice.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Engine.MobClasses.HeroMobs
{
    internal abstract class CasterMob : PlayerMob
    {
        private int maxMana; //Only casters get Mana
        private int mana;
        private int manaRegen;
        /// <summary>
        /// Point at which Mana regeneration adds to mana
        ///     Currently set to 1 effectively meaning mana regenerates whenever it wasnt used last turn
        /// </summary>
        private const int MANA_REGEN_THRESHOLD = 1;


        protected CasterMob(string name) : base(name)
        {
            mana = maxMana;
            manaRegen = 0;
        }

        //TODO: Are these needed?
        protected virtual int MaxMana { get => maxMana; set => maxMana = value; }
        protected virtual int Mana { get => mana; set => mana = value; }


        /// <summary>
        /// Run actual turn logic
        ///     Increase Mana every other turn
        /// TODO: Refactor: There has to be a better way to do this without overloading TakeTurn.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        protected void TakeTurn(List<MobData> allyTargetList, List<MobData> enemyTargetList)
        {
            //Regenerate Mana
            manaRegen++;

            //Increment Mana if manaRegen reaches the appropriate value
            if (manaRegen > MANA_REGEN_THRESHOLD)
            {
                mana++;
            }

            //Follow normal Turn Logic
            base.TakeTurn(allyTargetList, enemyTargetList);
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
                //Reset Mana regen to 0 then cast spell
                manaRegen = 0;
                CastSpell(target);
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
        protected abstract void CastSpell(MobData target);



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
