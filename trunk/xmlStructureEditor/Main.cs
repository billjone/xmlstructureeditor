/*******************************************************************************
Class Main
 (c) 2009-2010 S. Dluzewski

 Info to add later..
*******************************************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; // Required for XML -> String conversions
using System.Xml; // Required for all XML operations. Built into dotNet2.0
using System.Xml.Schema;
using System.Reflection;
using System.Web;
using mshtml;


namespace xmlStructureEditor
{
    public partial class Main : Form
    {        
        XmlDocument xmlDoc; // Accessible xmlDocument
        enum ObjectType { Schema, Element, SimpleType, ComplexType, Attribute };
        private string prevLabel;
        private XmlSchema schemaDoc;

        public Main()
        {
            InitializeComponent();

            CreateRootNode();
            schemaTree.LabelEdit = true;
            schemaTree.BeforeLabelEdit += new NodeLabelEditEventHandler(PreEventLabelChanged);
            schemaTree.AfterLabelEdit += new NodeLabelEditEventHandler(EventLabelChanged);
            schemaTree.MouseDown += new MouseEventHandler(EventMouseDown);
            schemaDoc = new XmlSchema();
            CompileSchema();
            

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainTabs.SelectedIndex == 0)
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        xmlDoc.Load(openFileDialog.FileName);
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
            }
            else
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StreamReader sr = new StreamReader(openFileDialog.FileName);
                        schemaDoc = XmlSchema.Read(sr, null);
                        
                        sr.Close();
                        CompileSchema();
                        CreateRootNode();
                        DecodeSchema(schemaDoc, schemaTree.Nodes[0]);
                        schemaTree.ExpandAll();
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

            }


        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
           xmlDoc = new XmlDocument();
           updateXmlDisplays(null, null);
           Main_Load(null, null);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            InitializeTreeView(); // Function to control events on the XML/Schema treeview representations.
                                  // This treeview will control the entire toolbar availability functionality


            if (File.Exists("oldstate.xml"))
                File.Delete("oldstate.xml");

            xmlDoc = new XmlDocument(); // create in memory an empty, default xml document to work with.
                                        // any loaded documents will be placed here



            // -- Events to control updating xmlTree/RTB
            xmlDoc.NodeInserted += new XmlNodeChangedEventHandler(updateXmlDisplays);
            xmlDoc.NodeRemoved += new XmlNodeChangedEventHandler(updateXmlDisplays);
            xmlDoc.NodeChanged += new XmlNodeChangedEventHandler(updateXmlDisplays);

            mainTabs.SelectedIndexChanged += new EventHandler(mainTabs_SelectedIndexChanged);

            tsbtnAddAttribute.Enabled = false;
            tsbtnAddCDATA.Enabled = false;
            tsbtnAddElement.Enabled = false;
            tsbtnComment.Enabled = false;
            tsbtnAddData.Enabled = false;
            tsbtnDelete.Enabled = false;
            tsbtnRootElement.Enabled = true;




            popupTips.tipM m_mb = new popupTips.tipM();
            m_mb.Parent = this.toolStrip;
            m_mb.Title = "Step One";
            m_mb.TitleIcon = TooltipIcon.Info;
            m_mb.Text = "You must add a Root Element to begin!";

            popupTips.tipAlignment ba = (popupTips.tipAlignment)Enum.Parse(typeof(popupTips.tipAlignment), "TopLeft");
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

            tsStatusXpath.Text = "XPATH: " + xmlTreeview.SelectedNode.Tag.ToString();
            tsStatusElementType.Text = "Element Type: " + treeviewNodeType(xmlTreeview).ToString();
            

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
            handleUndo(xmlDoc);

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
            handleUndo(xmlDoc);

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
            handleUndo(xmlDoc);

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
            handleUndo(xmlDoc);

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
            handleUndo(xmlDoc);

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
            handleUndo(xmlDoc);

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
            if (xmlTreeview.Nodes.Count == 2)
            {               
                xmlDoc = new XmlDocument();
                updateXmlDisplays(null, null);
                Main_Load(null, null);
            }

            if (File.Exists("oldstate.xml"))
            {                
                xmlDoc.Load("oldstate.xml");
                File.Delete("oldstate.xml");                
            }
            else
                MessageBox.Show("No former state found!");

            
        }

             
        
        public class NodeElementType
        {
            // -- Constructor to populate Global Types for the Schema Editor
            public NodeElementType(string newNodeName, XmlSchemaObject newNodeType)
            {
                this.nodeName = newNodeName;
                this.nodeType = newNodeType;
            }

            public override string ToString() { return nodeName; }
            public string Get() { return nodeName; }
            public void Set(string name) { this.nodeName = name; }
            public string nodeName;
            public XmlSchemaObject nodeType;
        } // Class: NodeElementType

   


        private void MouseMenuAddAttribute_Click(object sender, EventArgs e)
        {
            // -- Create Schema and TreeNodes to Contain the Attribute
            TreeNode newNode = CreateNode("Attribute");
            XmlSchemaAttribute newSchemaAttrib = new XmlSchemaAttribute();            
            newSchemaAttrib.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            newSchemaAttrib.Name = "Attribute";
                        
            // -- Attribute can be added to root or Complex, so check if rootNode.
            if (newNode.Parent == schemaTree.Nodes[0])
            {
                addCollection(schemaDoc.Items, newSchemaAttrib, newNode, ObjectType.Attribute);
                comboGlobal.Items.Add(new NodeElementType(newSchemaAttrib.Name, newSchemaAttrib));
                comboGlobal.Enabled = false;
                comboSimple.SelectedItem = -1;
                comboSimple.Enabled = true;
                comboSimple.SelectedItem = "string";
            }
            else
            {
                // -- Can only be added to root or ComplexType Object
                XmlSchemaComplexType ct = newNode.Parent.Tag as XmlSchemaComplexType;
                if (ct == null)
                {
                    newNode.Parent.Nodes.Remove(newNode);
                    MessageBox.Show("Attributes cannot be added here!");
                }
                else
                {
                    addCollection(ct.Attributes, newSchemaAttrib, newNode, ObjectType.Attribute);
                    comboGlobal.SelectedItem = -1;
                    comboGlobal.Enabled = true;
                    comboSimple.SelectedItem = "string";
                    comboSimple.Enabled = true;
                }
            }
        } // MouseMenuAddAttribute_Click()
        

        private void MenuMouseAddEle_Click(object sender, EventArgs e)
        {
            TreeNode newNode = CreateNode("Element");
            XmlSchemaElement element = new XmlSchemaElement();
            element.Name = "Element";
            element.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");

            if (newNode.Parent == schemaTree.Nodes[0])
            {                
                addCollection(schemaDoc.Items, element, newNode, ObjectType.Element);
                comboGlobal.Items.Add(new NodeElementType(element.Name, element));
                comboGlobal.Enabled = true;
                comboSimple.SelectedItem = -1;
                comboSimple.Enabled = true;
                comboSimple.SelectedItem = "string";
            }
            else
            {                
                bool ret = AddToComplexType(newNode.Parent.Tag, element, newNode, ObjectType.Element);
                if (ret)
                {
                    comboGlobal.SelectedItem = -1;
                    comboGlobal.Enabled = true;
                    comboSimple.SelectedItem = "string";
                    comboSimple.Enabled = true;  
                }
                else
                {
                    newNode.Parent.Nodes.Remove(newNode);
                    MessageBox.Show("Cannot add an element to this item.");
                }
            }
        } // MenuMouseAddElement_Click()


        private void MenuMouseAddST_Click(object sender, EventArgs e)
        {
            TreeNode newSTNode = CreateNode("SimpleType");
            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            simpleType.Name = "SimpleType";
            XmlSchemaSimpleTypeRestriction restriction = new XmlSchemaSimpleTypeRestriction();
            restriction.BaseTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            simpleType.Content = restriction;
            if (newSTNode.Parent == schemaTree.Nodes[0])
            {
                addCollection(schemaDoc.Items, simpleType, newSTNode, ObjectType.SimpleType);
                comboGlobal.Items.Add(new NodeElementType(simpleType.Name, simpleType));
                comboGlobal.Enabled = false;
                comboSimple.SelectedItem = -1;
                comboSimple.Enabled = true;
                comboSimple.SelectedItem = "string";
            }
            else
            {
                XmlSchemaElement el = new XmlSchemaElement();
                el.Name = "SimpleType";
                el.SchemaType = simpleType;
                simpleType.Name = null;
                bool b_sF = AddToComplexType(newSTNode.Parent.Tag, el, newSTNode, ObjectType.SimpleType);
                if (!b_sF)
                {
                    newSTNode.Parent.Nodes.Remove(newSTNode);
                    MessageBox.Show("Cannot add a simple type to this item.");
                }
                else
                {
                    comboGlobal.SelectedItem = -1;
                    comboGlobal.Enabled = true;
                    comboSimple.SelectedItem = "string";
                    comboSimple.Enabled = true;
                }
            }
        }
        private void MenuMouseAddCT_Click(object sender, EventArgs e)
        {
            TreeNode newCTNode = CreateNode("ComplexType");

            XmlSchemaComplexType ct = new XmlSchemaComplexType();
            XmlSchemaSequence s = new XmlSchemaSequence();
            ct.Particle = s;
            ct.Name = "ComplexType";

            if (newCTNode.Parent == schemaTree.Nodes[0])
            {
                addCollection(schemaDoc.Items, ct, newCTNode, ObjectType.ComplexType);
                comboGlobal.Items.Add(new NodeElementType(ct.Name, ct));
                comboSimple.SelectedItem = -1;
                comboGlobal.SelectedItem = -1;
                comboSimple.Enabled = false;
                comboGlobal.Enabled = false;
            }
            else
            {
                XmlSchemaElement es = new XmlSchemaElement();
                es.Name = "ComplexType";
                es.SchemaType = ct;
                ct.Name = null;
                bool ret = AddToComplexType(newCTNode.Parent.Tag, es, newCTNode, ObjectType.ComplexType);
                if (!ret)
                {
                    newCTNode.Parent.Nodes.Remove(newCTNode);
                    MessageBox.Show("Cannot add a complex type to this item.");
                }
                else
                {
                    comboSimple.SelectedItem = -1;
                    comboGlobal.SelectedItem = -1;
                    comboSimple.Enabled = false;
                    comboGlobal.Enabled = false;
                }
            }
        }

        private void MenuMouseNew_Click(object sender, EventArgs e)
        {
            CreateRootNode();
            schemaDoc = new XmlSchema();
            CompileSchema();
        }

        private void mnuRemoveNode_Click(object sender, EventArgs e)
        {
            TreeNode tnParent = schemaTree.SelectedNode.Parent;
            XmlSchemaObject obj = schemaTree.SelectedNode.Tag as XmlSchemaObject;
            bool success = false;
            if ((tnParent != null) && (obj != null))
            {
                switch ((ObjectType)tnParent.ImageIndex)
                {
                    case ObjectType.Schema:                        
                            schemaDoc.Items.Remove(obj);
                            int idx = comboGlobal.FindStringExact(schemaTree.SelectedNode.Text);
                            if (idx != -1)
                                  comboGlobal.Items.RemoveAt(idx);
                            success = true;
                            break;
                    case ObjectType.SimpleType:
                            {
                                XmlSchemaSimpleType st = tnParent.Tag as XmlSchemaSimpleType;
                                if (obj is XmlSchemaAnnotation)
                                {
                                    st.Annotation.Items.Remove(obj);
                                    success = true;
                                }
                                else if (obj is XmlSchemaFacet)
                                {
                                    XmlSchemaSimpleTypeRestriction rest = st.Content as XmlSchemaSimpleTypeRestriction;
                                    if (rest != null)
                                    {
                                        rest.Facets.Remove(obj);
                                        success = true;
                                    }
                                }
                                break;
                            }
                    case ObjectType.ComplexType:
                        {
                        XmlSchemaComplexType ct = tnParent.Tag as XmlSchemaComplexType;
                        if (ct != null)
                        {
                            if (obj is XmlSchemaAttribute)
                            {
                                ct.Attributes.Remove(obj);
                                success = true;
                            }
                            else if (obj is XmlSchemaAnnotation)
                            {
                                ct.Annotation.Items.Remove(obj);
                                success = true;
                            }
                            else
                            {
                                XmlSchemaSequence seq = ct.Particle as XmlSchemaSequence;
                                if (seq != null)
                                {
                                    seq.Items.Remove(obj);
                                    success = true;
                                }
                            }
                        }
                            break;
                        }
                }
            }
            if (success)
            {
                tnParent.Nodes.Remove(schemaTree.SelectedNode);
                CompileSchema();
            }
            else        
                MessageBox.Show("Unable to remove this node");           
        }

                
        private void PreEventLabelChanged(object sender, NodeLabelEditEventArgs e)
        {
            prevLabel = schemaTree.SelectedNode.Text;
            if ((e.Node.Tag is XmlSchemaFacet) ||
                (e.Node.Tag is XmlSchemaAnnotation) ||
                (e.Node.Tag is XmlSchemaDocumentation) ||
                (e.Node.Tag is XmlSchemaAppInfo))
            {
                e.Node.EndEdit(false);
            }
        }

        private void EventLabelChanged(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node != null)            
                if (e.Label != null)                
                    UpdateTypeAndName(null, e.Label, null);
                    if (e.Node.Parent == schemaTree.Nodes[0])
                    {
                        int idx = comboGlobal.FindStringExact(prevLabel);
                        if (idx != -1)                       
                            comboGlobal.Items[idx] = new NodeElementType(e.Label, ((NodeElementType)comboGlobal.Items[idx]).nodeType);
                     
                    }
        }
        
        private void EventMouseDown(object sender, MouseEventArgs e)
        {
            TreeNode tn = schemaTree.GetNodeAt(e.X, e.Y);
            if (tn != null)
                   schemaTree.SelectedNode = tn;            
        }

        private void tvSchema_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            XmlSchemaObject obj = e.Node.Tag as XmlSchemaObject;
             if (obj != null)
            {

                XmlQualifiedName name = null;
                if (obj is XmlSchemaAttribute)
                    name = ((XmlSchemaAttribute)obj).SchemaTypeName;              
                else if (obj is XmlSchemaSimpleType)
                {
                     XmlSchemaSimpleTypeRestriction restriction = ((XmlSchemaSimpleType)obj).Content as XmlSchemaSimpleTypeRestriction;
                    if (restriction != null)                    
                        name = restriction.BaseTypeName;                    
                }
                else if (obj is XmlSchemaElement)
                {                 
                    XmlSchemaElement el = obj as XmlSchemaElement;
                    if (el.SchemaType is XmlSchemaSimpleType)
                    {
                        XmlSchemaSimpleType st = el.SchemaType as XmlSchemaSimpleType;
                        XmlSchemaSimpleTypeRestriction rest = st.Content as XmlSchemaSimpleTypeRestriction;
                        if (rest != null)                     
                            name = rest.BaseTypeName;                        
                    }
                    else
                    {
                        name = el.SchemaTypeName;
                        if (name.Name == "")                   
                            name = el.RefName;                        
                    }
                }
                if (name != null)
                {
                    int idx = comboSimple.FindStringExact(name.Name);
                    comboSimple.SelectedIndex = idx;
                    comboSimple.Enabled = true;
                    idx = comboGlobal.FindStringExact(name.Name);
                    comboGlobal.SelectedIndex = idx;
                    comboGlobal.Enabled = true;
                }
                else
                {
                    comboSimple.SelectedIndex = -1;
                    comboGlobal.SelectedIndex = -1;
                    comboSimple.Enabled = false;
                    comboGlobal.Enabled = false;
                }
            }
            else
            {
                comboSimple.SelectedIndex = -1;
                comboGlobal.SelectedIndex = -1;
                comboSimple.Enabled = false;
                comboGlobal.Enabled = false;
            }
        }
        private void cbSimpleTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSimple.SelectedIndex != -1)
            {
                UpdateTypeAndName(comboSimple.SelectedItem.ToString(), null, "http://www.w3.org/2001/XMLSchema");
                comboGlobal.SelectedIndex = -1;
            }
        }

        private void cbGlobalTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboGlobal.SelectedIndex != -1)
            {
                UpdateTypeAndName(comboGlobal.SelectedItem.ToString(), null, null);
                comboSimple.SelectedIndex = -1;
            }
        }

        
        

        private void mainTabs_SelectedIndexChanged(Object sender, EventArgs e)
        {


            if (mainTabs.SelectedIndex == 1)
            {
                toolStripLabel1.Visible = false;
                toolStripLabel2.Visible = false;
                toolStripLabel3.Visible = false;
                toolStripLabel4.Visible = false;
                toolStripLabel5.Visible = false;
                toolStripLabel6.Visible = false;
                toolStripLabel7.Visible = false;

                
                tsbtnAddAttribute.Visible = false;
                tsbtnAddCDATA.Visible = false;
                tsbtnAddElement.Visible = false;
                tsbtnComment.Visible = false;
                tsbtnAddData.Visible = false;
                tsbtnDelete.Visible = false;
                tsbtnRootElement.Visible = false;
            }
            else
            {
                toolStripLabel1.Visible = true;
                toolStripLabel2.Visible = true;
                toolStripLabel3.Visible = true;
                toolStripLabel4.Visible = true;
                toolStripLabel5.Visible = true;
                toolStripLabel6.Visible = true;
                toolStripLabel7.Visible = true;

                tsbtnRootElement.Visible = true;
                tsbtnAddAttribute.Visible = true;
                tsbtnAddCDATA.Visible = true;
                tsbtnAddElement.Visible = true;
                tsbtnComment.Visible = true;
                tsbtnAddData.Visible = true;
                tsbtnDelete.Visible = true;
            }

        }


        
        private void CreateRootNode()
        {
            schemaTree.Nodes.Clear();
            TreeNode rootNode = new TreeNode("Root");
            schemaTree.Nodes.Add(rootNode);
            rootNode.ImageIndex = (int)ObjectType.Schema;
            rootNode.SelectedImageIndex = (int)ObjectType.Schema;
            comboGlobal.Items.Clear();
        }

        private void SchemaValidationHandler(object sender, ValidationEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
        
        private void CompileSchema()
        {
            schemaDoc.Compile(new ValidationEventHandler(SchemaValidationHandler));            
            XmlTextWriter wr = new XmlTextWriter("CurrentDocument.xsd", Encoding.UTF8);
            schemaDoc.Write(wr);            
            wr.Close();
            this.schemaBrowser.Navigate(Application.StartupPath + "\\CurrentDocument.xsd");
        }

        private TreeNode CreateNode(string name)
        {
            TreeNode tn = schemaTree.SelectedNode;
            TreeNode newNode = new TreeNode(name);
            tn.Nodes.Add(newNode);
            tn.Expand();
            schemaTree.SelectedNode = newNode;
            newNode.BeginEdit();
            return newNode;
        }

        void addCollection(XmlSchemaObjectCollection coll, XmlSchemaObject obj, TreeNode node, ObjectType imgIdx)
        {
            coll.Add(obj);
            node.Tag = obj;
            node.ImageIndex = (int)imgIdx;
            node.SelectedImageIndex = (int)imgIdx;
            CompileSchema();
        }


        private XmlSchemaComplexType ChangeElementToComplexType(XmlSchemaElement el, TreeNode tn)
        {
            if (el.RefName != null)
            {
                el.Name = el.RefName.Name;
                el.RefName = null;
            }
            XmlSchemaComplexType ct = new XmlSchemaComplexType();
            el.SchemaType = ct;
            el.SchemaTypeName = null;
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            ct.Particle = sequence;
            tn.ImageIndex = (int)ObjectType.ComplexType;
            tn.SelectedImageIndex = (int)ObjectType.ComplexType;

            return ct;
        }

        private bool AddToComplexType(object obj, XmlSchemaObject item, TreeNode newNode, ObjectType imgIdx)
        {
            bool success = true;
            XmlSchemaElement el = obj as XmlSchemaElement;
            XmlSchemaComplexType complexType = obj as XmlSchemaComplexType;
            if (el != null)
            {
                XmlSchemaComplexType ct = el.SchemaType as XmlSchemaComplexType;
                if (ct != null)
                {
                    XmlSchemaSequence seq = ct.Particle as XmlSchemaSequence;
                    if (seq != null)
                        addCollection(seq.Items, item, newNode, imgIdx);
                    else
                    {
                        MessageBox.Show("Complex type is missing sequence!");
                        success = false;
                    }
                }
                else
                {
                    ct = ChangeElementToComplexType(el, newNode.Parent);
                    addCollection(((XmlSchemaSequence)ct.Particle).Items, item, newNode, imgIdx);
                }
            }
            else if (complexType != null)
            {
                XmlSchemaSequence seq = complexType.Particle as XmlSchemaSequence;
                if (seq != null)
                     addCollection(seq.Items, item, newNode, imgIdx);
                else
                {
                    MessageBox.Show("Complex type is missing sequence!");
                    success = false;
                }
            }
            else
                success = false;
            return success;
        }

        private void UpdateTypeAndName(string typeName, string name, string nameSpace)
        {
            XmlSchemaObject obj = schemaTree.SelectedNode.Tag as XmlSchemaObject;
            if (obj != null)
            {
                if (obj is XmlSchemaAttribute)
                {
                    XmlSchemaAttribute attrib = obj as XmlSchemaAttribute;
                    if (name != null)
                    {
                        attrib.Name = name;
                        CompileSchema();
                    }
                    if (typeName != null)
                    {
                        int idx = comboGlobal.FindStringExact(typeName);
                        NodeElementType glet = null;
                        if (idx != -1)
                        {
                            glet = comboGlobal.Items[idx] as NodeElementType;
                        }
                        if ((glet != null) && (glet.nodeType is XmlSchemaAttribute))
                        {
                            attrib.SchemaTypeName = null;
                            attrib.RefName = new XmlQualifiedName(typeName, nameSpace);
                            attrib.Name = null;
                        }
                        else
                        {
                            attrib.SchemaTypeName = new XmlQualifiedName(typeName, nameSpace);
                            attrib.RefName = null;
                            if (attrib.Name == null)
                            {
                                attrib.Name = schemaTree.SelectedNode.Text;
                            }
                        }
                        CompileSchema();
                        schemaTree.SelectedNode.Text = attrib.QualifiedName.Name;
                    }
                }
                else if (obj is XmlSchemaElement)
                {
                    XmlSchemaElement el = obj as XmlSchemaElement;
                    if (name != null)
                    {
                        el.Name = name;
                        CompileSchema();
                    }
                    
                    if (typeName != null)
                    {
                        if (el.SchemaType is XmlSchemaSimpleType)
                        {
                            XmlSchemaSimpleType st = el.SchemaType as XmlSchemaSimpleType;
                            XmlSchemaSimpleTypeRestriction rest = st.Content as XmlSchemaSimpleTypeRestriction;
                            if (rest != null)
                            {
                                rest.BaseTypeName = new XmlQualifiedName(typeName, nameSpace);
                                CompileSchema();
                            }
                        }
                        else
                        {
                            int idx = comboGlobal.FindStringExact(typeName);
                            NodeElementType glet = null;
                            if (idx != -1)
                                glet = comboGlobal.Items[idx] as NodeElementType;                            
                            if ((glet != null) && (glet.nodeType is XmlSchemaElement))
                            {
                                el.SchemaTypeName = null;
                                el.RefName = new XmlQualifiedName(typeName, nameSpace);
                                el.Name = null;
                            }
                            else
                            {
                                el.SchemaTypeName = new XmlQualifiedName(typeName, nameSpace);
                                el.RefName = null;
                                if (el.Name == null)
                                    el.Name = schemaTree.SelectedNode.Text;                                
                            }
                            CompileSchema();
                            schemaTree.SelectedNode.Text = el.QualifiedName.Name;
                        }
                    }
                }
                else if (obj is XmlSchemaSimpleType)
                {
                    XmlSchemaSimpleType st = obj as XmlSchemaSimpleType;
                    if (name != null)
                    {
                        st.Name = name;
                        CompileSchema();
                    }

                    if (typeName != null)
                    {
                        XmlSchemaSimpleTypeRestriction restriction = st.Content as XmlSchemaSimpleTypeRestriction;
                        if (restriction != null)
                        {
                            restriction.BaseTypeName = new XmlQualifiedName(typeName, nameSpace);
                            CompileSchema();
                            comboGlobal.SelectedIndex = -1;
                        }
                    }
                }
                else if (obj is XmlSchemaComplexType)
                {
                    if (name != null)
                    {
                        ((XmlSchemaComplexType)obj).Name = name;
                        CompileSchema();
                    }
                }
            }
        }

        public static XmlNode[] TextToNodeArray(string text)
        {
            XmlDocument doc = new XmlDocument();
            return new XmlNode[1] { doc.CreateTextNode(text) };
        }


        private void DecodeSchema(XmlSchema schema, TreeNode node)
        {
            try
            {
                DecSchem(schema, node);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private TreeNode DecSchem(XmlSchemaObject obj, TreeNode node)
        {
            TreeNode newNode = node;

            XmlSchemaAnnotation annot = obj as XmlSchemaAnnotation;
            XmlSchemaAttribute attrib = obj as XmlSchemaAttribute;
            XmlSchemaFacet facet = obj as XmlSchemaFacet;
            XmlSchemaDocumentation doc = obj as XmlSchemaDocumentation;
            XmlSchemaAppInfo appInfo = obj as XmlSchemaAppInfo;
            XmlSchemaElement element = obj as XmlSchemaElement;
            XmlSchemaSimpleType simpleType = obj as XmlSchemaSimpleType;
            XmlSchemaComplexType complexType = obj as XmlSchemaComplexType;

            if (node == schemaTree.Nodes[0])
            {
                if (attrib != null)                
                    if (attrib.Name != null)                  
                        comboGlobal.Items.Add(new NodeElementType(attrib.Name, attrib)); 
                else if (element != null)                
                    if (element.Name != null)                   
                        comboGlobal.Items.Add(new NodeElementType(element.Name, element));
                else if (simpleType != null)                
                    if (simpleType.Name != null)                    
                        comboGlobal.Items.Add(new NodeElementType(simpleType.Name, simpleType));
                else if (complexType != null)                
                    if (complexType.Name != null)  
                        comboGlobal.Items.Add(new NodeElementType(complexType.Name, complexType));
            }

            if (annot != null)
            {
                newNode = new TreeNode("--annotation--");
                newNode.Tag = annot;
                newNode.ImageIndex = 4;
                newNode.SelectedImageIndex = 4;
                node.Nodes.Add(newNode);
                foreach (XmlSchemaObject schemaObject in annot.Items)
                {
                    DecSchem(schemaObject, newNode);
                }
            }
            else
                if (attrib != null)
                {
                    newNode = new TreeNode(attrib.QualifiedName.Name);
                    newNode.Tag = attrib;
                    newNode.ImageIndex = 7;
                    newNode.SelectedImageIndex = 7;
                    node.Nodes.Add(newNode);
                }
                else
                    if (facet != null)
                    {
                        newNode = new TreeNode(facet.ToString());
                        newNode.Tag = facet;
                        newNode.ImageIndex = 8;
                        newNode.SelectedImageIndex = 8;
                        node.Nodes.Add(newNode);
                    }
                    else
                        if (doc != null)
                        {
                            newNode = new TreeNode("--documentation--");
                            newNode.Tag = doc;
                            newNode.ImageIndex = 5;
                            newNode.SelectedImageIndex = 5;
                            node.Nodes.Add(newNode);
                        }
                        else
                            if (appInfo != null)
                            {
                                newNode = new TreeNode("--app info--");
                                newNode.Tag = annot;
                                newNode.ImageIndex = 6;
                                newNode.SelectedImageIndex = 6;
                                node.Nodes.Add(newNode);
                            }
                            else
                                if (element != null)
                                {
                                    XmlSchemaSimpleType st = element.SchemaType as XmlSchemaSimpleType;
                                    XmlSchemaComplexType ct = element.SchemaType as XmlSchemaComplexType;

                                    if (st != null)
                                    {
                                        TreeNode node2 = DecSchem(st, newNode);
                                        node2.Text = element.Name;
                                    }
                                    else if (ct != null)
                                    {
                                        TreeNode node2 = DecSchem(ct, newNode);
                                        node2.Text = element.Name;
                                    }
                                    else
                                    {
                                        newNode = new TreeNode(element.QualifiedName.Name);
                                        newNode.Tag = element;
                                        newNode.ImageIndex = 1;
                                        newNode.SelectedImageIndex = 1;
                                        node.Nodes.Add(newNode);
                                    }
                                }
                                else
                                    if (simpleType != null)
                                    {
                                        newNode = new TreeNode(simpleType.QualifiedName.Name);
                                        newNode.Tag = simpleType;
                                        newNode.ImageIndex = 2;
                                        newNode.SelectedImageIndex = 2;
                                        node.Nodes.Add(newNode);
                                        XmlSchemaSimpleTypeRestriction rest = simpleType.Content as XmlSchemaSimpleTypeRestriction;
                                        if (rest != null)
                                        {
                                            foreach (XmlSchemaFacet schemaObject in rest.Facets)
                                            {
                                                DecSchem(schemaObject, newNode);
                                            }
                                        }
                                    }
                                    else
                                        if (complexType != null)
                                        {
                                            newNode = new TreeNode(complexType.Name);
                                            newNode.Tag = complexType;
                                            newNode.ImageIndex = 3;
                                            newNode.SelectedImageIndex = 3;
                                            node.Nodes.Add(newNode);

                                            XmlSchemaSequence seq = complexType.Particle as XmlSchemaSequence;
                                            if (seq != null)
                                            {
                                                foreach (XmlSchemaObject schemaObject in seq.Items)
                                                {
                                                    DecSchem(schemaObject, newNode);
                                                }
                                            }
                                        }

            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                if (property.PropertyType.FullName == "System.Xml.Schema.XmlSchemaObjectCollection")
                {
                    XmlSchemaObjectCollection childObjectCollection = (XmlSchemaObjectCollection)property.GetValue(obj, null);
                    foreach (XmlSchemaObject schemaObject in childObjectCollection)
                    {
                        DecSchem(schemaObject, newNode);
                    }
                }
            }
            return newNode;
        }

        private void xmlBrowserWindow_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // code to auto highlight                    
          
        }        


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainTabs.SelectedIndex == 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML File|*.xml";
                saveFileDialog.Title = "Save an XML File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    XmlTextWriter writer = new XmlTextWriter(saveFileDialog.OpenFile(), null);
                    writer.Formatting = Formatting.Indented;
                    xmlDoc.Save(writer);

                }
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XSD File|*.XSD";
                saveFileDialog.Title = "Save an XSD File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    CompileSchema();
                    StreamWriter sw = new StreamWriter(saveFileDialog.FileName.ToString(), false, System.Text.Encoding.UTF8);                    
                    schemaDoc.Write(sw);
                    sw.Flush();
                    sw.Close();
                }            
            }

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xmlBrowserWindow.Print();            
        }

        private void handleUndo(XmlDocument uState)
        {
            // -- Write old state to disk
            XmlTextWriter writer = new XmlTextWriter("oldstate.xml", null);
            writer.Formatting = Formatting.Indented;
            uState.Save(writer);
            writer.Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainTabs.SelectedIndex == 0)
            {               
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML File|*.xml";
                saveFileDialog.Title = "Save an XML File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    XmlTextWriter writer = new XmlTextWriter(saveFileDialog.OpenFile(), null);
                    writer.Formatting = Formatting.Indented;
                    xmlDoc.Save(writer);

                }
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XSD File|*.XSD";
                saveFileDialog.Title = "Save an XSD File";
                saveFileDialog.ShowDialog();
                
                if (saveFileDialog.FileName != "")
                {
                    CompileSchema();
                    StreamWriter sw = new StreamWriter(saveFileDialog.FileName.ToString(), false, System.Text.Encoding.UTF8);
                    schemaDoc.Write(sw);
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox abtBox = new AboutBox();
            abtBox.ShowDialog();
        }


        private void validateXMLFromSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValidateXml();
        
        
        }

        public void ValidateXml()
        {
            XmlTextWriter writer = new XmlTextWriter("temp.xml", null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            writer.Close();

            CompileSchema();
            StreamWriter sw = new StreamWriter("temp.xsd", false, System.Text.Encoding.UTF8);
            schemaDoc.Write(sw);
            sw.Flush();
            sw.Close();

            XmlTextReader r = new XmlTextReader("temp.xml");
            XmlValidatingReader validator = new XmlValidatingReader(r);
            validator.ValidationType = ValidationType.Schema;

            XmlSchemaCollection schemas = new XmlSchemaCollection();
            try
            {
                schemas.Add(null, "temp.xsd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            validator.Schemas.Add(schemas);

            validator.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);

            try
            {
                while (validator.Read())
                { }
            }
            catch (XmlException err)
            {
                this.txtOutput.Text += err + "\n";
            }
            finally
            {
                validator.Close();
            }
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs args)
        {
            txtOutput.Text += args.Message + "\n";            
        }

        



    }
}
