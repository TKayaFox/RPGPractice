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
        private Queue<TargetedAbility> targetedAbilityQueue = new Queue<TargetedAbility>();

        public string TurnSummary { get => turnSummary; set => turnSummary = value; }
        public MobData Attacker { get => attacker; set => attacker = value; }
        public bool HasTargetedActions { get {return (TargetedAbilityQueue.Count > 0);} }

        public Queue<TargetedAbility> TargetedAbilityQueue { get => targetedAbilityQueue; set => targetedAbilityQueue = value; }

        /// <summary>
        /// Targets are stored as a Queue and must be queued or dequeued. So new targets can be added if needed
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public void QueueAttack(TargetedAbility attack)
        {
            TargetedAbilityQueue.Enqueue(attack);
        }
        public TargetedAbility DequeueAction()
        {
            return TargetedAbilityQueue.Dequeue();
        }
    }
}
