using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace GraphicModellingLibrary._3D_Display
{
    public interface IFormDisplayer : IDisposable
    {
        bool BuildUp(Control render_object);
        void RetrieveCamera();
        void RetrieveFocus();
        void Paint();
        void UserControl(KeyEventArgs e);

        int Width { get; set; }
        int Height { get; set; }

        event InvalidateDelegate On_Invalidate;
    }
}
