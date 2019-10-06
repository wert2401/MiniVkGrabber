using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoundImage
{
    public class RoundPicture : PictureBox
    {
        Point standartLocation;
        Point sizedLocation;
        Size stSize;
        Size bgSize;
        Color ellipseColor = Color.FromArgb(150, 200, 255);
        float sizeCoef = 1.05f;
        public RoundPicture()
        {
            stSize = new Size(264, 264); // Менять размер картинки
            Size = stSize;
            bgSize = new Size((int)(Width * sizeCoef), (int)(Height * sizeCoef));

            standartLocation = Location;
            sizedLocation = new Point(Location.X - (int)(Width * (sizeCoef-1) / 2) + 2, Location.Y - (int)(Height * (sizeCoef - 1) / 2) + 2);

            MouseEnter += RoundPicture_MouseEnter;
            MouseLeave += RoundPicture_MouseLeave;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
           
            Graphics g = pe.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.Red);
            p.Width = 10f;
            Rectangle ellipseRect = new Rectangle((int)(0.1 / 2 * Width), (int)(0.1 / 2 * Height), Width - (int)(0.1 * Width), (int)(Height - 0.1 * Height));
            Rectangle secElRect = new Rectangle((int)(0.06 / 2 * Width), (int)(0.06 / 2 * Height), Width - (int)(0.06 * Width), (int)(Height - 0.06 * Height));
            Rectangle rect = new Rectangle(-1, -1, Width + 10, Height + 10);

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rect);
            path.AddEllipse(ellipseRect);
            Brush b = new SolidBrush(Parent.BackColor);
            g.FillPath(b, path);
            g.DrawEllipse(new Pen(ellipseColor, 4), ellipseRect);
            g.DrawEllipse(new Pen(Color.BlueViolet, 2), secElRect);
        }

        private void RoundPicture_MouseEnter(object sender, EventArgs e)
        {
            Location = sizedLocation;
            Size = bgSize;
        }

        private void RoundPicture_MouseLeave(object sender, EventArgs e)
        {
            Location = standartLocation;
            Size = stSize;
        }

    }
}
