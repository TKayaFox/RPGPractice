using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice
{
    internal class MobUpdateArgs
    {
        private string name;
        private string sprite;
        private int hitPoints;
        private int mana;
        private bool isAlive;

        public string Name { get => name; set => name = value; }
        public string Sprite { get => sprite; set => sprite = value; }
        public int HitPoints { get => hitPoints; set => hitPoints = value; }
        public int Mana { get => mana; set => mana = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }
    }
}
