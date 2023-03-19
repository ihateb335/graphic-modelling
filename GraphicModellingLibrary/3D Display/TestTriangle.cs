using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace GraphicModellingLibrary._3D_Display
{
    public class TestTriangle : IObjectToDisplay
    {

        private CustomVertex.PositionColored[] verts;
        int Width, Height;
        float t = 0;
        public TestTriangle(IObservable<Device> observable, int Width, int Height)
        {
            observable.Subscribe(this);

            this.Width = Width;
            this.Height = Height;

            verts = new CustomVertex.PositionColored[3];
            verts[0].Position = new Vector3(0.0f, 1.0f, 1.0f);
            verts[0].Color = System.Drawing.Color.Aqua.ToArgb();
            verts[1].Position = new Vector3(-1.0f, -1.0f, 1.0f);
            verts[1].Color = System.Drawing.Color.Black.ToArgb();
            verts[2].Position = new Vector3(1.0f, -1.0f, 1.0f);
            verts[2].Color = System.Drawing.Color.Purple.ToArgb();
        }

        public void Dispose()
        {

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
            d3d.VertexFormat = CustomVertex.PositionColored.Format;
            t += 0.01f;
            d3d.Transform.World = Matrix.RotationZ((float)Math.PI * t / 6.0f);
            d3d.DrawUserPrimitives(PrimitiveType.TriangleList, 1, verts);

            d3d.RenderState.CullMode = Cull.None;
        }
    }
}
