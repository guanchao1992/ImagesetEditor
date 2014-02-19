namespace ImageSetEditor
{
    partial class ImagesetEditControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.usedListView = new System.Windows.Forms.ListView();
            this.usedColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.unsedImageContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delusedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usedSelectSortNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usedSelectSortNameReverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.usedSelectSortSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usedSelectSortSizeReverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usedToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addImageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.clearUsedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.imageCountToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.imageSetBox = new System.Windows.Forms.PictureBox();
            this.imageSetBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imagename01ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagename02ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagename03ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagename04ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagename05ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagename06ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagename07ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagename08ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagename09ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagename10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.delusedToolStripMenuItem_Click2 = new System.Windows.Forms.ToolStripMenuItem();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.fitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.sizeSetToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.nameToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.posToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.sizeToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.unsedImageContextMenuStrip.SuspendLayout();
            this.usedToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageSetBox)).BeginInit();
            this.imageSetBoxContextMenuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.usedListView);
            this.splitContainer.Panel1.Controls.Add(this.usedToolStrip);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.imageSetBox);
            this.splitContainer.Panel2.Controls.Add(this.vScrollBar);
            this.splitContainer.Panel2.Controls.Add(this.hScrollBar);
            this.splitContainer.Panel2.Controls.Add(this.toolStrip);
            this.splitContainer.Size = new System.Drawing.Size(956, 503);
            this.splitContainer.SplitterDistance = 179;
            this.splitContainer.TabIndex = 0;
            // 
            // usedListView
            // 
            this.usedListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.usedColumnHeader});
            this.usedListView.ContextMenuStrip = this.unsedImageContextMenuStrip;
            this.usedListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usedListView.HideSelection = false;
            this.usedListView.Location = new System.Drawing.Point(0, 25);
            this.usedListView.Name = "usedListView";
            this.usedListView.Size = new System.Drawing.Size(179, 478);
            this.usedListView.TabIndex = 1;
            this.usedListView.UseCompatibleStateImageBehavior = false;
            this.usedListView.View = System.Windows.Forms.View.Details;
            this.usedListView.SelectedIndexChanged += new System.EventHandler(this.usedListView_SelectedIndexChanged);
            this.usedListView.SizeChanged += new System.EventHandler(this.usedListView_SizeChanged);
            // 
            // usedColumnHeader
            // 
            this.usedColumnHeader.Text = "";
            // 
            // unsedImageContextMenuStrip
            // 
            this.unsedImageContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.delusedToolStripMenuItem,
            this.toolStripMenuItem4,
            this.sortToolStripMenuItem});
            this.unsedImageContextMenuStrip.Name = "unusedImageContextMenuStrip";
            this.unsedImageContextMenuStrip.Size = new System.Drawing.Size(166, 82);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.selectAllToolStripMenuItem.Text = "全选";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // delusedToolStripMenuItem
            // 
            this.delusedToolStripMenuItem.Name = "delusedToolStripMenuItem";
            this.delusedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.delusedToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.delusedToolStripMenuItem.Text = "删除";
            this.delusedToolStripMenuItem.Click += new System.EventHandler(this.delusedToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(162, 6);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usedSelectSortNameToolStripMenuItem,
            this.usedSelectSortNameReverseToolStripMenuItem,
            this.toolStripMenuItem5,
            this.usedSelectSortSizeToolStripMenuItem,
            this.usedSelectSortSizeReverseToolStripMenuItem,
            this.toolStripMenuItem6,
            this.invertToolStripMenuItem});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.sortToolStripMenuItem.Text = "排序";
            // 
            // usedSelectSortNameToolStripMenuItem
            // 
            this.usedSelectSortNameToolStripMenuItem.Name = "usedSelectSortNameToolStripMenuItem";
            this.usedSelectSortNameToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.usedSelectSortNameToolStripMenuItem.Text = "按名称顺序";
            this.usedSelectSortNameToolStripMenuItem.Click += new System.EventHandler(this.usedSelectSortNameToolStripMenuItem_Click);
            // 
            // usedSelectSortNameReverseToolStripMenuItem
            // 
            this.usedSelectSortNameReverseToolStripMenuItem.Name = "usedSelectSortNameReverseToolStripMenuItem";
            this.usedSelectSortNameReverseToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.usedSelectSortNameReverseToolStripMenuItem.Text = "按名称倒序";
            this.usedSelectSortNameReverseToolStripMenuItem.Click += new System.EventHandler(this.usedSelectSortNameReverseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(150, 6);
            // 
            // usedSelectSortSizeToolStripMenuItem
            // 
            this.usedSelectSortSizeToolStripMenuItem.Name = "usedSelectSortSizeToolStripMenuItem";
            this.usedSelectSortSizeToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.usedSelectSortSizeToolStripMenuItem.Text = "按尺寸顺序";
            this.usedSelectSortSizeToolStripMenuItem.Click += new System.EventHandler(this.usedSelectSortSizeToolStripMenuItem_Click);
            // 
            // usedSelectSortSizeReverseToolStripMenuItem
            // 
            this.usedSelectSortSizeReverseToolStripMenuItem.Name = "usedSelectSortSizeReverseToolStripMenuItem";
            this.usedSelectSortSizeReverseToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.usedSelectSortSizeReverseToolStripMenuItem.Text = "按尺寸倒序";
            this.usedSelectSortSizeReverseToolStripMenuItem.Click += new System.EventHandler(this.usedSelectSortSizeReverseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(150, 6);
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.invertToolStripMenuItem.Text = "倒置";
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.invertToolStripMenuItem_Click);
            // 
            // usedToolStrip
            // 
            this.usedToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.usedToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.imageCountToolStripLabel});
            this.usedToolStrip.Location = new System.Drawing.Point(0, 0);
            this.usedToolStrip.Name = "usedToolStrip";
            this.usedToolStrip.Size = new System.Drawing.Size(179, 25);
            this.usedToolStrip.TabIndex = 2;
            this.usedToolStrip.Text = "toolStrip2";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addImageMenuItem,
            this.toolStripSeparator5,
            this.clearUsedToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(66, 25);
            this.toolStripMenuItem1.Text = "图片组";
            // 
            // addImageMenuItem
            // 
            this.addImageMenuItem.Name = "addImageMenuItem";
            this.addImageMenuItem.Size = new System.Drawing.Size(153, 24);
            this.addImageMenuItem.Text = "添加图片";
            this.addImageMenuItem.Click += new System.EventHandler(this.addImageMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(150, 6);
            // 
            // clearUsedToolStripMenuItem
            // 
            this.clearUsedToolStripMenuItem.Name = "clearUsedToolStripMenuItem";
            this.clearUsedToolStripMenuItem.Size = new System.Drawing.Size(153, 24);
            this.clearUsedToolStripMenuItem.Text = "清空图片组";
            this.clearUsedToolStripMenuItem.Click += new System.EventHandler(this.clearUsedToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // imageCountToolStripLabel
            // 
            this.imageCountToolStripLabel.Name = "imageCountToolStripLabel";
            this.imageCountToolStripLabel.Size = new System.Drawing.Size(86, 22);
            this.imageCountToolStripLabel.Text = "共 0 个图片";
            // 
            // imageSetBox
            // 
            this.imageSetBox.BackColor = System.Drawing.Color.Gray;
            this.imageSetBox.ContextMenuStrip = this.imageSetBoxContextMenuStrip;
            this.imageSetBox.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.imageSetBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageSetBox.Location = new System.Drawing.Point(0, 27);
            this.imageSetBox.Name = "imageSetBox";
            this.imageSetBox.Size = new System.Drawing.Size(752, 455);
            this.imageSetBox.TabIndex = 2;
            this.imageSetBox.TabStop = false;
            this.imageSetBox.SizeChanged += new System.EventHandler(this.imageSetBox_SizeChanged);
            this.imageSetBox.Paint += new System.Windows.Forms.PaintEventHandler(this.imageSetBox_Paint);
            this.imageSetBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageSetBox_MouseDown);
            this.imageSetBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageSetBox_MouseMove);
            this.imageSetBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageSetBox_MouseUp);
            // 
            // imageSetBoxContextMenuStrip
            // 
            this.imageSetBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imagename01ToolStripMenuItem,
            this.imagename02ToolStripMenuItem,
            this.imagename03ToolStripMenuItem,
            this.imagename04ToolStripMenuItem,
            this.imagename05ToolStripMenuItem,
            this.imagename06ToolStripMenuItem,
            this.imagename07ToolStripMenuItem,
            this.imagename08ToolStripMenuItem,
            this.imagename09ToolStripMenuItem,
            this.imagename10ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.delusedToolStripMenuItem_Click2});
            this.imageSetBoxContextMenuStrip.Name = "imageSetBoxContextMenuStrip";
            this.imageSetBoxContextMenuStrip.Size = new System.Drawing.Size(182, 274);
            this.imageSetBoxContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.imageSetBoxContextMenuStrip_Opening);
            // 
            // imagename01ToolStripMenuItem
            // 
            this.imagename01ToolStripMenuItem.Name = "imagename01ToolStripMenuItem";
            this.imagename01ToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.imagename01ToolStripMenuItem.Text = "imagename01";
            this.imagename01ToolStripMenuItem.Click += new System.EventHandler(this.imagenameToolStripMenuItem_Click);
            // 
            // imagename02ToolStripMenuItem
            // 
            this.imagename02ToolStripMenuItem.Name = "imagename02ToolStripMenuItem";
            this.imagename02ToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.imagename02ToolStripMenuItem.Text = "imagename02";
            this.imagename02ToolStripMenuItem.Click += new System.EventHandler(this.imagenameToolStripMenuItem_Click);
            // 
            // imagename03ToolStripMenuItem
            // 
            this.imagename03ToolStripMenuItem.Name = "imagename03ToolStripMenuItem";
            this.imagename03ToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.imagename03ToolStripMenuItem.Text = "imagename03";
            this.imagename03ToolStripMenuItem.Click += new System.EventHandler(this.imagenameToolStripMenuItem_Click);
            // 
            // imagename04ToolStripMenuItem
            // 
            this.imagename04ToolStripMenuItem.Name = "imagename04ToolStripMenuItem";
            this.imagename04ToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.imagename04ToolStripMenuItem.Text = "imagename04";
            this.imagename04ToolStripMenuItem.Click += new System.EventHandler(this.imagenameToolStripMenuItem_Click);
            // 
            // imagename05ToolStripMenuItem
            // 
            this.imagename05ToolStripMenuItem.Name = "imagename05ToolStripMenuItem";
            this.imagename05ToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.imagename05ToolStripMenuItem.Text = "imagename05";
            this.imagename05ToolStripMenuItem.Click += new System.EventHandler(this.imagenameToolStripMenuItem_Click);
            // 
            // imagename06ToolStripMenuItem
            // 
            this.imagename06ToolStripMenuItem.Name = "imagename06ToolStripMenuItem";
            this.imagename06ToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.imagename06ToolStripMenuItem.Text = "imagename06";
            this.imagename06ToolStripMenuItem.Click += new System.EventHandler(this.imagenameToolStripMenuItem_Click);
            // 
            // imagename07ToolStripMenuItem
            // 
            this.imagename07ToolStripMenuItem.Name = "imagename07ToolStripMenuItem";
            this.imagename07ToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.imagename07ToolStripMenuItem.Text = "imagename07";
            this.imagename07ToolStripMenuItem.Click += new System.EventHandler(this.imagenameToolStripMenuItem_Click);
            // 
            // imagename08ToolStripMenuItem
            // 
            this.imagename08ToolStripMenuItem.Name = "imagename08ToolStripMenuItem";
            this.imagename08ToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.imagename08ToolStripMenuItem.Text = "imagename08";
            this.imagename08ToolStripMenuItem.Click += new System.EventHandler(this.imagenameToolStripMenuItem_Click);
            // 
            // imagename09ToolStripMenuItem
            // 
            this.imagename09ToolStripMenuItem.Name = "imagename09ToolStripMenuItem";
            this.imagename09ToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.imagename09ToolStripMenuItem.Text = "imagename09";
            this.imagename09ToolStripMenuItem.Click += new System.EventHandler(this.imagenameToolStripMenuItem_Click);
            // 
            // imagename10ToolStripMenuItem
            // 
            this.imagename10ToolStripMenuItem.Name = "imagename10ToolStripMenuItem";
            this.imagename10ToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.imagename10ToolStripMenuItem.Text = "imagename10";
            this.imagename10ToolStripMenuItem.Click += new System.EventHandler(this.imagenameToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(178, 6);
            // 
            // delusedToolStripMenuItem_Click2
            // 
            this.delusedToolStripMenuItem_Click2.Name = "delusedToolStripMenuItem_Click2";
            this.delusedToolStripMenuItem_Click2.Size = new System.Drawing.Size(181, 24);
            this.delusedToolStripMenuItem_Click2.Text = "删除";
            this.delusedToolStripMenuItem_Click2.Click += new System.EventHandler(this.delusedToolStripMenuItem_Click);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.LargeChange = 20;
            this.vScrollBar.Location = new System.Drawing.Point(752, 27);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(21, 455);
            this.vScrollBar.TabIndex = 1;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            // 
            // hScrollBar
            // 
            this.hScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar.Location = new System.Drawing.Point(0, 482);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(773, 21);
            this.hScrollBar.TabIndex = 0;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.sizeSetToolStripComboBox,
            this.toolStripSeparator4,
            this.toolStripLabel2,
            this.nameToolStripTextBox,
            this.toolStripLabel3,
            this.posToolStripTextBox,
            this.toolStripLabel4,
            this.sizeToolStripTextBox});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(773, 27);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fitToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(51, 27);
            this.toolStripMenuItem2.Text = "画布";
            // 
            // fitToolStripMenuItem
            // 
            this.fitToolStripMenuItem.Name = "fitToolStripMenuItem";
            this.fitToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.fitToolStripMenuItem.Text = "自动贴合";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 24);
            this.toolStripLabel1.Text = "尺寸";
            // 
            // sizeSetToolStripComboBox
            // 
            this.sizeSetToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sizeSetToolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.sizeSetToolStripComboBox.Items.AddRange(new object[] {
            "64*64",
            "128*128",
            "256*256",
            "512*512",
            "1024*1024",
            "2048*2048",
            "4096*4096"});
            this.sizeSetToolStripComboBox.Name = "sizeSetToolStripComboBox";
            this.sizeSetToolStripComboBox.Size = new System.Drawing.Size(121, 27);
            this.sizeSetToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.sizeSetToolStripComboBox_SelectedIndexChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(39, 24);
            this.toolStripLabel2.Text = "名称";
            // 
            // nameToolStripTextBox
            // 
            this.nameToolStripTextBox.Name = "nameToolStripTextBox";
            this.nameToolStripTextBox.Size = new System.Drawing.Size(150, 27);
            this.nameToolStripTextBox.Leave += new System.EventHandler(this.nameToolStripTextBox_Leave);
            this.nameToolStripTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nameToolStripTextBox_KeyUp);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(39, 24);
            this.toolStripLabel3.Text = "位置";
            // 
            // posToolStripTextBox
            // 
            this.posToolStripTextBox.Name = "posToolStripTextBox";
            this.posToolStripTextBox.Size = new System.Drawing.Size(100, 27);
            this.posToolStripTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.posToolStripTextBox_KeyUp);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(39, 24);
            this.toolStripLabel4.Text = "尺寸";
            // 
            // sizeToolStripTextBox
            // 
            this.sizeToolStripTextBox.Name = "sizeToolStripTextBox";
            this.sizeToolStripTextBox.ReadOnly = true;
            this.sizeToolStripTextBox.Size = new System.Drawing.Size(100, 27);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "图片文件(png,bmp,jpg)|*.png;*.bmp;*.jpg;";
            this.openFileDialog.Multiselect = true;
            // 
            // ImagesetEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "ImagesetEditControl";
            this.Size = new System.Drawing.Size(956, 503);
            this.Load += new System.EventHandler(this.ImagesetEditControl_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.unsedImageContextMenuStrip.ResumeLayout(false);
            this.usedToolStrip.ResumeLayout(false);
            this.usedToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageSetBox)).EndInit();
            this.imageSetBoxContextMenuStrip.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.PictureBox imageSetBox;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripComboBox sizeSetToolStripComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView usedListView;
        private System.Windows.Forms.ToolStrip usedToolStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem clearUsedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem fitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ContextMenuStrip unsedImageContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem delusedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usedSelectSortNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usedSelectSortNameReverseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem usedSelectSortSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usedSelectSortSizeReverseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox nameToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox posToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox sizeToolStripTextBox;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader usedColumnHeader;
        private System.Windows.Forms.ToolStripMenuItem addImageMenuItem;
        private System.Windows.Forms.ContextMenuStrip imageSetBoxContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem delusedToolStripMenuItem_Click2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel imageCountToolStripLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagename01ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagename02ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagename03ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagename04ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagename05ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagename06ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagename07ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagename08ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagename09ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagename10ToolStripMenuItem;
    }
}
