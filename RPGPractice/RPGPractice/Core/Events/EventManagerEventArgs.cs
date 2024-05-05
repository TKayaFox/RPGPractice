using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.Core.Events
{
    public class EventManagerEventArgs : EventArgs
    {
        List<Object> targetList = new List<object>();
        bool isActive;


        /// <summary>
        /// - If true, subscription relationships between target and EventManager will be added/live/active
        /// - If false, the relationship will be severed/unsubscribed
        /// </summary>
        public bool IsActive { get => isActive; set => isActive = value; }
        public List<object> TargetList { get => targetList; set => targetList = value; }

        /// <summary>
        /// AddTargetArr adds all items from an array to the targetList
        /// TODO: Remove once Mobs stored only in lists
        /// </summary>
        public object[] AddTargetArr
        {
            set
            {
                foreach (object target in value)
                {
                    TargetList.Add(target);
                }
            }
        }

        /// <summary>
        /// AddTarget adds a single object to be managed or unmanaged
        /// </summary>
        public object AddTarget
        {
            set
            {
                targetList.Add(value);
            }
        }
    }
}
