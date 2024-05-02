using System;
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
        private int uniqueID;
        private MobActions specialAction; 

        //TODO: Move Mana related items to interface.
        //          Not all Mobs need mana
        //Game specific stats
        private int maxHitPoints;
        private int hitPoints;
        private int maxMana; //Only casters get Mana
        private int mana;
        private int initiative;
        private int intelligence;
        private int strength;
        private int attackMod;
        private int defense;
        private int magicDefense;
        private bool isDefending;
        private bool canUseSpecial;
        #endregion

        //TODO: Implement Defend Logic
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
            canUseSpecial = false;

            //Initialize all variables
            Initialize();

            //Set mana and hitpoints to max
            mana = MaxMana;
            HitPoints = MaxHitPoints;
            turnSummary = "";
        }

        /// <summary>
        /// Called on Mob's turn in initiative. For Mobs
        /// </summary>
        public abstract void TakeTurn(List<MobData> allyTargetList, List<MobData> enemyTargetList);

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

            //add attack roll to turn summary
            AppendTurnSummary($"{name} Attacks {target.Name}. \t[Attack roll: {attackRoll} Damage {damage}]");

            //Tell targetQueue they are being attacked
            OnTurnEnd(target);
        }

        public virtual void Defend()
        {
            //Start defending
            isDefending = true;

            //TODO: Somehow turn off Defend at start of next turn
            //TODO: give ALL mobs a TakeTurn method
            //          Move appropriate event into Mob
            //          Instead of NPC subtype, just go back to using isPlayerControlled
        }

        /// <summary>
        /// 
        /// Called when an attack (or heal) hits MobID
        /// Modifies HP as needed
        /// Async is purely so that can delay the task slightly so things feel less instantaneous
        /// </summary>
        /// <param name="attackRoll">Value used to determine hit or miss</param>
        /// <param name="damage">Value determining damage on hit</param>
        /// <param name="attacker">name of the attacker</param>
        /// <param name="damageType">type of damage (ex: physical, magical)</param>
        /// <returns>String summarizing result of hit</returns>
        public virtual string Hit(int attackRoll, int damage, string attacker, DamageType damageType)
        {
            //turnSummary is returned to attacker so it knows what happened
            string turnSummary = "";

            //If attack is for negative damage, dont attempt to defend just take the heal
            if (attackRoll < 0)
            {
                turnSummary = Heal(-damage, attacker);
            }
            //determine appropriate defense and calculate damage if needed
            else
            {
                //call different defend method depending on damage type
                switch (damageType)
                {
                    case DamageType.Physical:
                        turnSummary = DefendPhysical(attackRoll, damage, attacker);
                        break;
                    case DamageType.Magic:
                        turnSummary = DefendMagic(attackRoll, damage, attacker);
                        break;
                }
            }

            //raise appropriate events in case of MobID death
            if (!IsAlive)
            {
                turnSummary = ($"{name} has died");
                OnDeath();
            }

            return turnSummary;
        }


        /// <summary>
        /// Special is called when a Mob makes a special ability.
        ///     Not all Mob types have a special ability
        /// Only tries to use Special if CanUseSpecial returns true
        /// </summary>
        /// <param name="target"></param>
        public virtual void Special(Mob target)
        {
            //Only attempt Special if CanUseSpecial returns true
            //  Logic for CanUseSpecial is determined by subclass
            if (CanUseSpecial)
            {
                UseSpecialAbility(target);
            }
            else
            {
                //TODO: Throw exception tellng calling class that Turn was not finished properly
            }
        }


        /// <summary>
        /// Heals hitpoints
        /// If no inputs provided, heals Mob to full health
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="healer"></param>
        public virtual string Heal(int damage, string healer)
        {
            hitPoints += damage;

            //prevent over-healing
            if (hitPoints > MaxHitPoints)
            {
                hitPoints = MaxHitPoints;
            }

            //return string stating result
            return ($"{healer} healed {name} back to {hitPoints} health!");
        }
        public virtual string Heal()
        {
            //if not specified, heal all
            hitPoints = MaxHitPoints;
            return $"{name} healed to full health";
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

        /// <summary>
        /// Sets All stats for MobID
        /// </summary>
        protected abstract void Initialize();
        #endregion

        #region Protected Methods
        /// <summary>
        /// Special is called when a Mob makes a special attack.
        ///     Not all Mob types have a special attack, this should not be called in such cases
        /// </summary>
        /// <param name="target"></param>
        protected virtual void UseSpecialAbility(Mob target)
        {
            //TODO: Throw exception tellng calling class that Turn was not finished properly
        }

        /// <summary>
        /// Called when hit by a physical attack
        /// </summary>
        /// <param name="attackRoll"></param>
        /// <param name="damage"></param>
        /// <param name="attacker"></param>
        /// <returns>string summarizing what happened</returns>
        protected virtual string DefendPhysical(int attackRoll, int damage, string attacker)
        {
            string turnSummary;

            //If attack hits, take damage. Either way update turnSummary with what happened
            if (attackRoll > defense)
            {
                hitPoints -= damage;
                turnSummary = ($"{attacker} hit {name} for {damage} Damage!\t[{hitPoints} health remaining]");
            }
            else
            {
                turnSummary = ($"{name} dodged {attacker}'s attack. \t[{attackRoll} < {defense}]");
            }

            return turnSummary;
        }

        /// <summary>
        /// Called when hit by a Magical attack
        /// </summary>
        /// <param name="attackRoll"></param>
        /// <param name="damage"></param>
        /// <param name="attacker"></param>
        /// <returns></returns>
        protected virtual string DefendMagic(int attackRoll, int damage, string attacker)
        {
            string turnSummary;
            //If attack hits reduce hitpoints as needed
            //attackRoll < defense
            if (attackRoll > magicDefense)
            {
                hitPoints -= damage;
                turnSummary = ($"{attacker} hit {name} for {damage} Magic Damage!\t[{hitPoints} health remaining]");
            }
            //Else it was just a miss
            else
            {
                turnSummary = ($"{name} dodged {attacker}'s attack. \t[{attackRoll} < {defense}]");
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
            if (turnSummary == "")
            {
                turnSummary = eventMessage;
            }
            else //append
            {
                turnSummary += $"\r\n{eventMessage}";
            }
        }

        /// <summary>
        /// Rolls attack dice (1d20) and adds a modifier
        /// TODO: Setup Dice class to do this instead
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        protected virtual int RollAttack(ref int damage, int modifier)
        {
            //Determine Attack Roll (attackMod + 1d20)
            int attackRoll = random.Next(1, 21);

            //Critical Hit: If attack roll was a 20, then slightly boost hit chance (attackRoll) and boost damage
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
        /// Called to determine if Mob can actually use their special attack
        /// </summary>
        protected virtual bool CanUseSpecial { get => canUseSpecial; }
        protected virtual int Defense { get => defense; set => defense = value; }
        protected virtual int MaxHitPoints { get => maxHitPoints; set => maxHitPoints = value; }
        protected virtual int HitPoints { get => hitPoints; set => hitPoints = value; }
        protected virtual int MaxMana { get => maxMana; set => maxMana = value; }
        protected virtual int Mana { get => mana; set => mana = value; }
        protected virtual int Intelligence { get => intelligence; set => intelligence = value; }
        protected virtual int Strength { get => strength; set => strength = value; }
        protected virtual int MagicDefense { get => magicDefense; set => magicDefense = value; }
        protected virtual int AttackMod { get => attackMod; set => attackMod = value; }
        #endregion

        //=========================================
        //                  Events
        //=========================================
        public event EventHandler<TurnEndEventArgs>? BattleEvent;
        public event EventHandler<TurnEndEventArgs> TurnEnd;
        public event EventHandler? Death;

        #region Event Invokers

        /// <summary>
        /// Packages and raisesBattle Events 
        /// (Which display for user a readout of what has happened in the battle so far)
        /// Overloaded because sometimes there will be a target and sometimes there will not.
        /// </summary>
        /// <param name="target">Optional parameter if attacking a target</param>
        protected virtual void OnTurnEnd(MobData target)
        {
            //Package
            TurnEndEventArgs args = new TurnEndEventArgs();
            args.AddTarget(target);

            //reset turnSummary for next turn
            turnSummary = "";
        }
        protected virtual void OnTurnEnd()
        {
            TurnEndEventArgs args = new TurnEndEventArgs();
            OnTurnEnd(args);
        }
        protected virtual void OnTurnEnd(TurnEndEventArgs args)
        {
            //This data will always be stored in args
            args.Attacker = MobData;
            args.TurnSummary = turnSummary;

            //invoke method
            TurnEnd?.Invoke(this, args);
        }

        /// <summary>
        /// When MobID HP is reduced below 0HP they are dead.
        /// </summary>
        protected void OnDeath()
        {
            //Raise a death event stating that MobID has died
            Death?.Invoke(this, EventArgs.Empty);
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
            TurnEnd += eventManager.OnTurnEnd_Aggregator;
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
            Death -= eventManager.OnDeath_Aggregator;
        }
        #endregion
    }
}