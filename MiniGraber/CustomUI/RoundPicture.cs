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
        public RoundPicture()
        {
            stSize = new Size(264, 264);
            Size = stSize;
            bgSize = new Size((int)(Width * 1.1), (int)(Height * 1.1));

            standartLocation = Location;
            sizedLocation = new Point(Location.X - (int)(Width * 0.09 / 2) + 1, Location.Y - (int)(Height * 0.09 / 2) + 1);

            MouseEnter += RoundPicture_MouseEnter;
            MouseLeave += RoundPicture_MouseLeave;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;
            Pen p = new Pen(Color.Red);
            p.Width = 10f;
            Rectangle r = new Rectangle((int)(0.1 / 2 * Width), (int)(0.1 / 2 * Height), Width - (int)(0.1 * Width), (int)(Height - 0.1 * Height));
            Rectangle r2 = new Rectangle(0, 0, Width, Height);

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(r2);
            path.AddEllipse(r);
            Brush b = new SolidBrush(Parent.BackColor);
            g.FillPath(b, path);
            g.DrawEllipse(new Pen(Brushes.AliceBlue, 4), r);
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
