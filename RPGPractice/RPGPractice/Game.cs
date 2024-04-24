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

        public void HealParty()
        {
            throw new System.NotImplementedException();
        }
    }
}