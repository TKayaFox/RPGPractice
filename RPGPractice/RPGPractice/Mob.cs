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
        private int maxHitPoints;
        private int initiative;


        //Properties Getters/Setters. Protected so subclasses can modify them, but 
        protected int maxHitPoints
        {
            get => default;
            set { }
        }

        protected int initiative
        {
            get => default;
            set { }
        }

        protected bool userControlled
        {
            get => default;
            set { }
        }

        protected int defenseMagMod
        {
            get => default;
            set { }
        }

        protected int defensePhysMod
        {
            get => default;
            set { }
        }

        protected int intelligence
        {
            get => default;
            set { }
        }

        protected int strength
        {
            get => default;
            set { }
        }

        protected int name
        {
            get => default;
            set { }
        }

        public int Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Defend()
        {
            throw new System.NotImplementedException();
        }
    }
}