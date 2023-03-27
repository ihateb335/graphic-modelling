using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.DirectX;

namespace GraphicModellingLibrary._3D_Display
{
    public static class DirecctXExtensions
    {
        public static Vector3 OrientVector(this Vector3 vector)
        {
            return vector.Vector3FromDX().OrientVector().Vector3DX();
        }
        public static Vector3 Vector3DX(this System.Numerics.Vector3 vector) => new Vector3 { X = vector.X, Y = vector.Y, Z = vector.Z };

        public static Matrix OrientVector(this Vector3 vector, Vector3 to_orient)
        {
          
            Vector3 axis = Vector3.Normalize(Vector3.Cross(vector, to_orient));
            float angle = (float)Math.Acos(Vector3.Dot(vector, to_orient) / vector.Length() / to_orient.Length());
            return Matrix.RotationAxis(axis, angle);
        }
    }
}
