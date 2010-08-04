using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace xmlStructureEditor
{
    class xmlFunctions
    {

        
    public static void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
      {
         XmlNode xNode;
         TreeNode tNode;
         XmlNodeList nodeList;
         int i;

         // Loop through the XML nodes until the leaf is reached.
         // Add the nodes to the TreeView during the looping process.
         if (inXmlNode.HasChildNodes)
         {
            nodeList = inXmlNode.ChildNodes;
            for(i = 0; i<=nodeList.Count - 1; i++)
            {
               xNode = inXmlNode.ChildNodes[i];
               inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
               tNode = inTreeNode.Nodes[i];
               AddNode(xNode, tNode);
            }
         }
         else
         {
            // Here you need to pull the data from the XmlNode based on the
            // type of node, whether attribute values are required, and so forth.
            inTreeNode.Text = (inXmlNode.OuterXml).Trim();
         }
      }


    public static void ConvertXmlNodeToTreeNode(XmlNode xmlNode, // function turns xml doc into a LABELLED treeview
      TreeNodeCollection treeNodes) {

        
        TreeNode newTreeNode = treeNodes.Add(xmlNode.Name);

        switch (xmlNode.NodeType) {
            case XmlNodeType.ProcessingInstruction:
            case XmlNodeType.XmlDeclaration:
                newTreeNode.Text = "<?" + xmlNode.Name + " " + 
                  xmlNode.Value + "?>";
                break;
            case XmlNodeType.Element:
                newTreeNode.Text = "<" + xmlNode.Name + ">";
                break;
            case XmlNodeType.Attribute:
                newTreeNode.Text = "ATTRIBUTE: " + xmlNode.Name;
                break;
            case XmlNodeType.Text:
            case XmlNodeType.CDATA:
                newTreeNode.Text = xmlNode.Value;
                break;
            case XmlNodeType.Comment:
                newTreeNode.Text = "<!--" + xmlNode.Value + "-->";
                break;
        }

        if (xmlNode.Attributes != null) {
            foreach (XmlAttribute attribute in xmlNode.Attributes) {
                ConvertXmlNodeToTreeNode(attribute, newTreeNode.Nodes);
            }
        }
        foreach (XmlNode childNode in xmlNode.ChildNodes) {
            ConvertXmlNodeToTreeNode(childNode, newTreeNode.Nodes);
        }
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

			
    
