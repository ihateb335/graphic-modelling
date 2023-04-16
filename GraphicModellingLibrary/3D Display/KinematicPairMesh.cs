using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace GraphicModellingLibrary._3D_Display
{
    public sealed class KinematicPairMesh : IObjectToDisplay
    {
        public static readonly float Slope = (float)Math.PI / 100.0f;
        public readonly float CylinderRadius;

        public KinematicPairMesh(IFormDisplayer observable, KinematicPair pair, Color color, float cylinder_radius = 0.025f)
        {
            observable.Subscribe(this);

            Pair = pair;
            CylinderRadius = cylinder_radius;

            CylinderMaterial  = new Material
            {
                Diffuse = color,
                Specular = Color.White,
            };

            Cylinder = Mesh.Cylinder(observable.d3d, cylinder_radius, cylinder_radius, pair.Length + 2.0f * cylinder_radius, 100, 100);
        }
        public KinematicPairMesh(IFormDisplayer observable, KinematicPair pair, float cylinder_radius = 0.25f) : this(observable, pair, Color.Pink, cylinder_radius) { }
        public KinematicPair Pair { get; private set; }

        private Mesh Cylinder;
        public Material CylinderMaterial { get; set; }
        public void Dispose()
        {
            if (Cylinder != null) Cylinder.Dispose();
        }

        public void OnCompleted()
        {
            Dispose();
        }

        public void OnError(Exception error)
        {
            Dispose();
        }
        internal Vector3 Previous { get
            {
                Vector3 previous = new Vector3();
                if (Pair.LinkHolder != null)
                {
                    previous = Pair.LinkHolder.AbsLink().Vector3DX();
                }
                return previous;
            }
        }

        internal Vector3 Current => Pair.AbsLink().Vector3DX();
        internal Vector3 CylinderVector => new Vector3(0, 0, Pair.Length);

        private double current_angle;
        private double max_angle;
        private bool direction_positive;
        public bool Ready { get; set; } = false;
        public bool InPosition => current_angle == max_angle;

        public double Angle
        {
            get { return current_angle; }
            set {
                Ready = false;
                current_angle = 0;
                max_angle = value;
                direction_positive = current_angle < max_angle;
            }
        }

        private void IncrementAngle()
        {
            if (!Ready) return;
            if(direction_positive)
            {
                if (current_angle < max_angle) current_angle += Slope;
                if (current_angle > max_angle) current_angle = max_angle;
            }
            else
            {
                if (current_angle > max_angle) current_angle -= Slope;
                if (current_angle < max_angle) current_angle = max_angle;
            }
            
            Pair.Fi = current_angle;
        }

        public void OnNext(Device d3d)
        {
            if (Cylinder != null)
            {
                d3d.Material = CylinderMaterial;

                IncrementAngle();
              
                Vector3 translate = (Previous + Current) * 0.5f;
                // A * M + T

                d3d.Transform.World = CylinderVector.OrientVector(Current - Previous) * Matrix.Translation(translate);
                Cylinder.DrawSubset(0);
            }
        }
    }
}
