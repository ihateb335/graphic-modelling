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
    public class AxisDisplay : IObjectToDisplay
    {
        public AxisDisplay(IDirectXFormDisplayer observable)
        {
            observable.Subscribe(this);

            Cylinder = Mesh.Cylinder(observable.d3d, 0.01f, 0.01f, 100.0f, 10, 10);
        }

        private Mesh Cylinder;
        private Material CylinderMaterial = new Material
        {
            Diffuse = Color.Red,
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
                CylinderMaterial.Diffuse = Color.Pink;
                d3d.Transform.World = Matrix.RotationY(Convert.ToSingle(Math.PI / 2.0));
                Cylinder.DrawSubset(0);

                CylinderMaterial.Diffuse = Color.Lavender;
                d3d.Material = CylinderMaterial;
                d3d.Transform.World = Matrix.RotationX(Convert.ToSingle(Math.PI / 2.0));
                Cylinder.DrawSubset(0);

                CylinderMaterial.Diffuse = Color.ForestGreen;
                d3d.Material = CylinderMaterial;
                d3d.Transform.World = Matrix.RotationZ(Convert.ToSingle(Math.PI / 2.0));
                Cylinder.DrawSubset(0);
            }
        }
    }
}
