namespace JoystickCurves
{
    partial class TrimmerTrackBar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numericTrimValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericTrimValue)).BeginInit();
            this.SuspendLayout();
            // 
            // numericTrimValue
            // 
            this.numericTrimValue.DecimalPlaces = 2;
            this.numericTrimValue.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericTrimValue.Location = new System.Drawing.Point(5, 5);
            this.numericTrimValue.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericTrimValue.Name = "numericTrimValue";
            this.numericTrimValue.Size = new System.Drawing.Size(61, 20);
            this.numericTrimValue.TabIndex = 2;
            this.numericTrimValue.ValueChanged += new System.EventHandler(this.numericTrimValue_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "%";
            // 
            // TrimmerTrackBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericTrimValue);
            this.Controls.Add(this.label1);
            this.Name = "TrimmerTrackBar";
            this.Size = new System.Drawing.Size(83, 30);
            ((System.ComponentModel.ISupportInitialize)(this.numericTrimValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericTrimValue;
        private System.Windows.Forms.Label label1;

    }
}
