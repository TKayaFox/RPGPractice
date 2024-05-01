using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGPractice.Events;
using RPGPractice.GUI;

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
        #endregion

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

            //Initialize all variables
            Initialize();

            //Set mana and hitpoints to max
            mana = MaxMana;
            HitPoints = MaxHitPoints;
            turnSummary = "";
        }

        /// <summary>
        /// Builds a GUI specific MobData object which holds only the information needed for MobData
        /// Refactor: Can eventually subscribe it to a MobUpdate event which allows the MobID object to update it's GUI counterpart directly
        /// </summary>
        public MobData GetMobData()
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

        /// <summary>
        /// Returns initiative for battle initiative order
        /// Currently just a getter, but will eventually actually make a roll
        /// </summary>
        /// <returns></returns>
        public int RollInitiative()
        {
            return initiative;
        }

        /// <summary>
        /// Called from Game when a MobID attacks another mobID.
        /// Returns an initial Attack Roll to see if hits
        /// </summary>
        /// <returns></returns>
        public virtual void Attack(Mob target)
        {
            //Determine Attack Roll (attackMod + 1d20)
            int attackRoll = random.Next(1,21) + attackMod;

            //Determine damage Roll (strength + 1d8)
            int damage = random.Next(1,9) + strength;

            //Critical Hit: If attack roll was a 20, then slightly boost hit chance (attackRoll) and boost damage
            if (attackRoll >= 20)
            {
                attackRoll += 5;
                damage += random.Next(9); //add another d8
            }

            //Add modifiers
            attackRoll += attackMod;
            damage += strength;

            //add attack roll to turn summary
            AppendTurnSummary($"{name} Attacks {target.name}. \t[Attack roll: {attackRoll} Damage {damage}]");

            //Tell target they are being attacked
            //  Be polite though and tell them who you are
            //  Give them turnData so they can update it
            string targetTurnSummary = target.Hit(attackRoll, damage, name);
            AppendTurnSummary(targetTurnSummary);

            //Raise TurnEnd event
            OnTurnEnd();
        }

        /// <summary>
        /// Called when an attack (or heal) hits MobID
        /// Modifies HP as needed
        /// </summary>
        public virtual string Hit(int attackRoll, int damage, string attacker)
        {
            //turnSummary is returned to attacker so it knows what happened
            string turnSummary = "";


            //If attack is for negative damage, dont attempt to defend just take the heal
            if (attackRoll < 0)
            {
                turnSummary = Heal(-damage, attacker);
            }

            //If attack hits reduce hitpoints as needed
            //attackRoll < defense
            else if (attackRoll > defense)
            {
                hitPoints -= damage;
                turnSummary = ($"{attacker} hit {name} for {damage} Damage!\t[{hitPoints} health remaining]");
            }

            //if attack just barely met defense, then tell user it was a "close" attack
            else if (attackRoll == defense)
            {
                turnSummary = ($"{name} barely dodged {attacker}'s attack. \t[{attackRoll} meets {defense}]");
            }

            //Else it was just a miss
            else
            {
                turnSummary = ($"{name} dodged {attacker}'s attack. \t[{attackRoll} < {defense}]");
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
        /// Heals hitpoints
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="healer"></param>
        public string Heal(int damage, string healer)
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

        //EDIT: Add magicAttack
        //EDIT: Add magicHit

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets All stats for MobID
        /// </summary>
        protected abstract void Initialize();

        private void AppendTurnSummary(string eventMessage)
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

        #endregion

        //=========================================
        //          Public Getters/Setters
        //=========================================
        #region Public Getters/Setters
        /// <summary>
        /// Public Getters are used throughout the game
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return (hitPoints > 0);
            }
        }
        public System.Drawing.Bitmap Sprite { get => sprite; set => sprite = value; }
        public int UniqueID { get => uniqueID; set => uniqueID = value; }
        public string Name { get => name; set => name = value; }
        #endregion

        //=========================================
        //        Protected Getters/Setters
        //=========================================
        #region Protected Getters/Setters

        protected int Defense { get => defense; set => defense = value; }
        protected int MaxHitPoints { get => maxHitPoints; set => maxHitPoints = value; }
        protected int HitPoints { get => hitPoints; set => hitPoints = value; }
        protected int MaxMana { get => maxMana; set => maxMana = value; }
        protected int Mana { get => mana; set => mana = value; }
        protected int Initiative {set => initiative = value; }
        protected int Intelligence { get => intelligence; set => intelligence = value; }
        protected int Strength { get => strength; set => strength = value; }
        protected int MagicDefense { get => magicDefense; set => magicDefense = value; }
        protected int AttackMod { get => attackMod; set => attackMod = value; }

        #endregion

        //=========================================
        //                  Events
        //=========================================
        public event EventHandler<TurnEndEventArgs>? BattleEvent;
        public event EventHandler<TurnEndEventArgs> TurnEnd;
        public event EventHandler? Death;

        #region Events

        /// <summary>
        /// Packages and raisesBattle Events (Which display for user a readout of what has happened in the battle so far)
        /// </summary>
        /// <param name="output"></param>
        private void OnTurnEnd()
        {
            //Package and send battle message
            TurnEndEventArgs args = new TurnEndEventArgs();
            args.TurnSummary = turnSummary;

            //reset turnSummary for next turn
            turnSummary = "";

            //invoke method
            TurnEnd?.Invoke(this, args);
        }
        
        /// <summary>
        /// When MobID HP is reduced below 0HP they are dead.
        /// </summary>
        private void OnDeath()
        {
            //Raise a death event stating that MobID has died
            Death?.Invoke(this, EventArgs.Empty);
        }


        #endregion

        //=========================================
        //              Event Handlers
        //=========================================
        #region Event Handlers
        /// <summary>
        /// Publishes MobData and subscribes to all events
        /// </summary>
        /// <param name="eventManager"></param>
        public void ManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            TurnEnd += eventManager.OnTurnEnd_Aggregator;
            Death += eventManager.OnDeath_Aggregator;
        }


        /// <summary>
        /// UnPublishes MobData and unsubscribes from all events
        /// </summary>
        /// <param name="eventManager"></param>
        public void UnManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            TurnEnd -= eventManager.OnTurnEnd_Aggregator;
            Death -= eventManager.OnDeath_Aggregator;
        }
        #endregion
    }
}