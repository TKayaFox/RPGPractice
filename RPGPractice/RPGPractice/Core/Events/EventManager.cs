using RPGPractice.Core.Events.Args;
using RPGPractice.Engine;
using RPGPractice.Engine.MobClasses;

namespace RPGPractice.Core.Events
{
    /// <summary>
    /// FileManager class
    /// Developer: Taylor Fox
    /// Subscriber Data tracks all Objects and what evens they may raise.
    /// Trying to juggle subscribing to events for every object, and what events should be subscribed to, can be messy.
    /// Public methods allow the program to subscribe to all events that it needs at once.
    /// </summary>
    public class EventManager
    {
        //=========================================
        //      Events that can be published
        //=========================================
        //  Remember to add an aggregator below as well to relay the event, and target in appropriate method

        public event EventHandler<NewBattleEventArgs> NewBattle;
        public event EventHandler<PlayerActionEventArgs> PlayerAction;
        public event EventHandler<BattleResultEventArgs> BattleResult;
        public event EventHandler<PlayerTurnEventArgs> PlayerTurn;
        public event EventHandler<TurnEndEventArgs> TurnEnd;
        public event EventHandler BattleStart;
        public event EventHandler NewGame;

        //===========================================
        //              Managers
        //  Relay Events to EventManager subscribers
        //===========================================
        #region Public/Main Methods

        /// <summary>
        /// Unsubscribes all target and eventManager relationships
        ///     If isActive is true, then re-subscribe to all relationships
        ///     Automatically prevents dual subscription
        /// </summary>
        /// <param name="target">Object that is to be Managed</param>
        /// <param name="isActive">
        /// - If true, subscription relationships between target and EventManager will be added/live/active
        /// - If false, the relationship will be severed/unsubscribed
        ///</param>
        public void ManageObjectSort(object target, bool isActive)
        {
            switch (target)
            {
                case Mob mob:
                    ManageMob(mob, isActive);
                    break;
                case Battle battle:
                    ManageBattle(battle, isActive);
                    break;
                case BattleScreen battlefield:
                    ManageBattleField(battlefield, isActive);
                    break;
                case GameEngine engine:
                    ManageGameEngine(engine, isActive);
                    break;
                case GameForm form:
                    ManageGameForm(form, isActive);
                    break;
            }
        }

        #endregion

        #region Private Type Specific Methods

        private void ManageMob(Mob target, bool addMe)
        {
            //Unsubscribe target first to prevent double subscription.
            target.PlayerTurn -= OnPlayerTurn_Relay;

            //Only add new subscriptons if addMe = true
            if (addMe)
            {
                target.PlayerTurn += OnPlayerTurn_Relay;
            }
        }

        private void ManageBattle(Battle target, bool addMe)
        {
            //Unsubscribe first to prevent double subscription.
            target.ManageObject -= OnManageObject_Handler;
            target.NewBattle -= OnNewBattle_Relay;
            target.BattleResult -= OnBattleResult_Relay;
            target.TurnEnd -= OnTurnEnd_Relay;
            PlayerAction -= target.OnPlayerAction_handler;
            BattleStart -= target.OnBattleStart_Handler;

            //Only add new subscriptons if addMe = true
            if (addMe)
            {
                target.ManageObject += OnManageObject_Handler;
                target.NewBattle += OnNewBattle_Relay;
                target.BattleResult += OnBattleResult_Relay;
                target.TurnEnd += OnTurnEnd_Relay;
                PlayerAction += target.OnPlayerAction_handler;
                BattleStart += target.OnBattleStart_Handler;
            }
        }


        private void ManageGameEngine(GameEngine target, bool addMe)
        {
            //Unsubscribe first to prevent double subscription.
            target.ManageObject -= OnManageObject_Handler;
            BattleResult -= target.OnBattleResult_Handler;
            NewGame -= target.OnNewGame_Handler;

            //Only add new subscriptons if addMe = true
            if (addMe)
            {
                target.ManageObject += OnManageObject_Handler;
                BattleResult += target.OnBattleResult_Handler;
                NewGame += target.OnNewGame_Handler;
            }
        }


        private void ManageBattleField(BattleScreen target, bool addMe)
        {
            //Unsubscribe first to prevent double subscription.
            target.PlayerAction -= OnPlayerAction_Relay;
            target.BattleStart -= OnBattleStart_Relay;
            PlayerTurn -= target.OnPlayerTurn_Handler;
            TurnEnd -= target.OnTurnEnd_Handler;

            //Only add new subscriptons if addMe = true
            if (addMe)
            {
                target.PlayerAction += OnPlayerAction_Relay;
                target.BattleStart += OnBattleStart_Relay;
                PlayerTurn += target.OnPlayerTurn_Handler;
                TurnEnd += target.OnTurnEnd_Handler;
            }
        }

        private void ManageGameForm(GameForm target, bool addMe)
        {
            //Unsubscribe first to prevent double subscription.
            target.ManageObject -= OnManageObject_Handler;
            target.NewGame -= OnNewGame_Relay;
            NewBattle -= target.OnNewBattle_Handler;

            //Only add new subscriptons if addMe = true
            if (addMe)
            {
                target.ManageObject += OnManageObject_Handler;
                target.NewGame += OnNewGame_Relay;
                NewBattle += target.OnNewBattle_Handler;
            }
        }

        #endregion

        //===========================================
        //            Event Handlers
        //  used to subscribe or unsubscribe members
        //             Not Relayed
        //===========================================
        #region Event Handlers

        /// <summary>
        /// When called subscribes to all object events 
        /// and subscribes object (or array of objects) to all appropriate events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnManageObject_Handler(object sender, EventManagerEventArgs args)
        {
            bool isActive = args.IsActive;

            //Manage all targets in targetList
            foreach (object target in args.TargetList)
            {
                ManageObjectSort(target, isActive);
            }
        }



        #endregion

        //===========================================
        //          Event Relays
        //  Relay Events to EventManager subscribers
        //===========================================
        #region Event Relays

        public void OnPlayerAction_Relay(object sender, PlayerActionEventArgs e)
        {
            PlayerAction?.Invoke(sender, e);
        }
        public void OnBattleStart_Relay(object sender, EventArgs e)
        {
            BattleStart?.Invoke(sender, e);
        }
        public void OnBattleResult_Relay(object sender, BattleResultEventArgs e)
        {
            BattleResult?.Invoke(sender, e);
        }
        public void OnNewBattle_Relay(object sender, NewBattleEventArgs e)
        {
            NewBattle?.Invoke(sender, e);
        }
        public void OnNewGame_Relay(object sender, EventArgs e)
        {
            NewGame?.Invoke(sender, e);
        }
        public void OnPlayerTurn_Relay(object sender, PlayerTurnEventArgs e)
        {
            PlayerTurn?.Invoke(sender, e);
        }
        public void OnTurnEnd_Relay(object sender, TurnEndEventArgs e)
        {
            TurnEnd?.Invoke(sender, e);
        }
        #endregion
    }
}
