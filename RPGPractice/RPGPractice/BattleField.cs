using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice
{
    public class BattleField
    {
        public event System.EventHandler Attack;

        public string[] villianList
        {
            get => default;
            set
            {
            }
        }

        public void OnPlayerTurn_Handler()
        {
            throw new System.NotImplementedException();
        }

        public void OnDeath_Handler()
        {
            throw new System.NotImplementedException();
        }

        public void OnHit()
        {
            throw new System.NotImplementedException();
        }

        public void OnAttack()
        {
            throw new System.NotImplementedException();
        }
    }
}