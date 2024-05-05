using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Core.Events
{
    public class BattleEndEventArgs : EventArgs
    {
        private bool victory;

        public bool Victory { get => victory; set => victory = value; }
    }
}
