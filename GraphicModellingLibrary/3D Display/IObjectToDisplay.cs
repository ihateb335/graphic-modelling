using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace GraphicModellingLibrary._3D_Display
{
    public interface IObjectToDisplay: IObserver<Device>, IDisposable
    {
    }
}
