﻿using System;
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
        private static readonly double Alpha = Math.PI / 2.0;

        public KinematicPair(float Length, int K = 1) {
            this.Length = Length;
            this.K = K;
        }

        public float Length { get => Link.Y; set => Link = new Vector3(0,value,0); }
        public Vector3 Link { get; private set; } = Vector3.Zero;
        public KinematicPair LinkHolder { get; set; } = null;
        public double Fi { get; set; } = Math.PI / 2.0;
        public int K { get; set; } = 1;

        /// <summary>
        /// Rotation matrix - M in notation
        /// </summary>
        public double[,] M => new double[3,3] {
                 { Math.Cos(Fi),  Math.Sin(Fi), 0.0 },
                 {-Math.Sin(Fi),  Math.Cos(Fi), 0.0 },
                 { 0.0,           0.0,          1.0 },
        };

        /// <summary>
        /// Transition matrix - A 
        /// </summary>
        public double[,] A => new double[3,3] {
                 { Math.Cos(K * Alpha), 0.0, -Math.Sin(K * Alpha), },
                 { Math.Sin(K * Alpha), 0.0, Math.Cos(K * Alpha), },
                 { 0.0,          1.0, 0.0           },
        };

        public Vector3 AbsLink
        {
            get {
                if (LinkHolder == null) return Link;
                var current_holder = this;
                var result_vector = Link;
                while(current_holder.LinkHolder != null)
                {
                   result_vector = result_vector.RightMultiply(current_holder.M).RightMultiply(A);
                   current_holder = current_holder.LinkHolder;
                }
                return LinkHolder.AbsLink + result_vector;
            }
        }

    }
}