using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Core.Events
{
    /// <summary>
    /// Handles all Battle Messages to be displayed for user
    /// </summary>
    public class TurnEndEventArgs : EventArgs
    {
        private string turnSummary;

        public string TurnSummary { get => turnSummary; set => turnSummary = value; }
    }
}
