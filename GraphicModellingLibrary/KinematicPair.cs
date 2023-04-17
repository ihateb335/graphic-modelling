using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace GraphicModellingLibrary
{
    public class KinematicPair
    {

        public float Length => Link.Length();
        public Vector3 Link { get; set; } = Vector3.Zero;
        public KinematicPair LinkHolder { get; set; } = null;
        public double Fi { get; set; } = 0;

        /// <summary>
        /// Rotation matrix - M in notation
        /// </summary>
        public double[,] M => MForm(Fi);
        public Func<double, double[,]> MForm = GetM;

        public static double[,] GetM(double Fi) => new double[3, 3] {
                 { Math.Cos(Fi),  -Math.Sin(Fi), 0.0 },
                 { Math.Sin(Fi),  Math.Cos(Fi), 0.0 },
                 { 0.0,           0.0,          1.0 },
        };

        /// <summary>
        /// Transition matrix - A 
        /// </summary>
        public double[,] A { get; set; }

        public Vector3 AbsLink(bool transformed_vector = false)
        {

            if (LinkHolder == null) return Link;
            var result_vector = Link;

            foreach (var current_holder in LinkHolder.ToCollection())
            {
                result_vector = result_vector.RightMultiply(current_holder.M).RightMultiply(current_holder.A);
            }
           
           
            return (transformed_vector ? Vector3.Zero : LinkHolder.AbsLink(transformed_vector)) + result_vector;

        }

        public IEnumerable<KinematicPair> ToCollection()
        {
            var collection = new List<KinematicPair>() { this };
            var current_holder = this;
            while(current_holder.LinkHolder != null)
            {
                current_holder = current_holder.LinkHolder;
                collection.Add(current_holder);
            }
            return collection;
        }

        public override string ToString()
        {
            var vector = AbsLink();
            return $"X: {vector.X, 2:f2}, Y: {vector.Y,2:f2}, Z: {vector.Z,2:f2} ";
        }

        public static double[] ReverseJob(KinematicPair last, Vector3 point) {
            var Pairs = last.ToCollection().Reverse().ToArray();
            var result = new double[Pairs.Length - 1];

            //Крок 1 Початкові значення OA та 07
            var OA = point;
            var O7 = Pairs.Last().AbsLink();

            for (int i = 0; i < Pairs.Length - 1; i++) {

                #region MyRegion

                //var Link = collection[i];

                //var OO1 = Link.Link;

                ////Крок 2: Віднімання від OA та поточної останньої поточну ланку
                ////Крок 8
                //O7 = O7 - OO1;
                //OA = OA - OO1;

                ////Крок 9
                //var A = Link.A.Transpose();

                ////Крок 3: Домножання на A транспоноване і Крок 4, отримання векторів A та 07 у системі O1x1y1z1
                //OA = OA.RightMultiply(A);
                //O7 = O7.RightMultiply(A);

                ////Крок 5.1 Проектування векторів на площину O1x1y1
                //var OAxy = new Vector3(OA.X, OA.Y, 0);
                //var OOxy = new Vector3(O7.X, O7.Y, 0);
                ////Крок 5.2 Знаходження векторного множення
                //var cross = Vector3.Cross(OOxy, OAxy);

                ////Крок 5.3 Знаходження куту за допомогою функції arcsin, sign від величини Z вектроного множення та sin fi
                ////Крок 10

                //var l1 = cross.Length();
                ////l1 = Vector3.Dot(OOxy, OAxy);
                //var l2 = OA.Length();
                //var l3 = O7.Length();

                //var rad = l1 / l2 / l3;

                //double Angle = Math.Asin(rad);
                ////double Angle = Math.Acos(rad);

                ////Крок 6-7 вектору O1A1(M1)^-1 як O1A1`
                //OA = OA.RightMultiply(GetM(Angle).Transpose());
                //// result[i] = (float)Angle;
                //result[i] = (float)Angle;

                #endregion


                var currLink = Pairs[i].Link;
                //
                var currA = Pairs[i].A;
                var inversedA = currA.Transpose();
                //
                OA = (OA - currLink).RightMultiply(inversedA);
                O7 = (O7 - currLink).RightMultiply(inversedA);
                //
                var OAProj = new Vector3(OA.X, OA.Y, 0);
                var O7Proj = new Vector3(O7.X, O7.Y, 0);
                //
                var dotVector = Vector3.Dot(OAProj, O7Proj);
                var cosF = dotVector / OAProj.Length() / O7Proj.Length();
                // 
                var crossVector = Vector3.Cross(OAProj, O7Proj);
                var sign = crossVector.Z > 0 ? 1 : -1;

                var sinF = sign * crossVector.Length() / OAProj.Length() / O7Proj.Length();
                //
                var angle = Math.Acos(cosF) * sign;
                //
                double[,] currM = new double[3, 3] {
                     { cosF,  sinF, 0.0 },
                     { -sinF,  cosF, 0.0 },
                     { 0.0, 0.0, 1.0 },
                };

                OA = OA.RightMultiply(currM);

                result[i] = angle;
            }


            return result;
        } 

    }
}