﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.Engine.MobClasses;

namespace RPGPractice.Events
{
    public class PlayerTurnEventArgs
    {
        Mob mob;

        public Mob Mob { get => mob; set => mob = value; }
    }
}