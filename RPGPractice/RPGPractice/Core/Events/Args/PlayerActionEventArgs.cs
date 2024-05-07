using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.Core.Enumerations;

namespace RPGPractice.Core.Events.Args
{
    public class PlayerActionEventArgs : EventArgs
    {
        private int attackerID = -1;
        private int targetID = -1;
        private MobActions action;

        public int AttackerID { get => attackerID; set => attackerID = value; }
        public int TargetID { get => targetID; set => targetID = value; }
        public MobActions Action { get => action; set => action = value; }
    }
}
