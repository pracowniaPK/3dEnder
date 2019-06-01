using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj2
{
    class Triangle
    {
        public Vector3d[] vs;
        public Vector3d normal;

        public Triangle(Vector3d v1, Vector3d v2, Vector3d v3)
        {
            vs = new Vector3d[3];
            this.vs[0] = v1;
            this.vs[1] = v2;
            this.vs[2] = v3;
        }

        public void UpdateNormal()
        {
            Vector3d dv1 = Vector3d.newFromSubtract(vs[0], vs[1]);
            Vector3d dv2 = Vector3d.newFromSubtract(vs[0], vs[2]);
            normal = Vector3d.GetNormal(dv1, dv2);
        }

        public void RotateZ(Vector3d axisZ, float angle)
        {
            foreach (Vector3d v in vs)
            {
                v.RotateZ(axisZ, angle);
            }
        }

        public void RotateX(Vector3d axisX, float angle)
        {
            foreach (Vector3d v in vs)
            {
                v.RotateX(axisX, angle);
            }
        }

        public void PrintMe()
        {
            string res = "";
            for (int i = 0; i < 3; i++)
            {
                res += i + ":" + vs[i] + ", ";
            }
            Console.WriteLine(res);
        }

        public static Triangle[] SortTriangles(Triangle[] ts)
        {
            float[] zs = new float[ts.Length];
            for (int i = 0; i < ts.Length; i++)
            {
                zs[i] = Math.Min(ts[i].vs[0].z, ts[i].vs[1].z);
                zs[i] = Math.Min(zs[i], ts[i].vs[2].z);
            }
            Array.Sort(zs, ts);
            return ts;
        }

        public Triangle Copy()
        {
            Vector3d[] vs = new Vector3d[3];
            for (int i = 0; i < 3; i++)
            {
                vs[i] = this.vs[i].Copy();
            }
            return new Triangle(vs[0], vs[1], vs[2]);
        }

    }
}
