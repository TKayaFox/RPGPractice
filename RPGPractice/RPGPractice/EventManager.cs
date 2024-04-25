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
    public class EventManager
    {
        //=========================================
        //      Events that can be published
        //=========================================
        //  Remember to add an aggregator below as well to relay the event, and subscriber in appropriate method
        public event EventHandler<BattleEndEventArgs>? BattleEnd;
        public event System.EventHandler Death;
        public event EventHandler<BattleEventArgs>? BattleEvent;

        //=========================================
        //             Public Methods
        //=========================================
        #region Public Methods
        /// <summary>
        /// Subscribes publisher to an object if possible for relay
        /// </summary>
        /// <param name="mob"></param>
        /// <returns>number of published objects</returns>
        public int Publish<T>(T publisher)
        {
            int numPublished = 0;

            //ensure publisher actually exists
            if (publisher != null)
            {
                //send publisher to correct subscription method if implemented
                switch (publisher)
                {
                    case Mob mob:
                        Subscribe(mob);
                        numPublished ++;
                        break;
                    case Battle battle:
                        Subscribe(battle);
                        numPublished++;
                        break;
                    default:
                        //Refactor: Eventually come back here and add exception throwing and on publisher side add exception handling
                        break;
                }
            }
            return numPublished;
        }
        public int Publish<T>(T[] publishers)
        {
            int numPublished = 0;

            foreach( T publisher in publishers)
            {
                numPublished += Publish(publisher);
            }
            return numPublished;
        }


        /// <summary>
        /// Unsubscribes publisher to an object, stopping the relay
        /// </summary>
        /// <param name="Publisher"></param>
        /// <returns>number of removed objects</returns>
        public int Unpublish<T>(T publisher)
        {
            int unpublished = 0;

            //ensure publisher actually exists
            if (publisher != null)
            {

                //send publisher to correct subscription method if implemented
                switch (publisher)
                {
                    case Mob mob:
                        UnSubscribe(mob);
                        unpublished++;
                        break;
                    case Battle battle:
                        UnSubscribe(battle);
                        unpublished++;
                        break;
                    default:
                        //Refactor: Eventually come back here and add exception throwing and on publisher side add exception handling
                        break;
                }
            }
            return unpublished;
        }
        public int Unpublish<T>(T[] publishers)
        {
            int numUnpublished = 0;

            foreach (T publisher in publishers)
            {
                numUnpublished += Publish(publisher);
            }
            return numUnpublished;
        }
        #endregion

        //=========================================
        //            Private Methods
        //  Object specific Subscribers/Unsubscribers
        //  Once Object type is determined, it can
        //=========================================
        #region Private Methods

        /// <summary>
        /// Subscribes publisher so that it relays events for specified type
        /// overloaded for all allowed types
        /// </summary>
        private void Subscribe(Mob mob)
        {
            //Events
            mob.BattleEvent += OnBattleEvent_Aggregator;
            mob.Death += OnDeath_Aggregator;
        }
        private void Subscribe(Battle battle)
        {
            //Events
            battle.BattleEnd += OnBattleEnd_Aggregator;
            battle.BattleEvent += OnBattleEvent_Aggregator;
        }

        /// <summary>
        /// UnSubscribes publisher so that it stops relayings events for specified type
        /// overloaded for all allowed types
        /// </summary>
        private void UnSubscribe(Mob mob)
        {
            //Events
            mob.BattleEvent -= OnBattleEvent_Aggregator;
            mob.Death -= OnDeath_Aggregator;
        }
        private void UnSubscribe(Battle battle)
        {
            //Events
            battle.BattleEnd -= OnBattleEnd_Aggregator;
            battle.BattleEvent -= OnBattleEvent_Aggregator;
        }

        #endregion

        //=========================================
        //      Event Publisher Relays
        //  Simple re-raise events to subscribers
        //=========================================
        #region Event Relays
        private void OnBattleEvent_Aggregator(object sender, BattleEventArgs e)
        {
            //Relay the event
            BattleEvent?.Invoke(sender, e);
        }
        private void OnDeath_Aggregator(object sender, EventArgs e)
        {
            //Relay event
            Death?.Invoke(sender, e);
        }
        private void OnBattleEnd_Aggregator(object? sender, BattleEndEventArgs e)
        {
            //Relay the event
            BattleEnd?.Invoke(sender, e);
        }
        #endregion
    }
}
