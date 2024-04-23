using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice
{
    public class Game
    {
        private int numWins;

        public Mob[] Heroes
        {
            get => default;
            set
            {
            }
        }

        public Battle Battle
        {
            get => default;
            set
            {
            }
        }

        public int playerName
        {
            get => default;
            set
            {
            }
        }

        public void NewBattle()
        {
            throw new System.NotImplementedException();
        }

        public void ReviveParty()
        {
            throw new System.NotImplementedException();
        }

        public void OnBattleEnd_Handler()
        {
            throw new System.NotImplementedException();
        }

        public void HealParty()
        {
            throw new System.NotImplementedException();
        }
    }
}