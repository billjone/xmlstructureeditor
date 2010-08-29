/*******************************************************************************
Class xmlFunctions
 (c) 2009-2010 S. Dluzewski

 Info to add later..
*******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;

namespace xmlStructureEditor
{
    class xmlFunctions
    {

        
    public static void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
      {
         XmlNode node;
         TreeNode treeNode;
         XmlNodeList nl;
   

         if (inXmlNode.HasChildNodes)
         {
            nl = inXmlNode.ChildNodes;
            for(int i = 0; i <= nl.Count - 1; i++)
            {
               node = inXmlNode.ChildNodes[i];
               inTreeNode.Nodes.Add(new TreeNode(node.Name));
               treeNode = inTreeNode.Nodes[i];
               AddNode(node, treeNode);
            }
         }
         else
             inTreeNode.Text = (inXmlNode.OuterXml).Trim();         
      }
        
        public static void createTree(XmlNode xmlNode, TreeNodeCollection treeNodes)
        {
            TreeNodeCollection tn = treeNodes;
            ConvertXmlNodeToTreeNode(xmlNode, tn);
            treeNodes = tn;            
        }
        

    public static void ConvertXmlNodeToTreeNode(XmlNode xmlNode, // function turns xml doc into a LABELLED treeview
      TreeNodeCollection treeNodes)
    {
            TreeNode newTreeNode = treeNodes.Add(xmlNode.Name);
            switch (xmlNode.NodeType)
            {
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.XmlDeclaration:
                    newTreeNode.ForeColor = Color.Blue;
                    newTreeNode.Text = "<?" + xmlNode.Name + " " +
                      xmlNode.Value + "?>";
                    break;
                case XmlNodeType.Element:
                    newTreeNode.ForeColor = Color.Maroon;
                    newTreeNode.Text = "<" + xmlNode.Name + ">";
                    break;
                case XmlNodeType.Attribute:
                    newTreeNode.ForeColor = Color.Purple;
                    newTreeNode.Text = "ATTRIBUTE: " + xmlNode.Name;
                    break;
                case XmlNodeType.Text:
                    newTreeNode.NodeFont = new Font("Verdana", 10, FontStyle.Bold);
                    newTreeNode.Text = "[" + xmlNode.Value + "]";
                    break;
                case XmlNodeType.CDATA:
                    newTreeNode.Text = "CDATA: " + xmlNode.Value;
                    break;
                case XmlNodeType.Comment:
                    newTreeNode.Text = "<!--" + xmlNode.Value + "-->";
                    break;
            }

            if (xmlNode.Attributes != null)
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                   ConvertXmlNodeToTreeNode(attribute, newTreeNode.Nodes);           
          
            foreach (XmlNode childNode in xmlNode.ChildNodes)
                ConvertXmlNodeToTreeNode(childNode, newTreeNode.Nodes);
        }


    public static string treeToXpath(string path) // function to repair pathing differences between Xpath and TreeviewPath.. Microsoft idiocy!
    {
        return path
            .Replace("#document", "")
            .Replace("<", "")
            .Replace(">", "")
        //    .Replace("\\", "/")
            .Replace(@"\", "/");          

        // return "/" + path.Replace(@"\", "/"); <-- old tree return
    }

    
    
}
   }

			
    
