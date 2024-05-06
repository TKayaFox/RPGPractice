using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGPractice.Core;
using RPGPractice.Core.Enumerations;
using RPGPractice.Core.Events;
using RPGPractice.Engine.MobClasses.EnemyMobs;
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
        private string name;
        private string turnSummary;
        private string specialActionString;
        private int uniqueID;
        private MobActions specialAction;
        private MobData data;
        private System.Drawing.Bitmap sprite;
        Queue<TargetedAbility> targetedAbilityQueue;

        //Game specific stats
        private int maxHitPoints;
        private int hitPoints;
        private int initiative;
        private int intelligence;
        private int strength;
        private int attackMod;
        private int defense;
        private int magicDefense;
        private int blockingBonus;
        private bool isBlocking;
        #endregion

        //=========================================
        //              Main Methods
        //=========================================
        #region Public Methods

        /// <summary>
        /// Constructor initializes default fields
        /// </summary>
        public Mob(string name)
        {
            this.name = name;
            isBlocking = false;
            specialActionString = "";
            blockingBonus = 2;

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
            //reset turnSummary and targetedAbilityQueue
            TurnSummary = "";
            TargetedAbilityQueue = new Queue<TargetedAbility>();

            //Stop defending
            isBlocking = false;

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
            //Determine healValue and Attack Rolls (attackMod + 1d20)
            (int attackRoll,int damage) = Dice.RollAttack(attackMod, strength);

            //add ability roll to turn summary
            AppendTurnSummary($"{name} Attacks {target.Name}. \t[Attack roll: {attackRoll} Damage {damage}]");

            //Build a new TargetedAction object and add to Queue
            TargetedAbility attack = new TargetedAbility();
            attack.Attacker = Data;
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
        /// <param name="healValue"></param>
        /// <param name="healer"></param>
        public virtual string Heal(int healValue)
        {
            string result;

            //see if healing will even do anything
            if (hitPoints == MaxHitPoints)
            {
                result = $"{Name} is already fully healed.";
            }
            else
            {
                int initialHP = hitPoints;
                HitPoints += healValue;

                //prevent over-healing
                if (hitPoints >= MaxHitPoints)
                {
                    hitPoints = MaxHitPoints;
                    result = $"Has reached full health! {maxHitPoints}";
                }

                //return string stating result
                result = $"{Name} gained {hitPoints - initialHP} health!\t[HP={hitPoints}]";
            }

            return result;
        }

        /// <summary>
        /// Updates MobData fields (hpString, isAlive)
        /// </summary>
        protected virtual void UpdateData()
        {
            data.HitPointString = $"{hitPoints}/{maxHitPoints}";
            data.IsAlive = IsAlive;
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
                //Calculate Block result and add Strength if currently isBlocking
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
                //Calculate Block result and add Intelligence if currently isBlocking
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
            isBlocking = true;


            AppendTurnSummary($"{name} takes a defensive stance!");
            OnTurnEnd();
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
        /// Builds a GUI specific Data object which holds only the information needed for Data
        /// Refactor: Can eventually subscribe it to a MobUpdate event which allows the MobID object to update it's GUI counterpart directly
        /// </summary>
        public virtual MobData Data
        {
            get
            {
                //if MobData object not yet created, create it
                if (data == null)
                {
                    BuildData();
                }

                return data;
            }
        }

        protected virtual void BuildData()
        {
            data = new MobData();
            data.Sprite = sprite;
            data.Name = name;
            data.HitPointString = hitPoints;
            data.UniqueID = uniqueID;
            data.IsNPC = (this is EnemyMob); //if this object falls under NPC (MobID subclass)
            data.IsAlive = IsAlive;
            data.SpecialActionString = SpecialActionString;
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

            result = ($"{Name} took {damage} damage   [HP {HitPoints}]");


            //Check for death
            if (!IsAlive)
            {
                result += ($"\r\n{Name} has Died!");
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

        protected virtual void CompileTargetLists(List<MobData> allyTargetList, List<MobData> enemyTargetList, PlayerTurnEventArgs args)
        {
            //Make lists of viable targets
            args.AttackTargetList = enemyTargetList;

            //Unless overriden provide a list of all possible targets for Special Action
            //  combine both lists
            List<MobData> FullTargetList = new List<MobData>();
            FullTargetList.AddRange(allyTargetList);
            FullTargetList.AddRange(enemyTargetList);

            args.SpecialTargetList = FullTargetList;
        }

        /// <summary>
        /// Handles end logic for Block family of methods
        ///     Assigns healValue as needed and returns turnSummary
        /// </summary>
        /// <param name="attackRoll"></param>
        /// <param name="dodgeValue"></param>
        /// <param name="damage"></param>
        /// <returns>string describing what occurred during this turn</returns>
        protected string DefendResult(int attackRoll, int defense, int defenseMod, int damage)
        {
            string turnSummary = "";

            //If ability hits, take healValue. Update turnSummary with what happened either way
            if (attackRoll > defense)
            {
                //check if Blocking prevented healValue
                bool blockPrevents = ((defense + BlockingBonus) > attackRoll);
                if (isBlocking && blockPrevents)
                {
                    turnSummary = ($"{name} was able to deflect the attack! [Blocking]");
                }
                else
                {
                    turnSummary += Hurt(damage); 
                }
            }
            //If ability meets, then take half healValue (rounded down, so integer is not a problem)
            else if (attackRoll == defense)
            {
                int halfDamage = damage / 2;
                turnSummary += $"Glancing Blow! ";
                turnSummary += Hurt(halfDamage);
            }
            else
            {
                turnSummary = ($"{name} avoided the attack. \t[{attackRoll} < {defense}]");
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
        protected int BlockingBonus { get => blockingBonus; set => blockingBonus = value; }
        #endregion

        //=========================================
        //                  Events
        //=========================================
        public event EventHandler<TurnEndEventArgs> TurnEnd;
        public event EventHandler<PlayerTurnEventArgs> PlayerTurn;

        #region Event Invokers

        /// <summary>
        /// Packages and raisesBattle Events 
        /// (Which display for user a readout of what has happened in the battle so far)
        /// </summary>
        /// <param name="target">Optional parameter if attacking a target</param>
        protected virtual void OnTurnEnd()
        {
            //update MobData
            UpdateData();

            //This data will always be stored in args
            TurnEndEventArgs args = new TurnEndEventArgs();
            args.TargetedAbilityQueue = TargetedAbilityQueue;
            args.Attacker = Data;
            args.TurnSummary = TurnSummary;

            //invoke method
            TurnEnd.Invoke(this, args);
        }

        /// <summary>
        /// Tells the GUI to setup for players turn.
        /// </summary>
        public void OnPlayerTurn(List<MobData> allyTargetList, List<MobData> enemyTargetList)
        {
            //setup event arguments
            PlayerTurnEventArgs args = new PlayerTurnEventArgs();
            args.MobID = UniqueID;

            CompileTargetLists(allyTargetList, enemyTargetList, args);
            PlayerTurn?.Invoke(this, args);
        }
        #endregion
    }
}