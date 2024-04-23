using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice
{
    /// <summary>
    /// Mob represents any creature (Player Character or Non Player Character)
    /// All mobs should have the same basic functioning and Attributes, these attributes should be limited to 
    /// </summary>
    public abstract class Mob
    {
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
        protected Random random;

        //Events
        public event EventHandler<BattleEventArgs>? BattleEvent;
        public event EventHandler Death;


        /// <summary>
        /// Public Getters are used throughout the game
        /// </summary>
        public string Name { get => name; }
        public bool IsAlive()
        {
            return (hitPoints > 0);
        }

        /// <summary>
        /// Protected Setters are only for subclasses!
        /// Refactor: Most of these should only be set once, so there may be a way to optimize
        /// </summary>
        protected int Defense { get => defense;  set => defense = value; }
        protected bool UserControlled { get => userControlled; set => userControlled = value; }
        protected int MaxHitPoints { get => maxHitPoints; set => maxHitPoints = value; }
        protected int HitPoints { get => hitPoints; set => hitPoints = value; }
        protected int MaxMana { get => maxMana; set => maxMana = value; }
        protected int Mana { get => mana; set => mana = value; }
        protected int Initiative { get => initiative; set => initiative = value; }
        protected int Intelligence { get => intelligence; set => intelligence = value; }
        protected int Strength { get => strength; set => strength = value; }
        protected int MagicDefense { get => magicDefense; set => magicDefense = value; }
        protected int AttackMod { get => attackMod; set => attackMod = value; }

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
            if (attackRoll >=20)
            {
                attackRoll += 5;
                damage += random.Next(9); //add another d8
            }

            //Add modifiers
            attackRoll += attackMod;
            damage += strength;

            //Tell target they are being attacked
            //  Be polite though and tell them who you are
            target.Hit( attackRoll, damage, name);
        }

        /// <summary>
        /// Called when an attack (or heal) hits Mob
        /// Modifies HP as needed
        /// </summary>
        public virtual void Hit(int attackRoll, int damage, string attacker)
        {
            string eventMessage="";
            
            //If attack is for negative damage, dont attempt to defend just take the heal
            if (attackRoll < 0)
            {
                Heal(damage, attacker);
            }

            //If attack hits reduce hitpoints as needed
            //attackRoll < defense
            else if (attackRoll > defense)
            {
                hitPoints -= damage;
                eventMessage = $"{attacker} hit {name} for {damage} Damage!\n\t{hitPoints} health remaining.";
            }

            //if attack just barely met defense, then tell user it was a "close" attack
            else if (attackRoll == defense)
            {
                eventMessage = $"{name} barely dodged {attacker}'s attack.";
            }

            //Else it was just a miss
            else
            {
                eventMessage = $"{name} dodged {attacker}'s attack.";
            }

            //Raise BattleEvent to declare what happened
            OnBattleEvent(eventMessage);

            //raise appropriate events in case of Mob death
            if (!IsAlive())
            {
                OnBattleEvent($"{name} has died");
                OnDeath();
            }
        }

        /// <summary>
        /// Heals hitpoints
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="healer"></param>
        public void Heal(int damage, string healer)
        {
            hitPoints += damage;

            //prevent over-healing
            if (hitPoints > MaxHitPoints)
            {
                hitPoints = MaxHitPoints;
            }
            OnBattleEvent($"{healer} healed {name} back to {hitPoints} health!");
        }

        //EDIT: Add magicAttack
        //EDIT: Add magicDefense


        /// <summary>
        /// Packages and raisesBattle Events (Which display for user a readout of what has happened in the battle so far)
        /// </summary>
        /// <param name="output"></param>
        private void OnBattleEvent(string output)
        {
            //Package and send battle message
            BattleEventArgs e = new BattleEventArgs();
            e.EventMessage = output;
            BattleEvent?.Invoke(this, e);
        }

        /// <summary>
        /// When Mob HP is reduced below 0HP they are dead.
        /// </summary>
        private void OnDeath()
        {
            //Raise a death event stating that Mob has died
            Death?.Invoke(this, EventArgs.Empty);
        }
    }
}