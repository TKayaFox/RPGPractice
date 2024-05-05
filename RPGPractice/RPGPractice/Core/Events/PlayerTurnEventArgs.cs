using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.Engine.MobClasses;
using RPGPractice.GUI;

namespace RPGPractice.Core.Events
{
    public class PlayerTurnEventArgs : EventArgs
    {
        int mobID;
        List<MobData> attackTargetList = new List<MobData>();
        List<MobData> specialTargetList = new List<MobData>();

        //Propperty Setters/getters
        public int MobID { get => mobID; set => mobID = value; }
        public List<MobData> AttackTargetList { get => attackTargetList; set => attackTargetList = value; }
        public List<MobData> SpecialTargetList { get => specialTargetList; set => specialTargetList = value; }
    }
}
