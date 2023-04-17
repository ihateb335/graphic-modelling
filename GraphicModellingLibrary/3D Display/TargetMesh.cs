using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using System.Drawing;

namespace GraphicModellingLibrary._3D_Display
{
    public sealed class TargetMesh : IObjectToDisplay
    {

        public TargetMesh(IDirectXDisplayer observable, Color color, float target_radius = 0.35f)
        {
            observable.Subscribe(this);


            CylinderMaterial = new Material
            {
                Diffuse = color,
                Specular = Color.White,
            };

            Sphere = Mesh.Sphere(observable.d3d, target_radius, 100, 100);
        }

        private Mesh Sphere;
        public Material CylinderMaterial { get; set; }
        public bool Show { get; set; } = false;
        public Vector3 Position { get; set; } = new Vector3();
        public void Dispose()
        {
            if (Sphere != null) Sphere.Dispose();
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
            if(Sphere != null && Show)
            {
                d3d.Transform.World = Matrix.Translation(Position);
                Sphere.DrawSubset(0);
            }
        }
    }
}
