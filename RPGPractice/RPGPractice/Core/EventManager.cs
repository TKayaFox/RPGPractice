using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.Core.Events;
using RPGPractice.Engine;
using RPGPractice.Engine.MobClasses;

namespace RPGPractice.Core
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

        public event EventHandler<TurnEndEventArgs> TurnEnd;
        public event EventHandler? Death;
        public event EventHandler<BattleStartEventArgs> BattleStart;
        public event EventHandler<BattleEndEventArgs> BattleEnd;
        public event EventHandler<PlayerTurnEventArgs> PlayerTurn;
        public event EventHandler<PlayerActionEventArgs> PlayerAction;
        public event EventHandler NewGame;

        //===========================================
        //          Managers
        //  Relay Events to EventManager subscribers
        //===========================================
        #region Event Managers

        /// <summary>
        /// Publishes MobData and subscribes to all events
        /// </summary>
        /// <param name="eventManager"></param>
        public virtual void ManageMob(Mob mob)
        {
            //publish events to eventManager
            mob.Death += OnDeath_Aggregator;
        }

        /// <summary>
        /// Publishes MobData and subscribes to all events
        /// </summary>
        /// <param name="eventManager"></param>
        public virtual void UnManageMob(Mob mob)
        {
            //publish events to eventManager
            mob.Death += OnDeath_Aggregator;
        }



        #endregion

        //===========================================
        //          Event Aggregators
        //  Relay Events to EventManager subscribers
        //===========================================
        #region Event Aggregators

        public void OnPlayerAction_Aggregator(object sender, PlayerActionEventArgs e)
        {
            PlayerAction.Invoke(sender, e);
        }
        public void OnDeath_Aggregator(object sender, EventArgs e)
        {
            Death?.Invoke(sender, e);
        }
        public void OnBattleEnd_Aggregator(object sender, BattleEndEventArgs e)
        {
            BattleEnd.Invoke(sender, e);
        }
        public void OnBattleStart_Aggregator(object sender, BattleStartEventArgs e)
        {
            BattleStart.Invoke(sender, e);
        }
        public void OnNewGame_Aggregator(object sender, EventArgs e)
        {
            NewGame.Invoke(sender, e);
        }
        public void OnPlayerTurn_Aggregator(object sender, PlayerTurnEventArgs e)
        {

            System.Diagnostics.Debug.WriteLine($"PlayerTurn event raised by sender");
            PlayerTurn.Invoke(sender, e);
        }
        public void OnTurnEnd_Aggregator(object sender, TurnEndEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"TurnEnd event raised by sender");
            TurnEnd.Invoke(sender, e);
        }
        #endregion
    }
}
