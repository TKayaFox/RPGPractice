
namespace RPGPractice
{
    partial class BattleScreen
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
            TargetCancelButt = new Button();
            targetCBox = new ComboBox();
            TargetButt = new Button();
            TurnLabel = new Label();
            BattleStartButt = new Button();
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
            SuspendLayout();
            // 
            // ActionButtBox
            // 
            ActionButtBox.Controls.Add(SpecialButt);
            ActionButtBox.Controls.Add(DefendButt);
            ActionButtBox.Controls.Add(AttackButt);
            ActionButtBox.Location = new Point(8, 460);
            ActionButtBox.Margin = new Padding(0);
            ActionButtBox.Name = "ActionButtBox";
            ActionButtBox.Padding = new Padding(0);
            ActionButtBox.Size = new Size(323, 120);
            ActionButtBox.TabIndex = 1;
            ActionButtBox.TabStop = false;
            // 
            // SpecialButt
            // 
            SpecialButt.Font = new Font("Microsoft Sans Serif", 15.75F);
            SpecialButt.Location = new Point(94, 6);
            SpecialButt.Name = "SpecialButt";
            SpecialButt.Size = new Size(129, 32);
            SpecialButt.TabIndex = 2;
            SpecialButt.Text = "Special";
            SpecialButt.UseVisualStyleBackColor = true;
            SpecialButt.Click += ActionButton_Click;
            // 
            // DefendButt
            // 
            DefendButt.Font = new Font("Microsoft Sans Serif", 15.75F);
            DefendButt.Location = new Point(94, 82);
            DefendButt.Name = "DefendButt";
            DefendButt.Size = new Size(129, 32);
            DefendButt.TabIndex = 1;
            DefendButt.Text = "Defend";
            DefendButt.UseVisualStyleBackColor = true;
            DefendButt.Click += ActionButton_Click;
            // 
            // AttackButt
            // 
            AttackButt.Font = new Font("Microsoft Sans Serif", 15.75F);
            AttackButt.Location = new Point(94, 44);
            AttackButt.Name = "AttackButt";
            AttackButt.Size = new Size(129, 32);
            AttackButt.TabIndex = 0;
            AttackButt.Text = "Attack";
            AttackButt.UseVisualStyleBackColor = true;
            AttackButt.Click += ActionButton_Click;
            // 
            // heroSprite5
            // 
            heroSprite5.BackColor = Color.Transparent;
            heroSprite5.BackgroundImageLayout = ImageLayout.Stretch;
            heroSprite5.Location = new Point(38, 78);
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
            BatleArenaGroupBox.Location = new Point(3, 3);
            BatleArenaGroupBox.Name = "BatleArenaGroupBox";
            BatleArenaGroupBox.Size = new Size(722, 420);
            BatleArenaGroupBox.TabIndex = 3;
            BatleArenaGroupBox.TabStop = false;
            // 
            // enemySprite4
            // 
            enemySprite4.BackColor = Color.Transparent;
            enemySprite4.BackgroundImageLayout = ImageLayout.Stretch;
            enemySprite4.Location = new Point(607, 289);
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
            heroSprite4.Location = new Point(78, 289);
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
            heroSprite2.Location = new Point(54, 184);
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
            enemySprite2.Location = new Point(592, 184);
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
            heroSprite3.Location = new Point(160, 134);
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
            enemySprite3.Location = new Point(465, 134);
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
            heroSprite1.Location = new Point(182, 243);
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
            enemySprite5.Location = new Point(577, 78);
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
            enemySprite1.Location = new Point(486, 243);
            enemySprite1.Name = "enemySprite1";
            enemySprite1.Size = new Size(100, 100);
            enemySprite1.SizeMode = PictureBoxSizeMode.StretchImage;
            enemySprite1.TabIndex = 3;
            enemySprite1.TabStop = false;
            // 
            // ActionMenuGroup
            // 
            ActionMenuGroup.Controls.Add(TargetCancelButt);
            ActionMenuGroup.Controls.Add(targetCBox);
            ActionMenuGroup.Controls.Add(TargetButt);
            ActionMenuGroup.Location = new Point(8, 449);
            ActionMenuGroup.Margin = new Padding(3, 2, 3, 2);
            ActionMenuGroup.Name = "ActionMenuGroup";
            ActionMenuGroup.Padding = new Padding(3, 2, 3, 2);
            ActionMenuGroup.Size = new Size(323, 131);
            ActionMenuGroup.TabIndex = 4;
            ActionMenuGroup.TabStop = false;
            // 
            // TargetCancelButt
            // 
            TargetCancelButt.Font = new Font("Microsoft Sans Serif", 15.75F);
            TargetCancelButt.Location = new Point(160, 50);
            TargetCancelButt.Name = "TargetCancelButt";
            TargetCancelButt.Size = new Size(104, 32);
            TargetCancelButt.TabIndex = 7;
            TargetCancelButt.Text = "Cancel";
            TargetCancelButt.UseVisualStyleBackColor = true;
            TargetCancelButt.Click += TargetCancelButt_Click;
            // 
            // targetCBox
            // 
            targetCBox.Font = new Font("Microsoft Sans Serif", 15.75F);
            targetCBox.FormattingEnabled = true;
            targetCBox.Items.AddRange(new object[] { "DragonMob [HP: 100/100 Mana: 100/100]" });
            targetCBox.Location = new Point(10, 11);
            targetCBox.Name = "targetCBox";
            targetCBox.Size = new Size(304, 33);
            targetCBox.TabIndex = 4;
            // 
            // TargetButt
            // 
            TargetButt.Font = new Font("Microsoft Sans Serif", 15.75F);
            TargetButt.Location = new Point(50, 50);
            TargetButt.Name = "TargetButt";
            TargetButt.Size = new Size(104, 32);
            TargetButt.TabIndex = 0;
            TargetButt.Text = "Attack";
            TargetButt.UseVisualStyleBackColor = true;
            TargetButt.Click += TargetButt_Click;
            // 
            // TurnLabel
            // 
            TurnLabel.BorderStyle = BorderStyle.FixedSingle;
            TurnLabel.Font = new Font("Microsoft Sans Serif", 15.75F);
            TurnLabel.Location = new Point(6, 428);
            TurnLabel.Name = "TurnLabel";
            TurnLabel.Size = new Size(326, 34);
            TurnLabel.TabIndex = 8;
            TurnLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BattleStartButt
            // 
            BattleStartButt.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BattleStartButt.Location = new Point(4, 411);
            BattleStartButt.Name = "BattleStartButt";
            BattleStartButt.Size = new Size(721, 169);
            BattleStartButt.TabIndex = 6;
            BattleStartButt.Text = "Start Battle";
            BattleStartButt.UseVisualStyleBackColor = true;
            BattleStartButt.Click += BattleStartButt_Click;
            // 
            // battleSummaryTBox
            // 
            battleSummaryTBox.BackColor = Color.MediumTurquoise;
            battleSummaryTBox.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            battleSummaryTBox.Location = new Point(334, 423);
            battleSummaryTBox.Margin = new Padding(3, 2, 3, 2);
            battleSummaryTBox.Multiline = true;
            battleSummaryTBox.Name = "battleSummaryTBox";
            battleSummaryTBox.ReadOnly = true;
            battleSummaryTBox.ScrollBars = ScrollBars.Vertical;
            battleSummaryTBox.Size = new Size(391, 154);
            battleSummaryTBox.TabIndex = 5;
            // 
            // BattleScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(BattleStartButt);
            Controls.Add(TurnLabel);
            Controls.Add(ActionButtBox);
            Controls.Add(battleSummaryTBox);
            Controls.Add(ActionMenuGroup);
            Controls.Add(BatleArenaGroupBox);
            Name = "BattleField";
            Size = new Size(731, 587);
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
        private ComboBox targetCBox;
        private Button TargetButt;
        private TextBox textBox1;
        private Label TurnLabel;
        private GroupBox BatleArenaGroupBox;
        private Button BattleStartButt;
        private Button TargetCancelButt;
        private TextBox battleSummaryTBox;
    }
}
