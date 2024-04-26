using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGPractice.GUI
{
    public class MobData
    {
        private int uniqueID;
        private System.Drawing.Bitmap sprite;
        private PictureBox pictureBox;
        private string name;
        private bool isHero;

        public int UniqueID { get => uniqueID; set => uniqueID = value; }
        public Bitmap Sprite { get => sprite; set => sprite = value; }
        public PictureBox PictureBox { get => pictureBox; set => pictureBox = value; }
        public string Name { get => name; set => name = value; }
        public bool IsHero { get => isHero; set => isHero = value; }
    }
}
