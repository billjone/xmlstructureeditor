/*******************************************************************************
Class Main
 (c) 2009-2010 S. Dluzewski

 Info to add later..
*******************************************************************************/


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
    public partial class Main : Form
    {        
        XmlDocument xmlDoc; // Accessible xmlDocument
        
        public Main()
        {
            InitializeComponent();           
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            

            try
            {          
                
                xmlDoc.Load(@"c:\temp\books.xml");
                


            }
            catch (XmlException xmlEx)
            {
                MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }					

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Main_Load(object sender, EventArgs e)
        {
            InitializeTreeView(); // Function to control events on the XML/Schema treeview representations.
                                  // This treeview will control the entire toolbar availability functionality


            xmlDoc = new XmlDocument(); // create in memory an empty, default xml document to work with.
                                        // any loaded documents will be placed here



            // -- Events to control updating xmlTree/RTB
            xmlDoc.NodeInserted += new XmlNodeChangedEventHandler(updateXmlDisplays);
            xmlDoc.NodeRemoved += new XmlNodeChangedEventHandler(updateXmlDisplays);
            xmlDoc.NodeChanged += new XmlNodeChangedEventHandler(updateXmlDisplays);

            
            
        }

      
        private void updateXmlDisplays(object sender, XmlNodeChangedEventArgs e)
        {
            xmlTreeview.BeginUpdate();
            xmlTreeview.Nodes.Clear();
            xmlTreeview.EndUpdate();
            xmlFunctions.ConvertXmlNodeToTreeNode(xmlDoc, xmlTreeview.Nodes);
            xmlTreeview.ExpandAll();
            
            // -- Generate the complete path tags using the genTreeTags recursive function
            genTreeTags gen = new genTreeTags(xmlTreeview);
            
            // -- Display the XML as Text in the BrowserWindow
            // -- Without the Count, will error as XML Declarations must be added first. Silly MS.
            if (xmlDoc.ChildNodes.Count > 1)
            {
                XmlTextWriter wr = new XmlTextWriter("CurrentDocument.xml", Encoding.UTF8);
                xmlDoc.WriteTo(wr);
                wr.Close();
                this.xmlBrowserWindow.Navigate(Application.StartupPath + "\\CurrentDocument.xml");
            }
            

        }
      
        

        private void InitializeTreeView() // Handles Treeview drawing
        {          
            xmlTreeview.Paint += new PaintEventHandler(Treeview_Paint);
            schemaTreeview.Paint += new PaintEventHandler(Treeview_Paint);
            xmlTreeview.AfterSelect += new TreeViewEventHandler(xmlTreeview_AfterSelect);
            
        }

        private bool haveParent(TreeView tv)
        {
            try
            {
                int o = xmlTreeview.SelectedNode.Parent.Index;
                if (xmlTreeview.SelectedNode.Parent.Text.Contains("#document"))
                    return false;
                return true;
            }
            catch(Exception ex)
            {               
                return false;
            }
        }

      
        void xmlTreeview_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (xmlTreeview.SelectedNode.Text.Equals("#document"))
                xmlTreeview.SelectedNode.Tag = "/";

            if (haveParent(xmlTreeview) && xmlTreeview.SelectedNode.Parent.Text.Equals("#document"))
                xmlTreeview.SelectedNode.Parent.Tag = "/";

            tsStatusLabel.Text = xmlFunctions.treeToXpath(xmlTreeview.SelectedNode.FullPath.ToString());            
            tsStatusLabelClear.Text = xmlTreeview.SelectedNode.FullPath.ToString();
            tsLabelnodeIndex.Text = xmlTreeview.SelectedNode.Tag.ToString();

            XmlNodeType currentSelType = treeviewNodeType(xmlTreeview); // get type of element selected

            tsbtnAddAttribute.Enabled = false;
            tsbtnAddCDATA.Enabled = false;
            tsbtnAddElement.Enabled = false;
            tsbtnComment.Enabled = false;
            tsbtnAddData.Enabled = false;
            tsbtnDelete.Enabled = false;
            if (currentSelType.Equals(XmlNodeType.Element) && !haveParent(xmlTreeview))
            {
                tsbtnAddAttribute.Enabled = true;
                tsbtnAddCDATA.Enabled = true;
                tsbtnAddElement.Enabled = true;
                tsbtnComment.Enabled = true;
                tsbtnAddData.Enabled = true;
                tsbtnDelete.Enabled = false;
                return;
            }

            if (currentSelType.Equals(XmlNodeType.Element))
            {
                tsbtnAddAttribute.Enabled = true;
                tsbtnAddCDATA.Enabled = true;
                tsbtnAddElement.Enabled = true;
                tsbtnComment.Enabled = true;
                tsbtnAddData.Enabled = true;
                tsbtnDelete.Enabled = true;                
            }
            if (currentSelType.Equals(XmlNodeType.Attribute))
            {                
                tsbtnDelete.Enabled = true;
            }
            if (currentSelType.Equals(XmlNodeType.CDATA))
            {
                tsbtnAddCDATA.Enabled = true;
                tsbtnDelete.Enabled = true;
            }
            if (currentSelType.Equals(XmlNodeType.Text))
            {
                tsbtnAddData.Enabled = true;
                tsbtnDelete.Enabled = true;
            }
            if (currentSelType.Equals(XmlNodeType.Comment))
            {
                tsbtnComment.Enabled = true;
                tsbtnDelete.Enabled = true;
            }

            
        }

        XmlNodeType treeviewNodeType(TreeView tview)
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

        void Treeview_Paint(object sender, PaintEventArgs e)
        {            

            if (xmlTreeview.Nodes.Count == 0)
            {
                tsbtnAddAttribute.Enabled = false;
                tsbtnAddCDATA.Enabled = false;
                tsbtnAddElement.Enabled = false;
                tsbtnComment.Enabled = false;
                tsbtnAddData.Enabled = false;
                tsbtnDelete.Enabled = false;   
                tsbtnRootElement.Enabled = true;                
            }
            else
            {
                tsbtnRootElement.Enabled = false;                
            }
            

           



            

                        
            
            
        }

        private void tsbtnRootElement_Click(object sender, EventArgs e)
        {

            // Dialog to display root element name etc
            addRootElement frmGetRootName = new addRootElement();
            frmGetRootName.ShowDialog();

            

            // Create the XML Declaration
           

            // Create the ROOT element
            try
            {
                XmlElement root = xmlDoc.CreateElement(frmGetRootName.getRootElement());
               

                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", null, null);
                xmlDoc.AppendChild(dec);

                xmlDoc.AppendChild(root);

                root.AppendChild(xmlDoc.CreateTextNode("CreationDate: " + DateTime.Today.ToString()));
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                // tooltip popup error

            }         
           

            
            
        }

        
        private void tsbtnAddElement_Click(object sender, EventArgs e)
        {
            addElement frmElement = new addElement();
                              
            frmElement.ShowDialog();
      
             XmlNodeList nl = xmlDoc.SelectSingleNode(xmlTreeview
               .SelectedNode.Tag.ToString())
               .ParentNode.ChildNodes;
            
            int decC = calcIndex.calculateTreeIndex(xmlTreeview, xmlDoc);
            if (nl[decC].NodeType.Equals(XmlNodeType.XmlDeclaration))
                decC++;
            nl[decC].AppendChild(xmlDoc.CreateElement(frmElement.getElementName()));                  
        }


      

      

        private void tsbAddAttribute_Click(object sender, EventArgs e)
        {
            addAttribute frmAttrib = new addAttribute();
            frmAttrib.ShowDialog();
            
            XmlElement attribTarget = (XmlElement)xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString());

            XmlAttribute att = xmlDoc.CreateAttribute(frmAttrib.getAttribName());
            att.Value = frmAttrib.getAttribVal();

            attribTarget.Attributes.Append(att);              
            
        }


        private void tsbtnAddData_Click(object sender, EventArgs e)
        {
            // -- Create the Form to capture Node Data Information
            addData frm = new addData();                        
            XmlNode targNode;
            
            // -- Determine if the Selected Treeview Node is an Element or Data Node
            // -- If an Element, we use it's direct Xpath. Otherwise we use the Parent Xpath
            if (treeviewNodeType(xmlTreeview).Equals(XmlNodeType.Element))
                targNode = xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString());
            else
                targNode = xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Parent.Tag.ToString());
                        
            // -- Check if the Element already has data, if so, pass it to the new Form for editing
            if (targNode.InnerText.Length > 0)
                    frm.setData(targNode.InnerText);
            
            // -- Display the form
            frm.ShowDialog();

            // -- Check if Data has been changed, if so delete the old data node and append the new data
            if (!targNode.InnerText.Equals(frm.getData()) && !targNode.InnerText.Equals(""))
            {
                foreach (XmlNode n in targNode.ChildNodes)
                    if (n.NodeType.Equals(XmlNodeType.Text))
                        targNode.RemoveChild(n);     
                targNode.AppendChild(xmlDoc.CreateTextNode(frm.getData()));      
            }
            else
                targNode.AppendChild(xmlDoc.CreateTextNode(frm.getData()));   
        } // tsbtnAddData()
            
        

        private void tsbtnDelete_Click(object sender, EventArgs e) // change this to be more modular, i.e. use functions
        {
            if (xmlTreeview.SelectedNode.FullPath.ToString().EndsWith("]") && 
                !xmlFunctions.treeToXpath(xmlTreeview.SelectedNode.Parent.FullPath.ToString()).Contains("ATTRIBUTE"))
            {
                XmlNodeList nl = xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Parent.Tag.ToString()).ChildNodes;
                foreach (XmlNode n in nl)
                  if (n.NodeType == XmlNodeType.Text && n.Value.Equals(xmlTreeview.SelectedNode.Text.Replace("[","").Replace("]","")))
                          xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Parent.Tag.ToString()).RemoveChild(n);
            }
            else if (xmlTreeview.SelectedNode.FullPath.ToString().Contains("ATTRIBUTE")) // REMOVE ATTRIBUTE CODE
            {
                if (xmlTreeview.SelectedNode.Parent.FullPath.Contains("ATTRIBUTE"))
                     xmlDoc.SelectSingleNode(xmlTreeview
                 .SelectedNode.Parent.Parent.Tag.ToString())
                 .Attributes.RemoveNamedItem(xmlTreeview.SelectedNode.Parent.Text.Replace("ATTRIBUTE: ", ""));
                else
                      // if SelectedNode.parent doesn't contain attribute then you're on the attribute parent element!
                    xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Parent.Tag.ToString())
                      .Attributes.RemoveNamedItem(xmlTreeview.SelectedNode.Text.Replace("ATTRIBUTE: ", ""));
                }
            else if (xmlTreeview.SelectedNode.Tag.ToString().Contains("!--")) // if it's a comment, it has <!-- tags.
            {
                string cmt = xmlTreeview.SelectedNode.FullPath.ToString()
                    .Replace(xmlTreeview.SelectedNode.Parent.FullPath.ToString(), "")
                    .Replace("<!--", "")
                    .Replace("-->", "")
                    .Replace("\\", "");      
                
                foreach (XmlNode n in xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Parent.Tag.ToString()).ChildNodes)
                    if (n.Value == cmt && n.NodeType == XmlNodeType.Comment)
                        xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Parent.Tag.ToString()).RemoveChild(n);                
            }
            else if (xmlTreeview.SelectedNode.FullPath.ToString().Contains("CDATA")) // if it's a comment, it has <!-- tags.
            {
                string cdata = xmlTreeview.SelectedNode.FullPath.ToString()
                    .Replace(xmlTreeview.SelectedNode.Parent.FullPath.ToString(), "")
                    .Replace("CDATA: ", "")
                    .Replace("\\", "");

                foreach (XmlNode n in xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Parent.Tag.ToString()).ChildNodes)
                    if (n.Value == cdata && n.NodeType == XmlNodeType.CDATA)
                        xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Parent.Tag.ToString()).RemoveChild(n);
            }

            else
            {
                if (haveParent(xmlTreeview))
                {
                    XmlNodeList nl = xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Parent.Tag.ToString()).ChildNodes;
                    int test = calcIndex.calculateTreeIndex(xmlTreeview, xmlDoc);
                    switch (nl[test].NodeType)
                    {
                        case XmlNodeType.ProcessingInstruction:
                        case XmlNodeType.XmlDeclaration:
                            MessageBox.Show("xml dec!");
                            break;
                        case XmlNodeType.Element:
                            xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString())
                                .ParentNode.RemoveChild(xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString()));
                            break;
                        case XmlNodeType.CDATA:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Can't delete Root Element!");                    
                }
            }
            
           

        }

        private void tsbtnAddCDATA_Click(object sender, EventArgs e)
        {
            XmlNodeList nl = xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                .SelectedNode.FullPath.ToString()))
                .ParentNode.ChildNodes;


            addCdata frm = new addCdata();
            frm.ShowDialog();                               

            XmlCDataSection cdata = xmlDoc.CreateCDataSection(frm.getCdata());

            int i_cData = calcIndex.calculateTreeIndex(xmlTreeview, xmlDoc);

            if (nl[i_cData].NodeType.Equals(XmlNodeType.XmlDeclaration))
                i_cData++;

            nl[i_cData].AppendChild(cdata);
        
        }

        private void tsbtnComment_Click(object sender, EventArgs e)
        {   
            // -- Instanciate a new addComment form and display as a Dialogue box
            addComment frm = new addComment();
            frm.ShowDialog();

            // -- Create a new XmlComment and append it to the selected Element
            xmlDoc.SelectSingleNode(xmlTreeview
                .SelectedNode.Tag.ToString())
                .AppendChild(xmlDoc.CreateComment(frm.getComment()));   

        } // tsbtnComment_Click()

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // -- Close the application
            this.Close();
        }

        


    }
}
