using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.Core.Enumerations;

namespace RPGPractice.GUI
{
    public class MobData
    {
        private int uniqueID;
        private System.Drawing.Bitmap? sprite;
        private PictureBox? pictureBox;
        private string? name;
        private bool isNPC;
        private string specialAction = "";

        //Updatable values
        private bool isAlive;
        private int hitPoints;

        /// <summary>
        /// Override ToString so object is identified by name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string stringReturn = $"{name} [{HitPoints}]";
            return name;
        }


        // Property setters/getters
        public Bitmap Sprite { set => sprite = value; }
        public int UniqueID { get => uniqueID; set => uniqueID = value; }
        public string? Name { get => name; set => name = value; }
        public bool IsNPC { get => isNPC; set => isNPC = value; }
        public string SpecialActionString { get => specialAction; set => specialAction = value; }
        public int HitPoints { get => hitPoints; set => hitPoints = value; }

        public bool IsAlive 
        {
            get => isAlive;
            set
            {
                isAlive = value;
                
                //reflect death in sprites
                if(!value && pictureBox != null)
                {
                        pictureBox.Image = null;
                }
            }
        }

        /// <summary>
        /// Property but when PictureBox is set, Sprite is already applied to the PictureBox
        /// </summary>
        public PictureBox? PictureBox 
        { 
            get => pictureBox;
            set
            {
                if (value != null)
                {
                    pictureBox = value;
                    pictureBox.Image = sprite;
                    pictureBox.Visible = true;
                }
            }
        }

        /// <summary>
        /// Turn on and off Picture Border depending on boolean
        /// </summary>
        public bool Selected
        {
            set
            {
                if (value)
                {
                    Select();
                }
                else
                {
                    DeSelect();
                }
            }
        }


        /// <summary>
        /// Called by Selected to turn on picturebox border
        /// </summary>
        private void Select()
        {
            if (pictureBox != null)
            {
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        /// <summary>
        /// Called by Selected to turn off picturebox border
        /// </summary>
        private void DeSelect()
        {
            if (pictureBox != null)
            {
                pictureBox.BorderStyle = BorderStyle.None;
            }
        }
    }
}
