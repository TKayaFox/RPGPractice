using RPGPractice.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Core.Events
{
    public class TargetedAbility : EventArgs
    {
        private MobData attacker;
        private MobData target;
        private DamageType damageType;
        private int damage;
        private int attackRoll;

        public MobData Attacker { get => attacker; set => attacker = value; }
        public MobData Target { get => target; set => target = value; }
        public DamageType DamageType { get => damageType; set => damageType = value; }
        public int Damage { get => damage; set => damage = value; }
        public int AttackRoll { get => attackRoll; set => attackRoll = value; }
    }
}
