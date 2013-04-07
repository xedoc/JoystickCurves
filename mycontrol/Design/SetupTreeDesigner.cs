using System;
using System.Reflection;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace mycontrol.Design
{
    [ToolboxBitmap(typeof(SettingsTree),"SettingsTree")]
    public class SetupTreeDesigner : ParentControlDesigner
    {
#region Private properties
        private IDesignerHost designerHost;
        private IMenuCommandService menuService;
        private ISelectionService selectionService;
        private SettingsTree settingsTree;
#endregion
        
        public SetupTreeDesigner()
        {
            Trace.WriteLine("SetupTreeDesigner ctor");

            this.Verbs.Add(
                new DesignerVerb("Add new page",
                new EventHandler(OnPageAdd))
                );
            this.Verbs.Add(
                new DesignerVerb("Remove page",
                new EventHandler(OnPageRemove))
                );
            this.Verbs.Add(
                new DesignerVerb("Edit page title (TAB to finish)",
                new EventHandler(OnEditTitle))
                );
            this.Verbs.Add(
                new DesignerVerb("Clear All",
                new EventHandler(OnClearList))
                );
        }
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            if (Control is SettingsTree)
            {
                designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
                menuService = GetService(typeof(IMenuCommandService)) as IMenuCommandService;
                selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
                settingsTree = Control as SettingsTree;
                settingsTree.TreeView.TreeViewNodeSorter = new NodeSorter();
                settingsTree.TreeView.AfterSelect += OnNodeSelect;
            }


        }
        protected override bool GetHitTest(System.Drawing.Point point)
        {
            var treeView = settingsTree.TreeView;
            return treeView.HitTest(treeView.PointToClient(point)) != null;
        }
        private void OnNodeSelect(object sender, TreeViewEventArgs e)
        {
            Trace.WriteLine("Node selected");
        }
        private void OnPageRemove(object sender, EventArgs e)
        {
            Trace.WriteLine("OnPageRemove event");
            var curPage = settingsTree.CurrentPage;
            var curNode = settingsTree.CurrentNode;
            TreeNode prevNode;

            if (curPage != null)
            {
                if (curNode != null)
                {
                    prevNode = curNode.PrevNode;
                    if (prevNode != null)
                        settingsTree.CurrentNode = prevNode;

                    curNode.Remove();
                    designerHost.DestroyComponent(curPage);
                }
            }
            
        }
        private void OnClearList(object sender, EventArgs e)
        {
            Trace.WriteLine("OnClearList event");
            foreach (var p in settingsTree.Pages)
            {
                designerHost.DestroyComponent(p);
            }

            settingsTree.TreeView.Nodes.Clear();
        }
        private void OnEditTitle(object sender, EventArgs e)
        {
            Trace.WriteLine("OnEditTitle event");
            settingsTree.TreeView.LabelEdit = true;
            settingsTree.CurrentNode.BeginEdit();
        }
        private void OnPageAdd(object sender, EventArgs e)
        {
            Trace.WriteLine("OnPageAdd event");
            var newPage = designerHost.CreateComponent(typeof(SettingsPage)) as SettingsPage;
            TreeNode newNode;
            newPage.isActive = true;
            var nodeIndex = settingsTree.Pages.Count;
            var title = String.Format("page {0}", nodeIndex);
            if( settingsTree.TreeView.SelectedNode == null )
                newNode = settingsTree.TreeView.Nodes.Add(title);
            else
                newNode = settingsTree.CurrentNode.Nodes.Add(title);
            
            
            settingsTree.Cursor = Cursors.Arrow;
            newPage.ParentNode = newNode;

            settingsTree.AddSettingsPage(newPage);

        }
        private void SelectComponent(IComponent component)
        {
            selectionService.SetSelectedComponents(new IComponent[] { component });
        }
        protected override void WndProc(ref Message message)
        {
            TreeNode node;
            Point position;
            if (settingsTree.TreeView.Created &&
                (message.HWnd == settingsTree.TreeView.Handle ||
                message.HWnd == settingsTree.Handle))
            {

                switch ((WM)message.Msg)
                {
                    case WM.WM_LBUTTONDOWN:
                        Trace.WriteLine("WM_LBUTTONDOWN");
                        SelectComponent(settingsTree);
                        node = settingsTree.TreeView.GetNodeAt( new Point(message.LParam.ToInt32()) );
                        if (node != null)
                            settingsTree.CurrentNode = node;
                        BaseWndProc(ref message);
                        break;
                    case WM.WM_RBUTTONDOWN:
                        BaseWndProc(ref message);
                        break;
                    case WM.WM_RBUTTONUP:
                        Trace.WriteLine("WM_RBUTTONUP");
                        SelectComponent(settingsTree);
                        position = new Point(message.LParam.ToInt32());

                        Point nodePos = settingsTree.PointToScreen(position);
                        node = settingsTree.TreeView.GetNodeAt(position);

                        settingsTree.CurrentNode = node;
                        menuService.ShowContextMenu(MenuCommands.SelectionMenu, nodePos.X, nodePos.Y);
                        BaseWndProc(ref message);
                        break;

                    default:
                        base.WndProc(ref message);
                        break;
                }
            }
            
            else
            {
                base.WndProc(ref message);
            }
        }

        public class NodeSorter : IComparer
        {

            public int Compare(object x, object y)
            {
                var tx = x as TreeNode;
                var ty = y as TreeNode;
                return string.Compare(tx.Text, ty.Text);
            }
        }

    }
}
