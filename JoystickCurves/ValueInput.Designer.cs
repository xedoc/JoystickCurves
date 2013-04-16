namespace JoystickCurves
{
    partial class ValueInput
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
            this.panelNumeric = new System.Windows.Forms.Panel();
            this.numericValue = new System.Windows.Forms.NumericUpDown();
            this.labelNumeric = new System.Windows.Forms.Label();
            this.panelBool = new System.Windows.Forms.Panel();
            this.checkBool = new System.Windows.Forms.CheckBox();
            this.panelString = new System.Windows.Forms.Panel();
            this.labelText = new System.Windows.Forms.Label();
            this.text = new System.Windows.Forms.TextBox();
            this.panelNumeric.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericValue)).BeginInit();
            this.panelBool.SuspendLayout();
            this.panelString.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNumeric
            // 
            this.panelNumeric.Controls.Add(this.panelString);
            this.panelNumeric.Controls.Add(this.numericValue);
            this.panelNumeric.Controls.Add(this.labelNumeric);
            this.panelNumeric.Controls.Add(this.panelBool);
            this.panelNumeric.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNumeric.Location = new System.Drawing.Point(0, 0);
            this.panelNumeric.Name = "panelNumeric";
            this.panelNumeric.Size = new System.Drawing.Size(234, 25);
            this.panelNumeric.TabIndex = 2;
            // 
            // numericValue
            // 
            this.numericValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numericValue.Location = new System.Drawing.Point(177, 2);
            this.numericValue.Name = "numericValue";
            this.numericValue.Size = new System.Drawing.Size(56, 20);
            this.numericValue.TabIndex = 12;
            // 
            // labelNumeric
            // 
            this.labelNumeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNumeric.Location = new System.Drawing.Point(2, 1);
            this.labelNumeric.Name = "labelNumeric";
            this.labelNumeric.Size = new System.Drawing.Size(167, 23);
            this.labelNumeric.TabIndex = 11;
            this.labelNumeric.Text = "label6";
            this.labelNumeric.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelBool
            // 
            this.panelBool.Controls.Add(this.checkBool);
            this.panelBool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBool.Location = new System.Drawing.Point(0, 0);
            this.panelBool.Name = "panelBool";
            this.panelBool.Size = new System.Drawing.Size(234, 25);
            this.panelBool.TabIndex = 13;
            // 
            // checkBool
            // 
            this.checkBool.Location = new System.Drawing.Point(2, 3);
            this.checkBool.Name = "checkBool";
            this.checkBool.Size = new System.Drawing.Size(229, 20);
            this.checkBool.TabIndex = 0;
            this.checkBool.Text = "checkBox1";
            this.checkBool.UseVisualStyleBackColor = true;
            // 
            // panelString
            // 
            this.panelString.Controls.Add(this.text);
            this.panelString.Controls.Add(this.labelText);
            this.panelString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelString.Location = new System.Drawing.Point(0, 0);
            this.panelString.Name = "panelString";
            this.panelString.Size = new System.Drawing.Size(234, 25);
            this.panelString.TabIndex = 14;
            // 
            // labelText
            // 
            this.labelText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelText.Location = new System.Drawing.Point(2, 1);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(89, 23);
            this.labelText.TabIndex = 11;
            this.labelText.Text = "label6";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // text
            // 
            this.text.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.text.Location = new System.Drawing.Point(97, 3);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(134, 20);
            this.text.TabIndex = 12;
            // 
            // ValueInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelNumeric);
            this.Name = "ValueInput";
            this.Size = new System.Drawing.Size(234, 25);
            this.panelNumeric.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericValue)).EndInit();
            this.panelBool.ResumeLayout(false);
            this.panelString.ResumeLayout(false);
            this.panelString.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNumeric;
        private System.Windows.Forms.Panel panelString;
        private System.Windows.Forms.TextBox text;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.NumericUpDown numericValue;
        private System.Windows.Forms.Label labelNumeric;
        private System.Windows.Forms.Panel panelBool;
        private System.Windows.Forms.CheckBox checkBool;

    }
}
