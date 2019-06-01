using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj2
{
    class Vector3d
    {
        public double x;
        public double y;
        public double z;
        public double clr;
        public List<Triangle> neighbors;
        public Vector3d vertex_normal;


        public Vector3d(double x, double y, double z, double clr=0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.clr = clr;
            neighbors = new List<Triangle>();
        }

        public Vector3d Copy()
        {
            return new Vector3d(this.x, this.y, this.z);
        }

        public void RotateX(Vector3d axisX, double angle)
        {
            double ny = y - axisX.y;
            double nz = z - axisX.z;
            double nny = ny * Math.Cos(angle) - nz * Math.Sin(angle);
            nz = ny * Math.Sin(angle) + nz * Math.Cos(angle);
            nny += axisX.y;
            nz += axisX.z;
            y = nny;
            z = nz;
        }

        public void RotateZ(Vector3d axisZ, double angle)
        {
            double nx = x - axisZ.x;
            double ny = y - axisZ.y;
            double nnx = nx * Math.Cos(angle) - ny * Math.Sin(angle);
            ny = nx * Math.Sin(angle) + ny * Math.Cos(angle);
            nnx += axisZ.x;
            ny += axisZ.y;
            x = nnx;
            y = ny;
        }

        public static double DotProduct(Vector3d v1, Vector3d v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        public static Vector3d GetNormal(Vector3d v1, Vector3d v2)
        {
            double x = v1.y * v2.z - v1.z * v2.y;
            double y = v1.z * v2.x - v1.x * v2.z;
            double z = v1.x * v2.y - v1.y * v2.x;
            double l = Math.Sqrt(x * x + y * y + z * z);
            return new Vector3d(x / l, y / l, z / l);
        }

        public static Vector3d GetNormal(Vector3d v)
        {
            double l = Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
            return new Vector3d(v.x / l, v.y / l, v.z / l);
        }

        public static Vector3d newFromAdd(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public static Vector3d newFromSubtract(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }

        public override string ToString()
        {
            return "[" + x + ", " + y + ", " + z + "]";
        }
    }
}
