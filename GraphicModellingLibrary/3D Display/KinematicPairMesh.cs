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

        public void OnNext(Device d3d)
        {
            if (Cylinder != null)
            {
                d3d.Material = CylinderMaterial;

                Vector3 previous = new Vector3();
                Vector3 current = Pair.AbsLink().Vector3DX();

                if (Pair.LinkHolder != null)
                {
                    previous = Pair.LinkHolder.AbsLink().Vector3DX();
                }

                Vector3 diff = (current - previous);

                Vector3 orient = diff.OrientVector();

                d3d.Transform.World = Matrix.RotationY(orient.Y) * Matrix.RotationZ(orient.Z) * Matrix.RotationX(orient.X) * Matrix.Translation((previous + current) * 0.5f);
                Cylinder.DrawSubset(0);
            }
        }
    }
}
