using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Events
{
    public class PlayerActionEventArgs
    {
        private int attackerID;
        private int targetID;
        private ActionEnum action;

        public int AttackerID { get => attackerID; set => attackerID = value; }
        public int TargetID { get => targetID; set => targetID = value; }
        public ActionEnum Action { get => action; set => action = value; }
    }
}
