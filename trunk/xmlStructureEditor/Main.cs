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
        XmlDocument undoDoc; // Undo Functionality Document
        
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

            tsbtnAddAttribute.Enabled = false;
            tsbtnAddCDATA.Enabled = false;
            tsbtnAddElement.Enabled = false;
            tsbtnComment.Enabled = false;
            tsbtnAddData.Enabled = false;
            tsbtnDelete.Enabled = false;
            tsbtnRootElement.Enabled = true;




            MessageBalloon m_mb = new MessageBalloon();
            m_mb.Parent = this.toolStrip;
            m_mb.Title = "Step One";
            m_mb.TitleIcon = TooltipIcon.Info;
            m_mb.Text = "You must add a Root Element to begin!";

            BalloonAlignment ba = (BalloonAlignment)Enum.Parse(typeof(BalloonAlignment), "TopLeft");
            m_mb.Align = ba;
            m_mb.CenterStem = true;
            m_mb.UseAbsolutePositioning = false;
            m_mb.Show();
	
            
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

            XmlNodeType currentSelType = treeviewNodeType(xmlTreeview);

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
                return;
            }
            if (currentSelType.Equals(XmlNodeType.Attribute))
            {                
                tsbtnDelete.Enabled = true;
                return;
            }
            if (currentSelType.Equals(XmlNodeType.CDATA))
            {
                tsbtnAddCDATA.Enabled = true;
                tsbtnDelete.Enabled = true;
                return;
            }
            if (currentSelType.Equals(XmlNodeType.Text))
            {
                tsbtnAddData.Enabled = true;
                tsbtnDelete.Enabled = true;
                return;
            }
            if (currentSelType.Equals(XmlNodeType.Comment))
            {
                tsbtnComment.Enabled = true;
                tsbtnDelete.Enabled = true;
                return;
            }

            
        }

        XmlNodeType treeviewNodeType(TreeView tview)
        {
            // -- Calculate TreeView Node as an XmlNodeType in an XmlDocument
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

            // -- Return Notation as overflow, error handler essentially
            return XmlNodeType.Notation;
            
        } // treeviewNodeType()

        private void tsbtnRootElement_Click(object sender, EventArgs e)
        {
            // -- Prepare Undo State
            undoDoc = xmlDoc;

            // -- Instanciate a form to get the root element information
            addRootElement frmGetRootName = new addRootElement();
            frmGetRootName.ShowDialog();            
                        
            try
            {
                // -- Create the Root Element and the XML Declaration Entity
                XmlElement root = xmlDoc.CreateElement(frmGetRootName.getRootElement());
                XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", null, null);
                
                // -- Append both to the XML Document, Declaration must come first!
                xmlDoc.AppendChild(dec);
                xmlDoc.AppendChild(root);

                // -- Append a generic creation date textnode, for display purposes.
                root.AppendChild(xmlDoc.CreateTextNode("CreationDate: " + DateTime.Today.ToString()));

                // -- Disable the ability to add a root element
                tsbtnRootElement.Enabled = false;  
            }
            catch (Exception ex)
            {
                // -- Catch execution errors
                MessageBox.Show(ex.ToString());
            }                     
        } // tsbtnRootElement_Click()

        
        private void tsbtnAddElement_Click(object sender, EventArgs e)
        {
            // -- Prepare Undo State
            undoDoc = xmlDoc;

            // -- Instanciate the Element Form and display it
            addElement frmElement = new addElement();                              
            frmElement.ShowDialog();
      

            // -- Generate a Nodelist
             XmlNodeList nl = xmlDoc.SelectSingleNode(xmlTreeview
               .SelectedNode.Tag.ToString())
               .ParentNode.ChildNodes;
            
            // -- Calculate the TreeIndex, then Append the new Element based on it
            int decC = calcIndex.calculateTreeIndex(xmlTreeview, xmlDoc);
            if (nl[decC].NodeType.Equals(XmlNodeType.XmlDeclaration))
                decC++;
            nl[decC].AppendChild(xmlDoc.CreateElement(frmElement.getElementName()));                  
        } // tsBtnAddElement_Click()


      

      

        private void tsbAddAttribute_Click(object sender, EventArgs e)
        {
            // -- Prepare Undo State
            undoDoc = xmlDoc;

            // -- Store the targeted Element
            XmlElement attribTarget = (XmlElement)xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString());

            // -- Instanciate an Attribute Addition Form
            addAttribute frmAttrib = new addAttribute();
            frmAttrib.ShowDialog();
            
            

            // -- Create an Attribute to Append, based on the values given in the form.
            XmlAttribute att = xmlDoc.CreateAttribute(frmAttrib.getAttribName());
            att.Value = frmAttrib.getAttribVal();

            // -- Append the Attribute
            attribTarget.Attributes.Append(att);              
            
        } // tsAddAttribute_Click()


        private void tsbtnAddData_Click(object sender, EventArgs e)
        {
            // -- Prepare Undo State
            undoDoc = xmlDoc;

            // -- Create the Form to capture Node Data Information
            addData frm = new addData();                        
            XmlNode targNode;
            
            // -- Determine if the Selected Treeview Node is an Element or Data Node
            // -- If an Element, we use it's direct Xpath. Otherwise we use the Parent Xpath
            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        } // tsbtnAddData()
            
        

        private void tsbtnDelete_Click(object sender, EventArgs e) // change this to be more modular, i.e. use functions
        {
            // -- Prepare Undo State
            undoDoc = xmlDoc;

            try
            {
                if (xmlTreeview.SelectedNode.FullPath.ToString().EndsWith("]") &&
                    !xmlFunctions.treeToXpath(xmlTreeview.SelectedNode.Parent.FullPath.ToString()).Contains("ATTRIBUTE"))
                {
                    XmlNodeList nl = xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Parent.Tag.ToString()).ChildNodes;
                    foreach (XmlNode n in nl)
                        if (n.NodeType == XmlNodeType.Text && n.Value.Equals(xmlTreeview.SelectedNode.Text.Replace("[", "").Replace("]", "")))
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void tsbtnAddCDATA_Click(object sender, EventArgs e)
        {
            // -- Prepare Undo State
            undoDoc = xmlDoc;

            try
            {
                // -- Generate a nodelist for the selected Treeview Element

                XmlNodeList nl = xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString())
                  .ParentNode.ChildNodes;

                // -- Display the add CDATA form
                addCdata frm = new addCdata();
                frm.ShowDialog();

                int i_cData = calcIndex
        .calculateTreeIndex(xmlTreeview, xmlDoc);

                // -- If an XmlDeclaration is in the NodeList, adjust the Index count
                if (nl[i_cData].NodeType
                    .Equals(XmlNodeType.XmlDeclaration))
                    i_cData++;

                // -- Append the CDATA to the XmlDocument
                nl[i_cData].AppendChild(xmlDoc
                    .CreateCDataSection(frm.getCdata()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        
        } // tsbtnAddCDATA_Click()


        private void tsbtnComment_Click(object sender, EventArgs e)
        {
            // -- Prepare Undo State
            undoDoc = xmlDoc;

            // -- Instanciate a new addComment form and display as a Dialogue box
            addComment frm = new addComment();
            frm.ShowDialog();

            try
            {
                // -- Create a new XmlComment and append it to the selected Element
                xmlDoc.SelectSingleNode(xmlTreeview
                    .SelectedNode.Tag.ToString())
                    .AppendChild(xmlDoc.CreateComment(frm.getComment()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        } // tsbtnComment_Click()


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // -- Close the application
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // -- Undo.

            if (xmlDoc == undoDoc)
                MessageBox.Show("Same!");
        }


        #region Schema
        public class GlobalElementType
        {
            public GlobalElementType(string name, XmlSchemaObject type)
            {
                this.name = name;
                this.type = type;
            }

            public override string ToString()
            {
                return name;
            }

            public string Get()
            {
                return name;
            }

            public void Set(string name)
            {
                this.name = name;
            }

            public string name;
            public XmlSchemaObject type;
        }






        


    }
}
