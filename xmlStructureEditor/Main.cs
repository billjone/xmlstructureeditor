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
        XmlDocument undoDoc; // Undo Functionality Document
        enum TreeViewImages { Schema, Element, SimpleType, ComplexType, Annotation, Documentation, AppInfo, Attribute, Facet };
        private string prevLabel;
        private XmlSchema schema;

        public Main()
        {
            InitializeComponent();

            CreateRootNode();
            tvSchema.LabelEdit = true;
            tvSchema.KeyUp += new KeyEventHandler(EventTreeViewKeyUp);
            tvSchema.BeforeLabelEdit += new NodeLabelEditEventHandler(PreEventLabelChanged);
            tvSchema.AfterLabelEdit += new NodeLabelEditEventHandler(EventLabelChanged);
            tvSchema.MouseDown += new MouseEventHandler(EventMouseDown);
            schema = new XmlSchema();
            CompileSchema();
            

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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




            BalloonPopUp.MessageBalloon m_mb = new BalloonPopUp.MessageBalloon();
            m_mb.Parent = this.toolStrip;
            m_mb.Title = "Step One";
            m_mb.TitleIcon = TooltipIcon.Info;
            m_mb.Text = "You must add a Root Element to begin!";

            BalloonPopUp.BalloonAlignment ba = (BalloonPopUp.BalloonAlignment)Enum.Parse(typeof(BalloonPopUp.BalloonAlignment), "TopLeft");
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


        #region SCHEMA!!!

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

        #region Menu Commands

        #region Add Facets
        // There are several types of facets that can be added to a simple type.
        // This code adds a facet and specifies a default value (completely arbitrary)

        private void mnuFacetEnumeration_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaEnumerationFacet(), "1", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }

        private void mnuFacetMaxExclusive_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaMaxExclusiveFacet(), "101", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }

        private void mnuFacetMaxInclusive_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaMaxInclusiveFacet(), "100", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }

        private void mnuFacetMinExclusive_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaMinExclusiveFacet(), "0", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }

        private void mnuFacetMinInclusive_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaMinInclusiveFacet(), "1", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }

        private void mnuFacetFractionDigits_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaFractionDigitsFacet(), "2", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }

        private void mnuFacetLength_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaLengthFacet(), "16", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }

        private void mnuFacetMaxLength_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaMaxLengthFacet(), "16", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }

        private void mnuFacetMinLength_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaMinLengthFacet(), "1", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }

        private void mnuFacetTotalDigits_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaTotalDigitsFacet(), "8", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }


        private void mnuFacetPattern_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaPatternFacet(), "[0-9]", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }

        private void mnuFacetWhiteSpace_Click(object sender, System.EventArgs e)
        {
            bool ret = AddFacet(new XmlSchemaWhiteSpaceFacet(), "collapse", TreeViewImages.Facet);
            if (!ret)
            {
                MessageBox.Show("Cannot add an element to this item.");
            }
        }
        #endregion

        #region Add Annotation, Documentation, and AppInfo
        // create an annotation for the schema or for an annotatable object
        private void mnuAddAnnotation_Click(object sender, System.EventArgs e)
        {
            XmlSchemaAnnotation annot = new XmlSchemaAnnotation();
            TreeNode newNode = CreateNode("--annotation--");

            // if adding at the schema root level...
            if (newNode.Parent == tvSchema.Nodes[0])
            {
                AddToCollection(schema.Items, annot, newNode, TreeViewImages.Annotation);
            }
            else
            {
                // otherwise, get the object as an XmlSchemaAnnotated type
                XmlSchemaAnnotated obj = newNode.Parent.Tag as XmlSchemaAnnotated;
                if (obj != null)
                {
                    // if it exists, add the annotation
                    obj.Annotation = annot;
                    newNode.Tag = annot;
                    newNode.ImageIndex = (int)TreeViewImages.Annotation;
                    newNode.SelectedImageIndex = (int)TreeViewImages.Annotation;
                    CompileSchema();
                }
                else
                {
                    // otherwise, tell the user annotation cannot be added to this type.
                    newNode.Parent.Nodes.Remove(newNode);
                    MessageBox.Show("Cannot create an annotation for this item.");
                }
            }
        }

        private void mnuAddDocumentation_Click(object sender, System.EventArgs e)
        {
            bool success = false;
            XmlSchemaAnnotation annot = tvSchema.SelectedNode.Tag as XmlSchemaAnnotation;
            // if the parent node is an annotation type...
            if (annot != null)
            {
                // ...then add the documentation type
                TreeNode newNode = CreateNode("--documentation--");
                XmlSchemaDocumentation doc = new XmlSchemaDocumentation();
                doc.Markup = TextToNodeArray("Documentation");
                annot.Items.Add(doc);
                newNode.Tag = doc;
                newNode.ImageIndex = (int)TreeViewImages.Annotation;
                newNode.SelectedImageIndex = (int)TreeViewImages.Annotation;
                CompileSchema();
                success = true;
            }
            if (!success)
            {
                // otherwise create the annotation type first
                mnuAddAnnotation_Click(sender, e);
                annot = tvSchema.SelectedNode.Tag as XmlSchemaAnnotation;
                // if successful in adding the annotation type, add the documentation type now
                if (annot != null)
                {
                    mnuAddDocumentation_Click(sender, e);
                }
            }
        }

        private void mnuAddAppInfo_Click(object sender, System.EventArgs e)
        {
            bool success = false;
            XmlSchemaAnnotation annot = tvSchema.SelectedNode.Tag as XmlSchemaAnnotation;
            // if the parent node is an annotation type...
            if (annot != null)
            {
                // ...then add the AppInfo type
                TreeNode newNode = CreateNode("--app info--");
                XmlSchemaAppInfo doc = new XmlSchemaAppInfo();
                doc.Markup = TextToNodeArray("Application Information");
                annot.Items.Add(doc);
                newNode.Tag = doc;
                newNode.ImageIndex = (int)TreeViewImages.Annotation;
                newNode.SelectedImageIndex = (int)TreeViewImages.Annotation;
                CompileSchema();
                success = true;
            }
            if (!success)
            {
                // otherwise create the annotation type first
                mnuAddAnnotation_Click(sender, e);
                annot = tvSchema.SelectedNode.Tag as XmlSchemaAnnotation;
                // if successful in creating the annotation type, add the AppInfo type now
                if (annot != null)
                {
                    mnuAddAppInfo_Click(sender, e);
                }
            }
        }

        #endregion

        #region Add Attribute
        // Can only be added to schema root as a global or complex type.
        // For complex types, the attribute can reference a global type.
        // An optional type can be specified referring to a simple type.
        // If the type isn't used, the attribute must define a simple type.
        private void mnuAddAttribute_Click(object sender, System.EventArgs e)
        {
            TreeNode newNode = CreateNode("Attribute");
            XmlSchemaAttribute attrib = new XmlSchemaAttribute();
            attrib.Name = "Attribute";
            attrib.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");

            // root node?
            if (newNode.Parent == tvSchema.Nodes[0])
            {
                // add to root of schema.  An global attribute cannot reference a global type
                AddToCollection(schema.Items, attrib, newNode, TreeViewImages.Attribute);
                cbGlobalTypes.Items.Add(new GlobalElementType(attrib.Name, attrib));
                cbGlobalTypes.Enabled = false;
                cbSimpleTypes.SelectedItem = -1;
                cbSimpleTypes.Enabled = true;
                cbSimpleTypes.SelectedItem = "string";
            }
            else
            {
                // an attribute can only be added to a complex type
                XmlSchemaComplexType ct = newNode.Parent.Tag as XmlSchemaComplexType;
                if (ct != null)
                {
                    // and can reference a global attribute, so enable the global combobox
                    AddToCollection(ct.Attributes, attrib, newNode, TreeViewImages.Attribute);
                    cbGlobalTypes.SelectedItem = -1;
                    cbGlobalTypes.Enabled = true;
                    cbSimpleTypes.SelectedItem = "string";
                    cbSimpleTypes.Enabled = true;
                }
                else
                {
                    newNode.Parent.Nodes.Remove(newNode);
                    MessageBox.Show("Cannot create an attribute for this item.");
                }
            }
        }
        #endregion

        #region Add Element
        // An element can be added to the schema or to a complex type.
        // If adding to an element, convert the element to a complex type.
        // An element can be added to a global complex type or to a local complex type.
        private void mnuAddElement_Click(object sender, System.EventArgs e)
        {
            TreeNode newNode = CreateNode("Element");
            XmlSchemaElement element = new XmlSchemaElement();
            element.Name = "Element";
            element.SchemaTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");

            // root node?
            if (newNode.Parent == tvSchema.Nodes[0])
            {
                // add to root of schema.  A global element can reference another global type
                AddToCollection(schema.Items, element, newNode, TreeViewImages.Element);
                cbGlobalTypes.Items.Add(new GlobalElementType(element.Name, element));
                cbGlobalTypes.Enabled = true;
                cbSimpleTypes.SelectedItem = -1;
                cbSimpleTypes.Enabled = true;
                cbSimpleTypes.SelectedItem = "string";
            }
            else
            {
                // otherwise, an element must be part of a complex type
                bool ret = AddToComplexType(newNode.Parent.Tag, element, newNode, TreeViewImages.Element);
                if (!ret)
                {
                    // not successful
                    newNode.Parent.Nodes.Remove(newNode);
                    MessageBox.Show("Cannot add an element to this item.");
                }
                else
                {
                    // successful
                    cbGlobalTypes.SelectedItem = -1;
                    cbGlobalTypes.Enabled = true;
                    cbSimpleTypes.SelectedItem = "string";
                    cbSimpleTypes.Enabled = true;
                }
            }
        }
        #endregion

        #region Add Simple Type
        // A simple type can be added as a global type or as a local member of a complex type.
        // A simple type cannot contain elements or attributes.
        // Simple types are derived from basic types or other simple types.
        // The complex type can either be a global definition or a local complex type.
        // When adding to a complex type, the simple type must be wrapped in an element.
        private void mnuAddSimpleType_Click(object sender, System.EventArgs e)
        {
            TreeNode newNode = CreateNode("SimpleType");

            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
            simpleType.Name = "SimpleType";
            XmlSchemaSimpleTypeRestriction restriction = new XmlSchemaSimpleTypeRestriction();
            restriction.BaseTypeName = new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
            simpleType.Content = restriction;

            // root node?
            if (newNode.Parent == tvSchema.Nodes[0])
            {
                // add to root of schema.  A global simple type cannot reference another global
                AddToCollection(schema.Items, simpleType, newNode, TreeViewImages.SimpleType);
                cbGlobalTypes.Items.Add(new GlobalElementType(simpleType.Name, simpleType));
                cbGlobalTypes.Enabled = false;
                cbSimpleTypes.SelectedItem = -1;
                cbSimpleTypes.Enabled = true;
                cbSimpleTypes.SelectedItem = "string";
            }
            else
            {
                // if not a root node, then it must be created as an element of simple type...
                XmlSchemaElement el = new XmlSchemaElement();
                el.Name = "SimpleType";
                el.SchemaType = simpleType;
                simpleType.Name = null;
                // ...as a subnode to a complex type
                bool ret = AddToComplexType(newNode.Parent.Tag, el, newNode, TreeViewImages.SimpleType);
                if (!ret)
                {
                    // failure
                    newNode.Parent.Nodes.Remove(newNode);
                    MessageBox.Show("Cannot add a simple type to this item.");
                }
                else
                {
                    // success.  Can reference a global simple type
                    cbGlobalTypes.SelectedItem = -1;
                    cbGlobalTypes.Enabled = true;
                    cbSimpleTypes.SelectedItem = "string";
                    cbSimpleTypes.Enabled = true;
                }
            }
        }
        #endregion

        #region Add Complex Type
        // A complex type can be added as a global type to the schema or as a complex element.
        // If adding to an element, change the element to a complex type.
        // When adding to a complex type, the complex type must be wrapped in an element.
        private void mnuAddComplexType_Click(object sender, System.EventArgs e)
        {
            TreeNode newNode = CreateNode("ComplexType");

            XmlSchemaComplexType complexType = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            complexType.Particle = sequence;
            complexType.Name = "ComplexType";

            // root node?
            if (newNode.Parent == tvSchema.Nodes[0])
            {
                // add to root.  A complex type has no type reference information.
                AddToCollection(schema.Items, complexType, newNode, TreeViewImages.ComplexType);
                cbGlobalTypes.Items.Add(new GlobalElementType(complexType.Name, complexType));
                cbSimpleTypes.SelectedItem = -1;
                cbGlobalTypes.SelectedItem = -1;
                cbSimpleTypes.Enabled = false;
                cbGlobalTypes.Enabled = false;
            }
            else
            {
                // if not at root, create an element of complex type...
                XmlSchemaElement el = new XmlSchemaElement();
                el.Name = "ComplexType";
                el.SchemaType = complexType;
                complexType.Name = null;
                // ...that is added as a subnode to a complex type.
                bool ret = AddToComplexType(newNode.Parent.Tag, el, newNode, TreeViewImages.ComplexType);
                if (!ret)
                {
                    // failure
                    newNode.Parent.Nodes.Remove(newNode);
                    MessageBox.Show("Cannot add a complex type to this item.");
                }
                else
                {
                    // success.  No type is referenced.
                    cbSimpleTypes.SelectedItem = -1;
                    cbGlobalTypes.SelectedItem = -1;
                    cbSimpleTypes.Enabled = false;
                    cbGlobalTypes.Enabled = false;
                }
            }
        }
        #endregion

        #region New
        private void mnuNew_Click(object sender, System.EventArgs e)
        {
            CreateRootNode();		// create a new treeview root node
            schema = new XmlSchema();	// create a new schema
            CompileSchema();		// compile and show it
        }
        #endregion








        #region Remove Node
        // The parent type determines from what list the selected item must be removed.
        // Use the image index in the tree view to figure out the parent type.
        private void mnuRemoveNode_Click(object sender, System.EventArgs e)
        {
            TreeNode tnParent = tvSchema.SelectedNode.Parent;
            XmlSchemaObject obj = tvSchema.SelectedNode.Tag as XmlSchemaObject;
            bool success = false;
            // if the node to remove has a parent and is of an XmlSchemaObject type...
            if ((tnParent != null) && (obj != null))
            {
                // look at the tree node image index to figure out what the parent is!
                switch ((TreeViewImages)tnParent.ImageIndex)
                {
                    // if the parent is the schema root:
                    case TreeViewImages.Schema:
                        {
                            // remove the object from the schema and from the global list
                            schema.Items.Remove(obj);
                            int idx = cbGlobalTypes.FindStringExact(tvSchema.SelectedNode.Text);
                            if (idx != -1)
                            {
                                cbGlobalTypes.Items.RemoveAt(idx);
                            }
                            success = true;
                            break;
                        }

                    // if the parent is an annotation type
                    case TreeViewImages.Annotation:
                        {
                            XmlSchemaAnnotation annot = tnParent.Tag as XmlSchemaAnnotation;
                            if (annot != null)
                            {
                                annot.Items.Remove(obj);
                                success = true;
                            }
                            break;
                        }

                    // if the parent is a simple type
                    case TreeViewImages.SimpleType:
                        {
                            // a simple type can have an annotation or a facet type as children
                            XmlSchemaSimpleType st = tnParent.Tag as XmlSchemaSimpleType;
                            if (obj is XmlSchemaAnnotation)
                            {
                                // remove from annotation list if it's an annotation type
                                st.Annotation.Items.Remove(obj);
                                success = true;
                            }
                            else if (obj is XmlSchemaFacet)
                            {
                                XmlSchemaSimpleTypeRestriction rest = st.Content as XmlSchemaSimpleTypeRestriction;
                                if (rest != null)
                                {
                                    // remove from facet list if it's a facet type
                                    rest.Facets.Remove(obj);
                                    success = true;
                                }
                            }
                            break;
                        }

                    // if the parent is a complex type...
                    case TreeViewImages.ComplexType:
                        {
                            XmlSchemaComplexType ct = tnParent.Tag as XmlSchemaComplexType;
                            if (ct != null)
                            {
                                // then we are removing an attribute
                                if (obj is XmlSchemaAttribute)
                                {
                                    ct.Attributes.Remove(obj);
                                    success = true;
                                }
                                // or an annotation
                                else if (obj is XmlSchemaAnnotation)
                                {
                                    ct.Annotation.Items.Remove(obj);
                                    success = true;
                                }
                                // or an element type
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
            // if successful, remove the node from the tree view
            if (success)
            {
                tnParent.Nodes.Remove(tvSchema.SelectedNode);
                CompileSchema();
            }
            else
            {
                MessageBox.Show("Unable to remove this node");
            }
        }
        #endregion

        #endregion


        #region Events

        #region TreeView PreEventLabelChanged and EventLabelChanged
        // exclude certain tree nodes from being editable
        private void PreEventLabelChanged(object sender, NodeLabelEditEventArgs e)
        {
            prevLabel = tvSchema.SelectedNode.Text;
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
            // if a node is selected...
            if (e.Node != null)
            {
                // and a label has been changed... (e.Label==null if ESC key pressed)
                if (e.Label != null)
                {
                    // then change the name of the type in the schema
                    UpdateTypeAndName(null, e.Label, null);

                    // if this is a root node...
                    if (e.Node.Parent == tvSchema.Nodes[0])
                    {
                        // ...then also change the name in the global list
                        // (this is not case sensitive!)
                        int idx = cbGlobalTypes.FindStringExact(prevLabel);
                        if (idx != -1)
                        {
                            cbGlobalTypes.Items[idx] = new GlobalElementType(e.Label, ((GlobalElementType)cbGlobalTypes.Items[idx]).type);
                        }
                    }
                }
            }
        }
        #endregion

        #region EventTreeViewKeyPress
        // shortcuts:
        // F2 - edit tree node label
        // Ctrl+A - bring up popup menu to Add a subnode
        // Ctrl+T - go to Top of tree
        // Ctrl+P - go to Parent of currently selected node
        private void EventTreeViewKeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            // F2
            if (e.KeyCode == Keys.F2)
            {
                if (tvSchema.SelectedNode != tvSchema.Nodes[0])
                {
                    tvSchema.SelectedNode.BeginEdit();
                    e.Handled = true;
                }
            }
            // Ctrl+A
            else if ((e.KeyCode == Keys.A) && (e.Modifiers == Keys.Control))
            {
                e.Handled = true;
                tvcmSchema.Show(this, tvSchema.Location + new Size(50, 50));
            }
            // Ctrl+T
            else if ((e.KeyCode == Keys.T) && (e.Modifiers == Keys.Control))
            {
                e.Handled = true;
                tvSchema.SelectedNode = tvSchema.Nodes[0];
            }
            // Ctrl+P
            else if ((e.KeyCode == Keys.P) && (e.Modifiers == Keys.Control))
            {
                e.Handled = true;
                TreeNode node = tvSchema.SelectedNode.Parent;
                if (node != null)
                {
                    tvSchema.SelectedNode = node;
                }
            }
        }
        #endregion

        #region Schema Text Box Lost Focus

        #region TreeView MouseDown
        // On a mouse down, we want to select the node on which the mouse clicked.
        // This way we know what node the user right-clicked on when adding sub-nodes
        private void EventMouseDown(object sender, MouseEventArgs e)
        {
            TreeNode tn = tvSchema.GetNodeAt(e.X, e.Y);
            if (tn != null)
            {
                tvSchema.SelectedNode = tn;
            }
        }
        #endregion

        #region TreeView AfterSelect
        // After selecting a node, we want to:
        // 1. highlight the line in the schema that corresponds to the tree node
        // 2. update the simple and global type combo boxes to show what the type is for the selected node.
        private void tvSchema_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            XmlSchemaObject obj = e.Node.Tag as XmlSchemaObject;
            // if this is a schema object...
            if (obj != null)
            {

                // Update simple and global type combo boxes.
                // Get the name of the element type so that we can find it in the simple or global type list
                XmlQualifiedName name = null;		// assume failure
                if (obj is XmlSchemaAttribute)
                {
                    // this is easy.
                    name = ((XmlSchemaAttribute)obj).SchemaTypeName;
                }
                else if (obj is XmlSchemaSimpleType)
                {
                    // if it's a simple type, get the restriction type, if it exists
                    XmlSchemaSimpleTypeRestriction restriction = ((XmlSchemaSimpleType)obj).Content as XmlSchemaSimpleTypeRestriction;
                    if (restriction != null)
                    {
                        name = restriction.BaseTypeName;
                    }
                }
                else if (obj is XmlSchemaElement)
                {
                    // if it's an element, determine if it's a simple type subnode of a complex type...
                    // and then get the restriction type, if it exists
                    XmlSchemaElement el = obj as XmlSchemaElement;
                    if (el.SchemaType is XmlSchemaSimpleType)
                    {
                        XmlSchemaSimpleType st = el.SchemaType as XmlSchemaSimpleType;
                        XmlSchemaSimpleTypeRestriction rest = st.Content as XmlSchemaSimpleTypeRestriction;
                        if (rest != null)
                        {
                            name = rest.BaseTypeName;
                        }
                    }
                    else
                    {
                        // otherwise get the element name
                        name = el.SchemaTypeName;

                        // if the name is null, then there must be a reference instead
                        if (name.Name == "")
                        {
                            name = el.RefName;
                        }
                    }
                }

                // select the name from either the simple type list or the global element type list
                if (name != null)
                {
                    // see if the name exists in the simple type list
                    int idx = cbSimpleTypes.FindStringExact(name.Name);
                    cbSimpleTypes.SelectedIndex = idx;
                    cbSimpleTypes.Enabled = true;

                    // see if the name exists in the complex type list
                    idx = cbGlobalTypes.FindStringExact(name.Name);
                    cbGlobalTypes.SelectedIndex = idx;
                    cbGlobalTypes.Enabled = true;
                }
                else
                {
                    // if there is no name, then disable the comboboxes
                    cbSimpleTypes.SelectedIndex = -1;
                    cbGlobalTypes.SelectedIndex = -1;
                    cbSimpleTypes.Enabled = false;
                    cbGlobalTypes.Enabled = false;
                }
            }
            else
            {
                // if this isn't a schema object, then disable the comboboxes
                cbSimpleTypes.SelectedIndex = -1;
                cbGlobalTypes.SelectedIndex = -1;
                cbSimpleTypes.Enabled = false;
                cbGlobalTypes.Enabled = false;
            }
        }
        #endregion

        #region cbSimpleTypes Index Changed
        // the user has selected a simple type.  Select it and clear the global type combo box.
        private void cbSimpleTypes_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbSimpleTypes.SelectedIndex != -1)
            {
                UpdateTypeAndName(cbSimpleTypes.SelectedItem.ToString(), null, "http://www.w3.org/2001/XMLSchema");
                cbGlobalTypes.SelectedIndex = -1;
            }
        }
        #endregion

        #region cbGlobalTypes Index Changed
        // the user has selected a global type.  Select it and clear the simple type combo box.
        private void cbGlobalTypes_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbGlobalTypes.SelectedIndex != -1)
            {
                UpdateTypeAndName(cbGlobalTypes.SelectedItem.ToString(), null, null);
                cbSimpleTypes.SelectedIndex = -1;
            }
        }
        #endregion


        #endregion

        #region Misc. Helpers

        #region CreateRootNode
        // This helper function destroys the existing tree and creates a new tree.
        // It also clears the global type list, since there are no global types defined
        //	with a blank tree.
        private void CreateRootNode()
        {
            tvSchema.Nodes.Clear();
            TreeNode rootNode = new TreeNode("Schema:");
            tvSchema.Nodes.Add(rootNode);
            rootNode.ImageIndex = (int)TreeViewImages.Schema;
            rootNode.SelectedImageIndex = (int)TreeViewImages.Schema;
            cbGlobalTypes.Items.Clear();
        }
        #endregion
        #region Schema Validation Handler
        // report any errors to the schema error edit box
        private void SchemaValidationHandler(object sender, ValidationEventArgs args)
        {
            Console.WriteLine(args.Message);
            //		edSchemaErrors.Text+=args.Message+"\r\n";
        }
        #endregion
        #region CompileSchema
        // Compile the schema as it exists in the XmlSchema structure, displaying any
        // errors in the schema error edit box and displaying the schema itself in
        // the schema edit box.
        private void CompileSchema()
        {
            schema.Compile(new ValidationEventHandler(SchemaValidationHandler));
            XmlTextWriter wr = new XmlTextWriter("CurrentDocument.xsd", Encoding.UTF8);
            schema.Write(wr);            
            wr.Close();
            this.schemaBrowser.Navigate(Application.StartupPath + "\\CurrentDocument.xsd");
        }
        #endregion

        #region CreateNode
        // This helper function creates a subnode off of the selected node and requests
        // that the node label be immediately editable.
        private TreeNode CreateNode(string name)
        {
            TreeNode tn = tvSchema.SelectedNode;
            TreeNode newNode = new TreeNode(name);
            tn.Nodes.Add(newNode);
            tn.Expand();
            tvSchema.SelectedNode = newNode;
            newNode.BeginEdit();
            return newNode;
        }
        #endregion

        #region AddToCollection
        // Add a schema type to the specified schema object collection.
        // The tree node references the schema type being added.
        // The image indices are set.
        // The schema is recompiled, which shows any errors and updates the schema display.
        void AddToCollection(XmlSchemaObjectCollection coll, XmlSchemaObject obj, TreeNode node, TreeViewImages imgIdx)
        {
            coll.Add(obj);
            node.Tag = obj;
            node.ImageIndex = (int)imgIdx;
            node.SelectedImageIndex = (int)imgIdx;
            CompileSchema();
        }
        #endregion

        #region ChangeElementToComplexType
        // When adding a subnode to an element or a simple type, the node must be changed to a complex type.
        private XmlSchemaComplexType ChangeElementToComplexType(XmlSchemaElement el, TreeNode tn)
        {
            // if the element is currently a reference, we need to remove the reference
            // and set the name to something, which in this case is the old ref name.
            if (el.RefName != null)
            {
                el.Name = el.RefName.Name;
                el.RefName = null;
            }
            // creat the complex type.
            XmlSchemaComplexType ct = new XmlSchemaComplexType();
            el.SchemaType = ct;
            // a complex type cannot have a type name
            el.SchemaTypeName = null;
            // create the sequence
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            ct.Particle = sequence;

            // update the image indices
            tn.ImageIndex = (int)TreeViewImages.ComplexType;
            tn.SelectedImageIndex = (int)TreeViewImages.ComplexType;

            return ct;
        }
        #endregion

        #region AddToComplexType
        // add a type to a complex type
        private bool AddToComplexType(object obj, XmlSchemaObject item, TreeNode newNode, TreeViewImages imgIdx)
        {
            bool success = true;
            XmlSchemaElement el = obj as XmlSchemaElement;
            XmlSchemaComplexType complexType = obj as XmlSchemaComplexType;

            // is it an element...
            if (el != null)
            {
                // of complex type (so we know it's a local complex type, not a global one!)
                XmlSchemaComplexType ct = el.SchemaType as XmlSchemaComplexType;
                if (ct != null)
                {
                    // does it have a sequence?
                    XmlSchemaSequence seq = ct.Particle as XmlSchemaSequence;
                    if (seq != null)
                    {
                        // yes--add it to the sequence collection
                        AddToCollection(seq.Items, item, newNode, imgIdx);
                    }
                    else
                    {
                        // no--we can't add it.
                        MessageBox.Show("Complex type is missing sequence!");
                        success = false;
                    }
                }
                else
                {
                    // change the basic element to a complex type by adding a sequence and then
                    // add the sub-element
                    ct = ChangeElementToComplexType(el, newNode.Parent);
                    AddToCollection(((XmlSchemaSequence)ct.Particle).Items, item, newNode, imgIdx);
                }
            }
            // or is it a global complex type...
            else if (complexType != null)
            {
                XmlSchemaSequence seq = complexType.Particle as XmlSchemaSequence;
                // which should have a sequence!
                if (seq != null)
                {
                    // yes--add it to the collection
                    AddToCollection(seq.Items, item, newNode, imgIdx);
                }
                else
                {
                    // no--we can't add it.
                    MessageBox.Show("Complex type is missing sequence!");
                    success = false;
                }
            }
            else
            {
                // failure--current node is not an element and not a complex type
                success = false;
            }
            return success;
        }
        #endregion

        #region UpdateTypeAndName
        // This is a dual purpose function that updates either the type or the name.
        // It can also update both at once, but isn't used in that way in this program.
        // Pass in a null for typeName if only changing the name.
        // Pass in a null for name if only changing the typeName.
        // Pass in a null for the nameSpace if the typeName is a global type.
        private void UpdateTypeAndName(string typeName, string name, string nameSpace)
        {
            // Is the type referenced by the tree node an XmlSchemaObject?
            XmlSchemaObject obj = tvSchema.SelectedNode.Tag as XmlSchemaObject;
            if (obj != null)
            {
                // is it an attribute?
                if (obj is XmlSchemaAttribute)
                {
                    XmlSchemaAttribute attrib = obj as XmlSchemaAttribute;
                    // if we're changing the name:
                    if (name != null)
                    {
                        attrib.Name = name;
                        CompileSchema();
                    }
                    // if we're changing the type:
                    if (typeName != null)
                    {
                        // if the name refers to a global attribute, then use ref instead of type
                        int idx = cbGlobalTypes.FindStringExact(typeName);
                        GlobalElementType glet = null;
                        if (idx != -1)
                        {
                            glet = cbGlobalTypes.Items[idx] as GlobalElementType;
                        }
                        // if a global exists of this type...
                        if ((glet != null) && (glet.type is XmlSchemaAttribute))
                        {
                            // then get rid of the type name and use a ref instead
                            attrib.SchemaTypeName = null;
                            attrib.RefName = new XmlQualifiedName(typeName, nameSpace);
                            // can't have a name
                            attrib.Name = null;
                        }
                        else
                        {
                            // otherwise, use a type name and get rid of the ref
                            attrib.SchemaTypeName = new XmlQualifiedName(typeName, nameSpace);
                            attrib.RefName = null;
                            if (attrib.Name == null)
                            {
                                // if changing from ref to type, we need some sort of default name
                                attrib.Name = tvSchema.SelectedNode.Text;
                            }
                        }
                        CompileSchema();
                        // update the tree node name
                        tvSchema.SelectedNode.Text = attrib.QualifiedName.Name;
                    }
                }
                // is it an element?
                else if (obj is XmlSchemaElement)
                {
                    XmlSchemaElement el = obj as XmlSchemaElement;
                    // if we're changing the name:
                    if (name != null)
                    {
                        el.Name = name;
                        CompileSchema();
                    }

                    // if we're changing the type:
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
                            // if the name refers to a global element, then use ref instead of type
                            // Same deal as for changing the element type name.
                            // Since the SchemaTypeName and RefName are not part of any base class shared
                            // between the SchemaXmlAttribute and SchemaXmlElement, we need separate code.
                            int idx = cbGlobalTypes.FindStringExact(typeName);
                            GlobalElementType glet = null;
                            if (idx != -1)
                            {
                                glet = cbGlobalTypes.Items[idx] as GlobalElementType;
                            }
                            if ((glet != null) && (glet.type is XmlSchemaElement))
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
                                {
                                    el.Name = tvSchema.SelectedNode.Text;
                                }
                            }
                            CompileSchema();
                            tvSchema.SelectedNode.Text = el.QualifiedName.Name;
                        }
                    }
                }
                // is it a simple type?
                else if (obj is XmlSchemaSimpleType)
                {
                    XmlSchemaSimpleType st = obj as XmlSchemaSimpleType;
                    // if we're changing the name:
                    if (name != null)
                    {
                        st.Name = name;
                        CompileSchema();
                    }

                    // if we're changing the type name:
                    if (typeName != null)
                    {
                        // this must be done in the Restriction object
                        XmlSchemaSimpleTypeRestriction restriction = st.Content as XmlSchemaSimpleTypeRestriction;
                        if (restriction != null)
                        {
                            restriction.BaseTypeName = new XmlQualifiedName(typeName, nameSpace);
                            CompileSchema();
                            cbGlobalTypes.SelectedIndex = -1;
                        }
                    }
                }
                // is it a complex type?
                else if (obj is XmlSchemaComplexType)
                {
                    // if we're changing the name:
                    if (name != null)
                    {
                        ((XmlSchemaComplexType)obj).Name = name;
                        CompileSchema();
                    }
                    // there is no such thing as changing the type of a complex type
                }
            }
        }
        #endregion

        #region Add Facet
        // Add a facet to a simple type.
        bool AddFacet(XmlSchemaFacet facet, string val, TreeViewImages imgIdx)
        {
            // get the simple type.
            XmlSchemaSimpleType st = tvSchema.SelectedNode.Tag as XmlSchemaSimpleType;

            // assume failure
            XmlSchemaSimpleTypeRestriction rs = null;
            bool success = false;

            // if the simple type exists, get the restriction type
            if (st != null)
            {
                rs = st.Content as XmlSchemaSimpleTypeRestriction;
            }

            // if the restriction type exists...
            if (rs != null)
            {
                // create the node and add it to the facet collection
                TreeNode newNode = CreateNode(facet.ToString());
                facet.Value = val;
                rs.Facets.Add(facet);
                CompileSchema();
                success = true;
            }
            return success;
        }
        #endregion

        #region TextToNodeArray
        // a helper to create some default documentation text.
        public static XmlNode[] TextToNodeArray(string text)
        {
            XmlDocument doc = new XmlDocument();
            return new XmlNode[1] { doc.CreateTextNode(text) };
        }
        #endregion

        #endregion

        #region DecodeSchema

        // stub that wraps the actual decoding in a try block so we can display errors
        private void DecodeSchema(XmlSchema schema, TreeNode node)
        {
            try
            {
                DecodeSchema2(schema, node);
            }
            catch (Exception err)
            {
            }
        }

        // recursive decoder
        private TreeNode DecodeSchema2(XmlSchemaObject obj, TreeNode node)
        {
            TreeNode newNode = node;

            // convert the object to all types and then check what type actually exists
            XmlSchemaAnnotation annot = obj as XmlSchemaAnnotation;
            XmlSchemaAttribute attrib = obj as XmlSchemaAttribute;
            XmlSchemaFacet facet = obj as XmlSchemaFacet;
            XmlSchemaDocumentation doc = obj as XmlSchemaDocumentation;
            XmlSchemaAppInfo appInfo = obj as XmlSchemaAppInfo;
            XmlSchemaElement element = obj as XmlSchemaElement;
            XmlSchemaSimpleType simpleType = obj as XmlSchemaSimpleType;
            XmlSchemaComplexType complexType = obj as XmlSchemaComplexType;

            // If the current node is the root node of the tree, then we are
            // possibly adding a global attribute, element, simple type, or complex type.
            if (node == tvSchema.Nodes[0])
            {
                if (attrib != null)
                {
                    if (attrib.Name != null)
                    {
                        // add to global list
                        cbGlobalTypes.Items.Add(new GlobalElementType(attrib.Name, attrib));
                    }
                }
                else if (element != null)
                {
                    if (element.Name != null)
                    {
                        // add to global list
                        cbGlobalTypes.Items.Add(new GlobalElementType(element.Name, element));
                    }
                }
                else if (simpleType != null)
                {
                    if (simpleType.Name != null)
                    {
                        // add to global list
                        cbGlobalTypes.Items.Add(new GlobalElementType(simpleType.Name, simpleType));
                    }
                }
                else if (complexType != null)
                {
                    if (complexType.Name != null)
                    {
                        // add to global list
                        cbGlobalTypes.Items.Add(new GlobalElementType(complexType.Name, complexType));
                    }
                }
            }

            // if annotation, add a tree node and recurse for documentation and app info
            if (annot != null)
            {
                newNode = new TreeNode("--annotation--");
                newNode.Tag = annot;
                newNode.ImageIndex = 4;
                newNode.SelectedImageIndex = 4;
                node.Nodes.Add(newNode);
                foreach (XmlSchemaObject schemaObject in annot.Items)
                {
                    DecodeSchema2(schemaObject, newNode);
                }
            }
            else
                // if attribute, add a tree node
                if (attrib != null)
                {
                    newNode = new TreeNode(attrib.QualifiedName.Name);
                    newNode.Tag = attrib;
                    newNode.ImageIndex = 7;
                    newNode.SelectedImageIndex = 7;
                    node.Nodes.Add(newNode);
                }
                else
                    // if facet, add a tree node
                    if (facet != null)
                    {
                        newNode = new TreeNode(facet.ToString());
                        newNode.Tag = facet;
                        newNode.ImageIndex = 8;
                        newNode.SelectedImageIndex = 8;
                        node.Nodes.Add(newNode);
                    }
                    else
                        // if documentation, add a tree node
                        if (doc != null)
                        {
                            newNode = new TreeNode("--documentation--");
                            newNode.Tag = doc;
                            newNode.ImageIndex = 5;
                            newNode.SelectedImageIndex = 5;
                            node.Nodes.Add(newNode);
                        }
                        else
                            // if app info, add a tree node
                            if (appInfo != null)
                            {
                                newNode = new TreeNode("--app info--");
                                newNode.Tag = annot;
                                newNode.ImageIndex = 6;
                                newNode.SelectedImageIndex = 6;
                                node.Nodes.Add(newNode);
                            }
                            else
                                // if an element, determine whether the element is a simple type or a complex type
                                if (element != null)
                                {
                                    XmlSchemaSimpleType st = element.SchemaType as XmlSchemaSimpleType;
                                    XmlSchemaComplexType ct = element.SchemaType as XmlSchemaComplexType;

                                    if (st != null)
                                    {
                                        // this is a simple type element.  Recurse.
                                        TreeNode node2 = DecodeSchema2(st, newNode);
                                        node2.Text = element.Name;
                                    }
                                    else if (ct != null)
                                    {
                                        // this is a complex type element.  Recurse.
                                        TreeNode node2 = DecodeSchema2(ct, newNode);
                                        node2.Text = element.Name;
                                    }
                                    else
                                    {
                                        // This is a plain ol' fashioned element.
                                        newNode = new TreeNode(element.QualifiedName.Name);
                                        newNode.Tag = element;
                                        newNode.ImageIndex = 1;
                                        newNode.SelectedImageIndex = 1;
                                        node.Nodes.Add(newNode);
                                    }
                                }
                                else
                                    // if a simple type, then add a tree node and recurse facets
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
                                                DecodeSchema2(schemaObject, newNode);
                                            }
                                        }
                                    }
                                    else
                                        // if a complex type, add a tree node and recurse its sequence
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
                                                    DecodeSchema2(schemaObject, newNode);
                                                }
                                            }
                                        }

            // now recurse any object collection of the type.
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                if (property.PropertyType.FullName == "System.Xml.Schema.XmlSchemaObjectCollection")
                {
                    XmlSchemaObjectCollection childObjectCollection = (XmlSchemaObjectCollection)property.GetValue(obj, null);
                    foreach (XmlSchemaObject schemaObject in childObjectCollection)
                    {
                        DecodeSchema2(schemaObject, newNode);
                    }
                }
            }
            return newNode;
        }
        #endregion

        private void xmlBrowserWindow_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // code to auto highlight 

         
          



        }

        #endregion

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        #endregion

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML File|*.xml";
            saveFileDialog.Title = "Save an XML File";
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                XmlTextWriter writer = new XmlTextWriter(saveFileDialog.OpenFile(), null);
                writer.Formatting = Formatting.Indented;
                xmlDoc.Save(writer);              

            }

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xmlBrowserWindow.Print();            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML File|*.xml";
            saveFileDialog.Title = "Save an XML File";
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                XmlTextWriter writer = new XmlTextWriter(saveFileDialog.OpenFile(), null);
                writer.Formatting = Formatting.Indented;
                xmlDoc.Save(writer);

            }
        }






    }
}
