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
        public KinematicPairMesh(IFormDisplayer observable, KinematicPair pair, float cylinder_radius = 0.5f)
        {
            observable.Subscribe(this);

            Pair = pair;

            Cylinder = Mesh.Cylinder(observable.d3d, cylinder_radius, cylinder_radius, pair.Length, 100, 100);
        }
        public KinematicPair Pair { get; private set; }

        private Mesh Cylinder;
        public Material material { get; set; } = new Material
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
                d3d.Transform.World = Matrix.Translation(Pair.AbsLink.Vector3DX());
                Cylinder.DrawSubset(0);
            }
        }
    }
}
