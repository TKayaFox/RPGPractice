using RPGPractice.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Engine.MobClasses
{
    /// <summary>
    /// Stores data for any Mobactivity that targets a different Mob
    /// </summary>
    public class TargetedAbility : EventArgs
    {
        private MobData attacker;
        private MobData target;
        private DamageType damageType;
        private int damage;
        private int attackRoll;

        /// <summary>
        /// Identifies the Mob initiating the Attack
        /// </summary>
        public MobData Attacker { get => attacker; set => attacker = value; }

        /// <summary>
        /// Identifies the Mob being targeted
        /// </summary>
        public MobData Target { get => target; set => target = value; }

        /// <summary>
        /// Identifies the type of damage the attack causes
        /// </summary>
        public DamageType DamageType { get => damageType; set => damageType = value; }

        /// <summary>
        /// Identifies the amount of damage caused on a successful hit
        /// </summary>
        public int Damage { get => damage; set => damage = value; }

        /// <summary>
        /// Identifies the Attack Roll/chance to hit
        /// </summary>
        public int AttackRoll { get => attackRoll; set => attackRoll = value; }
    }
}
