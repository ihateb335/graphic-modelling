using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.DirectX;

using GraphicModellingLibrary._3D_Display;

namespace Drawing
{
    public partial class Form1 : Form
    {
        IFormDisplayer _formDisplayer;
        public Form1(IFormDisplayer Displayer)
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            _formDisplayer = Displayer;

            _formDisplayer.On_Invalidate += Invalidate;

            IObjectToDisplay test = new TestTriangle(_formDisplayer, Width, Height);


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _formDisplayer.Width = Width;
            _formDisplayer.Height = Height;
            if (_formDisplayer.BuildUp(this)) Close();
            _formDisplayer.POV = Matrix.PerspectiveFovLH((float)Math.PI / 4, this.Width / this.Height, 1.0f, 100.0f);
            _formDisplayer.Target = Matrix.LookAtLH(new Vector3(0, 3, 5.0f), new Vector3(), new Vector3(0, 1, 0));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            _formDisplayer.Paint();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            _formDisplayer.UserControl(e);
        }

        public void OnIdle(object sender, EventArgs e)
        {
            Invalidate(); // Помечаем главное окно (this) как требующее перерисовки
        }
    }
}
