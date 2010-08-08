using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; // Required for XML -> String conversions
using System.Xml; // Required for all XML operations. Built into dotNet2.0

namespace xmlStructureEditor
{
    class calcIndex
    {

        public static int calculateTreeIndex(TreeView tview, XmlDocument xmlDoc)
        {            
            if (!treeviewNodeType(tview).Equals(XmlNodeType.Element))
                return tview.SelectedNode.Index;
            if (haveParent(tview))
            {
                XmlNodeList nl;
                int count = 0;

                nl = xmlDoc.SelectNodes(tview.SelectedNode.Parent.Tag.ToString());

                for (int i = 0; i != tview.SelectedNode.Parent.Nodes.Count; i++)
                {
                    if (tview.SelectedNode.Parent.Nodes[i].Text.Contains("ATTRIBUTE"))
                        count++;
                }


                return tview.SelectedNode.Index - count;
            }
            else
            {
                XmlNodeList nl;
                int count = 0;

                nl = xmlDoc.SelectNodes(tview.SelectedNode.Tag.ToString());

                for (int i = 0; i != tview.SelectedNode.Nodes.Count; i++)
                {
                    if (tview.SelectedNode.Nodes[i].Text.Contains("ATTRIBUTE"))
                        count++;                    
                }


                return tview.SelectedNode.Index - count;

            }

        }


        public static XmlNodeType treeviewNodeType(TreeView tview)
        {
            if (tview.SelectedNode.Text.Contains("<!--"))
                return XmlNodeType.Comment;
            else if (tview.SelectedNode.Text.EndsWith(">"))
                return XmlNodeType.Element;

            if (tview.SelectedNode.Text.EndsWith("]") && !tview.SelectedNode.Parent.Text.Contains("ATTRIBUTE: "))
                return XmlNodeType.Text;

            if (haveParent(tview) && tview.SelectedNode.Parent.Text.Contains("ATTRIBUTE: ") || tview.SelectedNode.Text.Contains("ATTRIBUTE: "))
                return XmlNodeType.Attribute;

            if (tview.SelectedNode.Text.Contains("CDATA: "))
                return XmlNodeType.CDATA;

            return XmlNodeType.Notation;

        }

        private static bool haveParent(TreeView tv)
        {
            try
            {
                int o = tv.SelectedNode.Parent.Index;
                if (tv.SelectedNode.Parent.Text.Contains("#document"))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    
    
    }
}
