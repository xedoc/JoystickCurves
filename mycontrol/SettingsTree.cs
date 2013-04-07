using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using mycontrol.Design;

namespace mycontrol
{

    [Designer(typeof(SetupTreeDesigner), typeof(IRootDesigner))]
    public partial class SettingsTree : UserControl
    {
        #region Private properties
        
        #endregion

        #region Public methods
        public SettingsTree()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        }
        public void AddSettingsPage(SettingsPage page)
        {
            RightPanel.Controls.Add(page);
        }
        #endregion
        #region Public properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TreeView TreeView
        {
            get { return treeSettings; }
            set { treeSettings = value; }
        }
        [Browsable(false)]
        public TreeNode CurrentNode
        {
            get { return treeSettings.SelectedNode; }
            set { treeSettings.SelectedNode = value; }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SplitContainer SplitContainer
        {
            get { return splitContainer; }
        }
        [Browsable(false)]
        public Panel RightPanel
        {
            get { return splitContainer.Panel2; }
        }
        [Browsable(true)]
        public List<TreeNode> Nodes
        {
            get { return Pages.Select(p => p.ParentNode).ToList(); }
        }
        [Browsable(false)]
        public SettingsPage CurrentPage
        {
            get { return Pages.FirstOrDefault(p => p.Visible); }
            set { if (value != null) Pages.ForEach(p => p.Visible = (p == value ? true : false)); }
        }
        [Browsable(false)]
        public List<SettingsPage> Pages
        {
            get { return RightPanel.Controls.OfType<SettingsPage>().ToList(); }
        }
        [Browsable(true)]
        public bool LabelEdit
        {
            get { return TreeView.LabelEdit; }
            set { TreeView.LabelEdit = value; }
        }

        #endregion
        #region Private methods
        private void splitContainer_Panel2_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is SettingsPage)
            {
                var page = e.Control as SettingsPage;
                page.Dock = DockStyle.Fill;
                CurrentPage = page;
                CurrentNode = page.ParentNode;
            }
        }

        private void treeSettings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CurrentPage = Pages.FirstOrDefault(p => p.ParentNode == e.Node);
        }
        #endregion

        private void treeSettings_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            LabelEdit = false;
        }
    }
}
