using housing.CustomElements;

namespace AniX_APP.Forms_Utility
{
    partial class Anime_Add_Edit
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
            panelButtons = new Panel();
            genreList = new CheckedListBox();
            cbxType = new CustomElements.CustomComboBox();
            lbType = new Label();
            lbCountry = new Label();
            tbxCountry = new RoundTextBox();
            lbSummary = new Label();
            tbxSummary = new RoundTextBox();
            lbSeason = new Label();
            tbxSeason = new RoundTextBox();
            cbxAired = new CustomElements.CustomDatePicker();
            lbAired = new Label();
            cbxStatus = new CustomElements.CustomComboBox();
            lbStudio = new Label();
            tbxStudio = new RoundTextBox();
            lbEpisodes = new Label();
            tbxEpisode = new RoundTextBox();
            cbxPremiered = new CustomElements.CustomComboBox();
            lbPremiered = new Label();
            lbAnime = new Label();
            btnClose = new Button();
            btnSave = new CustomElements.RoundButton();
            lbStatus = new Label();
            lbName = new Label();
            tbxName = new RoundTextBox();
            lbTrailer = new Label();
            tbxTrailer = new RoundTextBox();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.FromArgb(11, 7, 17);
            panelButtons.Controls.Add(lbTrailer);
            panelButtons.Controls.Add(tbxTrailer);
            panelButtons.Controls.Add(genreList);
            panelButtons.Controls.Add(cbxType);
            panelButtons.Controls.Add(lbType);
            panelButtons.Controls.Add(lbCountry);
            panelButtons.Controls.Add(tbxCountry);
            panelButtons.Controls.Add(lbSummary);
            panelButtons.Controls.Add(tbxSummary);
            panelButtons.Controls.Add(lbSeason);
            panelButtons.Controls.Add(tbxSeason);
            panelButtons.Controls.Add(cbxAired);
            panelButtons.Controls.Add(lbAired);
            panelButtons.Controls.Add(cbxStatus);
            panelButtons.Controls.Add(lbStudio);
            panelButtons.Controls.Add(tbxStudio);
            panelButtons.Controls.Add(lbEpisodes);
            panelButtons.Controls.Add(tbxEpisode);
            panelButtons.Controls.Add(cbxPremiered);
            panelButtons.Controls.Add(lbPremiered);
            panelButtons.Controls.Add(lbAnime);
            panelButtons.Controls.Add(btnClose);
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(lbStatus);
            panelButtons.Controls.Add(lbName);
            panelButtons.Controls.Add(tbxName);
            panelButtons.Font = new Font("Cascadia Code", 10F, FontStyle.Regular, GraphicsUnit.Point);
            panelButtons.Location = new Point(3, 3);
            panelButtons.Margin = new Padding(4, 3, 4, 3);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(1107, 504);
            panelButtons.TabIndex = 5;
            // 
            // genreList
            // 
            genreList.BackColor = Color.FromArgb(11, 7, 17);
            genreList.BorderStyle = BorderStyle.None;
            genreList.ColumnWidth = 140;
            genreList.Font = new Font("Cascadia Code", 9F, FontStyle.Regular, GraphicsUnit.Point);
            genreList.ForeColor = Color.FromArgb(231, 34, 83);
            genreList.FormattingEnabled = true;
            genreList.Items.AddRange(new object[] { "Action", "Adventure", "Avant Grade", "Boys Love", "Comedy", "Demons", "Drama", "Ecchi", "Fantasy", "Girls Love", "Gourmet", "Harem", "Horror", "Isekai", "Iyashikei", "Josei", "Kids", "Magic", "Mahou Shoujo", "Martial Arts", "Mecha", "Military", "Music", "Mystery", "Parody", "Psychological", "Reverse Harem", "Romance", "School", "Sci-Fi", "Seinen", "Shoujo", "Shounen", "Slice Of Life", "Space", "Sports", "Super Power", "Supernatural", "Suspense", "Thriller", "Vampire" });
            genreList.Location = new Point(815, 50);
            genreList.MultiColumn = true;
            genreList.Name = "genreList";
            genreList.RightToLeft = RightToLeft.No;
            genreList.Size = new Size(291, 352);
            genreList.TabIndex = 35;
            // 
            // cbxType
            // 
            cbxType.BackColor = Color.FromArgb(11, 7, 17);
            cbxType.BorderColor = Color.FromArgb(231, 34, 83);
            cbxType.BorderSize = 2;
            cbxType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxType.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            cbxType.ForeColor = Color.FromArgb(231, 34, 83);
            cbxType.IconColor = Color.FromArgb(231, 34, 83);
            cbxType.ListBackColor = Color.FromArgb(11, 7, 17);
            cbxType.ListTextColor = Color.FromArgb(231, 34, 83);
            cbxType.Location = new Point(532, 50);
            cbxType.MinimumSize = new Size(200, 30);
            cbxType.Name = "cbxType";
            cbxType.Padding = new Padding(2);
            cbxType.Size = new Size(247, 33);
            cbxType.TabIndex = 34;
            cbxType.Texts = "< type >";
            // 
            // lbType
            // 
            lbType.AutoSize = true;
            lbType.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbType.ForeColor = Color.FromArgb(231, 34, 83);
            lbType.Location = new Point(423, 56);
            lbType.Name = "lbType";
            lbType.Size = new Size(55, 21);
            lbType.TabIndex = 33;
            lbType.Text = "Type:";
            // 
            // lbCountry
            // 
            lbCountry.AutoSize = true;
            lbCountry.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbCountry.ForeColor = Color.FromArgb(231, 34, 83);
            lbCountry.Location = new Point(38, 100);
            lbCountry.Name = "lbCountry";
            lbCountry.Size = new Size(82, 21);
            lbCountry.TabIndex = 32;
            lbCountry.Text = "Country:";
            // 
            // tbxCountry
            // 
            tbxCountry.BackColor = Color.FromArgb(231, 34, 83);
            tbxCountry.BorderColor = Color.FromArgb(231, 34, 83);
            tbxCountry.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxCountry.BorderRadius = 14;
            tbxCountry.BorderSize = 2;
            tbxCountry.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxCountry.ForeColor = Color.FromArgb(11, 7, 17);
            tbxCountry.Location = new Point(144, 94);
            tbxCountry.Margin = new Padding(4);
            tbxCountry.Multiline = false;
            tbxCountry.Name = "tbxCountry";
            tbxCountry.Padding = new Padding(10, 7, 10, 7);
            tbxCountry.PasswordChar = false;
            tbxCountry.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxCountry.PlaceholderText = "< country name >";
            tbxCountry.Size = new Size(250, 33);
            tbxCountry.TabIndex = 31;
            tbxCountry.Texts = "";
            tbxCountry.UnderlinedStyle = false;
            // 
            // lbSummary
            // 
            lbSummary.AutoSize = true;
            lbSummary.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbSummary.ForeColor = Color.FromArgb(231, 34, 83);
            lbSummary.Location = new Point(38, 270);
            lbSummary.Name = "lbSummary";
            lbSummary.Size = new Size(82, 21);
            lbSummary.TabIndex = 30;
            lbSummary.Text = "Summary:";
            // 
            // tbxSummary
            // 
            tbxSummary.BackColor = Color.FromArgb(231, 34, 83);
            tbxSummary.BorderColor = Color.FromArgb(231, 34, 83);
            tbxSummary.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxSummary.BorderRadius = 10;
            tbxSummary.BorderSize = 2;
            tbxSummary.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxSummary.ForeColor = Color.FromArgb(11, 7, 17);
            tbxSummary.Location = new Point(144, 270);
            tbxSummary.Margin = new Padding(4);
            tbxSummary.Multiline = true;
            tbxSummary.Name = "tbxSummary";
            tbxSummary.Padding = new Padding(10, 7, 10, 7);
            tbxSummary.PasswordChar = false;
            tbxSummary.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxSummary.PlaceholderText = "";
            tbxSummary.Size = new Size(635, 149);
            tbxSummary.TabIndex = 29;
            tbxSummary.Texts = "";
            tbxSummary.UnderlinedStyle = false;
            // 
            // lbSeason
            // 
            lbSeason.AutoSize = true;
            lbSeason.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbSeason.ForeColor = Color.FromArgb(231, 34, 83);
            lbSeason.Location = new Point(38, 144);
            lbSeason.Name = "lbSeason";
            lbSeason.Size = new Size(73, 21);
            lbSeason.TabIndex = 28;
            lbSeason.Text = "Season:";
            // 
            // tbxSeason
            // 
            tbxSeason.BackColor = Color.FromArgb(231, 34, 83);
            tbxSeason.BorderColor = Color.FromArgb(231, 34, 83);
            tbxSeason.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxSeason.BorderRadius = 14;
            tbxSeason.BorderSize = 2;
            tbxSeason.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxSeason.ForeColor = Color.FromArgb(11, 7, 17);
            tbxSeason.Location = new Point(144, 138);
            tbxSeason.Margin = new Padding(4);
            tbxSeason.Multiline = false;
            tbxSeason.Name = "tbxSeason";
            tbxSeason.Padding = new Padding(10, 7, 10, 7);
            tbxSeason.PasswordChar = false;
            tbxSeason.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxSeason.PlaceholderText = "< no. of season/s >";
            tbxSeason.Size = new Size(250, 33);
            tbxSeason.TabIndex = 27;
            tbxSeason.Texts = "";
            tbxSeason.UnderlinedStyle = false;
            // 
            // cbxAired
            // 
            cbxAired.BorderColor = Color.FromArgb(231, 34, 83);
            cbxAired.BorderSize = 2;
            cbxAired.CustomFormat = "MMM dd, yyy";
            cbxAired.Font = new Font("Cascadia Code", 9F, FontStyle.Bold, GraphicsUnit.Point);
            cbxAired.Format = DateTimePickerFormat.Custom;
            cbxAired.Location = new Point(532, 182);
            cbxAired.MaxDate = new DateTime(2024, 12, 31, 0, 0, 0, 0);
            cbxAired.MinDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            cbxAired.MinimumSize = new Size(0, 35);
            cbxAired.Name = "cbxAired";
            cbxAired.RightToLeft = RightToLeft.Yes;
            cbxAired.Size = new Size(247, 35);
            cbxAired.SkinColor = Color.FromArgb(11, 7, 17);
            cbxAired.TabIndex = 26;
            cbxAired.TextColor = Color.FromArgb(231, 34, 83);
            // 
            // lbAired
            // 
            lbAired.AutoSize = true;
            lbAired.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbAired.ForeColor = Color.FromArgb(231, 34, 83);
            lbAired.Location = new Point(423, 190);
            lbAired.Name = "lbAired";
            lbAired.Size = new Size(64, 21);
            lbAired.TabIndex = 25;
            lbAired.Text = "Aired:";
            // 
            // cbxStatus
            // 
            cbxStatus.BackColor = Color.FromArgb(11, 7, 17);
            cbxStatus.BorderColor = Color.FromArgb(231, 34, 83);
            cbxStatus.BorderSize = 2;
            cbxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxStatus.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            cbxStatus.ForeColor = Color.FromArgb(231, 34, 83);
            cbxStatus.IconColor = Color.FromArgb(231, 34, 83);
            cbxStatus.ListBackColor = Color.FromArgb(11, 7, 17);
            cbxStatus.ListTextColor = Color.FromArgb(231, 34, 83);
            cbxStatus.Location = new Point(532, 94);
            cbxStatus.MinimumSize = new Size(200, 30);
            cbxStatus.Name = "cbxStatus";
            cbxStatus.Padding = new Padding(2);
            cbxStatus.Size = new Size(247, 33);
            cbxStatus.TabIndex = 22;
            cbxStatus.Texts = "< status >";
            // 
            // lbStudio
            // 
            lbStudio.AutoSize = true;
            lbStudio.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbStudio.ForeColor = Color.FromArgb(231, 34, 83);
            lbStudio.Location = new Point(38, 229);
            lbStudio.Name = "lbStudio";
            lbStudio.Size = new Size(73, 21);
            lbStudio.TabIndex = 21;
            lbStudio.Text = "Studio:";
            // 
            // tbxStudio
            // 
            tbxStudio.BackColor = Color.FromArgb(231, 34, 83);
            tbxStudio.BorderColor = Color.FromArgb(231, 34, 83);
            tbxStudio.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxStudio.BorderRadius = 14;
            tbxStudio.BorderSize = 2;
            tbxStudio.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxStudio.ForeColor = Color.FromArgb(11, 7, 17);
            tbxStudio.Location = new Point(144, 223);
            tbxStudio.Margin = new Padding(4);
            tbxStudio.Multiline = false;
            tbxStudio.Name = "tbxStudio";
            tbxStudio.Padding = new Padding(10, 7, 10, 7);
            tbxStudio.PasswordChar = false;
            tbxStudio.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxStudio.PlaceholderText = "< studio name >";
            tbxStudio.Size = new Size(250, 33);
            tbxStudio.TabIndex = 20;
            tbxStudio.Texts = "";
            tbxStudio.UnderlinedStyle = false;
            // 
            // lbEpisodes
            // 
            lbEpisodes.AutoSize = true;
            lbEpisodes.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbEpisodes.ForeColor = Color.FromArgb(231, 34, 83);
            lbEpisodes.Location = new Point(38, 187);
            lbEpisodes.Name = "lbEpisodes";
            lbEpisodes.Size = new Size(91, 21);
            lbEpisodes.TabIndex = 19;
            lbEpisodes.Text = "Episodes:";
            // 
            // tbxEpisode
            // 
            tbxEpisode.BackColor = Color.FromArgb(231, 34, 83);
            tbxEpisode.BorderColor = Color.FromArgb(231, 34, 83);
            tbxEpisode.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxEpisode.BorderRadius = 14;
            tbxEpisode.BorderSize = 2;
            tbxEpisode.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxEpisode.ForeColor = Color.FromArgb(11, 7, 17);
            tbxEpisode.Location = new Point(144, 181);
            tbxEpisode.Margin = new Padding(4);
            tbxEpisode.Multiline = false;
            tbxEpisode.Name = "tbxEpisode";
            tbxEpisode.Padding = new Padding(10, 7, 10, 7);
            tbxEpisode.PasswordChar = false;
            tbxEpisode.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxEpisode.PlaceholderText = "< no. of episodes >";
            tbxEpisode.Size = new Size(250, 33);
            tbxEpisode.TabIndex = 18;
            tbxEpisode.Texts = "";
            tbxEpisode.UnderlinedStyle = false;
            // 
            // cbxPremiered
            // 
            cbxPremiered.BackColor = Color.FromArgb(11, 7, 17);
            cbxPremiered.BorderColor = Color.FromArgb(231, 34, 83);
            cbxPremiered.BorderSize = 2;
            cbxPremiered.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxPremiered.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            cbxPremiered.ForeColor = Color.FromArgb(231, 34, 83);
            cbxPremiered.IconColor = Color.FromArgb(231, 34, 83);
            cbxPremiered.ListBackColor = Color.FromArgb(11, 7, 17);
            cbxPremiered.ListTextColor = Color.FromArgb(231, 34, 83);
            cbxPremiered.Location = new Point(532, 138);
            cbxPremiered.MinimumSize = new Size(200, 30);
            cbxPremiered.Name = "cbxPremiered";
            cbxPremiered.Padding = new Padding(2);
            cbxPremiered.Size = new Size(247, 33);
            cbxPremiered.TabIndex = 17;
            cbxPremiered.Texts = "< period >";
            // 
            // lbPremiered
            // 
            lbPremiered.AutoSize = true;
            lbPremiered.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbPremiered.ForeColor = Color.FromArgb(231, 34, 83);
            lbPremiered.Location = new Point(423, 144);
            lbPremiered.Name = "lbPremiered";
            lbPremiered.Size = new Size(100, 21);
            lbPremiered.TabIndex = 16;
            lbPremiered.Text = "Premiered:";
            // 
            // lbAnime
            // 
            lbAnime.AutoSize = true;
            lbAnime.Font = new Font("Cascadia Code", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lbAnime.ForeColor = Color.FromArgb(231, 34, 83);
            lbAnime.Location = new Point(8, 12);
            lbAnime.Margin = new Padding(4, 0, 4, 0);
            lbAnime.Name = "lbAnime";
            lbAnime.Size = new Size(84, 16);
            lbAnime.TabIndex = 14;
            lbAnime.Text = "Anime stuff";
            // 
            // btnClose
            // 
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(224, 79, 95);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(1058, 1);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(47, 38);
            btnClose.TabIndex = 13;
            btnClose.Text = "X";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(231, 34, 83);
            btnSave.BackgroundColor = Color.FromArgb(231, 34, 83);
            btnSave.BorderColor = Color.FromArgb(231, 34, 83);
            btnSave.BorderRadius = 14;
            btnSave.BorderSize = 2;
            btnSave.ClickedColor = Color.FromArgb(231, 34, 83);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Cascadia Code", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnSave.ForeColor = Color.FromArgb(11, 7, 17);
            btnSave.Location = new Point(38, 439);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(1033, 43);
            btnSave.TabIndex = 12;
            btnSave.Text = "Save";
            btnSave.TextColor = Color.FromArgb(11, 7, 17);
            btnSave.UseVisualStyleBackColor = false;
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbStatus.ForeColor = Color.FromArgb(231, 34, 83);
            lbStatus.Location = new Point(423, 100);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new Size(73, 21);
            lbStatus.TabIndex = 9;
            lbStatus.Text = "Status:";
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbName.ForeColor = Color.FromArgb(231, 34, 83);
            lbName.Location = new Point(38, 56);
            lbName.Name = "lbName";
            lbName.Size = new Size(55, 21);
            lbName.TabIndex = 5;
            lbName.Text = "Name:";
            // 
            // tbxName
            // 
            tbxName.BackColor = Color.FromArgb(231, 34, 83);
            tbxName.BorderColor = Color.FromArgb(231, 34, 83);
            tbxName.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxName.BorderRadius = 14;
            tbxName.BorderSize = 2;
            tbxName.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxName.ForeColor = Color.FromArgb(11, 7, 17);
            tbxName.Location = new Point(144, 50);
            tbxName.Margin = new Padding(4);
            tbxName.Multiline = false;
            tbxName.Name = "tbxName";
            tbxName.Padding = new Padding(10, 7, 10, 7);
            tbxName.PasswordChar = false;
            tbxName.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxName.PlaceholderText = "< anime name >";
            tbxName.Size = new Size(250, 33);
            tbxName.TabIndex = 4;
            tbxName.Texts = "";
            tbxName.UnderlinedStyle = false;
            // 
            // lbTrailer
            // 
            lbTrailer.AutoSize = true;
            lbTrailer.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbTrailer.ForeColor = Color.FromArgb(231, 34, 83);
            lbTrailer.Location = new Point(423, 229);
            lbTrailer.Name = "lbTrailer";
            lbTrailer.Size = new Size(82, 21);
            lbTrailer.TabIndex = 37;
            lbTrailer.Text = "Trailer:";
            // 
            // tbxTrailer
            // 
            tbxTrailer.BackColor = Color.FromArgb(11, 7, 17);
            tbxTrailer.BorderColor = Color.FromArgb(231, 34, 83);
            tbxTrailer.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxTrailer.BorderRadius = 0;
            tbxTrailer.BorderSize = 2;
            tbxTrailer.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxTrailer.ForeColor = Color.FromArgb(231, 34, 83);
            tbxTrailer.Location = new Point(532, 227);
            tbxTrailer.Margin = new Padding(4);
            tbxTrailer.Multiline = false;
            tbxTrailer.Name = "tbxTrailer";
            tbxTrailer.Padding = new Padding(10, 7, 10, 7);
            tbxTrailer.PasswordChar = false;
            tbxTrailer.PlaceholderColor = Color.FromArgb(231, 34, 83);
            tbxTrailer.PlaceholderText = "< trailer link >";
            tbxTrailer.Size = new Size(247, 33);
            tbxTrailer.TabIndex = 36;
            tbxTrailer.Texts = "";
            tbxTrailer.UnderlinedStyle = false;
            // 
            // Anime_Add_Edit
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(231, 34, 83);
            ClientSize = new Size(1113, 510);
            Controls.Add(panelButtons);
            Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            ForeColor = Color.Black;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Anime_Add_Edit";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Anime_Add_Edit";
            panelButtons.ResumeLayout(false);
            panelButtons.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelButtons;
        private Label lbAnime;
        private Button btnClose;
        private CustomElements.RoundButton btnSave;
        private Label lbStatus;
        private Label lbName;
        private RoundTextBox tbxName;
        private Label lbPremiered;
        private CustomElements.CustomComboBox cbxPremiered;
        private CustomElements.CustomComboBox cbxStatus;
        private Label lbStudio;
        private RoundTextBox tbxStudio;
        private Label lbEpisodes;
        private RoundTextBox tbxEpisode;
        private Label lbAired;
        private CustomElements.CustomDatePicker cbxAired;
        private Label lbSeason;
        private RoundTextBox tbxSeason;
        private Label lbSummary;
        private RoundTextBox tbxSummary;
        private Label lbCountry;
        private RoundTextBox tbxCountry;
        private CheckedListBox genreList;
        private CustomElements.CustomComboBox cbxType;
        private Label lbType;
        private Label lbTrailer;
        private RoundTextBox tbxTrailer;
    }
}
