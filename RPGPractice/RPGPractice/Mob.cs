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
        private int maxMana;
        private int mana;
        private int initiative;
        private int intelligence;
        private int strength;
        private int defense;
        private int magicDefense;
        private string name = "";

        //Events


        /// <summary>
        /// Public Setters are used throughout the game
        /// </summary>
        public string Name { get => name; }

        /// <summary>
        /// Protected Setters are only for subclasses!
        /// Refactor: Most of these should only be set once, so there may be a way to optimize
        /// </summary>
        protected bool UserControlled { get => userControlled; set => userControlled = value; }
        protected int MaxHitPoints { get => maxHitPoints; set => maxHitPoints = value; }
        protected int HitPoints { get => hitPoints; set => hitPoints = value; }
        protected int MaxMana { get => maxMana; set => maxMana = value; }
        protected int Mana { get => mana; set => mana = value; }
        protected int Initiative { get => initiative; set => initiative = value; }
        protected int Intelligence { get => intelligence; set => intelligence = value; }
        protected int Strength { get => strength; set => strength = value; }
        protected int Defense { get => defense; set => defense = value; }
        protected int MagicDefense { get => magicDefense; set => magicDefense = value; }

        /// <summary>
        /// Called from Game when a Mob attacks another mob. Automatically calls that Mob's Hit() method
        /// </summary>
        /// <returns></returns>
        public void rollAttack(Mob target)
        {
            //Determine Attack Damage
            //EDIT: Make some sort of dice roll or something to slightly randomize results.
            int damage = Strength;

            //Create output text for Game Events and send to event method
            string eventMessage = ($"{name} Attacks {target.Name} for {damage}!");

            //Tell target that it has been hit
            target.Hit(damage, this);

            //Raise Battle Event to display what happens
            OnBattleEvent(eventMessage);
        }

        /// <summary>
        /// Called whenever the Mob is attacked
        /// Determines what damage Mob receives, then raises an event flag with the outcome. 
        ///     If damage being dealt is negative, do not apply defense and simply heal HP "Mob was healed for <damage>"
        ///     Else
        ///         subtract defense from damage
        ///         if remaining damage less than or equal to zero "Mobs avoided damage!"
        ///         else "Mob was hit for <damage>"
        /// </summary>
        public void Hit(int damage, Mob attacker)
        {
            String eventMessage;

            //Determine what the result of the damage is,
            //  and generate an message for Battle Event message
            if (damage < 0) 
            {
                eventMessage =  $"{name} was healed for {damage}" ;
            }
            else
            {
                if (damage == 0)
                {
                    eventMessage = "Attack was deflected!";
                }
                else
                {
                    eventMessage = ($"{name} is hit for {damage} Damage!");
                }
            }

            //Raise BattleEvent to declare what happened
            OnBattleEvent(eventMessage);
        }

        private void OnBattleEvent(string output)
        {
            //EDIT: Raise BattleEvent event and target the "output" string

        }
    }
}