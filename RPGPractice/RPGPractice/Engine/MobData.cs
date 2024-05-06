using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGPractice.Core.Enumerations;

namespace RPGPractice.Engine
{
    public class MobData
    {
        private int uniqueID;
        private Bitmap? sprite;
        private PictureBox? pictureBox;
        private string? name;
        private bool isNPC;
        private bool isAlive;
        private string specialAction = "";
        private string hpString = "";
        private string manaString = "";


        // Property setters/getters
        public Bitmap Sprite { set => sprite = value; }
        public int UniqueID { get => uniqueID; set => uniqueID = value; }
        public string? Name { get => name; set => name = value; }
        public bool IsNPC { get => isNPC; set => isNPC = value; }
        public string SpecialActionString { get => specialAction; set => specialAction = value; }
        public string HitPointString { get => hpString; set => hpString = value; }
        public string ManaString { get => manaString; set => manaString = value; }

        /// <summary>
        /// Gets or Sets isAlive boolean
        ///     if setting isAlive, updates pictureBox to match state
        /// </summary>
        public bool IsAlive
        {
            get => isAlive;
            set
            {
                isAlive = value;

                //reflect alive status  in sprites (hide pictureBox if dead)
                if (!value && pictureBox != null)
                {
                    pictureBox.Visible = false;
                }
                else
                {
                    pictureBox.Visible = true;
                }
            }
        }

        /// <summary>
        /// Override ToString so object is identified by name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (name == null) { name = ""; }
            string returnString = "";

            //check if ManaString holds actual data
            if (!manaString.Equals(""))
            {
                returnString += $" Mana: {ManaString}";
            }

            returnString += $"[HitPoints: {HitPointString}{returnString}]";

            return $"{Name} [{returnString}";
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
