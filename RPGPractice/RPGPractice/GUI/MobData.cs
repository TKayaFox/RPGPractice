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
        private bool isAlive;
        private string special = "";

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
                PictureBox.Visible = true;
            }
        }

        /// <summary>
        /// Override ToString so object is identified by name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name; // Or any other string format that includes more details about the mob
        }

        /// <summary>
        /// Updates isAlive to false and changes PictureBox to show death
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        internal void OnDeath_Handler(object? sender, EventArgs e)
        {
            isAlive = false;
            pictureBox.Image = null;
        }

        public string Special { get => special; set => special = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }

        private bool selected;

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
        /// Called by Selected to either turn on or off picturebox border
        /// </summary>
        private void Select()
        {
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
        }
        private void DeSelect()
        {
            pictureBox.BorderStyle = BorderStyle.None;
        }
    }
}
