using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

using System.Numerics;

using Vector3DX = Microsoft.DirectX.Vector3;

namespace GraphicModellingLibrary
{
    public static class Vector3Extensions
    {
        public static Vector3DX OrientVector(this Vector3DX vector)
        {
            return vector.Vector3FromDX().OrientVector().Vector3DX();
        }
        public static Vector3 OrientVector(this Vector3 vector)
        {
            float X = vector.X,
                Y = vector.Y,
                Z = vector.Z;               ;
            if (X == 0 && Y == 0) return Vector3.Zero;
            if (X == 0 && Y != 0)
            {
                float acos = (float)Math.Acos(Z / Math.Sqrt(Z * Z + Y * Y));
                if (X * Y > 0) return new Vector3(-acos, 0, 0);
                if (X * Y < 0) return new Vector3(acos, 0, 0);
                return new Vector3((float)(Math.PI / 2.0), 0, 0);
            }
            if(X != 0 && Y == 0)
            {
                float acos = (float)Math.Acos(Z / Math.Sqrt(Z * Z + X * X));
                if (X * Z > 0) return new Vector3(0, -acos, 0);
                if (X * Z < 0) return new Vector3(0, acos, 0);
                return new Vector3((float)(Math.PI / 2.0), 0, 0);
            }
            if(X != 0 && Y != 0 && Z == 0)
            {
                float acos = (float)Math.Acos(X / Math.Sqrt(Y * Y + X * X));
                if (X * Y > 0) return new Vector3(0, 0, acos);
                return new Vector3(0, (float)(Math.PI / 2.0), acos);
            }
            float atg = (float)Math.Atan(Y / Z);
            float lx = 0;
            if (Y > 0 && Z > 0) lx = -atg;
            if (Y < 0 && Z > 0) lx = atg;
            if (Y < 0 && Z < 0) lx = (float)Math.PI - atg;
            if (Y > 0 && Z < 0) lx = (float)Math.PI + atg;
            
            return new Vector3(lx,(float)(Math.Asin(X)),0);
        }

        public static string PrintVector(this Vector3 Vector) => $"{Vector.X,5:F2} {Vector.Y,5:F2} {Vector.Z,5:F2}";

        public static PointF Vector3ToPoint(this Vector3 vector) => new PointF { X = vector.X, Y = vector.Y };
        public static Vector3 PointToVector3(this PointF vector) => new Vector3 { X = vector.X, Y = vector.Y };
        public static Vector3DX Vector3DX(this Vector3 vector) => new Vector3DX { X = vector.X,Y = vector.Y, Z = vector.Z };
        public static Vector3 Vector3FromDX(this Vector3DX vector) => new Vector3 { X = vector.X,Y = vector.Y, Z = vector.Z };

        public static double[,] VectorsToMatrix(this Vector3[] vectors)
        {
            int length = vectors.Length;

            double[,] result = new double[length, length];

            for (int i = 0; i < length; i++)
            {
                result[i, 0] = vectors[i].X;
                result[i, 1] = vectors[i].Y;
                result[i, 2] = vectors[i].Z;
            }

            return result;
        }

        public static double[,] Transpose(this double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix[j, i];
                }
            }

            return result;
        }

        // Умножить матрицу слева на вектор справа
        public static Vector3 LeftMultiply(this Vector3 origin, double[,] matrix)
        {
            return origin.RightMultiply(matrix.Transpose());
        }

        // Умножить вектор слева на матрицу справа
        public static Vector3 RightMultiply(this Vector3 origin, double[,] matrix)
        {
            double[,] buffer = matrix;

            double newX = origin.X * buffer[0, 0] + origin.Y * buffer[1, 0] + origin.Z * buffer[2, 0];
            double newY = origin.X * buffer[0, 1] + origin.Y * buffer[1, 1] + origin.Z * buffer[2, 1];
            double newZ = origin.X * buffer[0, 2] + origin.Y * buffer[1, 2] + origin.Z * buffer[2, 2];

            Vector3 result = new Vector3(Convert.ToSingle(newX), Convert.ToSingle(newY), Convert.ToSingle(newZ));

            return result;
        }

        public static void Print(this double[,] matrix, string marker = "Маркер перехода")
        {
            Console.WriteLine(new String('-', 50));
            Console.WriteLine(marker);

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                Console.Write($"> ");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i, j],-8:F8} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(new String('-', 50));
        }

        public static void Print(this Vector3 vector3, string marker = "")
        {
            Console.WriteLine(marker);
            Console.WriteLine($">(X: {vector3.X:F4}, Y: {vector3.Y:F4}, Z: {vector3.Z:F4})\n");
        }
    }
}
