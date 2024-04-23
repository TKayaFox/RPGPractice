using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPractice
{
    public class Hero : Mob
    {
        public Hero (string name)
        {
            Super(name);

        }
    }
}