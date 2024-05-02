﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGPractice.Core.Enumerations;
using RPGPractice.Core.Events;
using RPGPractice.GUI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RPGPractice.Engine.MobClasses
{
    /// <summary>
    /// MobID represents any creature (Player Character or Non Player Character)
    /// All mobs should have the same basic functioning and Attributes, these attributes should be limited to 
    /// </summary>
    public abstract class Mob
    {
        //=========================================
        //                Variables
        //=========================================
        #region Variables
        //MobID identifying data
        protected Random random;
        private string name;
        private System.Drawing.Bitmap sprite;
        private string turnSummary;
        private string specialActionString;
        private int uniqueID;
        private MobActions specialAction;
        Queue<TargetedAbility> targetedAbilityQueue;

        //TODO: Move Mana related items to interface.
        //          Not all Mobs need mana
        //Game specific stats
        private int maxHitPoints;
        private int hitPoints;
        private int initiative;
        private int intelligence;
        private int strength;
        private int attackMod;
        private int defense;
        private int magicDefense;
        private bool isDefending;
        #endregion

        //TODO: Implement Block Logic
        //TODO: Implement Special Attack Logic

        //=========================================
        //              Main Methods
        //=========================================
        #region Public Methods

        /// <summary>
        /// Constructor initializes default fields
        /// </summary>
        public Mob(string name, Random random)
        {
            this.name = name;
            this.random = random;
            isDefending = false;
            specialActionString = "";

            //Initialize all variables
            Initialize();

            //make sure hitpoints are at max
            HitPoints = MaxHitPoints;
        }

        /// <summary>
        /// Called on Mob's turn in initiative. For Mobs
        /// </summary>
        public void StartTurn(List<MobData> allyTargetList, List<MobData> enemyTargetList)
        {
            //reset turnSummary and targetedAbilityQueue for next turn
            TurnSummary = "";
            TargetedAbilityQueue = new Queue<TargetedAbility>();

            //Stop defending
            isDefending = false;

            //Run subClass specific Turn Logic.
            TakeTurn(allyTargetList, enemyTargetList);
        }

        /// <summary>
        /// Run actual turn logic
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        protected abstract void TakeTurn(List<MobData> allyTargetList, List<MobData> enemyTargetList);


        /// <summary>
        /// Called from Game when a MobID attacks another mobID.
        /// Returns an initial Attack Roll to see if hits
        /// </summary>
        /// <returns></returns>
        public virtual void Attack(MobData target)
        {
            //Determine damage and Attack Rolls (attackMod + 1d20)
            int damage = RollDamage(strength);
            int attackRoll = RollAttack(ref damage, attackMod);

            //add ability roll to turn summary
            AppendTurnSummary($"{name} Attacks {target.Name}. \t[Attack roll: {attackRoll} Damage {damage}]");

            //Build a new TargetedAction object and add to Queue
            TargetedAbility attack = new TargetedAbility();
            attack.Attacker = MobData;
            attack.Target = target;
            attack.AttackRoll = attackRoll;
            attack.Damage = damage;
            attack.DamageType = DamageType.Physical;
            TargetedAbilityQueue.Enqueue(attack);

            //end turn
            OnTurnEnd();
        }


        /// <summary>
        /// Heals hitpoints
        /// If no inputs provided, heals Mob to full health
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="healer"></param>
        public virtual string Heal(int damage)
        {
            hitPoints += damage;

            //prevent over-healing
            if (hitPoints > MaxHitPoints)
            {
                hitPoints = MaxHitPoints;
            }

            //return string stating result
            return ($"{Name} healed back to {hitPoints} health!");
        }
        public virtual string Heal()
        {
            //if not specified, heal all
            hitPoints = MaxHitPoints;
            return $"{name} healed to full health";
        }


        /// <summary>
        /// Called when hit by a physical ability
        /// </summary>
        /// <param name="attackRoll"></param>
        /// <param name="damage"></param>
        /// <param name="attacker"></param>
        /// <returns>string describing what occurred during this turn</returns>
        public virtual string DefendPhysical(int attackRoll, int damage)
        {            
            //Make sure action wouldnt actually heal user, if it would allow it to do so
            if (damage < 0)
            {
                return Heal(damage);
            }
            else
            {
                //Calculate Block result and add Strength if currently isDefending
                return DefendResult(attackRoll,defense, Strength, damage);
            }
        }


        /// <summary>
        /// Called when hit by a Magical offensive ability
        /// </summary>
        /// <param name="attackRoll"></param>
        /// <param name="damage"></param>
        /// <param name="attacker"></param>
        /// <returns>string describing what occurred during this turn</returns>
        public virtual string DefendMagic(int attackRoll, int damage)
        {
            //Make sure action wouldnt actually heal user, if it would allow it to do so
            if (damage < 0)
            {
                return Heal(damage);
            }
            else
            {
                //Calculate Block result and add Intelligence if currently isDefending
                return DefendResult(attackRoll, magicDefense, Intelligence, damage);
            }
        }


        /// <summary>
        /// Special is called when a Mob makes a special ability.
        ///     Not all Mob types have a special ability
        /// Only tries to use Special if CanUseSpecial returns true
        /// </summary>
        /// <param name="target"></param>
        public virtual void Special(MobData target)
        {
                //throw exception telling caller to try again
                throw new NotSupportedException("This class does not have a special ability!");
        }

        public virtual void Block()
        {
            //temporarily give the Defending buff
            isDefending = true;
        }
        #endregion

        //=========================================
        //          Public Getters/Setters
        //=========================================
        #region Public Getters/Setters
        /// <summary>
        /// Determines if Mob is dead (below 0 hitpoints) and returns result
        /// </summary>
        public virtual bool IsAlive
        {
            get
            {
                return (hitPoints > 0);
            }
        }

        /// <summary>
        /// Builds a GUI specific MobData object which holds only the information needed for MobData
        /// Refactor: Can eventually subscribe it to a MobUpdate event which allows the MobID object to update it's GUI counterpart directly
        /// </summary>
        public virtual MobData MobData
        {
            get
            {
                MobData data = new MobData();
                data.Sprite = sprite;
                data.Name = name;
                data.UniqueID = uniqueID;
                data.IsNPC = (this is NPC); //if this object falls under NPC (MobID subclass)
                data.IsAlive = IsAlive;
                data.SpecialActionString = SpecialActionString;

                //subscibe Mob to Death events
                Death += data.OnDeath_Handler;

                return data;
            }
        }

        /// <summary>
        /// Rolls initiative for battle initiative order
        /// (Currently just a getter)but will eventually actually make a roll
        /// </summary>
        /// <returns></returns>
        public virtual int Initiative { get => initiative; set => initiative = value; }

        /// standard setters/getters
        public virtual int UniqueID { get => uniqueID; set => uniqueID = value; }
        public virtual System.Drawing.Bitmap Sprite { get => sprite; protected set => sprite = value; }
        public virtual string Name { get => name; protected set => name = value; }
        public virtual MobActions SpecialAction { get => specialAction; protected set => specialAction = value; }

        #endregion

        //=========================================
        //          Protected Methods
        //=========================================
        #region Abstract Methods
        protected virtual string Hurt(int damage)
        {
            string result;

            hitPoints -= damage;

            result = ($"{Name} took {damage} damage");


            //Check for death
            if (!IsAlive)
            {
                result += ($"\r\n{Name} has Died!");
                OnDeath();
            }

            //return string stating result
            return result;
        }

        /// <summary>
        /// Sets All stats for MobID
        /// </summary>
        protected abstract void Initialize();
        #endregion

        #region Protected Methods

        /// <summary>
        /// Handles end logic for Block family of methods
        ///     Assigns damage as needed and returns turnSummary
        /// </summary>
        /// <param name="attackRoll"></param>
        /// <param name="dodgeValue"></param>
        /// <param name="damage"></param>
        /// <returns>string describing what occurred during this turn</returns>
        private string DefendResult(int attackRoll, int defense, int defenseMod, int damage)
        {
            string turnSummary = "";

            //If defending, boost defense
            int dodgeValue = Defense;
            if (isDefending)
            {
                dodgeValue += defenseMod;
            }

            //If ability hits, take damage. Update turnSummary with what happened either way
            if (attackRoll > dodgeValue)
            {
                turnSummary += Hurt(damage);
            }
            //If ability meets, then take half damage (rounded down, so integer is not a problem)
            else if (attackRoll == dodgeValue)
            {
                int halfDamage = damage / 2;
                turnSummary += $"Glancing Blow! ";
                turnSummary += Hurt(halfDamage);
            }
            else
            {
                turnSummary = ($"{name} avoided the attack. \t[{attackRoll} < {dodgeValue}]");
            }

            return turnSummary;
        }

        /// <summary>
        /// essentially appends value to end of turnSummart string.
        /// TODO: Is there a String method for this?
        /// </summary>
        /// <param name="eventMessage"></param>
        protected virtual void AppendTurnSummary(string eventMessage)
        {
            //if String is empty just replace it
            if (TurnSummary == "")
            {
                TurnSummary = eventMessage;
            }
            else //append
            {
                TurnSummary += $"\r\n{eventMessage}";
            }
        }

        /// <summary>
        /// Rolls ability dice (1d20) and adds a modifier
        /// TODO: Setup Dice class to do this instead
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        protected virtual int RollAttack(ref int damage, int modifier)
        {
            //Determine Attack Roll (attackMod + 1d20)
            int attackRoll = random.Next(1, 21);

            //Critical Hit: If ability roll was a 20, then slightly boost hit chance (attackRoll) and boost damage
            if (attackRoll >= 20)
            {
                attackRoll += 5;
                damage += random.Next(9); //add another d8
            }
            attackRoll += modifier;
            return attackRoll;
        }

        /// <summary>
        /// rolls a damage dice and adds a modifier
        /// TODO: Setup Dice class to do this instead
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns></returns>
        protected virtual int RollDamage(int modifier)
        {
            //Determine damage Roll (strength + 1d8)
            int damage = random.Next(1, 9);
            damage += modifier;
            return damage;
        }
        #endregion


        //=========================================
        //        Protected Getters/Setters
        //         Only used by subclasses
        //=========================================
        #region Protected Getters/Setters

        /// <summary>
        /// Called to determine if Mob can actually use their special ability
        /// </summary>
        protected virtual int Defense { get => defense; set => defense = value; }
        protected virtual int MaxHitPoints { get => maxHitPoints; set => maxHitPoints = value; }
        protected virtual int HitPoints { get => hitPoints; set => hitPoints = value; }
        protected virtual int Intelligence { get => intelligence; set => intelligence = value; }
        protected virtual int Strength { get => strength; set => strength = value; }
        protected virtual int MagicDefense { get => magicDefense; set => magicDefense = value; }
        protected virtual int AttackMod { get => attackMod; set => attackMod = value; }
        protected virtual string TurnSummary { get => turnSummary; set => turnSummary = value; }
        protected virtual Queue<TargetedAbility> TargetedAbilityQueue { get => targetedAbilityQueue; set => targetedAbilityQueue = value; }
        protected string SpecialActionString { get => specialActionString; set => specialActionString = value; }
        #endregion

        //=========================================
        //                  Events
        //=========================================
        public event EventHandler<TurnEndEventArgs> TurnEnd;
        public event EventHandler Death;

        #region Event Invokers

        /// <summary>
        /// Packages and raisesBattle Events 
        /// (Which display for user a readout of what has happened in the battle so far)
        /// </summary>
        /// <param name="target">Optional parameter if attacking a target</param>
        protected virtual void OnTurnEnd()
        {
            //This data will always be stored in args
            TurnEndEventArgs args = new TurnEndEventArgs();
            args.TargetedAbilityQueue = TargetedAbilityQueue;
            args.Attacker = MobData;
            args.TurnSummary = TurnSummary;

            //invoke method
            TurnEnd?.Invoke(this, args);
        }

        /// <summary>
        /// When MobID HP is reduced below 0HP they are dead.
        /// </summary>
        protected void OnDeath()
        {
            //Raise a death event stating that MobID has died
            Death.Invoke(this, EventArgs.Empty);
        }

        #endregion
        #region Event Managers
        /// <summary>
        /// Publishes MobData and subscribes to all events
        /// </summary>
        /// <param name="eventManager"></param>
        public virtual void ManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            Death += eventManager.OnDeath_Aggregator;
        }

        /// <summary>
        /// UnPublishes MobData and unsubscribes from all events
        /// </summary>
        /// <param name="eventManager"></param>
        public virtual void UnManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            TurnEnd -= eventManager.OnTurnEnd_Aggregator;
        }
        #endregion
    }
}