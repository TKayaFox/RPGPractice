namespace RPGPractice
{
    public class Battle
    {

        public event System.EventHandler Hit;
        public event System.EventHandler BattleEnd;
        public event System.EventHandler Death;

        public Battle()
        {
            throw new System.NotImplementedException();
        }

        public Mob[] villians
        {
            get => default;
            set
            {
            }
        }

        public int heroes
        {
            get => default;
            set
            {
            }
        }

        public void RollInitiative()
        {
            throw new System.NotImplementedException();
        }

        public void NextTurn()
        {
            throw new System.NotImplementedException();
        }

        public void OnAttack_Handler()
        {
            throw new System.NotImplementedException();
        }

        public void OnHit()
        {
            throw new System.NotImplementedException();
        }

        public void OnDeath()
        {
            throw new System.NotImplementedException();
        }

        public void OnBattleEnd()
        {
            throw new System.NotImplementedException();
        }

        public void GenerateEncounter()
        {
            throw new System.NotImplementedException();
        }

        public void NewMob()
        {
            throw new System.NotImplementedException();
        }
    }
}