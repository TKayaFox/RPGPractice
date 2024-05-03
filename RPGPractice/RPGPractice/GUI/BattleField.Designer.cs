
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
            ActionButtBox = new GroupBox();
            SpecialButt = new Button();
            DefendButt = new Button();
            AttackButt = new Button();
            heroSprite5 = new PictureBox();
            BatleArenaGroupBox = new GroupBox();
            enemySprite4 = new PictureBox();
            heroSprite4 = new PictureBox();
            heroSprite2 = new PictureBox();
            enemySprite2 = new PictureBox();
            heroSprite3 = new PictureBox();
            enemySprite3 = new PictureBox();
            heroSprite1 = new PictureBox();
            enemySprite5 = new PictureBox();
            enemySprite1 = new PictureBox();
            ActionMenuGroup = new GroupBox();
            TurnLabel = new Label();
            ActionTargetBox = new GroupBox();
            targetCBox = new ComboBox();
            TargetButt = new Button();
            battleSummaryTBox = new TextBox();
            ActionButtBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)heroSprite5).BeginInit();
            BatleArenaGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)enemySprite4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemySprite2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemySprite3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemySprite5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)enemySprite1).BeginInit();
            ActionMenuGroup.SuspendLayout();
            ActionTargetBox.SuspendLayout();
            SuspendLayout();
            // 
            // ActionButtBox
            // 
            ActionButtBox.Controls.Add(SpecialButt);
            ActionButtBox.Controls.Add(DefendButt);
            ActionButtBox.Controls.Add(AttackButt);
            ActionButtBox.Location = new Point(182, 0);
            ActionButtBox.Name = "ActionButtBox";
            ActionButtBox.Padding = new Padding(0);
            ActionButtBox.Size = new Size(404, 56);
            ActionButtBox.TabIndex = 1;
            ActionButtBox.TabStop = false;
            // 
            // SpecialButt
            // 
            SpecialButt.Font = new Font("Microsoft Sans Serif", 15.75F);
            SpecialButt.Location = new Point(137, 13);
            SpecialButt.Name = "SpecialButt";
            SpecialButt.Size = new Size(129, 35);
            SpecialButt.TabIndex = 2;
            SpecialButt.Text = "Special";
            SpecialButt.UseVisualStyleBackColor = true;
            SpecialButt.Click += ActionButton_Click;
            // 
            // DefendButt
            // 
            DefendButt.Font = new Font("Microsoft Sans Serif", 15.75F);
            DefendButt.Location = new Point(270, 13);
            DefendButt.Name = "DefendButt";
            DefendButt.Size = new Size(129, 35);
            DefendButt.TabIndex = 1;
            DefendButt.Text = "Defend";
            DefendButt.UseVisualStyleBackColor = true;
            DefendButt.Click += ActionButton_Click;
            // 
            // AttackButt
            // 
            AttackButt.Font = new Font("Microsoft Sans Serif", 15.75F);
            AttackButt.Location = new Point(5, 13);
            AttackButt.Name = "AttackButt";
            AttackButt.Size = new Size(129, 35);
            AttackButt.TabIndex = 0;
            AttackButt.Text = "Attack";
            AttackButt.UseVisualStyleBackColor = true;
            AttackButt.Click += ActionButton_Click;
            // 
            // heroSprite5
            // 
            heroSprite5.BackColor = Color.Transparent;
            heroSprite5.BackgroundImageLayout = ImageLayout.Stretch;
            heroSprite5.Location = new Point(38, 71);
            heroSprite5.Name = "heroSprite5";
            heroSprite5.Size = new Size(100, 100);
            heroSprite5.SizeMode = PictureBoxSizeMode.StretchImage;
            heroSprite5.TabIndex = 2;
            heroSprite5.TabStop = false;
            // 
            // BatleArenaGroupBox
            // 
            BatleArenaGroupBox.BackgroundImage = Properties.Resources.game_background_1;
            BatleArenaGroupBox.BackgroundImageLayout = ImageLayout.Stretch;
            BatleArenaGroupBox.Controls.Add(enemySprite4);
            BatleArenaGroupBox.Controls.Add(heroSprite4);
            BatleArenaGroupBox.Controls.Add(heroSprite2);
            BatleArenaGroupBox.Controls.Add(enemySprite2);
            BatleArenaGroupBox.Controls.Add(heroSprite3);
            BatleArenaGroupBox.Controls.Add(enemySprite3);
            BatleArenaGroupBox.Controls.Add(heroSprite1);
            BatleArenaGroupBox.Controls.Add(enemySprite5);
            BatleArenaGroupBox.Controls.Add(heroSprite5);
            BatleArenaGroupBox.Controls.Add(enemySprite1);
            BatleArenaGroupBox.Location = new Point(0, 5);
            BatleArenaGroupBox.Name = "BatleArenaGroupBox";
            BatleArenaGroupBox.Size = new Size(730, 418);
            BatleArenaGroupBox.TabIndex = 3;
            BatleArenaGroupBox.TabStop = false;
            // 
            // enemySprite4
            // 
            enemySprite4.BackColor = Color.Transparent;
            enemySprite4.BackgroundImageLayout = ImageLayout.Stretch;
            enemySprite4.Location = new Point(617, 282);
            enemySprite4.Name = "enemySprite4";
            enemySprite4.Size = new Size(100, 100);
            enemySprite4.SizeMode = PictureBoxSizeMode.StretchImage;
            enemySprite4.TabIndex = 6;
            enemySprite4.TabStop = false;
            // 
            // heroSprite4
            // 
            heroSprite4.BackColor = Color.Transparent;
            heroSprite4.BackgroundImageLayout = ImageLayout.Stretch;
            heroSprite4.Location = new Point(78, 282);
            heroSprite4.Name = "heroSprite4";
            heroSprite4.Size = new Size(100, 100);
            heroSprite4.SizeMode = PictureBoxSizeMode.StretchImage;
            heroSprite4.TabIndex = 6;
            heroSprite4.TabStop = false;
            // 
            // heroSprite2
            // 
            heroSprite2.BackColor = Color.Transparent;
            heroSprite2.BackgroundImageLayout = ImageLayout.Stretch;
            heroSprite2.Location = new Point(54, 177);
            heroSprite2.Name = "heroSprite2";
            heroSprite2.Size = new Size(100, 100);
            heroSprite2.SizeMode = PictureBoxSizeMode.StretchImage;
            heroSprite2.TabIndex = 5;
            heroSprite2.TabStop = false;
            // 
            // enemySprite2
            // 
            enemySprite2.BackColor = Color.Transparent;
            enemySprite2.BackgroundImageLayout = ImageLayout.Stretch;
            enemySprite2.Location = new Point(592, 177);
            enemySprite2.Name = "enemySprite2";
            enemySprite2.Size = new Size(100, 100);
            enemySprite2.SizeMode = PictureBoxSizeMode.StretchImage;
            enemySprite2.TabIndex = 5;
            enemySprite2.TabStop = false;
            // 
            // heroSprite3
            // 
            heroSprite3.BackColor = Color.Transparent;
            heroSprite3.BackgroundImageLayout = ImageLayout.Stretch;
            heroSprite3.Location = new Point(160, 127);
            heroSprite3.Name = "heroSprite3";
            heroSprite3.Size = new Size(100, 100);
            heroSprite3.SizeMode = PictureBoxSizeMode.StretchImage;
            heroSprite3.TabIndex = 4;
            heroSprite3.TabStop = false;
            // 
            // enemySprite3
            // 
            enemySprite3.BackColor = Color.Transparent;
            enemySprite3.BackgroundImageLayout = ImageLayout.Stretch;
            enemySprite3.Location = new Point(465, 127);
            enemySprite3.Name = "enemySprite3";
            enemySprite3.Size = new Size(100, 100);
            enemySprite3.SizeMode = PictureBoxSizeMode.StretchImage;
            enemySprite3.TabIndex = 4;
            enemySprite3.TabStop = false;
            // 
            // heroSprite1
            // 
            heroSprite1.BackColor = Color.Transparent;
            heroSprite1.BackgroundImageLayout = ImageLayout.Stretch;
            heroSprite1.Location = new Point(182, 236);
            heroSprite1.Name = "heroSprite1";
            heroSprite1.Size = new Size(100, 100);
            heroSprite1.SizeMode = PictureBoxSizeMode.StretchImage;
            heroSprite1.TabIndex = 3;
            heroSprite1.TabStop = false;
            // 
            // enemySprite5
            // 
            enemySprite5.BackColor = Color.Transparent;
            enemySprite5.BackgroundImageLayout = ImageLayout.Stretch;
            enemySprite5.Location = new Point(577, 71);
            enemySprite5.Name = "enemySprite5";
            enemySprite5.Size = new Size(100, 100);
            enemySprite5.SizeMode = PictureBoxSizeMode.StretchImage;
            enemySprite5.TabIndex = 2;
            enemySprite5.TabStop = false;
            // 
            // enemySprite1
            // 
            enemySprite1.BackColor = Color.Transparent;
            enemySprite1.BackgroundImageLayout = ImageLayout.Stretch;
            enemySprite1.Location = new Point(486, 236);
            enemySprite1.Name = "enemySprite1";
            enemySprite1.Size = new Size(100, 100);
            enemySprite1.SizeMode = PictureBoxSizeMode.StretchImage;
            enemySprite1.TabIndex = 3;
            enemySprite1.TabStop = false;
            // 
            // ActionMenuGroup
            // 
            ActionMenuGroup.Controls.Add(TurnLabel);
            ActionMenuGroup.Controls.Add(ActionButtBox);
            ActionMenuGroup.Controls.Add(ActionTargetBox);
            ActionMenuGroup.Location = new Point(0, 563);
            ActionMenuGroup.Margin = new Padding(3, 2, 3, 2);
            ActionMenuGroup.Name = "ActionMenuGroup";
            ActionMenuGroup.Padding = new Padding(3, 2, 3, 2);
            ActionMenuGroup.Size = new Size(730, 56);
            ActionMenuGroup.TabIndex = 4;
            ActionMenuGroup.TabStop = false;
            // 
            // TurnLabel
            // 
            TurnLabel.Font = new Font("Microsoft Sans Serif", 15.75F);
            TurnLabel.Location = new Point(6, 14);
            TurnLabel.Name = "TurnLabel";
            TurnLabel.Size = new Size(175, 34);
            TurnLabel.TabIndex = 8;
            TurnLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ActionTargetBox
            // 
            ActionTargetBox.Controls.Add(targetCBox);
            ActionTargetBox.Controls.Add(TargetButt);
            ActionTargetBox.Location = new Point(182, 0);
            ActionTargetBox.Name = "ActionTargetBox";
            ActionTargetBox.Padding = new Padding(0);
            ActionTargetBox.Size = new Size(404, 56);
            ActionTargetBox.TabIndex = 7;
            ActionTargetBox.TabStop = false;
            // 
            // targetCBox
            // 
            targetCBox.Font = new Font("Microsoft Sans Serif", 20.25F);
            targetCBox.FormattingEnabled = true;
            targetCBox.Items.AddRange(new object[] { "- Select Target -" });
            targetCBox.Location = new Point(5, 14);
            targetCBox.Name = "targetCBox";
            targetCBox.Size = new Size(260, 39);
            targetCBox.TabIndex = 4;
            // 
            // TargetButt
            // 
            TargetButt.Font = new Font("Microsoft Sans Serif", 20.25F);
            TargetButt.Location = new Point(270, 14);
            TargetButt.Name = "TargetButt";
            TargetButt.Size = new Size(129, 35);
            TargetButt.TabIndex = 0;
            TargetButt.Text = "Attack";
            TargetButt.UseVisualStyleBackColor = true;
            TargetButt.Click += TargetButt_Click;
            // 
            // battleSummaryTBox
            // 
            battleSummaryTBox.BackColor = SystemColors.ActiveCaption;
            battleSummaryTBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            battleSummaryTBox.Location = new Point(3, 428);
            battleSummaryTBox.Margin = new Padding(3, 2, 3, 2);
            battleSummaryTBox.Multiline = true;
            battleSummaryTBox.Name = "battleSummaryTBox";
            battleSummaryTBox.ReadOnly = true;
            battleSummaryTBox.ScrollBars = ScrollBars.Vertical;
            battleSummaryTBox.Size = new Size(722, 144);
            battleSummaryTBox.TabIndex = 5;
            battleSummaryTBox.TextAlign = HorizontalAlignment.Center;
            // 
            // BattleField
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(battleSummaryTBox);
            Controls.Add(ActionMenuGroup);
            Controls.Add(BatleArenaGroupBox);
            Name = "BattleField";
            Size = new Size(737, 625);
            ActionButtBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)heroSprite5).EndInit();
            BatleArenaGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)enemySprite4).EndInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite4).EndInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite2).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemySprite2).EndInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite3).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemySprite3).EndInit();
            ((System.ComponentModel.ISupportInitialize)heroSprite1).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemySprite5).EndInit();
            ((System.ComponentModel.ISupportInitialize)enemySprite1).EndInit();
            ActionMenuGroup.ResumeLayout(false);
            ActionTargetBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox ArenaPictureBox;
        private GroupBox ActionButtBox;
        private Button SpecialButt;
        private Button DefendButt;
        private Button AttackButt;
        private PictureBox heroSprite5;
        private PictureBox heroSprite1;
        private PictureBox heroSprite4;
        private PictureBox heroSprite2;
        private PictureBox heroSprite3;
        private PictureBox enemySprite4;
        private PictureBox enemySprite2;
        private PictureBox enemySprite3;
        private PictureBox enemySprite5;
        private PictureBox enemySprite1;
        private GroupBox ActionMenuGroup;
        private GroupBox ActionTargetBox;
        private ComboBox targetCBox;
        private Button TargetButt;
        private TextBox textBox1;
        private Label TurnLabel;
        private GroupBox BatleArenaGroupBox;
        private TextBox battleSummaryTBox;
    }
}
