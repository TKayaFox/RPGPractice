using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice
{
    /// <summary>
    /// Subscriber Class tracks all Objects and what evens they may raise.
    /// Trying to juggle subscribing to events for every object, and what events should be subscribed to, can be messy.
    /// 
    /// Public methods allow the program to subscribe to all events that it needs at once.
    /// </summary>
    public abstract class EventSubscriber
    {
        private List<Object> publishers;

        /// <summary>
        /// Costructor initializes list of publishers
        /// </summary>
        public EventSubscriber()
        {
            publishers = new List<Object>();
        }

        public bool AddPublisher(Mob publisher)
        {
            if (publisher == null)
            {
                return false;
            }
            else
            {
                return AddToList(publisher, publishers);
            }
        }
        public bool AddToList<T>(T newMember, List<T> list)
        {
            bool added = false;
            // Add the new member to the list
            list.Add(newMember);
            added = true; // If the addition succeeds, set added to true

            return added;
        }

        public void remove(Object Publisher)
        {
            //edit: remove issuer from the list
        }

    }
}
