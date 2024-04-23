using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice
{
    /// <summary>
    /// Handles all Battle Messages to be displayed for user
    /// </summary>
    public class BattleEventArgs : EventArgs
    {
        private string eventMessage;

        public string EventMessage { get => eventMessage; set => eventMessage = value; }
    }
}
