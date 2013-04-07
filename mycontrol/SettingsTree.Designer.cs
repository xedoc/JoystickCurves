using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using mycontrol.Design;

namespace mycontrol
{
    //[DesignerSerializer(typeof(someserializerclass),typeof(CodeDomSerializer))]
    //[DefaultEvent("SomeDefaultEvent")]
    
    [Designer(typeof(SetupTreeDesigner))]
    [DesignTimeVisible(true)]
    public partial class SettingsTree : UserControl
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeSettings = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeSettings);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.AllowDrop = true;
            this.splitContainer.Panel2.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.splitContainer_Panel2_ControlAdded);
            this.splitContainer.Size = new System.Drawing.Size(687, 353);
            this.splitContainer.SplitterDistance = 229;
            this.splitContainer.TabIndex = 5;
            // 
            // treeSettings
            // 
            this.treeSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSettings.HideSelection = false;
            this.treeSettings.Location = new System.Drawing.Point(0, 0);
            this.treeSettings.Name = "treeSettings";
            this.treeSettings.Size = new System.Drawing.Size(229, 353);
            this.treeSettings.TabIndex = 0;
            this.treeSettings.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeSettings_AfterLabelEdit);
            this.treeSettings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSettings_AfterSelect);
            // 
            // SettingsTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.Name = "SettingsTree";
            this.Size = new System.Drawing.Size(687, 353);
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer;
        private TreeView treeSettings;



    }
}
