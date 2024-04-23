namespace RPGPractice
{
    public partial class GUI : Form
    {
        public event System.EventHandler TryAttack;
        public event System.EventHandler Attack;

        public GUI()
        {
            InitializeComponent();
        }

        public BattleField BattleField
        {
            get => default;
            set
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void OnBattleEnd_Handler()
        {
            throw new System.NotImplementedException();
        }

        public void OnNewBattle_Handler()
        {
            throw new System.NotImplementedException();
        }

        public void NewGame()
        {
            throw new System.NotImplementedException();
        }
    }
}
