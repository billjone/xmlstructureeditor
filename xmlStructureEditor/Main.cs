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
            
            XmlTextReader reader = new XmlTextReader(@"c:\temp\books.xml");
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        this.rtbXML.SelectionColor = Color.Blue;
                        this.rtbXML.AppendText("<");
                        this.rtbXML.SelectionColor = Color.Brown;
                        this.rtbXML.AppendText(reader.Name);
                        this.rtbXML.SelectionColor = Color.Blue;
                        this.rtbXML.AppendText(">");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        this.rtbXML.SelectionColor = Color.Black;
                        this.rtbXML.AppendText(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        this.rtbXML.SelectionColor = Color.Blue;
                        this.rtbXML.AppendText("</");
                        this.rtbXML.SelectionColor = Color.Brown;
                        this.rtbXML.AppendText(reader.Name);
                        this.rtbXML.SelectionColor = Color.Blue;
                        this.rtbXML.AppendText(">");
                        this.rtbXML.AppendText("\n");
                        break;
                }

            }


            try
            {
                // SECTION 1. Create a DOM Document and load the XML data into it.                
                
                xmlDoc.Load(@"c:\temp\books.xml");

                // SECTION 2. Initialize the TreeView control.
                xmlTreeview.Nodes.Clear();
                xmlTreeview.Nodes.Add(new TreeNode(xmlDoc.DocumentElement.Name));                
                TreeNode tNode = new TreeNode();
                tNode = xmlTreeview.Nodes[0];

                // SECTION 3. Populate the TreeView with the DOM nodes.
                xmlFunctions.AddNode(xmlDoc.DocumentElement, tNode);
                xmlTreeview.ExpandAll();
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

            xmlDoc.NodeChanged += new XmlNodeChangedEventHandler(xmlDoc_NodeChanged);



        }

        void xmlDoc_NodeChanged(object sender, XmlNodeChangedEventArgs e)
        {
            MessageBox.Show("xmlDoc Change event fired!");
        }

        

        private void InitializeTreeView() // Handles Treeview drawing
        {
            // xmlTreeview.ControlAdded +=new EventHandler(xmlTreeview_TabIndexChanged);

            xmlTreeview.Paint += new PaintEventHandler(Treeview_Paint);
            schemaTreeview.Paint += new PaintEventHandler(Treeview_Paint);

            xmlTreeview.AfterSelect += new TreeViewEventHandler(xmlTreeview_AfterSelect);
            
        }

        void xmlTreeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tsStatusLabel.Text = xmlFunctions.treeToXpath(xmlTreeview.SelectedNode.FullPath.ToString());
            tsStatusLabelClear.Text = xmlTreeview.SelectedNode.FullPath.ToString();
        }


        void Treeview_Paint(object sender, PaintEventArgs e)
        {            

            if (xmlTreeview.Nodes.Count == 0)
            {
                tsbtnRootElement.Enabled = true;
                tsbtnAddElement.Enabled = false;
            }
            else
            {
                tsbtnRootElement.Enabled = false;
                tsbtnAddElement.Enabled = true;
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

                root.AppendChild(xmlDoc.CreateTextNode("CreationDate: " + DateTime.Today.ToString()));
                xmlDoc.AppendChild(root);

                

                // Update the Tree/Text views
                updateTreeviewXml();
                updateRtbXml();

            }
            catch (Exception ex)
            {
               
                // tooltip popup error

            }

           

            
            
        }

        
        private void tsbtnAddElement_Click(object sender, EventArgs e)
        {
            if (!xmlTreeview.SelectedNode.FullPath.ToString().EndsWith(">")) // Check that the selected node is a valid target to add an element to..
                MessageBox.Show("Cannot add an element to a data object!"); // replace this with tooltip?
            else
            {
                addElement frmElement = new addElement();
                              
                frmElement.ShowDialog();



                // get a list of all the elements in the selected node
                XmlNodeList nl = xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                    .SelectedNode.FullPath.ToString()))
                    .ParentNode.ChildNodes;

                nl[xmlTreeview.SelectedNode.Index]
                    .AppendChild(xmlDoc.CreateElement(frmElement.getElementName()));
                                
              
            }

                    
    
            updateTreeviewXml();
            updateRtbXml();
            
        }


        private void updateTreeviewXml()
        {
            xmlTreeview.BeginUpdate();
            xmlTreeview.Nodes.Clear();
            xmlTreeview.EndUpdate();
            xmlFunctions.ConvertXmlNodeToTreeNode(xmlDoc, xmlTreeview.Nodes);
            xmlTreeview.ExpandAll();            
            

        }

        private void updateRtbXml()
        {

            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xmlDoc.WriteTo(xw);

            rtbXML.Text = sw.ToString();
        }

        private void tsbAddAttribute_Click(object sender, EventArgs e)
        {
            addAttribute frmAttrib = new addAttribute();
            frmAttrib.ShowDialog();
     
            XmlElement attribTarget = (XmlElement) xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview.SelectedNode.FullPath.ToString()));


            XmlNodeList nl = xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                   .SelectedNode.FullPath.ToString()))
                   .ParentNode.ChildNodes;

           
            XmlAttribute att = xmlDoc.CreateAttribute(frmAttrib.getAttribName());
            att.Value = frmAttrib.getAttribVal();

            nl[xmlTreeview.SelectedNode.Index].Attributes.Append(att);
                
            updateTreeviewXml();
            updateRtbXml();

        }

        private void tsbtnAddData_Click(object sender, EventArgs e)
        {
            
            // Grabs the location of the targetted node. Need to add checks to see if this is a valid target
            // for an element. E.g. an attribute would NOT be valid. Checks need to be added here!

    
            if (xmlTreeview.SelectedNode.FullPath.ToString().EndsWith(">")) // if an element is selected, take the main path
            {
                 XmlNodeList nl = xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                    .SelectedNode.FullPath.ToString()))
                    .ParentNode.ChildNodes;
                 
                string it = nl[xmlTreeview.SelectedNode.Index].InnerText;

                addData frm = new addData();

                if (nl[xmlTreeview.SelectedNode.Index].InnerText.Length > 0) // if there is data already, pass it to the form
                    frm.setData(nl[xmlTreeview.SelectedNode.Index].InnerText);

                 frm.ShowDialog();

                 if (!nl[xmlTreeview.SelectedNode.Index].InnerText.Equals(frm.getData()))
                 {

                     XmlText txtData = xmlDoc.CreateTextNode(frm.getData());                     
                     nl[xmlTreeview.SelectedNode.Index].AppendChild(txtData);
                   //  nl[xmlTreeview.SelectedNode.Index].InnerText = frm.getData(); // update the data ONLY if the data is new                              

                 }

            }
            else
            {

                XmlNodeList nl = xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                    .SelectedNode.Parent.FullPath.ToString()))
                    .ParentNode.ChildNodes;

                string it = nl[xmlTreeview.SelectedNode.Parent.Index].InnerText;

                addData frm = new addData();

                if (nl[xmlTreeview.SelectedNode.Parent.Index].InnerText.Length > 0) // if there is data already, pass it to the form
                    frm.setData(nl[xmlTreeview.SelectedNode.Parent.Index].InnerText);

                frm.ShowDialog();

                if (!nl[xmlTreeview.SelectedNode.Parent.Index].InnerText.Equals(frm.getData()))
                {
                    XmlText txtData = xmlDoc.CreateTextNode(frm.getData());
                    nl[xmlTreeview.SelectedNode.Index].AppendChild(txtData);
                }
                 
               

            }
                        
            
            updateTreeviewXml();
            updateRtbXml();
        }

        private void tsbtnDelete_Click(object sender, EventArgs e) // change this to be more modular, i.e. use functions
        {
            XmlNodeList nl;

            /*
             * If targeted node = Element (contains <>); remove the element
             * If targeted node = Attribute, get collection of attributes, then remove the correct one (index)
             * If targeted node = Data, .InnerText = "";
             * 
             * ez
            */

            if (xmlTreeview.SelectedNode.FullPath.ToString().EndsWith("]") && 
                !xmlFunctions.treeToXpath(xmlTreeview.SelectedNode.Parent.FullPath.ToString()).Contains("ATTRIBUTE"))
            {
                nl = xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                   .SelectedNode.Parent.FullPath.ToString()))
                   .ParentNode.ChildNodes;
                
                nl[xmlTreeview.SelectedNode.Parent.Index]
                    .RemoveChild(nl[xmlTreeview.SelectedNode.Parent.Index].FirstChild);

                xmlTreeview.SelectedNode.Remove();
                

            }
            else if (xmlTreeview.SelectedNode.FullPath.ToString().Contains("ATTRIBUTE")) // REMOVE ATTRIBUTE CODE
            {


                // if SelectedNode.parent contains 'attribute' then you're on the attribute text value

                if (xmlTreeview.SelectedNode.Parent.FullPath.Contains("ATTRIBUTE"))
                {
                    xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                 .SelectedNode.Parent.Parent.FullPath.ToString()))
                 .Attributes.RemoveNamedItem(xmlTreeview.SelectedNode.Parent.Text.Replace("ATTRIBUTE: ", ""));
                }
                else
                {
                    // if SelectedNode.parent doesn't contain attribute then you're on the attribute parent element!
                    xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                      .SelectedNode.Parent.FullPath.ToString()))
                      .Attributes.RemoveNamedItem(xmlTreeview.SelectedNode.Text.Replace("ATTRIBUTE: ", ""));
                }
                

            }
            else
            {


                nl = xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                                 .SelectedNode.FullPath.ToString()))
                                 .ParentNode.ChildNodes;

                switch (nl[xmlTreeview.SelectedNode.Index].NodeType)
                {
                    case XmlNodeType.ProcessingInstruction:
                    case XmlNodeType.XmlDeclaration:

                        break;
                    case XmlNodeType.Element:
                            nl[xmlTreeview.SelectedNode.Index].ParentNode.RemoveChild(nl[xmlTreeview.SelectedNode.Index]);                        
                        break;                                     
                    case XmlNodeType.CDATA:

                        break;
                    case XmlNodeType.Comment:

                        break;
                }
            }
            



            

        

            
            updateTreeviewXml();
            updateRtbXml();

        }


    }
}
