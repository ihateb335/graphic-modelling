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
        IDirectXFormDisplayer _formDisplayer;

        Dictionary<Keys, bool> _displayKeys = new Dictionary<Keys, bool>();

        Point Previous = new Point();
        Point Current = new Point();

        List<KinematicPairMesh> _pairs = new List<KinematicPairMesh>();

        PointF MouseSpeed => new PointF((Current - (Size)Previous).X * MOUSEMODIFIER, (Current - (Size)Previous).Y * MOUSEMODIFIER);

        /// <summary>
        /// Кількість пар
        /// </summary>
        const int PAIRS = 7;

        /// <summary>
        /// Модифікатор миші
        /// </summary>
        const float MOUSEMODIFIER = 1E-2f;

        /// <summary>
        /// Модифікатор миші
        /// </summary>
        const float CAMERA_POSITION_MODIFIER = 2.0f;

        List<NumericUpDown> updowns; 
        List<Control> controls;
        public Form1(IDirectXFormDisplayer Displayer)
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
                Fi6_Updown,
                NX,
                NY,
                NZ,
            };

            updowns.ForEach(updown => updown.Enabled = false);
            controls = new List<Control>(updowns)
            {
                test_output,
                Calculate,
                GRUB
            };

            Calculate.Enabled = false;
        }
        Color[] Rainbow = new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.DarkBlue };

        float[] lengths = { 1.0f, 0.03f, .5f, 0.025f, 0.25f, 0.02f, 0.125f };



        List<double[,]> matrices;

        TargetMesh Target;
        KinematicArmMesh KinematicArm;

        private void InitializeTransformMatrices()
        {
            double[,] defaultMtx = { { 0, 0, 1 }, { 1, 0, 0 }, { 0, 1, 0 } };
            double[,] ordinaryMtx = { 
                { 0, 1, 0 },
                { 1, 0, 0 },
                { 0, 0, 1 } 
            };

            matrices = new List<double[,]>
            {
                defaultMtx,
                defaultMtx,
                defaultMtx,
                defaultMtx,
                defaultMtx,
                defaultMtx,

                ordinaryMtx,
            };
        }

        private void InitiatePairs() {

            KinematicPair previous_pair = null;
            for (int i = 0; i < PAIRS; i++)
            {
                var _pair = new KinematicPair()
                {
                    Link = new System.Numerics.Vector3(0, lengths[i], 0),
                    LinkHolder = previous_pair,
                    A = matrices[i],
                };
                KinematicPairMesh pair = new KinematicPairMesh(_formDisplayer, _pair, Rainbow[i]);
                previous_pair = _pair;
                _pairs.Add(pair);
            }

            KinematicArm = new KinematicArmMesh(_formDisplayer, previous_pair, Color.Pink);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _formDisplayer.Width = Width;
            _formDisplayer.Height = Height;

            if (_formDisplayer.BuildUp(this)) Close();

            InitializeTransformMatrices();
            InitiatePairs();

            //IObjectToDisplay test = new TestTriangle(_formDisplayer, Width, Height);
            IObjectToDisplay axis = new AxisDisplay(_formDisplayer);
            Target = new TargetMesh(_formDisplayer, Color.Pink, 0.05f);

          
            _formDisplayer.RetrieveCamera(new Vector3(CAMERA_POSITION_MODIFIER, CAMERA_POSITION_MODIFIER, -CAMERA_POSITION_MODIFIER));

            var vect = _pairs.Last().Pair.AbsLink();

            NXYZ_ValueChanged(sender, EventArgs.Empty);

            test_output.Text = $"X: {vect.X,2:f2},Y: {vect.Y,2:f2},Z: {vect.Z,2:f2}";
        }

        private void IterateCurrent()
        {
            if (operating_on < 0) return;

            var current = _pairs[operating_on];

            if (current.InPosition)
            {
                current.Ready = false;
                operating_on++;

                if (operating_on >= PAIRS)
                {
                    operating_on = -1;
                    return;
                }
                _pairs[operating_on].Ready = true;
                
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            IterateCurrent();

            _formDisplayer.Paint();
            controls.ForEach(c => c.Update());

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

        /// <summary>
        /// Зміна вуглів маніпулятора
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpDownValueChanged(object sender, EventArgs e)
        {
            var updown = (NumericUpDown)sender;

            var index = Convert.ToInt32(updown.Tag);
            if (index < 0) return;
            if (index >= PAIRS) return;

            if (updown.Value > 360) updown.Value %= 360;
            if (updown.Value < 0) updown.Value = 360 - Math.Abs(updown.Value) % 360;
            if(operating_on < 0) _pairs[index - 1].Pair.Fi = (double)updown.Value * Math.PI / 180.0;

            var vect = _pairs.Last().Pair.AbsLink();

            test_output.Text = $"X: {vect.X, 2:f2},Y: {vect.Y,2:f2},Z: {vect.Z,2:f2}" ;
        }

        /// <summary>
        /// Увімкнути/Вимкнути елементи форми, щоб повернути контроль
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Click(object sender, EventArgs e)
        {
            updowns.ForEach(updown => updown.Enabled = !updown.Enabled);
            Calculate.Enabled = !Calculate.Enabled;
            GRUB.Enabled = !GRUB.Enabled;
        }

        private int operating_on = -1;

        private void Calculate_Click(object sender, EventArgs e)
        {
            foreach(var u in updowns.Take(6))
            {
                u.Value = 0;
            }
            var X = Convert.ToSingle(NX.Value);
            var Y = Convert.ToSingle(NY.Value);
            var Z = Convert.ToSingle(NZ.Value);
            var TargetPosition = new Vector3(X, Y, Z);

            Target.Position = TargetPosition;
            Target.Show = true;

            var fis = KinematicPair.ReverseJob(_pairs.Last().Pair, new System.Numerics.Vector3(X,Y,Z));
            
            operating_on = 0;
            
            for (int i = 0; i < PAIRS - 1; i++)
            {
                _pairs[i].Angle = fis[i];

                var updown = updowns[i];
                var Fi = fis[i] / Math.PI * 180.0;
                Fi = Fi < 0 ? 360 - Math.Abs(Fi) % 360 : Fi % 360;
                updown.Value = Convert.ToDecimal(Fi);
            }

            _pairs[operating_on].Ready = true;
        }

        private void CheckDesiredPoint(Vector3 desiredPoint)
        {
            double IsNanToZero(double value) => double.IsNaN(value) ? 0 : value;

            // d - длина звена
            double[] d = new double[PAIRS];

            for (int i = 0; i < d.Length; i++)
            {
                d[i] = _pairs[i].Pair.Length;
            }

            // Максимальное растояние от первого неподвижного хвата
            double dpMax = Math.Sqrt((d[2] + d[4] + d[6]) * (d[2] + d[4] + d[6])
                + (d[1] + d[3] + d[5]) * (d[1] + d[3] + d[5]));

            // set Y bound
            double a = d[0] - (d[2] + d[4] + d[6]);
            double b = d[0] + (d[2] + d[4] + d[6]);

            //  Label #1: "a<yA<b"
            double yA = desiredPoint.Y;

            // Если значение Y за пределом - выдать ошибку.
            NY.Minimum = (decimal)IsNanToZero(a);
            NY.Maximum = (decimal)IsNanToZero(b);

            // set X bound
            double c = Math.Sqrt(dpMax * dpMax - (d[0] - yA) * (d[0] - yA));
            //  Label #2: "|xA|<c"
            double xA = Math.Abs(desiredPoint.X);

            // Если значение X за пределом - выдать ошибку.
            NX.Minimum = (decimal)IsNanToZero(-c);
            NX.Maximum = (decimal)IsNanToZero(c);

            // set Z bound
            double f = Math.Sqrt(c * c - xA * xA);
            //  Label #3: "|zA|< f"
            double zA = Math.Abs(desiredPoint.Z);

            // Если значение Z за пределом - выдать ошибку.
            NZ.Minimum = (decimal)IsNanToZero(- f);
            NZ.Maximum = (decimal)IsNanToZero(f);
        }

        private void GRUB_ValueChanged(object sender, EventArgs e)
        {
            KinematicArm.Fi = (double)GRUB.Value / 180 * Math.PI;
        }

        private void NXYZ_ValueChanged(object sender, EventArgs e)
        {
            var X = Convert.ToSingle(NX.Value);
            var Y = Convert.ToSingle(NY.Value);
            var Z = Convert.ToSingle(NZ.Value);
            var TargetPosition = new Vector3(X, Y, Z);

            CheckDesiredPoint(TargetPosition);
        }
    }
}
