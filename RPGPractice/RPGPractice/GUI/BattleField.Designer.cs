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
            ActionButtonBox = new GroupBox();
            button2 = new Button();
            DefendButton = new Button();
            attackButton = new Button();
            heroSprite5 = new PictureBox();
            battleSummaryTBox = new GroupBox();
            villianSprite4 = new PictureBox();
            heroSprite4 = new PictureBox();
            heroSprite2 = new PictureBox();
            villianSprite2 = new PictureBox();
            heroSprite3 = new PictureBox();
            villianSprite3 = new PictureBox();
            heroSprite1 = new PictureBox();
            villianSprite5 = new PictureBox();
            villianSprite1 = new PictureBox();
            ActionMenuGrou = new GroupBox();
            ActionTargetBox = new GroupBox();
            TargetCBox = new ComboBox();
            TargetButt = new Button();
            textBox1 = new TextBox();
            ActionButtonBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)heroSprite5).BeginInit();
            battleSummaryTBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)villianSprite4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)villianSprite2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)villianSprite3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)villianSprite5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)villianSprite1).BeginInit();
            ActionMenuGrou.SuspendLayout();
            ActionTargetBox.SuspendLayout();
            SuspendLayout();
            // 
            // ActionButtonBox
            // 
            ActionButtonBox.Controls.Add(button2);
            ActionButtonBox.Controls.Add(DefendButton);
            ActionButtonBox.Controls.Add(attackButton);
            ActionButtonBox.Location = new Point(208, 0);
            ActionButtonBox.Margin = new Padding(3, 4, 3, 4);
            ActionButtonBox.Name = "ActionButtonBox";
            ActionButtonBox.Padding = new Padding(0);
            ActionButtonBox.Size = new Size(462, 74);
            ActionButtonBox.TabIndex = 1;
            ActionButtonBox.TabStop = false;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft Sans Serif", 20.25F);
            button2.Location = new Point(309, 17);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(147, 47);
            button2.TabIndex = 2;
            button2.Text = "Special";
            button2.UseVisualStyleBackColor = true;
            button2.Click += ActionButton_Click;
            // 
            // DefendButton
            // 
            DefendButton.Font = new Font("Microsoft Sans Serif", 20.25F);
            DefendButton.Location = new Point(156, 17);
            DefendButton.Margin = new Padding(3, 4, 3, 4);
            DefendButton.Name = "DefendButton";
            DefendButton.Size = new Size(147, 47);
            DefendButton.TabIndex = 1;
            DefendButton.Text = "Defend";
            DefendButton.UseVisualStyleBackColor = true;
            DefendButton.Click += ActionButton_Click;
            // 
            // attackButton
            // 
            attackButton.Font = new Font("Microsoft Sans Serif", 20.25F);
            attackButton.Location = new Point(6, 17);
            attackButton.Margin = new Padding(3, 4, 3, 4);
            attackButton.Name = "attackButton";
            attackButton.Size = new Size(147, 47);
            attackButton.TabIndex = 0;
            attackButton.Text = "Attack";
            attackButton.UseVisualStyleBackColor = true;
            attackButton.Click += ActionButton_Click;
            // 
            // heroSprite5
            // 
            heroSprite5.BackColor = Color.Transparent;
            heroSprite5.Image = Properties.Resources.Fighter;
            heroSprite5.Location = new Point(43, 95);
            heroSprite5.Margin = new Padding(3, 4, 3, 4);
            heroSprite5.Name = "heroSprite5";
            heroSprite5.Size = new Size(114, 133);
            heroSprite5.SizeMode = PictureBoxSizeMode.StretchImage;
            heroSprite5.TabIndex = 2;
            heroSprite5.TabStop = false;
            // 
            // battleSummaryTBox
            // 
            battleSummaryTBox.BackgroundImageLayout = ImageLayout.Stretch;
            battleSummaryTBox.Controls.Add(villianSprite4);
            battleSummaryTBox.Controls.Add(heroSprite4);
            battleSummaryTBox.Controls.Add(heroSprite2);
            battleSummaryTBox.Controls.Add(villianSprite2);
            battleSummaryTBox.Controls.Add(heroSprite3);
            battleSummaryTBox.Controls.Add(villianSprite3);
            battleSummaryTBox.Controls.Add(heroSprite1);
            battleSummaryTBox.Controls.Add(villianSprite5);
            battleSummaryTBox.Controls.Add(heroSprite5);
            battleSummaryTBox.Controls.Add(villianSprite1);
            battleSummaryTBox.Location = new Point(0, 7);
            battleSummaryTBox.Margin = new Padding(3, 4, 3, 4);
            battleSummaryTBox.Name = "battleSummaryTBox";
            battleSummaryTBox.Padding = new Padding(3, 4, 3, 4);
            battleSummaryTBox.Size = new Size(834, 557);
            battleSummaryTBox.TabIndex = 3;
            battleSummaryTBox.TabStop = false;
            battleSummaryTBox.Text = "groupBox1";
            // 
            // villianSprite4
            // 
            villianSprite4.BackColor = Color.Transparent;
            villianSprite4.Image = Properties.Resources.Bandit;
            villianSprite4.Location = new Point(705, 376);
            villianSprite4.Margin = new Padding(3, 4, 3, 4);
            villianSprite4.Name = "villianSprite4";
            villianSprite4.Size = new Size(114, 133);
            villianSprite4.SizeMode = PictureBoxSizeMode.StretchImage;
            villianSprite4.TabIndex = 6;
            villianSprite4.TabStop = false;
            // 
            // heroSprite4
            // 
            heroSprite4.BackColor = Color.Transparent;
            heroSprite4.Image = Properties.Resources.Fighter;
            heroSprite4.Location = new Point(89, 376);
            heroSprite4.Margin = new Padding(3, 4, 3, 4);
            heroSprite4.Name = "heroSprite4";
            heroSprite4.Size = new Size(114, 133);
            heroSprite4.SizeMode = PictureBoxSizeMode.StretchImage;
            heroSprite4.TabIndex = 6;
            heroSprite4.TabStop = false;
            // 
            // heroSprite2
            // 
            heroSprite2.BackColor = Color.Transparent;
            heroSprite2.Image = Properties.Resources.Fighter;
            heroSprite2.Location = new Point(62, 236);
            heroSprite2.Margin = new Padding(3, 4, 3, 4);
            heroSprite2.Name = "heroSprite2";
            heroSprite2.Size = new Size(114, 133);
            heroSprite2.SizeMode = PictureBoxSizeMode.StretchImage;
            heroSprite2.TabIndex = 5;
            heroSprite2.TabStop = false;
            // 
            // villianSprite2
            // 
            villianSprite2.BackColor = Color.Transparent;
            villianSprite2.Image = Properties.Resources.Bandit;
            villianSprite2.Location = new Point(677, 236);
            villianSprite2.Margin = new Padding(3, 4, 3, 4);
            villianSprite2.Name = "villianSprite2";
            villianSprite2.Size = new Size(114, 133);
            villianSprite2.SizeMode = PictureBoxSizeMode.StretchImage;
            villianSprite2.TabIndex = 5;
            villianSprite2.TabStop = false;
            // 
            // heroSprite3
            // 
            heroSprite3.BackColor = Color.Transparent;
            heroSprite3.Image = Properties.Resources.Fighter;
            heroSprite3.Location = new Point(183, 169);
            heroSprite3.Margin = new Padding(3, 4, 3, 4);
            heroSprite3.Name = "heroSprite3";
            heroSprite3.Size = new Size(114, 133);
            heroSprite3.SizeMode = PictureBoxSizeMode.StretchImage;
            heroSprite3.TabIndex = 4;
            heroSprite3.TabStop = false;
            // 
            // villianSprite3
            // 
            villianSprite3.BackColor = Color.Transparent;
            villianSprite3.Image = Properties.Resources.Bandit;
            villianSprite3.Location = new Point(531, 169);
            villianSprite3.Margin = new Padding(3, 4, 3, 4);
            villianSprite3.Name = "villianSprite3";
            villianSprite3.Size = new Size(114, 133);
            villianSprite3.SizeMode = PictureBoxSizeMode.StretchImage;
            villianSprite3.TabIndex = 4;
            villianSprite3.TabStop = false;
            // 
            // heroSprite1
            // 
            heroSprite1.BackColor = Color.Transparent;
            heroSprite1.Image = Properties.Resources.Fighter;
            heroSprite1.Location = new Point(208, 315);
            heroSprite1.Margin = new Padding(3, 4, 3, 4);
            heroSprite1.Name = "heroSprite1";
            heroSprite1.Size = new Size(114, 133);
            heroSprite1.SizeMode = PictureBoxSizeMode.StretchImage;
            heroSprite1.TabIndex = 3;
            heroSprite1.TabStop = false;
            // 
            // villianSprite5
            // 
            villianSprite5.BackColor = Color.Transparent;
            villianSprite5.Image = Properties.Resources.Bandit;
            villianSprite5.Location = new Point(659, 95);
            villianSprite5.Margin = new Padding(3, 4, 3, 4);
            villianSprite5.Name = "villianSprite5";
            villianSprite5.Size = new Size(114, 133);
            villianSprite5.SizeMode = PictureBoxSizeMode.StretchImage;
            villianSprite5.TabIndex = 2;
            villianSprite5.TabStop = false;
            // 
            // villianSprite1
            // 
            villianSprite1.BackColor = Color.Transparent;
            villianSprite1.Image = Properties.Resources.Bandit;
            villianSprite1.Location = new Point(556, 315);
            villianSprite1.Margin = new Padding(3, 4, 3, 4);
            villianSprite1.Name = "villianSprite1";
            villianSprite1.Size = new Size(114, 133);
            villianSprite1.SizeMode = PictureBoxSizeMode.StretchImage;
            villianSprite1.TabIndex = 3;
            villianSprite1.TabStop = false;
            villianSprite1.Visible = false;
            // 
            // ActionMenuGrou
            // 
            ActionMenuGrou.Controls.Add(ActionButtonBox);
            ActionMenuGrou.Controls.Add(ActionTargetBox);
            ActionMenuGrou.Location = new Point(0, 751);
            ActionMenuGrou.Name = "ActionMenuGrou";
            ActionMenuGrou.Size = new Size(834, 74);
            ActionMenuGrou.TabIndex = 4;
            ActionMenuGrou.TabStop = false;
            // 
            // ActionTargetBox
            // 
            ActionTargetBox.Controls.Add(TargetCBox);
            ActionTargetBox.Controls.Add(TargetButt);
            ActionTargetBox.Location = new Point(208, 0);
            ActionTargetBox.Margin = new Padding(3, 4, 3, 4);
            ActionTargetBox.Name = "ActionTargetBox";
            ActionTargetBox.Padding = new Padding(0);
            ActionTargetBox.Size = new Size(462, 74);
            ActionTargetBox.TabIndex = 7;
            ActionTargetBox.TabStop = false;
            // 
            // TargetCBox
            // 
            TargetCBox.Font = new Font("Microsoft Sans Serif", 20.25F);
            TargetCBox.FormattingEnabled = true;
            TargetCBox.Items.AddRange(new object[] { "- Select Target -" });
            TargetCBox.Location = new Point(6, 18);
            TargetCBox.Margin = new Padding(3, 4, 3, 4);
            TargetCBox.Name = "TargetCBox";
            TargetCBox.Size = new Size(297, 47);
            TargetCBox.TabIndex = 4;
            // 
            // TargetButt
            // 
            TargetButt.Font = new Font("Microsoft Sans Serif", 20.25F);
            TargetButt.Location = new Point(309, 18);
            TargetButt.Margin = new Padding(3, 4, 3, 4);
            TargetButt.Name = "TargetButt";
            TargetButt.Size = new Size(147, 47);
            TargetButt.TabIndex = 0;
            TargetButt.Text = "Attack";
            TargetButt.UseVisualStyleBackColor = true;
            TargetButt.Click += TargetButt_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.ActiveCaption;
            textBox1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(3, 571);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(825, 190);
            textBox1.TabIndex = 5;
            textBox1.Text = "Battle Start!";
            // 
            // BattleField
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textBox1);
            Controls.Add(ActionMenuGrou);
            Controls.Add(battleSummaryTBox);
            Margin = new Padding(3, 4, 3, 4);
            Name = "BattleField";
            Size = new Size(842, 907);
            ActionButtonBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)heroSprite5).EndInit();
            battleSummaryTBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)villianSprite4).EndInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite4).EndInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite2).EndInit();
            ((System.ComponentModel.ISupportInitialize)villianSprite2).EndInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite3).EndInit();
            ((System.ComponentModel.ISupportInitialize)villianSprite3).EndInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite1).EndInit();
            ((System.ComponentModel.ISupportInitialize)villianSprite5).EndInit();
            ((System.ComponentModel.ISupportInitialize)villianSprite1).EndInit();
            ActionMenuGrou.ResumeLayout(false);
            ActionTargetBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox ArenaPictureBox;
        private GroupBox ActionButtonBox;
        private Button button2;
        private Button DefendButton;
        private Button attackButton;
        private PictureBox heroSprite5;
        private GroupBox battleSummaryTBox;
        private PictureBox heroSprite1;
        private PictureBox heroSprite4;
        private PictureBox heroSprite2;
        private PictureBox heroSprite3;
        private PictureBox villianSprite4;
        private PictureBox villianSprite2;
        private PictureBox villianSprite3;
        private PictureBox villianSprite5;
        private PictureBox villianSprite1;
        private GroupBox ActionMenuGrou;
        private GroupBox ActionTargetBox;
        private ComboBox TargetCBox;
        private Button TargetButt;
        private TextBox textBox1;
    }
}
