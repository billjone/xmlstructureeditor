﻿/*******************************************************************************
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
                

                /*
                xmlTreeview.Nodes.Clear();
                xmlTreeview.Nodes.Add(new TreeNode(xmlDoc.DocumentElement.Name));                
                TreeNode tNode = new TreeNode();
                tNode = xmlTreeview.Nodes[0];

                // SECTION 3. Populate the TreeView with the DOM nodes.
                xmlFunctions.AddNode(xmlDoc.DocumentElement, tNode);
                xmlTreeview.ExpandAll();
                 * */

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



            // Events to control updating xmlTree/RTB
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
            genTreeTags gen = new genTreeTags(xmlTreeview);

            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xmlDoc.WriteTo(xw);

            rtbXML.Text = sw.ToString();
        }
      
        

        private void InitializeTreeView() // Handles Treeview drawing
        {          
            xmlTreeview.Paint += new PaintEventHandler(Treeview_Paint);
            schemaTreeview.Paint += new PaintEventHandler(Treeview_Paint);
            xmlTreeview.AfterSelect += new TreeViewEventHandler(xmlTreeview_AfterSelect);
            
        }

        void xmlTreeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tsStatusLabel.Text = xmlFunctions.treeToXpath(xmlTreeview.SelectedNode.FullPath.ToString());            
            tsStatusLabelClear.Text = xmlTreeview.SelectedNode.FullPath.ToString();
            tsLabelnodeIndex.Text = xmlTreeview.SelectedNode.Tag.ToString();
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

                

               

            }
            catch (Exception ex)
            {
               
                // tooltip popup error

            }

           

            
            
        }

        
        private void tsbtnAddElement_Click(object sender, EventArgs e)
        {
           
            addElement frmElement = new addElement();
                              
            frmElement.ShowDialog();


            

            // get a list of all the elements in the selected node
            XmlNodeList nl = xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                .SelectedNode.FullPath.ToString()))
                .ParentNode.ChildNodes;

            
            
            



            /*
            int count = 0;

            foreach (XmlElement bob in xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                .SelectedNode.FullPath.ToString())).ChildNodes.OfType<XmlElement>())
                if (bob.Name == "")
                    count++;

            

            if (count > 0)
                MessageBox.Show("More than one of these!");
            */

            nl[xmlTreeview.SelectedNode.Index]
                .AppendChild(xmlDoc.CreateElement(frmElement.getElementName()));
                                
              
            
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
               // nl = xmlDoc.SelectSingleNode(xmlFunctions.treeToXpath(xmlTreeview
                //   .SelectedNode.Parent.FullPath.ToString()))
                 //     .ParentNode.ChildNodes;

                xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString()).ParentNode.RemoveChild(
                    xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Parent.Tag.ToString()).FirstChild);
                
//                nl[xmlTreeview.SelectedNode.Parent.Index]
 //                   .RemoveChild(nl[xmlTreeview.SelectedNode.Parent.Index].FirstChild);                               
                                                

   //             xmlTreeview.SelectedNode.Remove();
                

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






                switch (xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString()).NodeType)
                {
                    case XmlNodeType.ProcessingInstruction:
                    case XmlNodeType.XmlDeclaration:
                        break;                    
                    case XmlNodeType.Element:
                        xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString())
                            .ParentNode.RemoveChild(xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString()));
                        break;                                     
                    case XmlNodeType.CDATA:                        
                        break;
                    case XmlNodeType.Comment:

                        break;
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

            nl[xmlTreeview.SelectedNode.Index].AppendChild(cdata);
        ;
        }

        private void tsbtnComment_Click(object sender, EventArgs e)
        {     
            addComment frm = new addComment();
            frm.ShowDialog();


            XmlComment cmt = xmlDoc.CreateComment(frm.getComment());
            xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString()).AppendChild(cmt);
            
            

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show(xmlDoc.SelectSingleNode(xmlTreeview.SelectedNode.Tag.ToString()).InnerText);


        }


    }
}
