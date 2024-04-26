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
        private bool isNPC;

        public Bitmap Sprite { set => sprite = value; }
        public int UniqueID { get => uniqueID; set => uniqueID = value; }
        public string Name { get => name; set => name = value; }
        public bool IsNPC { get => isNPC; set => isNPC = value; }

        /// <summary>
        /// Property but when PictureBox is set, Sprite is already applied to the PictureBox
        /// </summary>
        public PictureBox PictureBox 
        { 
            get => pictureBox;
            set
            {
                pictureBox = value;
                pictureBox.Image = sprite;
            }
        }

    }
}
