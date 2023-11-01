using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AniX_APP.CustomElements
{
    public class RoundPanelListBox : Panel
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int borderRadius = 35;
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            grPath.AddLine(borderRadius, 0, Width - borderRadius, 0);
            grPath.AddArc(Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            grPath.AddLine(Width, borderRadius, Width, Height - borderRadius);
            grPath.AddArc(Width - borderRadius, Height - borderRadius, borderRadius, borderRadius, 0, 90);
            grPath.AddLine(Width - borderRadius, Height, borderRadius, Height);
            grPath.AddArc(0, Height - borderRadius, borderRadius, borderRadius, 90, 90);
            grPath.AddLine(0, Height - borderRadius, 0, borderRadius);

            Region = new Region(grPath);

            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            Region = null;
            base.OnResize(eventargs);
        }
    }
}
