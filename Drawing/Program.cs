using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using GraphicModellingLibrary._3D_Display;

namespace Drawing
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var Form = new Form1(new DirectX9Facade());
            Application.Idle += Form.OnIdle;
            Application.Run(Form);
        }
    }
}
