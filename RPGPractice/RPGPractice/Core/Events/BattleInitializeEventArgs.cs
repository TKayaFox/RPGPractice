using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.Engine.MobClasses;
using RPGPractice.GUI;

namespace RPGPractice.Core.Events
{
    public class BattleInitializeEventArgs : EventArgs
    {
        List<MobData> mobDataList;

        public List<MobData> MobDataList { get => mobDataList; set => mobDataList = value; }
    }
}
