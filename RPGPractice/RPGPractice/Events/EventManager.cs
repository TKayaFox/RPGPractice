using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.Engine;
using RPGPractice.Engine.MobClasses;

namespace RPGPractice.Events
{
    /// <summary>
    /// Subscriber MobData tracks all Objects and what evens they may raise.
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

        //MobID Events
        public event EventHandler<TurnEndEventArgs>? BattleEvent;
        public event EventHandler<TurnEndEventArgs> TurnEnd;
        public event EventHandler? Death;

        //Battle Events
        public event EventHandler<BattleStartEventArgs> BattleStart;
        public event EventHandler<BattleEndEventArgs> BattleEnd;
        public event EventHandler<PlayerTurnEventArgs> PlayerTurn;

        //BattleField Events
        public event EventHandler<PlayerActionEventArgs> PlayerAction;

        //GameForm Events
        public event System.EventHandler NewGame;


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
                        numPublished++;
                        break;
                    case Battle battle:
                        Subscribe(battle);
                        numPublished++;
                        break;

                    case GameForm form:
                        Subscribe(form);
                        numPublished++;
                        break;

                    case BattleField battlefield:
                        Subscribe(battlefield);
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

            foreach (T publisher in publishers)
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

                    case GameForm form:
                        UnSubscribe(form);
                        unpublished++;
                        break;

                    case BattleField battlefield:
                        UnSubscribe(battlefield);
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
                numUnpublished += Unpublish(publisher);
            }
            return numUnpublished;
        }
        #endregion

        //=========================================
        //            Private Methods
        //  Object specific Subscribers/Unsubscribers
        //  Once Object type is determined, it can
        //=========================================
        #region Subscribe Method Overloads
        /// <summary>
        /// Subscribes publisher so that it relays events for specified type
        /// overloaded for all allowed types
        /// </summary>
        private void Subscribe(Mob mob)
        {
            //Events
            mob.TurnEnd += OnTurnEnd_Aggregator;
            mob.Death += OnDeath_Aggregator;
        }
        private void Subscribe(Battle battle)
        {
            //Events
            battle.BattleStart += OnBattleStart_Aggregator;
            battle.BattleEnd += OnBattleEnd_Aggregator;
            battle.PlayerTurn += OnPlayerTurn_Aggregator;
        }
        private void Subscribe(GameForm gameForm)
        {
            //Events
            gameForm.NewGame += OnNewGame_Aggregator;
        }
        private void Subscribe(BattleField battlefield)
        {
            //Events
            battlefield.PlayerAction += OnPlayerAction_Aggregator;
        }
        #endregion

        #region Unsubscribe Overloads
        /// <summary>
        /// UnSubscribes publisher so that it stops relayings events for specified type
        /// overloaded for all allowed types
        /// </summary>
        private void UnSubscribe(Mob mob)
        {
            //Events
            mob.BattleEvent -= OnBattleEvent_Aggregator;
            mob.TurnEnd -= OnTurnEnd_Aggregator;
            mob.Death -= OnDeath_Aggregator;
        }
        private void UnSubscribe(Battle battle)
        {
            //Events
            battle.BattleStart -= OnBattleStart_Aggregator;
            battle.BattleEnd -= OnBattleEnd_Aggregator;
            battle.PlayerTurn -= OnPlayerTurn_Aggregator;
        }
        private void UnSubscribe(GameForm gameForm)
        {
            //Events
            gameForm.NewGame -= OnNewGame_Aggregator;
        }
        private void UnSubscribe(BattleField battlefield)
        {
            //Events
            battlefield.PlayerAction += OnPlayerAction_Aggregator;
        }

        #endregion

        //=========================================
        //      Event Publisher Relays
        //  Simple re-raise events to subscribers
        //=========================================
        #region Event Relays

        private void OnPlayerAction_Aggregator(object? sender, PlayerActionEventArgs e)
        {
            PlayerAction?.Invoke(sender, e);
        }
        private void OnBattleEvent_Aggregator(object sender, TurnEndEventArgs e)
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
        private void OnBattleStart_Aggregator(object? sender, BattleStartEventArgs e)
        {
            //Relay the event
            BattleStart?.Invoke(sender, e);
        }
        private void OnNewGame_Aggregator(object? sender, EventArgs e)
        {
            NewGame?.Invoke(sender, e);
        }
        private void OnPlayerTurn_Aggregator(object? sender, PlayerTurnEventArgs e)
        {
            PlayerTurn?.Invoke(sender, e);
        }
        private void OnTurnEnd_Aggregator(object? sender, TurnEndEventArgs e)
        {
            TurnEnd?.Invoke(sender, e);
        }
        #endregion
    }
}
