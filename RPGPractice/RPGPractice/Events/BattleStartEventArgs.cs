using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.MobClasses;

namespace RPGPractice.Events
{
    public class BattleStartEventArgs : EventArgs
    {
        Mob[] heroes;
        Mob[] villians;
        int combatLevel;

        public Mob[] Heroes { get => heroes; set => heroes = value; }
        public Mob[] Villians { get => villians; set => villians = value; }
        public int CombatLevel { get => combatLevel; set => combatLevel = value; }
    }
}
