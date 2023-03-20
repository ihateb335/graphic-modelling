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

        Dictionary<Keys, bool> _displayKeys = new Dictionary<Keys, bool>();

        Point Previous = new Point();
        Point Current = new Point();

        PointF MouseSpeed => new PointF((Current - (Size)Previous).X * _mouse_modifier, (Current - (Size)Previous).Y * _mouse_modifier);

        readonly float _mouse_modifier = 1E-2f;

        public Form1(IFormDisplayer Displayer)
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            _formDisplayer = Displayer;

            _formDisplayer.On_Invalidate += Invalidate;

            foreach (var item in _formDisplayer.keys)
            {
                _displayKeys[item] = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _formDisplayer.Width = Width;
            _formDisplayer.Height = Height;
            if (_formDisplayer.BuildUp(this)) Close();

            //IObjectToDisplay test = new TestTriangle(_formDisplayer, Width, Height);

            IObjectToDisplay pair = new KinematicPairMesh(_formDisplayer, new GraphicModellingLibrary.KinematicPair(5));


            _formDisplayer.RetrieveCamera(new Vector3(0, 0, 0.0f));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            _formDisplayer.Paint();

            var current_speed = MouseSpeed;
            _formDisplayer.UserControl(_displayKeys.Where(key => key.Value).Select(key => key.Key));
            _formDisplayer.MouseControl(0, 0);
            Current = Previous;
        }

        public void OnIdle(object sender, EventArgs e)
        {
            Invalidate(); // Помечаем главное окно (this) как требующее перерисовки
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (_displayKeys.ContainsKey(e.KeyCode))
            {
                _displayKeys[e.KeyCode] = true;
            }
            if (e.KeyCode == Keys.Escape) Close();
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (_displayKeys.ContainsKey(e.KeyCode))
            {
                _displayKeys[e.KeyCode] = false;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
        }

    }
}
