using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace proj2
{
    public partial class Form1 : Form
    {
        private int tick;
        private Triangle triangle;
        Vector3d axis_mundi = new Vector3d(100, 100, 100);
        Vector3d light = new Vector3d(-300, -300, 100);
        Triangle[] ts;
        Vector3d[] vs;
        DateTime stop_watch;
        Random rnd;
        
        public Form1()
        {
            InitializeComponent();
            this.tick = 0;
            // this.triangle = new Triangle(new Vector3d(30, 40, 30), new Vector3d(50, 150, 200), new Vector3d(120, 30, 100));
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            start_timer(50);
            rnd = new Random();

            ts = Utils.GetCircus(5);
            vs = Utils.VectorsFromTriangles(ts).ToArray();
            Utils.SetNeighbors(ts);
            
            //for (int i = 0; i < vs.Length; i++)
            //{
            //    vs[i].clr = rnd.NextDouble() * 250;
            //}

            stop_watch = DateTime.Now;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.tick += 1;
            if (tick % 100 == 0)
            {
                Console.WriteLine();
                Console.WriteLine("fps: " + 1000*100/((DateTime.Now - stop_watch).TotalMilliseconds));
                Console.WriteLine("mspf: " + (DateTime.Now - stop_watch).TotalMilliseconds / 100);
                stop_watch = DateTime.Now;
            }

            Bitmap bmap = Graphic.blankBitmap(200, 50);

            float angle = (float)Math.PI / 90;
            for (int i = 0; i < vs.Length; i++)
            {
                vs[i].RotateX(axis_mundi, angle * (float)0.9);
                vs[i].RotateZ(axis_mundi, angle);
            }
            //light.x = Math.Cos(-tick * Math.PI / 200) * 500 + 100;
            //light.y = Math.Sin(-tick * Math.PI / 200) * 500 + 100;
            //Console.WriteLine(light);

            Utils.SortByProximity(ts);
            foreach (var t in ts)
            {
                t.UpdateNormal();
            }
            Utils.CalculateVerticesNormals(vs);
            foreach (var v in vs)
            {
                Graphic.calculateShading(v, light);
            }
            foreach (var t in ts)
            {
                Triangle tp = Graphic.projectTriangle(t, 20000);
                //Graphics.printTriangle(tp, bmap);
                //double[] clrs = new double[3];
                //clrs[0] = 0;
                //clrs[1] = 250;
                //clrs[2] = 100;
                //Graphics.printTriangle(tp, bmap, clrs);
                Graphic.printTriangle(tp, bmap);
            }

            //triangle.RotateX(new Vector3d(100, 100, 100), Math.PI/18);
            //Triangle tp = Graphics.projectTriangle(triangle, 20000);
            //double[] clrs = new double[3];
            //clrs[0] = 0;
            //clrs[1] = 250;
            //clrs[2] = 70;
            //Graphics.printTriangle(tp, bmap, clrs);

            pictureBox1.Image = bmap;
        }

        private Image imgFromArray(byte[] arr, int width, int heigth)
        {
            Bitmap pic = new Bitmap(width, heigth, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < heigth; y++)
                {
                    int val = (int)arr[y * width + x];
                    Color c = Color.FromArgb(val, val, val);
                    pic.SetPixel(x, y, c);
                }
            }

            return (Image)pic;
        }

        private byte[] getExampleImg(int imgSize)
        {
            byte[] res = new byte[imgSize * imgSize];
            for (int i = 0; i < imgSize; i++)
            {
                for (int j = 0; j < imgSize; j++)
                {
                    // Console.WriteLine((i+j)/2);
                    res[i * imgSize + j] = Convert.ToByte(((i + j + 5*tick)/2)%255);
                }
            }
            return res;
        }

        private void start_timer(int ms)
        {
            Timer timer = new Timer();
            timer.Interval = (ms); // 10 secs
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }


    }
}
