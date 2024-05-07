using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.Engine;
using RPGPractice.Engine.MobClasses;

namespace RPGPractice.Core.Events.Args
{
    public class NewBattleEventArgs : EventArgs
    {
        List<MobData> mobDataList;

        public List<MobData> MobDataList { get => mobDataList; set => mobDataList = value; }
    }
}
