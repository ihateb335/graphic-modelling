using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

using Microsoft.DirectX;

using GraphicModellingLibrary;
using GraphicModellingLibrary._3D_Display;

namespace Drawing
{
    public partial class Form1 : Form
    {
        IFormDisplayer _formDisplayer;

        Dictionary<Keys, bool> _displayKeys = new Dictionary<Keys, bool>();

        Point Previous = new Point();
        Point Current = new Point();

        List<KinematicPairMesh> _pairs = new List<KinematicPairMesh>();

        PointF MouseSpeed => new PointF((Current - (Size)Previous).X * _mouse_modifier, (Current - (Size)Previous).Y * _mouse_modifier);

        readonly float _mouse_modifier = 1E-2f;

        List<NumericUpDown> updowns; 
        List<Control> controls;
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

            updowns = new List<NumericUpDown>()
            {
                Fi1_Updown,
                Fi2_Updown,
                Fi3_Updown,
                Fi4_Updown,
                Fi5_Updown,
                Fi6_Updown
            };

            updowns.ForEach(updown => updown.Enabled = false);
            controls = new List<Control>(updowns)
            {
                test_output,
                test_output1
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _formDisplayer.Width = Width;
            _formDisplayer.Height = Height;
            if (_formDisplayer.BuildUp(this)) Close();

            //IObjectToDisplay test = new TestTriangle(_formDisplayer, Width, Height);
            IObjectToDisplay axis = new AxisDisplay(_formDisplayer);


            KinematicPair previous_pair = null;
            for (int i = 0; i < 7; i++)
            {
                var _pair = new KinematicPair(5.0f)
                {
                    LinkHolder = previous_pair,
                    
                };
                KinematicPairMesh pair = new KinematicPairMesh(_formDisplayer, _pair);
                previous_pair = _pair;
                _pairs.Add(pair);
            }
                   
            _formDisplayer.RetrieveCamera(new Vector3(25, 25, 25));
            updowns.ForEach(updown => {
               updown.Value = Convert.ToDecimal(_pairs[Convert.ToInt32(updown.Tag)].Pair.Fi / Math.PI * 180.0);
            });
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            _formDisplayer.Paint();
            controls.ForEach(c => c.Update());

            var current_speed = MouseSpeed;
            _formDisplayer.UserControl(_displayKeys.Where(key => key.Value).Select(key => key.Key));
            _formDisplayer.MouseControl(0, 0);
            Current = Previous;

            Thread.Sleep(10);
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

            e.SuppressKeyPress = true;
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (_displayKeys.ContainsKey(e.KeyCode))
            {
                _displayKeys[e.KeyCode] = false;
            }

            e.SuppressKeyPress = true;
        }

        private void UpDownValueChanged(object sender, EventArgs e)
        {
            var updown = (NumericUpDown)sender;
            if (updown.Value > 360) updown.Value %= 360;
            if (updown.Value < 0) updown.Value = 360 - Math.Abs(updown.Value) % 360;
            _pairs[Convert.ToInt32(updown.Tag)].Pair.Fi = (double)updown.Value * Math.PI / 180.0;

            //var vect1 = _pairs[Convert.ToInt32(updown.Tag)].Diff;
            //var vect = _pairs[Convert.ToInt32(updown.Tag)].Test();
            //test_output1.Text = $"X: {vect1.X, 2:f2},Y: {vect1.Y,2:f2},Z: {vect1.Z,2:f2}" ;
            //test_output.Text = $"X: {vect.X, 2:f2},Y: {vect.Y,2:f2},Z: {vect.Z,2:f2}" ;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            updowns.ForEach(updown => updown.Enabled = !updown.Enabled);
        }
    }
}
