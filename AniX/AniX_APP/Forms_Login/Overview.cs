using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AniX_APP.Forms
{
    public partial class Overview : Form
    {
        #region BUTTON STYLE

        private class ButtonImages
        {
            public Image Original { get; set; }
            public Image Inverted { get; set; }
        }

        private Button activeButton = null;
        private Dictionary<Button, ButtonImages> buttonImages = new Dictionary<Button, ButtonImages>();
        private Form activeForm = null;

        private void SetButtonStyles()
        {
            SetButtonStyle(btnClose);

        }

        private void SetButtonImages()
        {
            SetImageButtonStyle(btnClose, btnClose.Image, AniX_APP.Properties.Resources.exit_invert);
        }

        private void SetImageButtonStyle(Button button, Image originalImage, Image invertedImage)
        {
            SetButtonStyle(button);
            buttonImages[button] = new ButtonImages { Original = originalImage, Inverted = invertedImage };
        }

        private void SetButtonStyle(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.FromArgb(35, 32, 39);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(35, 32, 39);
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 34, 83);
            button.FlatAppearance.BorderSize = 0; // No border
            button.MouseEnter += Button_MouseEnter;
            button.MouseLeave += Button_MouseLeave;
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                button.ForeColor = Color.Black;
                if (buttonImages.ContainsKey(button))
                {
                    button.Image = buttonImages[button].Inverted;
                }
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (button != activeButton)
                {
                    button.ForeColor = Color.Silver;
                    if (buttonImages.ContainsKey(button))
                    {
                        button.Image = buttonImages[button].Original;
                    }
                }
                else
                {
                    button.ForeColor = Color.Black;
                    if (buttonImages.ContainsKey(button))
                    {
                        button.Image = buttonImages[button].Inverted;
                    }
                }
            }
        }

        private void HighlightButton()
        {
            activeButton.BackColor = Color.FromArgb(231, 34, 83);
            activeButton.ForeColor = Color.FromArgb(11, 7, 17);
            if (buttonImages.ContainsKey(activeButton))
            {
                activeButton.Image = buttonImages[activeButton].Inverted;
            }
        }

        private void ResetButtonStyle()
        {
            activeButton.BackColor = Color.FromArgb(35, 32, 39);
            activeButton.ForeColor = Color.Silver;
            if (buttonImages.ContainsKey(activeButton))
            {
                activeButton.Image = buttonImages[activeButton].Original;
            }
        }

        #endregion
        public Overview()
        {
            InitializeComponent();
            SetButtonStyles();
            SetButtonImages();
        }

        private void btnInformation_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
