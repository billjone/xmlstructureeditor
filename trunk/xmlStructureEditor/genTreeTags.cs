
/*******************************************************************************
Class genTreeTags
 (c) 2009-2010 S. Dluzewski

 Recursive XML path Treeview tag generator.
 More Info to add later..
*******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;


namespace xmlStructureEditor
{
    class genTreeTags
    {
        public genTreeTags(TreeView tview)
        {
  
      
            for (int k = 0; k < tview.Nodes.Count; k++)
            {                
                int count = 0;
                for (int x = 0; x <= k; x++)            
                    if (tview.Nodes[x].Text == tview.Nodes[k].Text)
                        count++;   
                string xpath = "";               
                for (int i = 0; i < tview.Nodes[k].Nodes.Count; i++)
                {
                    tview.Nodes[k].Nodes[i].Tag = xpath + "/" + tview.Nodes[k].Nodes[i].Text
                        .Replace("<", "")
                        .Replace(">","")
                        .Replace("ATTRIBUTE: ", "")
                        .Replace("CDATA: ", "")
                        .Replace("[", "")
                        .Replace("]","") + "[" + count + "]";
                    visitChildNodes(tview.Nodes[k].Nodes[i], tview.Nodes[k].Nodes[i].Tag.ToString());
                }
            }
       
     
        }

        private void visitChildNodes(TreeNode node, String prepath)
        {
            for (int j = 0; j < node.Nodes.Count; j++)
            {
                int count = 0;
                for (int x = 0; x <= j; x++)
                    if (node.Nodes[x].Text == node.Nodes[j].Text)
                        count++;


                node.Nodes[j].Tag = prepath + "/" + node.Nodes[j].Text
                    .Replace("<", "")
                    .Replace(">", "")
                    .Replace("ATTRIBUTE: ", "")
                    .Replace("CDATA: ", "")
                    .Replace("[", "")
                    .Replace("]", "") + "[" + count + "]";

                visitChildNodes(node.Nodes[j], node.Nodes[j].Tag.ToString());
            }
        }
    }
}

