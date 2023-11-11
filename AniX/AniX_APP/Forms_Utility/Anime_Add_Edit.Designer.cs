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
                components?.Dispose();

                // Dispose of image streams
                _coverImageStream?.Dispose();
                _thumbnailImageStream?.Dispose();
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
            btnThumbnail = new CustomElements.RoundButton();
            tbxThumbnail = new RoundTextBox();
            btnBanner = new CustomElements.RoundButton();
            tbxBanner = new RoundTextBox();
            cbxLanguage = new CustomElements.CustomComboBox();
            cbxCountry = new CustomElements.CustomComboBox();
            cbxRating = new CustomElements.CustomComboBox();
            label4 = new Label();
            label3 = new Label();
            dpYear = new CustomElements.CustomDatePicker();
            label2 = new Label();
            dpRelease = new CustomElements.CustomDatePicker();
            label1 = new Label();
            lbThumbnail = new Label();
            lbBanner = new Label();
            lbTrailer = new Label();
            tbxTrailer = new RoundTextBox();
            genreList = new CheckedListBox();
            cbxType = new CustomElements.CustomComboBox();
            lbType = new Label();
            lbCountry = new Label();
            lbSummary = new Label();
            tbxDescription = new RoundTextBox();
            lbSeason = new Label();
            tbxSeason = new RoundTextBox();
            dpAired = new CustomElements.CustomDatePicker();
            lbAired = new Label();
            cbxStatus = new CustomElements.CustomComboBox();
            lbStudio = new Label();
            tbxStudio = new RoundTextBox();
            lbEpisodes = new Label();
            tbxEpisodes = new RoundTextBox();
            cbxPremiered = new CustomElements.CustomComboBox();
            lbPremiered = new Label();
            lbAnime = new Label();
            btnClose = new Button();
            btnSave = new CustomElements.RoundButton();
            lbStatus = new Label();
            lbName = new Label();
            tbxName = new RoundTextBox();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.FromArgb(11, 7, 17);
            panelButtons.Controls.Add(btnThumbnail);
            panelButtons.Controls.Add(tbxThumbnail);
            panelButtons.Controls.Add(btnBanner);
            panelButtons.Controls.Add(tbxBanner);
            panelButtons.Controls.Add(cbxLanguage);
            panelButtons.Controls.Add(cbxCountry);
            panelButtons.Controls.Add(cbxRating);
            panelButtons.Controls.Add(label4);
            panelButtons.Controls.Add(label3);
            panelButtons.Controls.Add(dpYear);
            panelButtons.Controls.Add(label2);
            panelButtons.Controls.Add(dpRelease);
            panelButtons.Controls.Add(label1);
            panelButtons.Controls.Add(lbThumbnail);
            panelButtons.Controls.Add(lbBanner);
            panelButtons.Controls.Add(lbTrailer);
            panelButtons.Controls.Add(tbxTrailer);
            panelButtons.Controls.Add(genreList);
            panelButtons.Controls.Add(cbxType);
            panelButtons.Controls.Add(lbType);
            panelButtons.Controls.Add(lbCountry);
            panelButtons.Controls.Add(lbSummary);
            panelButtons.Controls.Add(tbxDescription);
            panelButtons.Controls.Add(lbSeason);
            panelButtons.Controls.Add(tbxSeason);
            panelButtons.Controls.Add(dpAired);
            panelButtons.Controls.Add(lbAired);
            panelButtons.Controls.Add(cbxStatus);
            panelButtons.Controls.Add(lbStudio);
            panelButtons.Controls.Add(tbxStudio);
            panelButtons.Controls.Add(lbEpisodes);
            panelButtons.Controls.Add(tbxEpisodes);
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
            panelButtons.Size = new Size(1107, 636);
            panelButtons.TabIndex = 5;
            panelButtons.MouseDown += panelTitleBar_MouseDown;
            // 
            // btnThumbnail
            // 
            btnThumbnail.BackColor = Color.FromArgb(11, 7, 17);
            btnThumbnail.BackgroundColor = Color.FromArgb(11, 7, 17);
            btnThumbnail.BorderColor = Color.FromArgb(10, 189, 198);
            btnThumbnail.BorderRadius = 0;
            btnThumbnail.BorderSize = 2;
            btnThumbnail.ClickedColor = Color.FromArgb(231, 34, 83);
            btnThumbnail.FlatAppearance.BorderSize = 0;
            btnThumbnail.FlatStyle = FlatStyle.Flat;
            btnThumbnail.Font = new Font("Cascadia Code", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnThumbnail.ForeColor = Color.FromArgb(10, 189, 198);
            btnThumbnail.Location = new Point(533, 356);
            btnThumbnail.Name = "btnThumbnail";
            btnThumbnail.Size = new Size(248, 33);
            btnThumbnail.TabIndex = 57;
            btnThumbnail.Text = "Select Thumbnail";
            btnThumbnail.TextColor = Color.FromArgb(10, 189, 198);
            btnThumbnail.UseVisualStyleBackColor = false;
            btnThumbnail.Click += btnThumbnail_Click;
            // 
            // tbxThumbnail
            // 
            tbxThumbnail.BackColor = Color.FromArgb(11, 7, 17);
            tbxThumbnail.BorderColor = Color.FromArgb(231, 34, 83);
            tbxThumbnail.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxThumbnail.BorderRadius = 0;
            tbxThumbnail.BorderSize = 2;
            tbxThumbnail.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxThumbnail.ForeColor = Color.FromArgb(231, 34, 83);
            tbxThumbnail.Location = new Point(533, 356);
            tbxThumbnail.Margin = new Padding(4);
            tbxThumbnail.Multiline = false;
            tbxThumbnail.Name = "tbxThumbnail";
            tbxThumbnail.Padding = new Padding(10, 7, 10, 7);
            tbxThumbnail.PasswordChar = false;
            tbxThumbnail.PlaceholderColor = Color.FromArgb(231, 34, 83);
            tbxThumbnail.PlaceholderText = "";
            tbxThumbnail.Size = new Size(247, 33);
            tbxThumbnail.TabIndex = 56;
            tbxThumbnail.Texts = "";
            tbxThumbnail.UnderlinedStyle = false;
            // 
            // btnBanner
            // 
            btnBanner.BackColor = Color.FromArgb(11, 7, 17);
            btnBanner.BackgroundColor = Color.FromArgb(11, 7, 17);
            btnBanner.BorderColor = Color.FromArgb(10, 189, 198);
            btnBanner.BorderRadius = 0;
            btnBanner.BorderSize = 2;
            btnBanner.ClickedColor = Color.FromArgb(231, 34, 83);
            btnBanner.FlatAppearance.BorderSize = 0;
            btnBanner.FlatStyle = FlatStyle.Flat;
            btnBanner.Font = new Font("Cascadia Code", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnBanner.ForeColor = Color.FromArgb(10, 189, 198);
            btnBanner.Location = new Point(533, 315);
            btnBanner.Name = "btnBanner";
            btnBanner.Size = new Size(249, 33);
            btnBanner.TabIndex = 55;
            btnBanner.Text = "Select Banner";
            btnBanner.TextColor = Color.FromArgb(10, 189, 198);
            btnBanner.UseVisualStyleBackColor = false;
            btnBanner.Click += btnBanner_Click;
            // 
            // tbxBanner
            // 
            tbxBanner.BackColor = Color.FromArgb(11, 7, 17);
            tbxBanner.BorderColor = Color.FromArgb(231, 34, 83);
            tbxBanner.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxBanner.BorderRadius = 0;
            tbxBanner.BorderSize = 2;
            tbxBanner.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxBanner.ForeColor = Color.FromArgb(231, 34, 83);
            tbxBanner.Location = new Point(535, 315);
            tbxBanner.Margin = new Padding(4);
            tbxBanner.Multiline = false;
            tbxBanner.Name = "tbxBanner";
            tbxBanner.Padding = new Padding(10, 7, 10, 7);
            tbxBanner.PasswordChar = false;
            tbxBanner.PlaceholderColor = Color.FromArgb(231, 34, 83);
            tbxBanner.PlaceholderText = "";
            tbxBanner.Size = new Size(247, 33);
            tbxBanner.TabIndex = 54;
            tbxBanner.Texts = "";
            tbxBanner.UnderlinedStyle = false;
            // 
            // cbxLanguage
            // 
            cbxLanguage.BackColor = Color.FromArgb(11, 7, 17);
            cbxLanguage.BorderColor = Color.FromArgb(231, 34, 83);
            cbxLanguage.BorderSize = 2;
            cbxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxLanguage.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            cbxLanguage.ForeColor = Color.FromArgb(231, 34, 83);
            cbxLanguage.IconColor = Color.FromArgb(231, 34, 83);
            cbxLanguage.ListBackColor = Color.FromArgb(11, 7, 17);
            cbxLanguage.ListTextColor = Color.FromArgb(231, 34, 83);
            cbxLanguage.Location = new Point(147, 138);
            cbxLanguage.MinimumSize = new Size(200, 30);
            cbxLanguage.Name = "cbxLanguage";
            cbxLanguage.Padding = new Padding(2);
            cbxLanguage.Size = new Size(247, 33);
            cbxLanguage.TabIndex = 51;
            cbxLanguage.Texts = "< language >";
            // 
            // cbxCountry
            // 
            cbxCountry.BackColor = Color.FromArgb(11, 7, 17);
            cbxCountry.BorderColor = Color.FromArgb(231, 34, 83);
            cbxCountry.BorderSize = 2;
            cbxCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxCountry.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            cbxCountry.ForeColor = Color.FromArgb(231, 34, 83);
            cbxCountry.IconColor = Color.FromArgb(231, 34, 83);
            cbxCountry.ListBackColor = Color.FromArgb(11, 7, 17);
            cbxCountry.ListTextColor = Color.FromArgb(231, 34, 83);
            cbxCountry.Location = new Point(147, 94);
            cbxCountry.MinimumSize = new Size(200, 30);
            cbxCountry.Name = "cbxCountry";
            cbxCountry.Padding = new Padding(2);
            cbxCountry.Size = new Size(247, 33);
            cbxCountry.TabIndex = 50;
            cbxCountry.Texts = "< country >";
            // 
            // cbxRating
            // 
            cbxRating.BackColor = Color.FromArgb(11, 7, 17);
            cbxRating.BorderColor = Color.FromArgb(231, 34, 83);
            cbxRating.BorderSize = 2;
            cbxRating.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxRating.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            cbxRating.ForeColor = Color.FromArgb(231, 34, 83);
            cbxRating.IconColor = Color.FromArgb(231, 34, 83);
            cbxRating.ListBackColor = Color.FromArgb(11, 7, 17);
            cbxRating.ListTextColor = Color.FromArgb(231, 34, 83);
            cbxRating.Location = new Point(147, 359);
            cbxRating.MinimumSize = new Size(200, 30);
            cbxRating.Name = "cbxRating";
            cbxRating.Padding = new Padding(2);
            cbxRating.Size = new Size(247, 33);
            cbxRating.TabIndex = 49;
            cbxRating.Texts = "< type >";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.FromArgb(231, 34, 83);
            label4.Location = new Point(38, 362);
            label4.Name = "label4";
            label4.Size = new Size(73, 21);
            label4.TabIndex = 48;
            label4.Text = "Rating:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.FromArgb(231, 34, 83);
            label3.Location = new Point(38, 318);
            label3.Name = "label3";
            label3.Size = new Size(55, 21);
            label3.TabIndex = 47;
            label3.Text = "Year:";
            // 
            // dpYear
            // 
            dpYear.BorderColor = Color.FromArgb(231, 34, 83);
            dpYear.BorderSize = 2;
            dpYear.CustomFormat = "yyy";
            dpYear.Font = new Font("Cascadia Code", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dpYear.Format = DateTimePickerFormat.Custom;
            dpYear.Location = new Point(147, 314);
            dpYear.MaxDate = new DateTime(2030, 12, 31, 0, 0, 0, 0);
            dpYear.MinDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dpYear.MinimumSize = new Size(0, 35);
            dpYear.Name = "dpYear";
            dpYear.RightToLeft = RightToLeft.Yes;
            dpYear.Size = new Size(247, 35);
            dpYear.SkinColor = Color.FromArgb(11, 7, 17);
            dpYear.TabIndex = 46;
            dpYear.TextColor = Color.FromArgb(231, 34, 83);
            dpYear.Value = new DateTime(2023, 11, 7, 0, 0, 0, 0);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(231, 34, 83);
            label2.Location = new Point(38, 141);
            label2.Name = "label2";
            label2.Size = new Size(91, 21);
            label2.TabIndex = 45;
            label2.Text = "Language:";
            // 
            // dpRelease
            // 
            dpRelease.BorderColor = Color.FromArgb(231, 34, 83);
            dpRelease.BorderSize = 2;
            dpRelease.CustomFormat = "MMM dd, yyy";
            dpRelease.Font = new Font("Cascadia Code", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dpRelease.Format = DateTimePickerFormat.Custom;
            dpRelease.Location = new Point(535, 227);
            dpRelease.MaxDate = new DateTime(2030, 12, 31, 0, 0, 0, 0);
            dpRelease.MinDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dpRelease.MinimumSize = new Size(0, 35);
            dpRelease.Name = "dpRelease";
            dpRelease.RightToLeft = RightToLeft.Yes;
            dpRelease.Size = new Size(247, 35);
            dpRelease.SkinColor = Color.FromArgb(11, 7, 17);
            dpRelease.TabIndex = 43;
            dpRelease.TextColor = Color.FromArgb(231, 34, 83);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(231, 34, 83);
            label1.Location = new Point(426, 235);
            label1.Name = "label1";
            label1.Size = new Size(82, 21);
            label1.TabIndex = 42;
            label1.Text = "Release:";
            // 
            // lbThumbnail
            // 
            lbThumbnail.AutoSize = true;
            lbThumbnail.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbThumbnail.ForeColor = Color.FromArgb(231, 34, 83);
            lbThumbnail.Location = new Point(426, 361);
            lbThumbnail.Name = "lbThumbnail";
            lbThumbnail.Size = new Size(100, 21);
            lbThumbnail.TabIndex = 41;
            lbThumbnail.Text = "Thumbnail:";
            // 
            // lbBanner
            // 
            lbBanner.AutoSize = true;
            lbBanner.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbBanner.ForeColor = Color.FromArgb(231, 34, 83);
            lbBanner.Location = new Point(426, 318);
            lbBanner.Name = "lbBanner";
            lbBanner.Size = new Size(73, 21);
            lbBanner.TabIndex = 39;
            lbBanner.Text = "Banner:";
            // 
            // lbTrailer
            // 
            lbTrailer.AutoSize = true;
            lbTrailer.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbTrailer.ForeColor = Color.FromArgb(231, 34, 83);
            lbTrailer.Location = new Point(426, 274);
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
            tbxTrailer.Location = new Point(535, 272);
            tbxTrailer.Margin = new Padding(4);
            tbxTrailer.Multiline = false;
            tbxTrailer.Name = "tbxTrailer";
            tbxTrailer.Padding = new Padding(10, 7, 10, 7);
            tbxTrailer.PasswordChar = false;
            tbxTrailer.PlaceholderColor = Color.FromArgb(231, 34, 83);
            tbxTrailer.PlaceholderText = "";
            tbxTrailer.Size = new Size(247, 33);
            tbxTrailer.TabIndex = 36;
            tbxTrailer.Texts = "";
            tbxTrailer.UnderlinedStyle = false;
            // 
            // genreList
            // 
            genreList.BackColor = Color.FromArgb(11, 7, 17);
            genreList.BorderStyle = BorderStyle.None;
            genreList.ColumnWidth = 140;
            genreList.Font = new Font("Cascadia Code", 10F, FontStyle.Regular, GraphicsUnit.Point);
            genreList.ForeColor = Color.FromArgb(231, 34, 83);
            genreList.FormattingEnabled = true;
            genreList.Location = new Point(814, 50);
            genreList.MultiColumn = true;
            genreList.Name = "genreList";
            genreList.RightToLeft = RightToLeft.No;
            genreList.Size = new Size(291, 486);
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
            cbxType.Location = new Point(535, 50);
            cbxType.MinimumSize = new Size(200, 30);
            cbxType.Name = "cbxType";
            cbxType.Padding = new Padding(2);
            cbxType.Size = new Size(247, 33);
            cbxType.TabIndex = 34;
            cbxType.Texts = "< type >";
            cbxType.Load += Anime_Add_Edit_Load;
            // 
            // lbType
            // 
            lbType.AutoSize = true;
            lbType.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbType.ForeColor = Color.FromArgb(231, 34, 83);
            lbType.Location = new Point(426, 53);
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
            // lbSummary
            // 
            lbSummary.AutoSize = true;
            lbSummary.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbSummary.ForeColor = Color.FromArgb(231, 34, 83);
            lbSummary.Location = new Point(38, 406);
            lbSummary.Name = "lbSummary";
            lbSummary.Size = new Size(82, 21);
            lbSummary.TabIndex = 30;
            lbSummary.Text = "Summary:";
            // 
            // tbxDescription
            // 
            tbxDescription.BackColor = Color.FromArgb(231, 34, 83);
            tbxDescription.BorderColor = Color.FromArgb(231, 34, 83);
            tbxDescription.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxDescription.BorderRadius = 10;
            tbxDescription.BorderSize = 2;
            tbxDescription.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxDescription.ForeColor = Color.FromArgb(11, 7, 17);
            tbxDescription.Location = new Point(147, 406);
            tbxDescription.Margin = new Padding(4);
            tbxDescription.Multiline = true;
            tbxDescription.Name = "tbxDescription";
            tbxDescription.Padding = new Padding(10, 7, 10, 7);
            tbxDescription.PasswordChar = false;
            tbxDescription.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxDescription.PlaceholderText = "";
            tbxDescription.Size = new Size(635, 149);
            tbxDescription.TabIndex = 29;
            tbxDescription.Texts = "";
            tbxDescription.UnderlinedStyle = false;
            // 
            // lbSeason
            // 
            lbSeason.AutoSize = true;
            lbSeason.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbSeason.ForeColor = Color.FromArgb(231, 34, 83);
            lbSeason.Location = new Point(38, 182);
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
            tbxSeason.Location = new Point(144, 181);
            tbxSeason.Margin = new Padding(4);
            tbxSeason.Multiline = false;
            tbxSeason.Name = "tbxSeason";
            tbxSeason.Padding = new Padding(10, 7, 10, 7);
            tbxSeason.PasswordChar = false;
            tbxSeason.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxSeason.PlaceholderText = "";
            tbxSeason.Size = new Size(250, 33);
            tbxSeason.TabIndex = 27;
            tbxSeason.Texts = "";
            tbxSeason.UnderlinedStyle = false;
            // 
            // dpAired
            // 
            dpAired.BorderColor = Color.FromArgb(231, 34, 83);
            dpAired.BorderSize = 2;
            dpAired.CustomFormat = "MMM dd, yyy";
            dpAired.Font = new Font("Cascadia Code", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dpAired.Format = DateTimePickerFormat.Custom;
            dpAired.Location = new Point(535, 182);
            dpAired.MaxDate = new DateTime(2030, 12, 31, 0, 0, 0, 0);
            dpAired.MinDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dpAired.MinimumSize = new Size(0, 35);
            dpAired.Name = "dpAired";
            dpAired.RightToLeft = RightToLeft.Yes;
            dpAired.Size = new Size(247, 35);
            dpAired.SkinColor = Color.FromArgb(11, 7, 17);
            dpAired.TabIndex = 26;
            dpAired.TextColor = Color.FromArgb(231, 34, 83);
            // 
            // lbAired
            // 
            lbAired.AutoSize = true;
            lbAired.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbAired.ForeColor = Color.FromArgb(231, 34, 83);
            lbAired.Location = new Point(426, 190);
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
            cbxStatus.Location = new Point(535, 94);
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
            lbStudio.Location = new Point(38, 269);
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
            tbxStudio.Location = new Point(144, 268);
            tbxStudio.Margin = new Padding(4);
            tbxStudio.Multiline = false;
            tbxStudio.Name = "tbxStudio";
            tbxStudio.Padding = new Padding(10, 7, 10, 7);
            tbxStudio.PasswordChar = false;
            tbxStudio.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxStudio.PlaceholderText = "";
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
            lbEpisodes.Location = new Point(38, 225);
            lbEpisodes.Name = "lbEpisodes";
            lbEpisodes.Size = new Size(91, 21);
            lbEpisodes.TabIndex = 19;
            lbEpisodes.Text = "Episodes:";
            // 
            // tbxEpisodes
            // 
            tbxEpisodes.BackColor = Color.FromArgb(231, 34, 83);
            tbxEpisodes.BorderColor = Color.FromArgb(231, 34, 83);
            tbxEpisodes.BorderFocusColor = Color.FromArgb(231, 34, 83);
            tbxEpisodes.BorderRadius = 14;
            tbxEpisodes.BorderSize = 2;
            tbxEpisodes.Font = new Font("Cascadia Code", 10F, FontStyle.Bold, GraphicsUnit.Point);
            tbxEpisodes.ForeColor = Color.FromArgb(11, 7, 17);
            tbxEpisodes.Location = new Point(144, 224);
            tbxEpisodes.Margin = new Padding(4);
            tbxEpisodes.Multiline = false;
            tbxEpisodes.Name = "tbxEpisodes";
            tbxEpisodes.Padding = new Padding(10, 7, 10, 7);
            tbxEpisodes.PasswordChar = false;
            tbxEpisodes.PlaceholderColor = Color.FromArgb(11, 7, 17);
            tbxEpisodes.PlaceholderText = "";
            tbxEpisodes.Size = new Size(250, 33);
            tbxEpisodes.TabIndex = 18;
            tbxEpisodes.Texts = "";
            tbxEpisodes.UnderlinedStyle = false;
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
            cbxPremiered.Location = new Point(535, 138);
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
            lbPremiered.Location = new Point(426, 144);
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
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 34, 83);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Microsoft Sans Serif", 13F, FontStyle.Regular, GraphicsUnit.Point);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(1060, 0);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(47, 38);
            btnClose.TabIndex = 13;
            btnClose.Text = "X";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
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
            btnSave.Location = new Point(41, 573);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(1032, 43);
            btnSave.TabIndex = 12;
            btnSave.Text = "Save";
            btnSave.TextColor = Color.FromArgb(11, 7, 17);
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbStatus.ForeColor = Color.FromArgb(231, 34, 83);
            lbStatus.Location = new Point(426, 100);
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
            tbxName.PlaceholderText = "";
            tbxName.Size = new Size(250, 33);
            tbxName.TabIndex = 4;
            tbxName.Texts = "";
            tbxName.UnderlinedStyle = false;
            // 
            // Anime_Add_Edit
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(231, 34, 83);
            ClientSize = new Size(1113, 641);
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
        private RoundTextBox tbxEpisodes;
        private Label lbAired;
        private CustomElements.CustomDatePicker dpAired;
        private Label lbSeason;
        private RoundTextBox tbxSeason;
        private Label lbSummary;
        private RoundTextBox tbxDescription;
        private Label lbCountry;
        private CheckedListBox genreList;
        private CustomElements.CustomComboBox cbxType;
        private Label lbType;
        private Label lbTrailer;
        private RoundTextBox tbxTrailer;
        private Label lbBanner;
        private Label lbThumbnail;
        private Label label2;
        private CustomElements.CustomDatePicker dpRelease;
        private Label label1;
        private CustomElements.CustomComboBox cbxRating;
        private Label label4;
        private Label label3;
        private CustomElements.CustomDatePicker dpYear;
        private CustomElements.CustomComboBox cbxLanguage;
        private CustomElements.CustomComboBox cbxCountry;
        private CustomElements.RoundButton btnBanner;
        private RoundTextBox tbxBanner;
        private CustomElements.RoundButton btnThumbnail;
        private RoundTextBox tbxThumbnail;
    }
}
