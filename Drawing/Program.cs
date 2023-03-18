using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using GraphicModellingLibrary._3D_Display;
using GraphicModellingLibrary;

namespace Drawing
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                // Create facade object
                var f = new DirectX9Facade();
            }
            catch (Exception ex)
            {
                // Output error message to console
                MessageBox.Show(ex.Message, "Ошибка инициализации");
            }
            
            //Application.Run(new Form1(new DirectX9Facade()));
            Application.Run(new Form2());
        }
    }
}
