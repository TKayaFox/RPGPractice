namespace RPGPractice
{
    partial class BattleField
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ArenaPictureBox = new PictureBox();
            ActionGroupBox = new GroupBox();
            TargetComboBox = new ComboBox();
            TargetSelectLabel = new Label();
            button2 = new Button();
            button1 = new Button();
            AttackButton = new Button();
            ((System.ComponentModel.ISupportInitialize)ArenaPictureBox).BeginInit();
            ActionGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // ArenaPictureBox
            // 
            ArenaPictureBox.Location = new Point(3, 3);
            ArenaPictureBox.Name = "ArenaPictureBox";
            ArenaPictureBox.Size = new Size(727, 404);
            ArenaPictureBox.TabIndex = 0;
            ArenaPictureBox.TabStop = false;
            // 
            // ActionGroupBox
            // 
            ActionGroupBox.Controls.Add(TargetComboBox);
            ActionGroupBox.Controls.Add(TargetSelectLabel);
            ActionGroupBox.Controls.Add(button2);
            ActionGroupBox.Controls.Add(button1);
            ActionGroupBox.Controls.Add(AttackButton);
            ActionGroupBox.Location = new Point(3, 413);
            ActionGroupBox.Name = "ActionGroupBox";
            ActionGroupBox.Size = new Size(727, 217);
            ActionGroupBox.TabIndex = 1;
            ActionGroupBox.TabStop = false;
            // 
            // TargetComboBox
            // 
            TargetComboBox.Font = new Font("Microsoft Sans Serif", 20.25F);
            TargetComboBox.FormattingEnabled = true;
            TargetComboBox.Location = new Point(105, 16);
            TargetComboBox.Name = "TargetComboBox";
            TargetComboBox.Size = new Size(367, 39);
            TargetComboBox.TabIndex = 4;
            // 
            // TargetSelectLabel
            // 
            TargetSelectLabel.AutoSize = true;
            TargetSelectLabel.Font = new Font("Microsoft Sans Serif", 20.25F);
            TargetSelectLabel.Location = new Point(6, 19);
            TargetSelectLabel.Name = "TargetSelectLabel";
            TargetSelectLabel.Size = new Size(93, 31);
            TargetSelectLabel.TabIndex = 3;
            TargetSelectLabel.Text = "Target";
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft Sans Serif", 20.25F);
            button2.Location = new Point(276, 62);
            button2.Name = "button2";
            button2.Size = new Size(196, 59);
            button2.TabIndex = 2;
            button2.Text = "Special Attack";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft Sans Serif", 20.25F);
            button1.Location = new Point(141, 62);
            button1.Name = "button1";
            button1.Size = new Size(129, 59);
            button1.TabIndex = 1;
            button1.Text = "Defend";
            button1.UseVisualStyleBackColor = true;
            // 
            // AttackButton
            // 
            AttackButton.Font = new Font("Microsoft Sans Serif", 20.25F);
            AttackButton.Location = new Point(6, 62);
            AttackButton.Name = "AttackButton";
            AttackButton.Size = new Size(129, 59);
            AttackButton.TabIndex = 0;
            AttackButton.Text = "Attack";
            AttackButton.UseVisualStyleBackColor = true;
            // 
            // BattleField
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(ActionGroupBox);
            Controls.Add(ArenaPictureBox);
            Name = "BattleField";
            Size = new Size(826, 672);
            ((System.ComponentModel.ISupportInitialize)ArenaPictureBox).EndInit();
            ActionGroupBox.ResumeLayout(false);
            ActionGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox ArenaPictureBox;
        private GroupBox ActionGroupBox;
        private Button button2;
        private Button button1;
        private Button AttackButton;
        private ComboBox TargetComboBox;
        private Label TargetSelectLabel;
    }
}
