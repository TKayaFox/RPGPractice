using RPGPractice.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Core.Events
{
    /// <summary>
    /// Collects all End of turn data:
    /// for resolving actions (ex: attacks on other mobs) 
    /// and then for passing to GUI
    /// </summary>
    public class TurnEndEventArgs : EventArgs
    {
        private string turnSummary;
        private MobData attacker;
        private Queue<MobData> targetQueue = new Queue<MobData>();

        public string TurnSummary { get => turnSummary; set => turnSummary = value; }
        public MobData Attacker { get => attacker; set => attacker = value; }
        public Queue<MobData> TargetQueue { get => targetQueue; set => targetQueue = value; }

        /// <summary>
        /// Targets are stored as a Queue and must be queued or dequeued. So new targets can be added if needed
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public void AddTarget(MobData target)
        {
            targetQueue.Enqueue(target);
        }
        public MobData RemoveTarget()
        {
            return targetQueue.Dequeue();
        }
    }
}
