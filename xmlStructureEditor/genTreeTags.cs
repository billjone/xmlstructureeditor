
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
    class genTreeTags
    {
        public void TraverseTreeView(TreeView tview)
        {
            //Create a TreeNode to hold the Parent Node
            TreeNode temp = new TreeNode();
            
            //Loop through the Parent Nodes
            for (int k = 0; k < tview.Nodes.Count; k++)
            {

                int count = 0;

                for (int x = 0; x <= k; x++)
                {
                    if (tview.Nodes[x].Text == tview.Nodes[k].Text)
                        count++;
                }

                //Store the Parent Node in temp
                temp = tview.Nodes[k];

                string xpath = "/";

                tview.Nodes[k].Tag = xpath;

                //Display the Text of the Parent Node i.e. temp
                // MessageBox.Show(tview.Nodes[k].Text);

                //Now Loop through each of the child nodes in this parent node i.e.temp
                for (int i = 0; i < tview.Nodes[k].Nodes.Count; i++)
                {
                    temp.Nodes[i].Tag = xpath + tview.Nodes[k].Text + "[" + count + "]/";
                     // MessageBox.Show("Node Path: " + temp.Nodes[i].Tag);
                    visitChildNodes(tview.Nodes[k].Nodes[i], tview.Nodes[k].Nodes[i].Tag.ToString()); //send every child to the function for further traversal
                }
            }

            MessageBox.Show("Done!");
        }

        private void visitChildNodes(TreeNode node, String prepath)
        {
            //Display the path of the node

            
            
            
            //Loop Through this node and its childs recursively

            for (int j = 0; j < node.Nodes.Count; j++)
            {

                int count = 0;

                for (int x = 0; x <= j; x++)
                {
                    if (node.Nodes[x].Text == node.Nodes[j].Text)
                        count++;
                }

                node.Nodes[j].Tag = prepath + node.Text + "[" + count + "]/";
         //       MessageBox.Show("Node Path: " + node.Nodes[j].Tag);
                visitChildNodes(node.Nodes[j],  node.Nodes[j].Tag.ToString());
            }

        }
    
    }
}
