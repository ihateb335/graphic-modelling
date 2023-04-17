using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace GraphicModellingLibrary._3D_Display
{    

    public interface IDirectXDisplayer : IDisposable, IObservable<Device>
    {
        Device d3d { get; }
    }
    public interface IDirectXFormDisplayer : IDirectXDisplayer
    {
        bool BuildUp(Control render_object);
        void RetrieveCamera(Vector3 vector);
        void RetrieveFocus(Vector3 vector);
        void Paint();

        void UserControl(IEnumerable<Keys> keys);
        void MouseControl(float X, float Y);

        int Width { get; set; }
        int Height { get; set; }

        Keys[] keys { get; }

        event InvalidateDelegate On_Invalidate;
    }
}
