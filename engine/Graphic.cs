using System;
using System.Collections.Generic;
using System.Drawing;

namespace proj2
{
    class Graphic
    {
        public static Vector3d projection(Vector3d v, float d)
        {
            Vector3d res = new Vector3d(v.x * d / (v.z + d), v.y * d / (v.z + d), 0);
            res.clr = v.clr;
            return res;
        }

        public static void calculateShading(Vector3d v, Vector3d light)
        {
            Vector3d L = new Vector3d(light.x - v.x, light.y - v.y, light.z - v.z);
            L = Vector3d.GetNormal(L);
            double l = Vector3d.DotProduct(L, v.vertex_normal);
            //l = Math.Abs(l);
            v.clr = (float)Math.Max(l * 255, 0);
        }

        /// <summary>
        /// Returns new triangle projected on XY plane
        /// </summary>
        public static Triangle projectTriangle(Triangle t, float d)
        {
            Triangle res = new Triangle(projection(t.vs[0], d), projection(t.vs[1], d), projection(t.vs[2], d));
            return res;
        }

        public static void printTriangle(Triangle t, Bitmap bitmap, bool wireFrame = false)
        {
            double[] ty = new double[3];
            for (int i = 0; i < 3; i++)
            {
                ty[i] = t.vs[i].y;
            }
            Array.Sort(ty, t.vs);
            Vector3d[] vs = t.vs;

            for (float y = vs[0].y; y < vs[2].y; y++)
            {
                float k;
                float xstart;
                float xstop;
                float i4;
                float i6;

                k = (y - vs[0].y) / (vs[2].y - vs[0].y);
                xstart = vs[0].x + k * (vs[2].x - vs[0].x);
                i6 = (1 - k) * vs[0].clr + k * vs[2].clr;

                if (y < vs[1].y)
                {
                    k = (y - vs[0].y) / (vs[1].y - vs[0].y);
                    xstop = vs[0].x + k * (vs[1].x - vs[0].x);
                    i4 = (1 - k) * vs[0].clr + k * vs[1].clr;
                }
                else
                {
                    k = (y - vs[1].y) / (vs[2].y - vs[1].y);
                    xstop = vs[1].x + k * (vs[2].x - vs[1].x);
                    i4 = (1 - k) * vs[1].clr + k * vs[2].clr;
                }
                if (xstart > xstop)
                {
                    float tmp = xstop;
                    xstop = xstart;
                    xstart = tmp;
                    tmp = i4;
                    i4 = i6;
                    i6 = tmp;
                }

                for (float x = xstart; x < xstop + 1; x++)
                {
                    k = (x - xstart) / (xstop - xstart);
                    int tint = (int)(Math.Abs((1 - k) * i6 + k * i4));
                    tint = Math.Min(tint, 255);
                    tint = Math.Max(tint, 0);
                    Color c = Color.FromArgb(tint, tint, tint);
                    if (x > 0 && x < bitmap.Height && y > 0 && y < bitmap.Width)
                    {
                        bitmap.SetPixel((int)x, (int)y, c);
                    }
                }

                if (wireFrame)
                {
                    Pen myPen = new Pen(Color.White);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawLine(myPen, (float)vs[0].x, (float)vs[0].y, (float)vs[1].x, (float)vs[1].y);
                        g.DrawLine(myPen, (float)vs[0].x, (float)vs[0].y, (float)vs[2].x, (float)vs[2].y);
                        g.DrawLine(myPen, (float)vs[2].x, (float)vs[2].y, (float)vs[1].x, (float)vs[1].y);
                    }
                }
            }
        }

        public static void printVector3d(Vector3d v, Bitmap bitmap)
        {
            Bitmap res = (Bitmap)bitmap.Clone();
            
            Color c = Color.FromArgb(255, 255, 255);
            bitmap.SetPixel((int)v.x, (int)v.y, c);
        }

        public static Bitmap blankBitmap(int size, int color)
        {
            Bitmap pic = new Bitmap(size, size, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Color c = Color.FromArgb(color, color, color);
                    pic.SetPixel(x, y, c);
                }
            }

            return pic;
        }
    }
}
