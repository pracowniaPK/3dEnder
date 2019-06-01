using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj2
{
    class Vector3d
    {
        public float x;
        public float y;
        public float z;
        public float clr;
        public List<Triangle> neighbors;
        public Vector3d vertex_normal;


        public Vector3d(float x, float y, float z, float clr=0)
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

        public void RotateX(Vector3d axisX, float angle)
        {
            float ny = y - axisX.y;
            float nz = z - axisX.z;
            float nny = ny * (float)Math.Cos(angle) - nz * (float)Math.Sin(angle);
            nz = ny * (float)Math.Sin(angle) + nz * (float)Math.Cos(angle);
            nny += axisX.y;
            nz += axisX.z;
            y = nny;
            z = nz;
        }

        public void RotateZ(Vector3d axisZ, float angle)
        {
            float nx = x - axisZ.x;
            float ny = y - axisZ.y;
            float nnx = nx * (float)Math.Cos(angle) - ny * (float)Math.Sin(angle);
            ny = nx * (float)Math.Sin(angle) + ny * (float)Math.Cos(angle);
            nnx += axisZ.x;
            ny += axisZ.y;
            x = nnx;
            y = ny;
        }

        public static float DotProduct(Vector3d v1, Vector3d v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        public static Vector3d GetNormal(Vector3d v1, Vector3d v2)
        {
            float x = v1.y * v2.z - v1.z * v2.y;
            float y = v1.z * v2.x - v1.x * v2.z;
            float z = v1.x * v2.y - v1.y * v2.x;
            float l = (float)Math.Sqrt((float)(x * x + y * y + z * z));
            return new Vector3d(x / l, y / l, z / l);
        }

        public static Vector3d GetNormal(Vector3d v)
        {
            float l = (float)Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
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
