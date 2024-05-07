using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice.Engine.MobClasses.HeroMobs
{
    /// <summary>
    /// A Basic Fighter class does physical attacks only
    /// High physical defense
    /// weak to magic
    /// </summary>
    public class Warrior : PlayerMob
    {
        public Warrior(string name) : base(name) { }

        /// <summary>
        /// Sets All stats for MobID
        /// Meant to be overridden
        /// </summary>
        protected override void Initialize()
        {
            Sprite = Properties.Resources.Fighter;
            MaxHitPoints = 30;
            Initiative = 1;
            Intelligence = -2;
            Strength = 2;
            AttackMod = 2;
            Defense = 15;
            MagicDefense = 8;
            BlockingBonus = 3;
        }
    }
}