namespace Drawing
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();

                _formDisplayer.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.Calculate = new System.Windows.Forms.Button();
            this.NZ = new System.Windows.Forms.NumericUpDown();
            this.NY = new System.Windows.Forms.NumericUpDown();
            this.NX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.test_output1 = new System.Windows.Forms.TextBox();
            this.test_output = new System.Windows.Forms.TextBox();
            this.Fi6_Updown = new System.Windows.Forms.NumericUpDown();
            this.Fi5_Updown = new System.Windows.Forms.NumericUpDown();
            this.Fi4_Updown = new System.Windows.Forms.NumericUpDown();
            this.Fi3_Updown = new System.Windows.Forms.NumericUpDown();
            this.Fi2_Updown = new System.Windows.Forms.NumericUpDown();
            this.Fi1_Updown = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi6_Updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi5_Updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi4_Updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi3_Updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi2_Updown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi1_Updown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Calculate);
            this.panel1.Controls.Add(this.NZ);
            this.panel1.Controls.Add(this.NY);
            this.panel1.Controls.Add(this.NX);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.test_output1);
            this.panel1.Controls.Add(this.test_output);
            this.panel1.Controls.Add(this.Fi6_Updown);
            this.panel1.Controls.Add(this.Fi5_Updown);
            this.panel1.Controls.Add(this.Fi4_Updown);
            this.panel1.Controls.Add(this.Fi3_Updown);
            this.panel1.Controls.Add(this.Fi2_Updown);
            this.panel1.Controls.Add(this.Fi1_Updown);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(559, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(127, 390);
            this.panel1.TabIndex = 0;
            // 
            // Calculate
            // 
            this.Calculate.Location = new System.Drawing.Point(25, 305);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(75, 23);
            this.Calculate.TabIndex = 13;
            this.Calculate.Text = "Calc";
            this.Calculate.UseVisualStyleBackColor = true;
            this.Calculate.Click += new System.EventHandler(this.Calculate_Click);
            // 
            // NZ
            // 
            this.NZ.AutoSize = true;
            this.NZ.DecimalPlaces = 2;
            this.NZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NZ.Location = new System.Drawing.Point(1, 279);
            this.NZ.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.NZ.Name = "NZ";
            this.NZ.Size = new System.Drawing.Size(127, 20);
            this.NZ.TabIndex = 12;
            this.NZ.Tag = "-1";
            this.NZ.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.NZ.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // NY
            // 
            this.NY.AutoSize = true;
            this.NY.DecimalPlaces = 2;
            this.NY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NY.Location = new System.Drawing.Point(1, 253);
            this.NY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.NY.Name = "NY";
            this.NY.Size = new System.Drawing.Size(127, 20);
            this.NY.TabIndex = 11;
            this.NY.Tag = "-1";
            this.NY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.NY.Value = new decimal(new int[] {
            12,
            0,
            0,
            65536});
            // 
            // NX
            // 
            this.NX.AutoSize = true;
            this.NX.DecimalPlaces = 2;
            this.NX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NX.Location = new System.Drawing.Point(1, 227);
            this.NX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.NX.Name = "NX";
            this.NX.Size = new System.Drawing.Size(127, 20);
            this.NX.TabIndex = 10;
            this.NX.Tag = "-1";
            this.NX.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.NX.Value = new decimal(new int[] {
            5,
            0,
            0,
            -2147418112});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Grub";
            // 
            // test_output1
            // 
            this.test_output1.Location = new System.Drawing.Point(3, 334);
            this.test_output1.Name = "test_output1";
            this.test_output1.ReadOnly = true;
            this.test_output1.Size = new System.Drawing.Size(127, 20);
            this.test_output1.TabIndex = 8;
            this.test_output1.TabStop = false;
            // 
            // test_output
            // 
            this.test_output.Location = new System.Drawing.Point(1, 196);
            this.test_output.Name = "test_output";
            this.test_output.ReadOnly = true;
            this.test_output.Size = new System.Drawing.Size(127, 20);
            this.test_output.TabIndex = 7;
            this.test_output.TabStop = false;
            // 
            // Fi6_Updown
            // 
            this.Fi6_Updown.AutoSize = true;
            this.Fi6_Updown.DecimalPlaces = 2;
            this.Fi6_Updown.Dock = System.Windows.Forms.DockStyle.Top;
            this.Fi6_Updown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Fi6_Updown.Location = new System.Drawing.Point(0, 100);
            this.Fi6_Updown.Maximum = new decimal(new int[] {
            370,
            0,
            0,
            0});
            this.Fi6_Updown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.Fi6_Updown.Name = "Fi6_Updown";
            this.Fi6_Updown.Size = new System.Drawing.Size(127, 20);
            this.Fi6_Updown.TabIndex = 6;
            this.Fi6_Updown.Tag = "6";
            this.Fi6_Updown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.Fi6_Updown.ValueChanged += new System.EventHandler(this.UpDownValueChanged);
            // 
            // Fi5_Updown
            // 
            this.Fi5_Updown.AutoSize = true;
            this.Fi5_Updown.DecimalPlaces = 2;
            this.Fi5_Updown.Dock = System.Windows.Forms.DockStyle.Top;
            this.Fi5_Updown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Fi5_Updown.Location = new System.Drawing.Point(0, 80);
            this.Fi5_Updown.Maximum = new decimal(new int[] {
            370,
            0,
            0,
            0});
            this.Fi5_Updown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.Fi5_Updown.Name = "Fi5_Updown";
            this.Fi5_Updown.Size = new System.Drawing.Size(127, 20);
            this.Fi5_Updown.TabIndex = 5;
            this.Fi5_Updown.Tag = "5";
            this.Fi5_Updown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.Fi5_Updown.ValueChanged += new System.EventHandler(this.UpDownValueChanged);
            // 
            // Fi4_Updown
            // 
            this.Fi4_Updown.AutoSize = true;
            this.Fi4_Updown.DecimalPlaces = 2;
            this.Fi4_Updown.Dock = System.Windows.Forms.DockStyle.Top;
            this.Fi4_Updown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Fi4_Updown.Location = new System.Drawing.Point(0, 60);
            this.Fi4_Updown.Maximum = new decimal(new int[] {
            370,
            0,
            0,
            0});
            this.Fi4_Updown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.Fi4_Updown.Name = "Fi4_Updown";
            this.Fi4_Updown.Size = new System.Drawing.Size(127, 20);
            this.Fi4_Updown.TabIndex = 4;
            this.Fi4_Updown.Tag = "4";
            this.Fi4_Updown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.Fi4_Updown.ValueChanged += new System.EventHandler(this.UpDownValueChanged);
            // 
            // Fi3_Updown
            // 
            this.Fi3_Updown.AutoSize = true;
            this.Fi3_Updown.DecimalPlaces = 2;
            this.Fi3_Updown.Dock = System.Windows.Forms.DockStyle.Top;
            this.Fi3_Updown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Fi3_Updown.Location = new System.Drawing.Point(0, 40);
            this.Fi3_Updown.Maximum = new decimal(new int[] {
            370,
            0,
            0,
            0});
            this.Fi3_Updown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.Fi3_Updown.Name = "Fi3_Updown";
            this.Fi3_Updown.Size = new System.Drawing.Size(127, 20);
            this.Fi3_Updown.TabIndex = 3;
            this.Fi3_Updown.Tag = "3";
            this.Fi3_Updown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.Fi3_Updown.ValueChanged += new System.EventHandler(this.UpDownValueChanged);
            // 
            // Fi2_Updown
            // 
            this.Fi2_Updown.AutoSize = true;
            this.Fi2_Updown.DecimalPlaces = 2;
            this.Fi2_Updown.Dock = System.Windows.Forms.DockStyle.Top;
            this.Fi2_Updown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Fi2_Updown.Location = new System.Drawing.Point(0, 20);
            this.Fi2_Updown.Maximum = new decimal(new int[] {
            370,
            0,
            0,
            0});
            this.Fi2_Updown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.Fi2_Updown.Name = "Fi2_Updown";
            this.Fi2_Updown.Size = new System.Drawing.Size(127, 20);
            this.Fi2_Updown.TabIndex = 2;
            this.Fi2_Updown.Tag = "2";
            this.Fi2_Updown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.Fi2_Updown.ValueChanged += new System.EventHandler(this.UpDownValueChanged);
            // 
            // Fi1_Updown
            // 
            this.Fi1_Updown.AutoSize = true;
            this.Fi1_Updown.DecimalPlaces = 2;
            this.Fi1_Updown.Dock = System.Windows.Forms.DockStyle.Top;
            this.Fi1_Updown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Fi1_Updown.Location = new System.Drawing.Point(0, 0);
            this.Fi1_Updown.Maximum = new decimal(new int[] {
            370,
            0,
            0,
            0});
            this.Fi1_Updown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.Fi1_Updown.Name = "Fi1_Updown";
            this.Fi1_Updown.Size = new System.Drawing.Size(127, 20);
            this.Fi1_Updown.TabIndex = 1;
            this.Fi1_Updown.Tag = "1";
            this.Fi1_Updown.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.Fi1_Updown.ValueChanged += new System.EventHandler(this.UpDownValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 390);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi6_Updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi5_Updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi4_Updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi3_Updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi2_Updown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Fi1_Updown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown Fi6_Updown;
        private System.Windows.Forms.NumericUpDown Fi5_Updown;
        private System.Windows.Forms.NumericUpDown Fi4_Updown;
        private System.Windows.Forms.NumericUpDown Fi3_Updown;
        private System.Windows.Forms.NumericUpDown Fi2_Updown;
        private System.Windows.Forms.NumericUpDown Fi1_Updown;
        private System.Windows.Forms.TextBox test_output;
        private System.Windows.Forms.TextBox test_output1;
        private System.Windows.Forms.NumericUpDown NZ;
        private System.Windows.Forms.NumericUpDown NY;
        private System.Windows.Forms.NumericUpDown NX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Calculate;
    }
}
