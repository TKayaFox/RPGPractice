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

        //===========================================
        //          Event Aggregators
        //  Relay Events to EventManager subscribers
        //===========================================
        #region Event Relays

        public void OnPlayerAction_Aggregator(object? sender, PlayerActionEventArgs e)
        {
            PlayerAction?.Invoke(sender, e);
        }
        public void OnDeath_Aggregator(object sender, EventArgs e)
        {
            Death?.Invoke(sender, e);
        }
        public void OnBattleEnd_Aggregator(object? sender, BattleEndEventArgs e)
        {
            BattleEnd?.Invoke(sender, e);
        }
        public void OnBattleStart_Aggregator(object? sender, BattleStartEventArgs e)
        {
            BattleStart?.Invoke(sender, e);
        }
        public void OnNewGame_Aggregator(object? sender, EventArgs e)
        {
            NewGame?.Invoke(sender, e);
        }
        public void OnPlayerTurn_Aggregator(object? sender, PlayerTurnEventArgs e)
        {
            PlayerTurn?.Invoke(sender, e);
        }
        public void OnTurnEnd_Aggregator(object? sender, TurnEndEventArgs e)
        {
            TurnEnd?.Invoke(sender, e);
        }
        #endregion
    }
}
