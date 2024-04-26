using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGPractice.Events;

namespace RPGPractice.Engine.MobClasses
{
    /// <summary>
    /// Mob represents any creature (Player Character or Non Player Character)
    /// All mobs should have the same basic functioning and Attributes, these attributes should be limited to 
    /// </summary>
    public abstract class Mob
    {
        //=========================================
        //                Variables
        //=========================================
        #region Variables
        private bool userControlled;
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
        private string name;
        private string sprite;
        private string turnSummary;
        protected Random random;
        #endregion

        //=========================================
        //              Main Methods
        //=========================================
        #region Main Methods

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
        /// Returns initiative for battle initiative order
        /// Currently just a getter, but will eventually actually make a roll
        /// </summary>
        /// <returns></returns>
        public int RollInitiative()
        {
            return initiative;
        }


        /// <summary>
        /// Sets All stats for Mob
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Called from Game when a Mob attacks another mob.
        /// Returns an initial Attack Roll to see if hits
        /// </summary>
        /// <returns></returns>
        public virtual void Attack(Mob target)
        {
            //Determine Attack Roll (attackMod + 1d20)
            int attackRoll = random.Next(21);

            //Determine damage Roll (attackMod + 1d8)
            int damage = random.Next(9);

            //Critical Hit: If attack roll was a 20, then slightly boost hit chance (attackRoll) and boost damage
            if (attackRoll >= 20)
            {
                attackRoll += 5;
                damage += random.Next(9); //add another d8
            }

            //Add modifiers
            attackRoll += attackMod;
            damage += strength;


            //Tell target they are being attacked
            //  Be polite though and tell them who you are
            //  Give them turnData so they can update it
            string targetTurnSummary = target.Hit(attackRoll, damage, name);
            AppendTurnSummary(targetTurnSummary);

            //Raise TurnEnd event
            OnTurnEnd();
        }

        /// <summary>
        /// Called when an attack (or heal) hits Mob
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
                turnSummary = ($"{attacker} hit {name} for {damage} Damage!\n\t{hitPoints} health remaining.");
            }

            //if attack just barely met defense, then tell user it was a "close" attack
            else if (attackRoll == defense)
            {
                turnSummary = ($"{name} barely dodged {attacker}'s attack.");
            }

            //Else it was just a miss
            else
            {
                turnSummary = ($"{name} dodged {attacker}'s attack.");
            }

            //raise appropriate events in case of Mob death
            if (!IsAlive)
            {
                turnSummary = ($"{name} has died");
            }

            return turnSummary;
        }

        private void AppendTurnSummary(string eventMessage)
        {
            //if String is empty just replace it
            if (turnSummary == "")
            {
                turnSummary = eventMessage;
            }
            else //append
            {
                turnSummary += $"\n{eventMessage}";
            }
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
        public bool UserControlled { get => userControlled; set => userControlled = value; }
        public string Sprite { get => sprite; set => sprite = value; }
        #endregion

        //=========================================
        //        Protected Getters/Setters
        //=========================================
        #region Protected Getters/Setters

        protected string Name { get => name; set => name = value; }
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
        public event EventHandler<MobUpdateArgs>? MobUpdate;
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
            BattleEvent?.Invoke(this, args);
        }
        
        /// <summary>
        /// When Mob HP is reduced below 0HP they are dead.
        /// </summary>
        private void OnDeath()
        {
            //Raise a death event stating that Mob has died
            Death?.Invoke(this, EventArgs.Empty);
        }


        #endregion

        //=========================================
        //              Event Handlers
        //=========================================
        #region Event Handlers
        /// <summary>
        /// Publishes Class and subscribes to all events
        /// </summary>
        /// <param name="eventManager"></param>
        public void ManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            eventManager.Publish(this);

            //Subscribe to any needed events
        }


        /// <summary>
        /// UnPublishes Class and unsubscribes from all events
        /// </summary>
        /// <param name="eventManager"></param>
        public void UnManageEvents(EventManager eventManager)
        {
            //publish events to eventManager
            eventManager.Unpublish(this);

            //unSubscribe to any needed events
        }

        #endregion
    }
}