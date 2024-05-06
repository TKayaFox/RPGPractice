using RPGPractice.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RPGPractice.Engine.MobClasses.EnemyMobs
{
    public class Dragon : EnemyMob
    {
        public Dragon(string name, Random random) : base(name, random) { }

        /// <summary>
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Dragon;
            MaxHitPoints = 30;
            Initiative = -1;
            Intelligence = 4;
            Strength = 3;
            AttackMod = 3;
            Defense = 16;
            MagicDefense = 16;
        }

        /// <summary>
        /// Automate a turn for the Mob
        /// by default, attacks a random targetedAbilityQueue
        /// </summary>
        /// <param name="heroTargetList"></param>
        /// <param name="enemyTargetList"></param>
        protected override void TakeTurn(List<MobData> heroTargetList, List<MobData> enemyTargetList)
        {
            //Determine Attack type by rolling 1d2 (flipping a coin)
            if (Dice.Roll(2) == 1)
            {
                //choose a target
                MobData target = SetTarget(heroTargetList);
                FireBreath(target);
            }
            else
            {
                //Swipe attack targets ALL heroes
                SwipeAttack(heroTargetList);
            }
        }

        /// <summary>
        /// Initiates Dragon's Breath attack on a single target
        /// </summary>
        /// <param name="target">The poor soul being targeted</param>
        private void FireBreath(MobData target)
        {
            //Determine damage and Attack Rolls (attackMod + 1d20)
            (int attackRoll, int damage) = Dice.RollAttack(Intelligence, Intelligence);

            //add attack roll to turn summary
            AppendTurnSummary($"{Name} breathes fire on {target.Name}. \t[Attack roll: {attackRoll} Damage {damage}]");

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

        public void SwipeAttack(List<MobData> heroTargetList)
        {
            //Determine damage and Attack Rolls (attackMod + 1d20)
            (int attackRoll, int damage) = Dice.RollAttack(AttackMod, Strength);

            //add ability roll to turn summary
            AppendTurnSummary($"{Name} swipes at all targets with it's Claws!\r\n\t[Attack roll: {attackRoll} Damage {damage}]");

            //Build a new TargetedAction object and add to Queue
            //  Most this data will remain the same
            TargetedAbility attack = new TargetedAbility();
            attack.Attacker = Data;
            attack.AttackRoll = attackRoll;
            attack.Damage = damage;
            attack.DamageType = DamageType.Physical;


            //Add all targets to the Queue so they are handled properly
            foreach (MobData target in heroTargetList)
            {
                //change target
                attack.Target = target;
                TargetedAbilityQueue.Enqueue(attack);
            }

            //end turn
            OnTurnEnd();

        }

        // Static Methods
        /// <summary>
        /// Returns a minimum and maximum number of this Enemy type for encounter generation
        ///     required for all Enemy types, but cannot be made abstract due to static behavior
        /// </summary>
        /// <param name="combatLevel"></param>
        /// <returns></returns>
        public static (int min, int max) EncounterData(int combatLevel)
        {
            int min = 0;
            int max = 0;

            //use combatLevel to determine max.
            switch (combatLevel)
            {
                //Dragon introduction
                case 4:
                    max = 1;
                    min = 1;
                    break;

                case > 20:
                    max = 5;
                    break;
                case > 18:
                    max = 4;
                    break;
                case > 16:
                    max = 3;
                    break;
                case > 12:
                    max = 2;
                    break;
                case > 7:
                    max = 1;
                    break;
            }

            return (min, max);
        }
    }
}