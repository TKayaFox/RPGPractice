﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice
{
    public class BattleEndEventArgs
    {
        private bool victory;

        public bool Victory { get => victory; set => victory = value; }
    }
}
