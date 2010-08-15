namespace xmlStructureEditor
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateXMLFromSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusBar = new System.Windows.Forms.StatusStrip();
            this.tsStatusXpath = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsStatusElementType = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.tabXML = new System.Windows.Forms.TabPage();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.xmlBrowserWindow = new System.Windows.Forms.WebBrowser();
            this.xmlTreeview = new System.Windows.Forms.TreeView();
            this.tabSchema = new System.Windows.Forms.TabPage();
            this.lblGT = new System.Windows.Forms.Label();
            this.comboGlobal = new System.Windows.Forms.ComboBox();
            this.lblST = new System.Windows.Forms.Label();
            this.schemaBrowser = new System.Windows.Forms.WebBrowser();
            this.comboSimple = new System.Windows.Forms.ComboBox();
            this.schemaTree = new System.Windows.Forms.TreeView();
            this.schemaTreeView = new System.Windows.Forms.ContextMenu();
            this.RightMenuAddElement = new System.Windows.Forms.MenuItem();
            this.RightMenuAddST = new System.Windows.Forms.MenuItem();
            this.RightMenuAddCT = new System.Windows.Forms.MenuItem();
            this.RightMenuAddAttribute = new System.Windows.Forms.MenuItem();
            this.RightMenuRemoveNode = new System.Windows.Forms.MenuItem();
            this.mnuFacetEnumeration = new System.Windows.Forms.MenuItem();
            this.mnuFacetMaxExclusive = new System.Windows.Forms.MenuItem();
            this.mnuFacetMaxInclusive = new System.Windows.Forms.MenuItem();
            this.mnuFacetMinExclusive = new System.Windows.Forms.MenuItem();
            this.mnuFacetMinInclusive = new System.Windows.Forms.MenuItem();
            this.mnuFacetNumeric = new System.Windows.Forms.MenuItem();
            this.mnuFacetFractionDigits = new System.Windows.Forms.MenuItem();
            this.mnuFacetLength = new System.Windows.Forms.MenuItem();
            this.mnuFacetMaxLength = new System.Windows.Forms.MenuItem();
            this.mnuFacetMinLength = new System.Windows.Forms.MenuItem();
            this.mnuFacetTotalDigits = new System.Windows.Forms.MenuItem();
            this.mnuFacetPattern = new System.Windows.Forms.MenuItem();
            this.mnuFacetWhiteSpace = new System.Windows.Forms.MenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbtnRootElement = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnAddElement = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnAddAttribute = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnAddData = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnAddCDATA = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnComment = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainMenuStrip.SuspendLayout();
            this.mainStatusBar.SuspendLayout();
            this.mainTabs.SuspendLayout();
            this.tabXML.SuspendLayout();
            this.tabSchema.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1113, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.printToolStripMenuItem.Text = "&Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.toolStripSeparator3});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.validateXMLFromSchemaToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // validateXMLFromSchemaToolStripMenuItem
            // 
            this.validateXMLFromSchemaToolStripMenuItem.Name = "validateXMLFromSchemaToolStripMenuItem";
            this.validateXMLFromSchemaToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.validateXMLFromSchemaToolStripMenuItem.Text = "Validate XML from Schema";
            this.validateXMLFromSchemaToolStripMenuItem.Click += new System.EventHandler(this.validateXMLFromSchemaToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(119, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainStatusBar
            // 
            this.mainStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusXpath,
            this.tsStatusElementType});
            this.mainStatusBar.Location = new System.Drawing.Point(0, 504);
            this.mainStatusBar.Name = "mainStatusBar";
            this.mainStatusBar.Size = new System.Drawing.Size(1113, 22);
            this.mainStatusBar.TabIndex = 2;
            this.mainStatusBar.Text = "statusStrip1";
            // 
            // tsStatusXpath
            // 
            this.tsStatusXpath.ForeColor = System.Drawing.Color.Orange;
            this.tsStatusXpath.Name = "tsStatusXpath";
            this.tsStatusXpath.Size = new System.Drawing.Size(48, 17);
            this.tsStatusXpath.Text = "XPATH:";
            // 
            // tsStatusElementType
            // 
            this.tsStatusElementType.ForeColor = System.Drawing.Color.Red;
            this.tsStatusElementType.Name = "tsStatusElementType";
            this.tsStatusElementType.Size = new System.Drawing.Size(68, 17);
            this.tsStatusElementType.Text = "Node Type:";
            // 
            // mainTabs
            // 
            this.mainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabs.Controls.Add(this.tabXML);
            this.mainTabs.Controls.Add(this.tabSchema);
            this.mainTabs.Location = new System.Drawing.Point(90, 27);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(1011, 474);
            this.mainTabs.TabIndex = 4;
            // 
            // tabXML
            // 
            this.tabXML.Controls.Add(this.txtOutput);
            this.tabXML.Controls.Add(this.xmlBrowserWindow);
            this.tabXML.Controls.Add(this.xmlTreeview);
            this.tabXML.Location = new System.Drawing.Point(4, 22);
            this.tabXML.Name = "tabXML";
            this.tabXML.Padding = new System.Windows.Forms.Padding(3);
            this.tabXML.Size = new System.Drawing.Size(1003, 448);
            this.tabXML.TabIndex = 0;
            this.tabXML.Text = "XML";
            this.tabXML.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(401, 368);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(649, 72);
            this.txtOutput.TabIndex = 4;
            // 
            // xmlBrowserWindow
            // 
            this.xmlBrowserWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xmlBrowserWindow.Location = new System.Drawing.Point(400, 6);
            this.xmlBrowserWindow.MinimumSize = new System.Drawing.Size(20, 20);
            this.xmlBrowserWindow.Name = "xmlBrowserWindow";
            this.xmlBrowserWindow.Size = new System.Drawing.Size(597, 356);
            this.xmlBrowserWindow.TabIndex = 3;
            this.xmlBrowserWindow.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.xmlBrowserWindow_DocumentCompleted);
            // 
            // xmlTreeview
            // 
            this.xmlTreeview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.xmlTreeview.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmlTreeview.Location = new System.Drawing.Point(6, 3);
            this.xmlTreeview.Name = "xmlTreeview";
            this.xmlTreeview.Size = new System.Drawing.Size(388, 437);
            this.xmlTreeview.TabIndex = 2;
            // 
            // tabSchema
            // 
            this.tabSchema.Controls.Add(this.lblGT);
            this.tabSchema.Controls.Add(this.comboGlobal);
            this.tabSchema.Controls.Add(this.lblST);
            this.tabSchema.Controls.Add(this.schemaBrowser);
            this.tabSchema.Controls.Add(this.comboSimple);
            this.tabSchema.Controls.Add(this.schemaTree);
            this.tabSchema.Location = new System.Drawing.Point(4, 22);
            this.tabSchema.Name = "tabSchema";
            this.tabSchema.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchema.Size = new System.Drawing.Size(1003, 448);
            this.tabSchema.TabIndex = 1;
            this.tabSchema.Text = "Schema";
            this.tabSchema.UseVisualStyleBackColor = true;
            // 
            // lblGT
            // 
            this.lblGT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGT.Location = new System.Drawing.Point(6, 364);
            this.lblGT.Name = "lblGT";
            this.lblGT.Size = new System.Drawing.Size(80, 16);
            this.lblGT.TabIndex = 1;
            this.lblGT.Text = "Schema Types";
            // 
            // comboGlobal
            // 
            this.comboGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboGlobal.Location = new System.Drawing.Point(92, 361);
            this.comboGlobal.Name = "comboGlobal";
            this.comboGlobal.Size = new System.Drawing.Size(168, 21);
            this.comboGlobal.Sorted = true;
            this.comboGlobal.TabIndex = 3;
            this.comboGlobal.SelectedIndexChanged += new System.EventHandler(this.cbGlobalTypes_SelectedIndexChanged);
            // 
            // lblST
            // 
            this.lblST.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblST.Location = new System.Drawing.Point(6, 337);
            this.lblST.Name = "lblST";
            this.lblST.Size = new System.Drawing.Size(80, 16);
            this.lblST.TabIndex = 0;
            this.lblST.Text = "Types";
            // 
            // schemaBrowser
            // 
            this.schemaBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.schemaBrowser.Location = new System.Drawing.Point(405, 3);
            this.schemaBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.schemaBrowser.Name = "schemaBrowser";
            this.schemaBrowser.Size = new System.Drawing.Size(645, 534);
            this.schemaBrowser.TabIndex = 7;
            // 
            // comboSimple
            // 
            this.comboSimple.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboSimple.Items.AddRange(new object[] {
            "boolean",
            "byte",
            "date",
            "dateTime",
            "decimal",
            "double",
            "float",
            "gDay",
            "gMonth",
            "gMonthDay",
            "gYear",
            "gYearMonth",
            "ID",
            "int",
            "integer",
            "long",
            "negativeInteger",
            "nonNegativeInteger",
            "nonPositiveInteger",
            "positiveInteger",
            "short",
            "string",
            "time",
            "unsignedByte",
            "unsignedInt",
            "unsignedLong",
            "unsignedShort"});
            this.comboSimple.Location = new System.Drawing.Point(92, 334);
            this.comboSimple.Name = "comboSimple";
            this.comboSimple.Size = new System.Drawing.Size(168, 21);
            this.comboSimple.Sorted = true;
            this.comboSimple.TabIndex = 2;
            this.comboSimple.SelectedIndexChanged += new System.EventHandler(this.cbSimpleTypes_SelectedIndexChanged);
            // 
            // schemaTree
            // 
            this.schemaTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.schemaTree.ContextMenu = this.schemaTreeView;
            this.schemaTree.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.schemaTree.FullRowSelect = true;
            this.schemaTree.HideSelection = false;
            this.schemaTree.Location = new System.Drawing.Point(3, 3);
            this.schemaTree.Name = "schemaTree";
            this.schemaTree.Size = new System.Drawing.Size(396, 328);
            this.schemaTree.TabIndex = 0;
            this.schemaTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSchema_AfterSelect);
            // 
            // schemaTreeView
            // 
            this.schemaTreeView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.RightMenuAddElement,
            this.RightMenuAddST,
            this.RightMenuAddCT,
            this.RightMenuAddAttribute,
            this.RightMenuRemoveNode});
            // 
            // RightMenuAddElement
            // 
            this.RightMenuAddElement.Index = 0;
            this.RightMenuAddElement.Text = "Add Element";
            this.RightMenuAddElement.Click += new System.EventHandler(this.MenuMouseAddEle_Click);
            // 
            // RightMenuAddST
            // 
            this.RightMenuAddST.Index = 1;
            this.RightMenuAddST.Text = "Add SimpleType";
            this.RightMenuAddST.Click += new System.EventHandler(this.MenuMouseAddST_Click);
            // 
            // RightMenuAddCT
            // 
            this.RightMenuAddCT.Index = 2;
            this.RightMenuAddCT.Text = "Add ComplexType";
            this.RightMenuAddCT.Click += new System.EventHandler(this.MenuMouseAddCT_Click);
            // 
            // RightMenuAddAttribute
            // 
            this.RightMenuAddAttribute.Index = 3;
            this.RightMenuAddAttribute.Text = "Add Attribute";
            this.RightMenuAddAttribute.Click += new System.EventHandler(this.MouseMenuAddAttribute_Click);
            // 
            // RightMenuRemoveNode
            // 
            this.RightMenuRemoveNode.Index = 4;
            this.RightMenuRemoveNode.Text = "Remove Selected Node";
            this.RightMenuRemoveNode.Click += new System.EventHandler(this.mnuRemoveNode_Click);
            // 
            // mnuFacetEnumeration
            // 
            this.mnuFacetEnumeration.Index = -1;
            this.mnuFacetEnumeration.Text = "";
            // 
            // mnuFacetMaxExclusive
            // 
            this.mnuFacetMaxExclusive.Index = -1;
            this.mnuFacetMaxExclusive.Text = "";
            // 
            // mnuFacetMaxInclusive
            // 
            this.mnuFacetMaxInclusive.Index = -1;
            this.mnuFacetMaxInclusive.Text = "";
            // 
            // mnuFacetMinExclusive
            // 
            this.mnuFacetMinExclusive.Index = -1;
            this.mnuFacetMinExclusive.Text = "";
            // 
            // mnuFacetMinInclusive
            // 
            this.mnuFacetMinInclusive.Index = -1;
            this.mnuFacetMinInclusive.Text = "";
            // 
            // mnuFacetNumeric
            // 
            this.mnuFacetNumeric.Index = -1;
            this.mnuFacetNumeric.Text = "";
            // 
            // mnuFacetFractionDigits
            // 
            this.mnuFacetFractionDigits.Index = -1;
            this.mnuFacetFractionDigits.Text = "";
            // 
            // mnuFacetLength
            // 
            this.mnuFacetLength.Index = -1;
            this.mnuFacetLength.Text = "";
            // 
            // mnuFacetMaxLength
            // 
            this.mnuFacetMaxLength.Index = -1;
            this.mnuFacetMaxLength.Text = "";
            // 
            // mnuFacetMinLength
            // 
            this.mnuFacetMinLength.Index = -1;
            this.mnuFacetMinLength.Text = "";
            // 
            // mnuFacetTotalDigits
            // 
            this.mnuFacetTotalDigits.Index = -1;
            this.mnuFacetTotalDigits.Text = "";
            // 
            // mnuFacetPattern
            // 
            this.mnuFacetPattern.Index = -1;
            this.mnuFacetPattern.Text = "";
            // 
            // mnuFacetWhiteSpace
            // 
            this.mnuFacetWhiteSpace.Index = -1;
            this.mnuFacetWhiteSpace.Text = "";
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRootElement,
            this.toolStripLabel1,
            this.tsbtnAddElement,
            this.toolStripLabel2,
            this.tsbtnAddAttribute,
            this.toolStripLabel3,
            this.tsbtnDelete,
            this.toolStripLabel4,
            this.tsbtnAddData,
            this.toolStripLabel5,
            this.tsbtnAddCDATA,
            this.toolStripLabel6,
            this.tsbtnComment,
            this.toolStripLabel7});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip.Size = new System.Drawing.Size(87, 480);
            this.toolStrip.TabIndex = 5;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsbtnRootElement
            // 
            this.tsbtnRootElement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRootElement.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRootElement.Image")));
            this.tsbtnRootElement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRootElement.Name = "tsbtnRootElement";
            this.tsbtnRootElement.Size = new System.Drawing.Size(84, 29);
            this.tsbtnRootElement.Text = "Root Element";
            this.tsbtnRootElement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnRootElement.ToolTipText = "Insert a Root Element";
            this.tsbtnRootElement.Click += new System.EventHandler(this.tsbtnRootElement_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(84, 15);
            this.toolStripLabel1.Text = "Add Root";
            // 
            // tsbtnAddElement
            // 
            this.tsbtnAddElement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddElement.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddElement.Image")));
            this.tsbtnAddElement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddElement.Name = "tsbtnAddElement";
            this.tsbtnAddElement.Size = new System.Drawing.Size(84, 29);
            this.tsbtnAddElement.Text = "Add Element";
            this.tsbtnAddElement.Click += new System.EventHandler(this.tsbtnAddElement_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(84, 15);
            this.toolStripLabel2.Text = "Add Element";
            // 
            // tsbtnAddAttribute
            // 
            this.tsbtnAddAttribute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddAttribute.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddAttribute.Image")));
            this.tsbtnAddAttribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddAttribute.Name = "tsbtnAddAttribute";
            this.tsbtnAddAttribute.Size = new System.Drawing.Size(84, 29);
            this.tsbtnAddAttribute.Text = "Add Attribute";
            this.tsbtnAddAttribute.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbtnAddAttribute.Click += new System.EventHandler(this.tsbAddAttribute_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(84, 15);
            this.toolStripLabel3.Text = "Add Attribute";
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDelete.Image")));
            this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.Size = new System.Drawing.Size(84, 29);
            this.tsbtnDelete.Text = "toolStripButton1";
            this.tsbtnDelete.Click += new System.EventHandler(this.tsbtnDelete_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(84, 15);
            this.toolStripLabel4.Text = "Delete";
            // 
            // tsbtnAddData
            // 
            this.tsbtnAddData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddData.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddData.Image")));
            this.tsbtnAddData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddData.Name = "tsbtnAddData";
            this.tsbtnAddData.Size = new System.Drawing.Size(84, 29);
            this.tsbtnAddData.Text = "Add Data";
            this.tsbtnAddData.Click += new System.EventHandler(this.tsbtnAddData_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(84, 15);
            this.toolStripLabel5.Text = "Add Data";
            // 
            // tsbtnAddCDATA
            // 
            this.tsbtnAddCDATA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddCDATA.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddCDATA.Image")));
            this.tsbtnAddCDATA.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddCDATA.Name = "tsbtnAddCDATA";
            this.tsbtnAddCDATA.Size = new System.Drawing.Size(84, 29);
            this.tsbtnAddCDATA.Text = "Add CData";
            this.tsbtnAddCDATA.Click += new System.EventHandler(this.tsbtnAddCDATA_Click);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(84, 15);
            this.toolStripLabel6.Text = "Add CDATA";
            // 
            // tsbtnComment
            // 
            this.tsbtnComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnComment.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnComment.Image")));
            this.tsbtnComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnComment.Name = "tsbtnComment";
            this.tsbtnComment.Size = new System.Drawing.Size(84, 29);
            this.tsbtnComment.Text = "Add Comment";
            this.tsbtnComment.Click += new System.EventHandler(this.tsbtnComment_Click);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(84, 15);
            this.toolStripLabel7.Text = "Add Comment";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 526);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.mainTabs);
            this.Controls.Add(this.mainStatusBar);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "Main";
            this.Text = "xsAuthor v1.0a";
            this.Load += new System.EventHandler(this.Main_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainStatusBar.ResumeLayout(false);
            this.mainStatusBar.PerformLayout();
            this.mainTabs.ResumeLayout(false);
            this.tabXML.ResumeLayout(false);
            this.tabXML.PerformLayout();
            this.tabSchema.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.StatusStrip mainStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusXpath;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabs;
        private System.Windows.Forms.TabPage tabXML;
        private System.Windows.Forms.TabPage tabSchema;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbtnRootElement;
        private System.Windows.Forms.TreeView xmlTreeview;
        private System.Windows.Forms.ToolStripButton tsbtnAddElement;
        private System.Windows.Forms.ToolStripButton tsbtnAddAttribute;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private System.Windows.Forms.ToolStripButton tsbtnAddData;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusElementType;
        private System.Windows.Forms.ToolStripButton tsbtnAddCDATA;
        private System.Windows.Forms.ToolStripButton tsbtnComment;
        private System.Windows.Forms.WebBrowser xmlBrowserWindow;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboGlobal;
        private System.Windows.Forms.ComboBox comboSimple;
        private System.Windows.Forms.Label lblGT;
        private System.Windows.Forms.Label lblST;
        private System.Windows.Forms.TreeView schemaTree;
        private System.Windows.Forms.ContextMenu schemaTreeView;
        private System.Windows.Forms.MenuItem RightMenuAddElement;
        private System.Windows.Forms.MenuItem RightMenuAddST;
        private System.Windows.Forms.MenuItem RightMenuAddCT;
        private System.Windows.Forms.MenuItem RightMenuAddAttribute;
        private System.Windows.Forms.MenuItem mnuFacetEnumeration;
        private System.Windows.Forms.MenuItem mnuFacetMaxExclusive;
        private System.Windows.Forms.MenuItem mnuFacetMaxInclusive;
        private System.Windows.Forms.MenuItem mnuFacetMinExclusive;
        private System.Windows.Forms.MenuItem mnuFacetMinInclusive;
        private System.Windows.Forms.MenuItem mnuFacetNumeric;
        private System.Windows.Forms.MenuItem mnuFacetFractionDigits;
        private System.Windows.Forms.MenuItem mnuFacetLength;
        private System.Windows.Forms.MenuItem mnuFacetMaxLength;
        private System.Windows.Forms.MenuItem mnuFacetMinLength;
        private System.Windows.Forms.MenuItem mnuFacetTotalDigits;
        private System.Windows.Forms.MenuItem mnuFacetPattern;
        private System.Windows.Forms.MenuItem mnuFacetWhiteSpace;
        private System.Windows.Forms.MenuItem RightMenuRemoveNode;
        private System.Windows.Forms.WebBrowser schemaBrowser;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem validateXMLFromSchemaToolStripMenuItem;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
    }
}

