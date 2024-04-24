namespace RPGPractice
{
    public partial class GUI : Form
    {
        public event System.EventHandler TryAttack;
        public event System.EventHandler Attack;

        private BattleField battlefield;

        public GUI()
        {
            InitializeComponent();

            //Initialize battlefield
        }

        public void OnBattleEnd_Handler(object sender, BattleEndEventArgs e)
        {
            //IF result of battle was player victory, keep looping
            if (e.Result == true)
            {
                //Increment victory count

                //EDIT: heal heroes;

                //Edit: Start new Battle
            }
            else
            {
                //Edit: End game logic, save result to leaderboard, etc
            }
        }

        public void OnNewBattle_Handler()
        {
            throw new System.NotImplementedException();
        }

        public void NewGame()
        {
            //Initialize and subscribe to new game
            Game game = new Game();
            SubscribeGame(game);

            //EDIT: Get a name for the game save

            //Edit: Start the game

        }

        public void SubscribeGame(Game game)
        {

        }
    }
}
