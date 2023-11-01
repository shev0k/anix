namespace AniX_APP.Forms
{
    partial class Overview
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Overview));
            panelTop = new Panel();
            btnClose = new Button();
            roundPanel1 = new CustomElements.RoundPanel();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lbInfo = new Label();
            lbTitle = new Label();
            panelTop.SuspendLayout();
            roundPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(11, 7, 17);
            panelTop.Controls.Add(btnClose);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1041, 53);
            panelTop.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseDownBackColor = Color.FromArgb(23, 21, 32);
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(24, 22, 34);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.ForeColor = Color.Silver;
            btnClose.Image = Properties.Resources.logout;
            btnClose.ImageAlign = ContentAlignment.MiddleLeft;
            btnClose.Location = new Point(933, 0);
            btnClose.Name = "btnClose";
            btnClose.Padding = new Padding(5, 0, 0, 0);
            btnClose.Size = new Size(108, 53);
            btnClose.TabIndex = 14;
            btnClose.Text = "  Close";
            btnClose.TextAlign = ContentAlignment.MiddleLeft;
            btnClose.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnInformation_Click;
            // 
            // roundPanel1
            // 
            roundPanel1.BorderColor = Color.White;
            roundPanel1.BorderWidth = 5;
            roundPanel1.Controls.Add(label3);
            roundPanel1.Controls.Add(label2);
            roundPanel1.Controls.Add(label1);
            roundPanel1.Controls.Add(lbInfo);
            roundPanel1.Controls.Add(lbTitle);
            roundPanel1.FillColor = Color.FromArgb(11, 7, 17);
            roundPanel1.IsBorder = false;
            roundPanel1.IsFill = true;
            roundPanel1.Location = new Point(46, 93);
            roundPanel1.Name = "roundPanel1";
            roundPanel1.Radius = 20;
            roundPanel1.Size = new Size(946, 442);
            roundPanel1.TabIndex = 1;
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Cascadia Mono", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(29, 261);
            label3.Name = "label3";
            label3.Size = new Size(886, 118);
            label3.TabIndex = 4;
            label3.Text = resources.GetString("label3.Text");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Cascadia Mono", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(231, 34, 83);
            label2.Location = new Point(29, 215);
            label2.Name = "label2";
            label2.Size = new Size(126, 32);
            label2.TabIndex = 3;
            label2.Text = "Features";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Cascadia Mono", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(231, 34, 83);
            label1.Location = new Point(27, 92);
            label1.Name = "label1";
            label1.Size = new Size(126, 32);
            label1.TabIndex = 2;
            label1.Text = "Overview";
            // 
            // lbInfo
            // 
            lbInfo.BackColor = Color.Transparent;
            lbInfo.Font = new Font("Cascadia Mono", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbInfo.ForeColor = Color.White;
            lbInfo.Location = new Point(29, 137);
            lbInfo.Name = "lbInfo";
            lbInfo.Size = new Size(886, 67);
            lbInfo.TabIndex = 1;
            lbInfo.Text = resources.GetString("lbInfo.Text");
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.BackColor = Color.Transparent;
            lbTitle.Font = new Font("Cascadia Mono", 22F, FontStyle.Bold, GraphicsUnit.Point);
            lbTitle.ForeColor = Color.FromArgb(231, 34, 83);
            lbTitle.Location = new Point(29, 25);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(89, 40);
            lbTitle.TabIndex = 0;
            lbTitle.Text = "AniX";
            // 
            // Overview
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(231, 34, 83);
            ClientSize = new Size(1041, 573);
            Controls.Add(roundPanel1);
            Controls.Add(panelTop);
            Font = new Font("Cascadia Code", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Overview";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            panelTop.ResumeLayout(false);
            roundPanel1.ResumeLayout(false);
            roundPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Button btnClose;
        private CustomElements.RoundPanel roundPanel1;
        private Label lbInfo;
        private Label lbTitle;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}