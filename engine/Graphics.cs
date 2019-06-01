using System;
using System.Collections.Generic;
using System.Drawing;

namespace proj2
{
    class Graphics
    {
        public static Vector3d projection(Vector3d v, double d)
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
            v.clr = Math.Max(l * 255, 0);
        }

        /// <summary>
        /// Returns new triangle projected on XY plane
        /// </summary>
        public static Triangle projectTriangle(Triangle t, double d)
        {
            Triangle res = new Triangle(projection(t.vs[0], d), projection(t.vs[1], d), projection(t.vs[2], d));
            return res;
        }

        public static void printTriangle(Triangle t, Bitmap bitmap)
        {
            double[] ty = new double[3];
            for (int i = 0; i < 3; i++)
            {
                ty[i] = t.vs[i].y;
            }
            Array.Sort(ty, t.vs);
            Vector3d[] vs = t.vs;

            for (int y = (int)vs[0].y; y < (int)vs[2].y; y++)
            {
                double k;
                int xstart;
                int xstop;
                double i4;
                double i6;

                k = ((double)y - vs[0].y) / (vs[2].y - vs[0].y);
                xstop = (int)(k * vs[2].x + (1 - k) * vs[0].x);
                i6 = k * vs[2].clr + (1 - k) * vs[0].clr;
                if (y < (int)vs[1].y)
                {
                    k = ((double)y - vs[0].y) / (vs[1].y - vs[0].y);
                    xstart = (int)(k * vs[1].x + (1 - k) * vs[0].x);
                    i4 = k * vs[1].clr + (1 - k) * vs[0].clr;
                }
                else
                {
                    k = ((double)y - vs[1].y) / (vs[2].y - vs[1].y);
                    xstart = (int)(k * vs[2].x + (1 - k) * vs[1].x);
                    i4 = k * vs[2].clr + (1 - k) * vs[1].clr;
                }
                if (xstart > xstop)
                {
                    int tmp = xstop;
                    xstop = xstart;
                    xstart = tmp;
                    double tmp2 = i4;
                    i4 = i6;
                    i6 = tmp2;
                }
                for (int x = xstart; x < xstop; x++)
                {
                    double k2 = (double)(x - xstart) / (double)(xstop - xstart);
                    int tint = (int)(Math.Abs(k2 * i6 + (1 - k2) * i4));
                    tint = Math.Min(tint, 255);
                    tint = Math.Max(tint, 0);
                    Color c = Color.FromArgb(tint, tint, tint);
                    //Color c = Color.FromArgb(0,0,0);
                    if (x > 0 && x < bitmap.Height && y > 0 && y < bitmap.Width)
                    {
                        bitmap.SetPixel(x, y, c);
                    }

                }
            }

            // tmp crap:
            //for (int i = 0; i < 3; i++)
            //{
            //    Color c = Color.FromArgb(255, 255, 255);
            //    bitmap.SetPixel((int)t.vs[i].x, (int)t.vs[i].y, c);
            //}
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
