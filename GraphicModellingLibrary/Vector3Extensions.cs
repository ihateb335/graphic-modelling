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
        public static Vector3 OrientVector(this Vector3 vector)
        {
            float X = (float)Math.Round(vector.X, 2),
                Y = (float)Math.Round(vector.Y, 2),
                Z = (float)Math.Round(vector.Z, 2)
            ;
            float pi_2 = (float)(Math.PI / 2.0);

            //1) X=0, Y=0, Z=0
            if (X == 0 && Y == 0 && Z == 0) return Vector3.Zero;
            //2) X=0, Y!=0, Z=0
            else if (X == 0 && Y != 0 && Z == 0)
            {
                var pi_cur = pi_2;
                if (Y > 0) pi_cur = -pi_cur;

                return new Vector3(pi_cur, 0, 0);
            }
            //3) X=0,Y!=0,Z!=0
            else if (X == 0 && Y != 0 && Z != 0)
            {
                float acos = (float)Math.Acos(Z / Math.Sqrt(Z * Z + Y * Y));
                if (Y > 0) acos = -acos;
                return new Vector3(acos, 0, 0);
            }
            //4) X!=0, Y=0, Z=0
            else if(X != 0 && Y == 0 && Z == 0)
            {
                var pi_cur = pi_2;
                if (X < 0) pi_cur = -pi_cur;
                return new Vector3(0,  pi_cur, 0);
            }
            //5) X!=0, Y=0, Z!=0
            else if(X!=0 && Y == 0 && Z != 0)
            {
                float acos = (float)Math.Acos(Z / Math.Sqrt(Z * Z + X * X));
                if (X < 0) acos = -acos;
                return new Vector3(0, acos, 0);
            }

            //6) X!=0, Y!=0, Z=0 
            else if (X != 0 && Y != 0 && Z == 0)
            {
                float acos = (float)Math.Acos(X / Math.Sqrt(Y * Y + X * X));
                if (X < 0) acos = -acos;

                return new Vector3(0,pi_2,acos);
            }
            //7) X!=0, Y!=0, Z!=0
            else
            {
                float asinx = (float)Math.Asin(-Y / Math.Sqrt(1 - X * X));
                float asiny = (float)Math.Asin(X);

                return new Vector3(asinx, asiny, 0);               
            }
        }

        public static string PrintVector(this Vector3 Vector) => $"{Vector.X,5:F2} {Vector.Y,5:F2} {Vector.Z,5:F2}";

        public static PointF Vector3ToPoint(this Vector3 vector) => new PointF { X = vector.X, Y = vector.Y };
        public static Vector3 PointToVector3(this PointF vector) => new Vector3 { X = vector.X, Y = vector.Y };
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
