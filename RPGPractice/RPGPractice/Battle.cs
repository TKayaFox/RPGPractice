using System.Xml.Linq;

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



        private void Attack(Mob attacker, Mob target)
        {
            //Get all relevant data from attacker and defender
            int attackRoll = attacker.RollAttack();
            int defense = target.Defense;
            String eventMessage;

            //Check if attackRoll < target defense (If attack hits)
            if (attackRoll > defense) 
            {
                //roll damage
                int damage = attacker.RollDmg();
                target.Hit(damage);

                eventMessage = $"{attacker.Name} hit {target.Name} for {damage} Damage!";
            }

            //Check for Meeting defense
            else if (attackRoll == defense)
            {
                eventMessage = $"{target.Name} barely dodged {attacker.Name}'s attack.";
            }
            else
            {
                eventMessage = $"{target.Name} dodged {attacker.Name}'s attack.";
            }

            //Raise BattleEvent to declare what happened
            OnBattleEvent(eventMessage);
        }


        //Events

        /// <summary>
        /// Packages and raisesBattle Events (Which display for user a readout of what has happened in the battle so far)
        /// </summary>
        /// <param name="output"></param>
        private void OnBattleEvent(string output)
        {
            //EDIT: Raise BattleEvent event and target the "output" string

        }
    }
}