using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj2
{
    class Utils
    {
        public static Triangle[] GetCircus(int sides)
        {
            Vector3d[] dRing = new Vector3d[sides];
            double z = 90;
            double r = 70;
            double offset = 100;
            for (int i = 0; i < sides; i++)
            {
                dRing[i] = new Vector3d(r*Math.Cos(i*2*Math.PI/sides) + offset, r * Math.Sin(i * 2 * Math.PI / sides) + offset, z);
            }

            Vector3d[] uRing = new Vector3d[sides];
            z = 30;
            r = 40;
            for (int i = 0; i < sides; i++)
            {
                uRing[i] = new Vector3d(r * Math.Cos(2 * Math.PI * (0.5 + i) / sides) + offset, r * Math.Sin(2 * Math.PI * (0.5 + i) / sides) + offset, z);
            }

            Vector3d tip = new Vector3d(offset, offset, 0);
            
            Triangle[] res = new Triangle[sides*3];
            for (int i = 0; i < sides-1; i++)
            {
                res[3 * i] = new Triangle(dRing[i], dRing[i + 1], uRing[i]);
                res[3 * i + 1] = new Triangle(dRing[i + 1], uRing[i], uRing[i + 1]);
                res[3 * i+2] = new Triangle(uRing[i], uRing[i + 1], tip);
            }
            res[3 * (sides - 1)] = new Triangle(dRing[(sides - 1)], dRing[0], uRing[(sides - 1)]);
            res[3 * (sides - 1) + 1] = new Triangle(dRing[0], uRing[(sides - 1)], uRing[0]);
            res[3 * (sides - 1) + 2] = new Triangle(uRing[(sides - 1)], uRing[0], tip);
            
            return res;
        }

        public static void SetNeighbors(Triangle[] ts)
        {
            List<Vector3d> vs = VectorsFromTriangles(ts);
            foreach (var v in vs)
            {
                v.neighbors.AddRange(Array.FindAll<Triangle>(ts, t => t.vs.Contains<Vector3d>(v)).ToList<Triangle>());
            }
        }

        public static void CalculateVerticesNormals(Vector3d[] vs)
        {
            foreach (var v in vs)
            {
                Vector3d res = v.neighbors[0].normal;
                if (v.neighbors.Count > 1)
                {
                    for (int i = 1; i < v.neighbors.Count; i++)
                    {
                        res = Vector3d.newFromAdd(res, v.neighbors[i].normal);
                    }
                }
                v.vertex_normal = Vector3d.GetNormal(res);
            }
        }

        public static List<Vector3d> VectorsFromTriangles(Triangle[] ts)
        {
            List<Vector3d> res = new List<Vector3d>();
            foreach (Triangle t in ts)
            {
                foreach (Vector3d v in t.vs)
                {
                    if (!res.Contains(v))
                    {
                        res.Add(v);
                    }
                }
            }
            return res;
        }

        public static void SortByProximity(Triangle[] ts)
        {
            double[] dists = new double[ts.Length];

            for (int i = 0; i < dists.Length; i++)
            {
                dists[i] = Math.Min(ts[i].vs[0].z, ts[i].vs[1].z);
                dists[i] = Math.Min(dists[i], ts[i].vs[2].z);
            }

            Array.Sort(dists, ts);
            Array.Reverse(ts);
        }
    }
}
