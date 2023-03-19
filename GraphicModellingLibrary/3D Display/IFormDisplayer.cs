﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace GraphicModellingLibrary._3D_Display
{
    public interface IFormDisplayer : IDisposable, IObservable<Device>
    {
        bool BuildUp(Control render_object);
        void RetrieveCamera(Vector3 vector);
        void RetrieveFocus(Vector3 vector);
        void Paint();
        void UserControl(KeyEventArgs e);

        int Width { get; set; }
        int Height { get; set; }

        Matrix POV { get; set; }
        Matrix Target { get; set; }

        event InvalidateDelegate On_Invalidate;
    }
}