using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AniX_APP.CustomElements
{
    class CustomRadioButton : RadioButton
    {
        //Fields
        private Color checkedColor = Color.MediumSlateBlue;
        private Color unCheckedColor = Color.Gray;

        //Properties
        public Color CheckedColor
        {
            get
            {
                return checkedColor;
            }

            set
            {
                checkedColor = value;
                Invalidate();
            }
        }

        public Color UnCheckedColor
        {
            get
            {
                return unCheckedColor;
            }

            set
            {
                unCheckedColor = value;
                Invalidate();
            }
        }

        //Constructor
        public CustomRadioButton()
        {
            MinimumSize = new Size(0, 21);
            //Add a padding of 10 to the left to have a considerable distance between the text and the RadioButton.
            Padding = new Padding(10, 0, 0, 0);
        }

        //Overridden methods
        protected override void OnPaint(PaintEventArgs pevent)
        {
            //Fields
            Graphics graphics = pevent.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            float rbBorderSize = 18F;
            float rbCheckSize = 12F;
            RectangleF rectRbBorder = new RectangleF()
            {
                X = 0.5F,
                Y = (Height - rbBorderSize) / 2, //Center
                Width = rbBorderSize,
                Height = rbBorderSize
            };
            RectangleF rectRbCheck = new RectangleF()
            {
                X = rectRbBorder.X + (rectRbBorder.Width - rbCheckSize) / 2, //Center
                Y = (Height - rbCheckSize) / 2, //Center
                Width = rbCheckSize,
                Height = rbCheckSize
            };

            //Drawing
            using (Pen penBorder = new Pen(checkedColor, 1.6F))
            using (SolidBrush brushRbCheck = new SolidBrush(checkedColor))
            using (SolidBrush brushText = new SolidBrush(ForeColor))
            {
                //Draw surface
                graphics.Clear(BackColor);
                //Draw Radio Button
                if (Checked)
                {
                    graphics.DrawEllipse(penBorder, rectRbBorder);//Circle border
                    graphics.FillEllipse(brushRbCheck, rectRbCheck); //Circle Radio Check
                }
                else
                {
                    penBorder.Color = unCheckedColor;
                    graphics.DrawEllipse(penBorder, rectRbBorder); //Circle border
                }
                //Draw text
                graphics.DrawString(Text, Font, brushText,
                    rbBorderSize + 8, (Height - TextRenderer.MeasureText(Text, Font).Height) / 2);//Y=Center
            }
        }

        //X-> Obsolete code, this was replaced by the Padding property in the constructor
        //(this.Padding = new Padding(10,0,0,0);)
        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);
        //    this.Width = TextRenderer.MeasureText(this.Text, this.Font).Width + 30;
        //}
    }
}