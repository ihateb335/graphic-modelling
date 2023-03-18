﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using GraphicModellingLibrary._3D_Display;

namespace Drawing
{
    public partial class Form1 : Form
    {
        IFormDisplayer _formDisplayer;
        public Form1(IFormDisplayer Displayer)
        {
            InitializeComponent();
            _formDisplayer = Displayer;

            _formDisplayer.On_Invalidate += Invalidate;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _formDisplayer.Width = Width;
            _formDisplayer.Height = Height;
            _formDisplayer.BuildUp(this);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            _formDisplayer.Paint();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            _formDisplayer.UserControl(e);
        }
    }
}