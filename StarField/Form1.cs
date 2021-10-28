using System;
using System.Drawing;
using System.Windows.Forms;

namespace StarField
{
    public partial class Form : System.Windows.Forms.Form
    {
        class Star
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Z { get; set; }

        }

        private Star[] stars = new Star[5000];

        private Random random = new Random();

        private Graphics graphics;

        public Form()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            PictureBox.Image = new Bitmap(PictureBox.Width, PictureBox.Height);

            graphics = Graphics.FromImage(PictureBox.Image);

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star() 
                {
                    X = random.Next (-PictureBox.Width, PictureBox.Width),
                    Y = random.Next(-PictureBox.Height, PictureBox.Height),
                    Z = random.Next(1, PictureBox.Width)
                };
            }

            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.Black);

            foreach(var star in stars)
            {
                DrawStar(star);
                MoveStar(star);
            }

            PictureBox.Refresh();
        }

        private void MoveStar(Star star)
        {
            star.Z -= 10;

            if (star.Z < 1)
            {
                star.X = random.Next(-PictureBox.Width, PictureBox.Width);
                star.Y = random.Next(-PictureBox.Height, PictureBox.Height);
                star.Z = random.Next(1, PictureBox.Width); // main
            }
        }

        private void DrawStar(Star star)
        {
            float size = Scale(star.Z, 0, PictureBox.Width, 6, 0);

            float x = Scale(star.X / star.Z, 0, 1, 0, PictureBox.Width) + PictureBox.Width / 2;

            float y = Scale(star.Y / star.Z, 0, 1, 0, PictureBox.Height) + PictureBox.Height / 2;

            graphics.FillEllipse(Brushes.White, x, y, size, size);
        }

        private float Scale(float n, float start1, float stop1, float start2, float stop2)
        {
            return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            Application.Exit();
        }
    }
}
