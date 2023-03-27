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
        public readonly float CylinderRadius;
        public KinematicPairMesh(IFormDisplayer observable, KinematicPair pair, float cylinder_radius = 0.25f)
        {
            observable.Subscribe(this);

            Pair = pair;
            CylinderRadius = cylinder_radius;

            Cylinder = Mesh.Cylinder(observable.d3d, cylinder_radius, cylinder_radius, pair.Length + 2.0f * cylinder_radius, 100, 100);
        }
        public KinematicPair Pair { get; private set; }

        private Mesh Cylinder;
        public Material CylinderMaterial { get; set; } = new Material
        {
            Diffuse = Color.Pink,
            Specular = Color.White,
        };
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
        public void OnNext(Device d3d)
        {
            if (Cylinder != null)
            {
                d3d.Material = CylinderMaterial;
                Vector3 translate = (Previous + Current) * 0.5f;

                // A * My * Mz * Mx + T

                //d3d.Transform.World = Matrix.RotationY(orient.Y) * Matrix.RotationZ(orient.Z) * Matrix.RotationX(orient.X) * Matrix.Translation(translate);

                // A * M + T

                d3d.Transform.World = CylinderVector.OrientVector(Current - Previous) * Matrix.Translation(translate);
                Cylinder.DrawSubset(0);
            }
        }
    }
}
