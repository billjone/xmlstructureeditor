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
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listErrors = new System.Windows.Forms.ListBox();
            this.mainStatusBar = new System.Windows.Forms.StatusStrip();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsStatusLabelClear = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLabelnodeIndex = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.tabXML = new System.Windows.Forms.TabPage();
            this.xmlBrowserWindow = new System.Windows.Forms.WebBrowser();
            this.xmlTreeview = new NewControls.TreeViewWithPaint();
            this.tabSchema = new System.Windows.Forms.TabPage();
            this.schemaTreeview = new NewControls.TreeViewWithPaint();
            this.rtbSchema = new System.Windows.Forms.RichTextBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbtnRootElement = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAddElement = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAddAttribute = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAddData = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAddCDATA = new System.Windows.Forms.ToolStripButton();
            this.tsbtnComment = new System.Windows.Forms.ToolStripButton();
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
            this.printPreviewToolStripMenuItem,
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
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
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
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
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
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
            this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
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
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
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
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
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
            // 
            // listErrors
            // 
            this.listErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listErrors.FormattingEnabled = true;
            this.listErrors.Location = new System.Drawing.Point(37, 611);
            this.listErrors.Name = "listErrors";
            this.listErrors.Size = new System.Drawing.Size(1060, 95);
            this.listErrors.TabIndex = 1;
            // 
            // mainStatusBar
            // 
            this.mainStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusLabel,
            this.tsStatusLabelClear,
            this.tsLabelnodeIndex});
            this.mainStatusBar.Location = new System.Drawing.Point(0, 715);
            this.mainStatusBar.Name = "mainStatusBar";
            this.mainStatusBar.Size = new System.Drawing.Size(1113, 22);
            this.mainStatusBar.TabIndex = 2;
            this.mainStatusBar.Text = "statusStrip1";
            // 
            // tsStatusLabel
            // 
            this.tsStatusLabel.Name = "tsStatusLabel";
            this.tsStatusLabel.Size = new System.Drawing.Size(118, 17);
            this.tsStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // tsStatusLabelClear
            // 
            this.tsStatusLabelClear.Name = "tsStatusLabelClear";
            this.tsStatusLabelClear.Size = new System.Drawing.Size(118, 17);
            this.tsStatusLabelClear.Text = "toolStripStatusLabel1";
            // 
            // tsLabelnodeIndex
            // 
            this.tsLabelnodeIndex.Name = "tsLabelnodeIndex";
            this.tsLabelnodeIndex.Size = new System.Drawing.Size(118, 17);
            this.tsLabelnodeIndex.Text = "toolStripStatusLabel1";
            // 
            // mainTabs
            // 
            this.mainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabs.Controls.Add(this.tabXML);
            this.mainTabs.Controls.Add(this.tabSchema);
            this.mainTabs.Location = new System.Drawing.Point(37, 27);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(1064, 578);
            this.mainTabs.TabIndex = 4;
            // 
            // tabXML
            // 
            this.tabXML.Controls.Add(this.xmlBrowserWindow);
            this.tabXML.Controls.Add(this.xmlTreeview);
            this.tabXML.Location = new System.Drawing.Point(4, 22);
            this.tabXML.Name = "tabXML";
            this.tabXML.Padding = new System.Windows.Forms.Padding(3);
            this.tabXML.Size = new System.Drawing.Size(1056, 552);
            this.tabXML.TabIndex = 0;
            this.tabXML.Text = "XML";
            this.tabXML.UseVisualStyleBackColor = true;
            // 
            // xmlBrowserWindow
            // 
            this.xmlBrowserWindow.Location = new System.Drawing.Point(315, 6);
            this.xmlBrowserWindow.MinimumSize = new System.Drawing.Size(20, 20);
            this.xmlBrowserWindow.Name = "xmlBrowserWindow";
            this.xmlBrowserWindow.Size = new System.Drawing.Size(735, 538);
            this.xmlBrowserWindow.TabIndex = 3;
            // 
            // xmlTreeview
            // 
            this.xmlTreeview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.xmlTreeview.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmlTreeview.Location = new System.Drawing.Point(3, 3);
            this.xmlTreeview.Name = "xmlTreeview";
            this.xmlTreeview.Size = new System.Drawing.Size(305, 541);
            this.xmlTreeview.TabIndex = 2;
            // 
            // tabSchema
            // 
            this.tabSchema.Controls.Add(this.schemaTreeview);
            this.tabSchema.Controls.Add(this.rtbSchema);
            this.tabSchema.Location = new System.Drawing.Point(4, 22);
            this.tabSchema.Name = "tabSchema";
            this.tabSchema.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchema.Size = new System.Drawing.Size(1056, 552);
            this.tabSchema.TabIndex = 1;
            this.tabSchema.Text = "Schema";
            this.tabSchema.UseVisualStyleBackColor = true;
            // 
            // schemaTreeview
            // 
            this.schemaTreeview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.schemaTreeview.Location = new System.Drawing.Point(3, 4);
            this.schemaTreeview.Name = "schemaTreeview";
            this.schemaTreeview.Size = new System.Drawing.Size(183, 541);
            this.schemaTreeview.TabIndex = 2;
            // 
            // rtbSchema
            // 
            this.rtbSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbSchema.Location = new System.Drawing.Point(192, 4);
            this.rtbSchema.Name = "rtbSchema";
            this.rtbSchema.Size = new System.Drawing.Size(863, 541);
            this.rtbSchema.TabIndex = 1;
            this.rtbSchema.Text = "";
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnRootElement,
            this.tsbtnAddElement,
            this.tsbtnAddAttribute,
            this.tsbtnDelete,
            this.tsbtnAddData,
            this.tsbtnAddCDATA,
            this.tsbtnComment});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip.Size = new System.Drawing.Size(30, 691);
            this.toolStrip.TabIndex = 5;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsbtnRootElement
            // 
            this.tsbtnRootElement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRootElement.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRootElement.Image")));
            this.tsbtnRootElement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRootElement.Name = "tsbtnRootElement";
            this.tsbtnRootElement.Size = new System.Drawing.Size(27, 29);
            this.tsbtnRootElement.Text = "Root Element";
            this.tsbtnRootElement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnRootElement.ToolTipText = "Insert a Root Element";
            this.tsbtnRootElement.Click += new System.EventHandler(this.tsbtnRootElement_Click);
            // 
            // tsbtnAddElement
            // 
            this.tsbtnAddElement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddElement.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddElement.Image")));
            this.tsbtnAddElement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddElement.Name = "tsbtnAddElement";
            this.tsbtnAddElement.Size = new System.Drawing.Size(27, 29);
            this.tsbtnAddElement.Text = "Add Element";
            this.tsbtnAddElement.Click += new System.EventHandler(this.tsbtnAddElement_Click);
            // 
            // tsbtnAddAttribute
            // 
            this.tsbtnAddAttribute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddAttribute.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddAttribute.Image")));
            this.tsbtnAddAttribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddAttribute.Name = "tsbtnAddAttribute";
            this.tsbtnAddAttribute.Size = new System.Drawing.Size(27, 29);
            this.tsbtnAddAttribute.Text = "Add Attribute";
            this.tsbtnAddAttribute.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbtnAddAttribute.Click += new System.EventHandler(this.tsbAddAttribute_Click);
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDelete.Image")));
            this.tsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.Size = new System.Drawing.Size(27, 29);
            this.tsbtnDelete.Text = "toolStripButton1";
            this.tsbtnDelete.Click += new System.EventHandler(this.tsbtnDelete_Click);
            // 
            // tsbtnAddData
            // 
            this.tsbtnAddData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddData.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddData.Image")));
            this.tsbtnAddData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddData.Name = "tsbtnAddData";
            this.tsbtnAddData.Size = new System.Drawing.Size(27, 29);
            this.tsbtnAddData.Text = "Add Data";
            this.tsbtnAddData.Click += new System.EventHandler(this.tsbtnAddData_Click);
            // 
            // tsbtnAddCDATA
            // 
            this.tsbtnAddCDATA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddCDATA.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddCDATA.Image")));
            this.tsbtnAddCDATA.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddCDATA.Name = "tsbtnAddCDATA";
            this.tsbtnAddCDATA.Size = new System.Drawing.Size(27, 29);
            this.tsbtnAddCDATA.Text = "Add CData";
            this.tsbtnAddCDATA.Click += new System.EventHandler(this.tsbtnAddCDATA_Click);
            // 
            // tsbtnComment
            // 
            this.tsbtnComment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnComment.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnComment.Image")));
            this.tsbtnComment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnComment.Name = "tsbtnComment";
            this.tsbtnComment.Size = new System.Drawing.Size(27, 29);
            this.tsbtnComment.Text = "Add Comment";
            this.tsbtnComment.Click += new System.EventHandler(this.tsbtnComment_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 737);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.mainTabs);
            this.Controls.Add(this.mainStatusBar);
            this.Controls.Add(this.listErrors);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "Main";
            this.Text = "XML Structure Editor";
            this.Load += new System.EventHandler(this.Main_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainStatusBar.ResumeLayout(false);
            this.mainStatusBar.PerformLayout();
            this.mainTabs.ResumeLayout(false);
            this.tabXML.ResumeLayout(false);
            this.tabSchema.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ListBox listErrors;
        private System.Windows.Forms.StatusStrip mainStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
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
        private System.Windows.Forms.RichTextBox rtbSchema;
        private System.Windows.Forms.ToolStripButton tsbtnRootElement;
        private NewControls.TreeViewWithPaint xmlTreeview;
        private NewControls.TreeViewWithPaint schemaTreeview;
        private System.Windows.Forms.ToolStripButton tsbtnAddElement;
        private System.Windows.Forms.ToolStripButton tsbtnAddAttribute;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private System.Windows.Forms.ToolStripButton tsbtnAddData;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabelClear;
        private System.Windows.Forms.ToolStripButton tsbtnAddCDATA;
        private System.Windows.Forms.ToolStripButton tsbtnComment;
        private System.Windows.Forms.ToolStripStatusLabel tsLabelnodeIndex;
        private System.Windows.Forms.WebBrowser xmlBrowserWindow;
    }
}

