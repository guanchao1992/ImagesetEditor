﻿namespace ImagesetEditor
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Default", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagesetEditControl));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.usedListView = new System.Windows.Forms.ListView();
            this.usedColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.unsedImageContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delusedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usedSelectSortNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usedSelectSortNameReverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.usedSelectSortSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usedSelectSortSizeReverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.moveToGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usedToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addImageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.newGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editGroupItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.clearUsedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.imageCountToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.imageSetBox = new System.Windows.Forms.PictureBox();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rimViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorWorkspaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.canvasSizeToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.sizeSetToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.nameToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.nameToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.autoTypesettingToolStripLabel = new System.Windows.Forms.ToolStripDropDownButton();
            this.horizontalLimitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalLimitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.amountMinusToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.amountSetToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.amountAddToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.positionToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.posToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.sizeToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.sizeToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.imageSetBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delusedToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.alignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftOutsideAlignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftInsideAlignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightOutsideAlignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightInsideAlignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.topOutsideAlignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topInsideAlignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomOutsideAlignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomInsideAlignmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.unsedImageContextMenuStrip.SuspendLayout();
            this.usedToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageSetBox)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.imageSetBoxContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(2);
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
            this.splitContainer.Size = new System.Drawing.Size(716, 430);
            this.splitContainer.SplitterDistance = 132;
            this.splitContainer.SplitterWidth = 3;
            this.splitContainer.TabIndex = 0;
            // 
            // usedListView
            // 
            this.usedListView.AllowDrop = true;
            this.usedListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.usedColumnHeader});
            this.usedListView.ContextMenuStrip = this.unsedImageContextMenuStrip;
            this.usedListView.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Default";
            listViewGroup1.Name = "default";
            this.usedListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.usedListView.HideSelection = false;
            this.usedListView.Location = new System.Drawing.Point(0, 25);
            this.usedListView.Margin = new System.Windows.Forms.Padding(2);
            this.usedListView.Name = "usedListView";
            this.usedListView.Size = new System.Drawing.Size(132, 405);
            this.usedListView.TabIndex = 1;
            this.usedListView.UseCompatibleStateImageBehavior = false;
            this.usedListView.View = System.Windows.Forms.View.Details;
            this.usedListView.SelectedIndexChanged += new System.EventHandler(this.usedListView_SelectedIndexChanged);
            this.usedListView.SizeChanged += new System.EventHandler(this.usedListView_SizeChanged);
            this.usedListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.usedListView_DragDrop);
            this.usedListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.usedListView_DragEnter);
            this.usedListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.usedListView_KeyDown);
            this.usedListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.usedListView_KeyUp);
            // 
            // usedColumnHeader
            // 
            this.usedColumnHeader.Text = "";
            // 
            // unsedImageContextMenuStrip
            // 
            this.unsedImageContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delusedToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.toolStripMenuItem4,
            this.sortToolStripMenuItem,
            this.toolStripMenuItem8,
            this.moveToGroupToolStripMenuItem});
            this.unsedImageContextMenuStrip.Name = "unusedImageContextMenuStrip";
            this.unsedImageContextMenuStrip.Size = new System.Drawing.Size(173, 104);
            this.unsedImageContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.unsedImageContextMenuStrip_Opening);
            // 
            // delusedToolStripMenuItem
            // 
            this.delusedToolStripMenuItem.Name = "delusedToolStripMenuItem";
            this.delusedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.delusedToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.delusedToolStripMenuItem.Text = "Delete";
            this.delusedToolStripMenuItem.Click += new System.EventHandler(this.delusedToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.selectAllToolStripMenuItem.Text = "Select all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(169, 6);
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
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.sortToolStripMenuItem.Text = "Sort";
            // 
            // usedSelectSortNameToolStripMenuItem
            // 
            this.usedSelectSortNameToolStripMenuItem.Name = "usedSelectSortNameToolStripMenuItem";
            this.usedSelectSortNameToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.usedSelectSortNameToolStripMenuItem.Text = "Sort by name";
            this.usedSelectSortNameToolStripMenuItem.Click += new System.EventHandler(this.usedSelectSortNameToolStripMenuItem_Click);
            // 
            // usedSelectSortNameReverseToolStripMenuItem
            // 
            this.usedSelectSortNameReverseToolStripMenuItem.Name = "usedSelectSortNameReverseToolStripMenuItem";
            this.usedSelectSortNameReverseToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.usedSelectSortNameReverseToolStripMenuItem.Text = "Reverse by name";
            this.usedSelectSortNameReverseToolStripMenuItem.Click += new System.EventHandler(this.usedSelectSortNameReverseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(173, 6);
            // 
            // usedSelectSortSizeToolStripMenuItem
            // 
            this.usedSelectSortSizeToolStripMenuItem.Name = "usedSelectSortSizeToolStripMenuItem";
            this.usedSelectSortSizeToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.usedSelectSortSizeToolStripMenuItem.Text = "Sort by size";
            this.usedSelectSortSizeToolStripMenuItem.Click += new System.EventHandler(this.usedSelectSortSizeToolStripMenuItem_Click);
            // 
            // usedSelectSortSizeReverseToolStripMenuItem
            // 
            this.usedSelectSortSizeReverseToolStripMenuItem.Name = "usedSelectSortSizeReverseToolStripMenuItem";
            this.usedSelectSortSizeReverseToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.usedSelectSortSizeReverseToolStripMenuItem.Text = "Reverse by size";
            this.usedSelectSortSizeReverseToolStripMenuItem.Click += new System.EventHandler(this.usedSelectSortSizeReverseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(173, 6);
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.invertToolStripMenuItem.Text = "Reverse";
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.invertToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(169, 6);
            // 
            // moveToGroupToolStripMenuItem
            // 
            this.moveToGroupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toGroupToolStripMenuItem});
            this.moveToGroupToolStripMenuItem.Name = "moveToGroupToolStripMenuItem";
            this.moveToGroupToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.moveToGroupToolStripMenuItem.Text = "Move to group";
            // 
            // toGroupToolStripMenuItem
            // 
            this.toGroupToolStripMenuItem.Name = "toGroupToolStripMenuItem";
            this.toGroupToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.toGroupToolStripMenuItem.Text = "toGroup";
            this.toGroupToolStripMenuItem.Click += new System.EventHandler(this.toGroupToolStripMenuItem_Click);
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
            this.usedToolStrip.Size = new System.Drawing.Size(132, 25);
            this.usedToolStrip.TabIndex = 2;
            this.usedToolStrip.Text = "toolStrip2";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addImageMenuItem,
            this.toolStripMenuItem7,
            this.newGroupToolStripMenuItem,
            this.editGroupToolStripMenuItem,
            this.toolStripSeparator5,
            this.clearUsedToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(63, 25);
            this.toolStripMenuItem1.Text = "Images";
            this.toolStripMenuItem1.DropDownOpening += new System.EventHandler(this.toolStripMenuItem1_DropDownOpening);
            // 
            // addImageMenuItem
            // 
            this.addImageMenuItem.Name = "addImageMenuItem";
            this.addImageMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addImageMenuItem.Text = "Add Files";
            this.addImageMenuItem.Click += new System.EventHandler(this.addImageMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(166, 6);
            // 
            // newGroupToolStripMenuItem
            // 
            this.newGroupToolStripMenuItem.Name = "newGroupToolStripMenuItem";
            this.newGroupToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.newGroupToolStripMenuItem.Text = "New Group";
            this.newGroupToolStripMenuItem.Click += new System.EventHandler(this.newGroupToolStripMenuItem_Click);
            // 
            // editGroupToolStripMenuItem
            // 
            this.editGroupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editGroupItemToolStripMenuItem});
            this.editGroupToolStripMenuItem.Name = "editGroupToolStripMenuItem";
            this.editGroupToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.editGroupToolStripMenuItem.Text = "Edit Group";
            // 
            // editGroupItemToolStripMenuItem
            // 
            this.editGroupItemToolStripMenuItem.Name = "editGroupItemToolStripMenuItem";
            this.editGroupItemToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.editGroupItemToolStripMenuItem.Text = "editGroup";
            this.editGroupItemToolStripMenuItem.Click += new System.EventHandler(this.editGroupItemToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(166, 6);
            // 
            // clearUsedToolStripMenuItem
            // 
            this.clearUsedToolStripMenuItem.Name = "clearUsedToolStripMenuItem";
            this.clearUsedToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.clearUsedToolStripMenuItem.Text = "Clear all images";
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
            this.imageCountToolStripLabel.Size = new System.Drawing.Size(110, 17);
            this.imageCountToolStripLabel.Text = "Total of 0 images";
            // 
            // imageSetBox
            // 
            this.imageSetBox.BackColor = System.Drawing.Color.Gray;
            this.imageSetBox.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.imageSetBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageSetBox.Location = new System.Drawing.Point(0, 25);
            this.imageSetBox.Margin = new System.Windows.Forms.Padding(2);
            this.imageSetBox.Name = "imageSetBox";
            this.imageSetBox.Size = new System.Drawing.Size(560, 384);
            this.imageSetBox.TabIndex = 2;
            this.imageSetBox.TabStop = false;
            this.imageSetBox.SizeChanged += new System.EventHandler(this.imageSetBox_SizeChanged);
            this.imageSetBox.Click += new System.EventHandler(this.imageSetBox_Click);
            this.imageSetBox.Paint += new System.Windows.Forms.PaintEventHandler(this.imageSetBox_Paint);
            this.imageSetBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageSetBox_MouseDown);
            this.imageSetBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageSetBox_MouseMove);
            this.imageSetBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageSetBox_MouseUp);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.LargeChange = 20;
            this.vScrollBar.Location = new System.Drawing.Point(560, 25);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(21, 384);
            this.vScrollBar.TabIndex = 1;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            // 
            // hScrollBar
            // 
            this.hScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar.Location = new System.Drawing.Point(0, 409);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(581, 21);
            this.hScrollBar.TabIndex = 0;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.toolStripSeparator1,
            this.canvasSizeToolStripLabel,
            this.sizeSetToolStripComboBox,
            this.toolStripSeparator4,
            this.nameToolStripLabel,
            this.nameToolStripTextBox,
            this.autoTypesettingToolStripLabel,
            this.amountMinusToolStripButton,
            this.amountSetToolStripTextBox,
            this.amountAddToolStripButton,
            this.positionToolStripLabel,
            this.posToolStripTextBox,
            this.sizeToolStripLabel,
            this.sizeToolStripTextBox});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(581, 25);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "toolStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rimViewToolStripMenuItem,
            this.colorWorkspaceToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(47, 25);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // rimViewToolStripMenuItem
            // 
            this.rimViewToolStripMenuItem.Checked = true;
            this.rimViewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rimViewToolStripMenuItem.Name = "rimViewToolStripMenuItem";
            this.rimViewToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.rimViewToolStripMenuItem.Text = "Always show images rim";
            this.rimViewToolStripMenuItem.Click += new System.EventHandler(this.rimViewToolStripMenuItem_Click);
            // 
            // colorWorkspaceToolStripMenuItem
            // 
            this.colorWorkspaceToolStripMenuItem.Name = "colorWorkspaceToolStripMenuItem";
            this.colorWorkspaceToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.colorWorkspaceToolStripMenuItem.Text = "Workspace color";
            this.colorWorkspaceToolStripMenuItem.BackColorChanged += new System.EventHandler(this.colorWorkspaceToolStripMenuItem_BackColorChanged);
            this.colorWorkspaceToolStripMenuItem.Click += new System.EventHandler(this.colorWorkspaceToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // canvasSizeToolStripLabel
            // 
            this.canvasSizeToolStripLabel.Name = "canvasSizeToolStripLabel";
            this.canvasSizeToolStripLabel.Size = new System.Drawing.Size(75, 22);
            this.canvasSizeToolStripLabel.Text = "Canvas size";
            // 
            // sizeSetToolStripComboBox
            // 
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
            this.sizeSetToolStripComboBox.Size = new System.Drawing.Size(92, 25);
            this.sizeSetToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.sizeSetToolStripComboBox_SelectedIndexChanged);
            this.sizeSetToolStripComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.sizeSetToolStripComboBox_KeyUp);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // nameToolStripLabel
            // 
            this.nameToolStripLabel.Name = "nameToolStripLabel";
            this.nameToolStripLabel.Size = new System.Drawing.Size(43, 22);
            this.nameToolStripLabel.Text = "Name";
            // 
            // nameToolStripTextBox
            // 
            this.nameToolStripTextBox.Name = "nameToolStripTextBox";
            this.nameToolStripTextBox.Size = new System.Drawing.Size(114, 25);
            this.nameToolStripTextBox.Leave += new System.EventHandler(this.nameToolStripTextBox_Leave);
            this.nameToolStripTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nameToolStripTextBox_KeyUp);
            // 
            // autoTypesettingToolStripLabel
            // 
            this.autoTypesettingToolStripLabel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalLimitToolStripMenuItem,
            this.verticalLimitToolStripMenuItem});
            this.autoTypesettingToolStripLabel.Name = "autoTypesettingToolStripLabel";
            this.autoTypesettingToolStripLabel.Size = new System.Drawing.Size(116, 22);
            this.autoTypesettingToolStripLabel.Text = "Auto typesetting";
            this.autoTypesettingToolStripLabel.Visible = false;
            // 
            // horizontalLimitToolStripMenuItem
            // 
            this.horizontalLimitToolStripMenuItem.Checked = true;
            this.horizontalLimitToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.horizontalLimitToolStripMenuItem.Name = "horizontalLimitToolStripMenuItem";
            this.horizontalLimitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.horizontalLimitToolStripMenuItem.Text = "Horizontal limit";
            this.horizontalLimitToolStripMenuItem.Click += new System.EventHandler(this.horizontalLimitToolStripMenuItem_Click);
            // 
            // verticalLimitToolStripMenuItem
            // 
            this.verticalLimitToolStripMenuItem.Name = "verticalLimitToolStripMenuItem";
            this.verticalLimitToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.verticalLimitToolStripMenuItem.Text = "Vertical limit";
            this.verticalLimitToolStripMenuItem.Click += new System.EventHandler(this.verticalLimitToolStripMenuItem_Click);
            // 
            // amountMinusToolStripButton
            // 
            this.amountMinusToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.amountMinusToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("amountMinusToolStripButton.Image")));
            this.amountMinusToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.amountMinusToolStripButton.Name = "amountMinusToolStripButton";
            this.amountMinusToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.amountMinusToolStripButton.Text = "-";
            this.amountMinusToolStripButton.Visible = false;
            this.amountMinusToolStripButton.Click += new System.EventHandler(this.amountMinusToolStripButton_Click);
            // 
            // amountSetToolStripTextBox
            // 
            this.amountSetToolStripTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.amountSetToolStripTextBox.MaxLength = 3;
            this.amountSetToolStripTextBox.Name = "amountSetToolStripTextBox";
            this.amountSetToolStripTextBox.Size = new System.Drawing.Size(22, 25);
            this.amountSetToolStripTextBox.Text = "1";
            this.amountSetToolStripTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.amountSetToolStripTextBox.Visible = false;
            this.amountSetToolStripTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.amountSetToolStripTextBox_KeyUp);
            // 
            // amountAddToolStripButton
            // 
            this.amountAddToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.amountAddToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("amountAddToolStripButton.Image")));
            this.amountAddToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.amountAddToolStripButton.Name = "amountAddToolStripButton";
            this.amountAddToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.amountAddToolStripButton.Text = "+";
            this.amountAddToolStripButton.Visible = false;
            this.amountAddToolStripButton.Click += new System.EventHandler(this.amountAddToolStripButton_Click);
            // 
            // positionToolStripLabel
            // 
            this.positionToolStripLabel.Name = "positionToolStripLabel";
            this.positionToolStripLabel.Size = new System.Drawing.Size(54, 22);
            this.positionToolStripLabel.Text = "Position";
            // 
            // posToolStripTextBox
            // 
            this.posToolStripTextBox.Name = "posToolStripTextBox";
            this.posToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            this.posToolStripTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.posToolStripTextBox_KeyUp);
            // 
            // sizeToolStripLabel
            // 
            this.sizeToolStripLabel.Name = "sizeToolStripLabel";
            this.sizeToolStripLabel.Size = new System.Drawing.Size(31, 17);
            this.sizeToolStripLabel.Text = "Size";
            // 
            // sizeToolStripTextBox
            // 
            this.sizeToolStripTextBox.Name = "sizeToolStripTextBox";
            this.sizeToolStripTextBox.ReadOnly = true;
            this.sizeToolStripTextBox.Size = new System.Drawing.Size(100, 23);
            // 
            // imageSetBoxContextMenuStrip
            // 
            this.imageSetBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delusedToolStripMenuItem2,
            this.toolStripMenuItem3,
            this.alignmentToolStripMenuItem});
            this.imageSetBoxContextMenuStrip.Name = "imageSetBoxContextMenuStrip";
            this.imageSetBoxContextMenuStrip.Size = new System.Drawing.Size(135, 54);
            this.imageSetBoxContextMenuStrip.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.imageSetBoxContextMenuStrip_Closing);
            this.imageSetBoxContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.imageSetBoxContextMenuStrip_Opening);
            // 
            // delusedToolStripMenuItem2
            // 
            this.delusedToolStripMenuItem2.Name = "delusedToolStripMenuItem2";
            this.delusedToolStripMenuItem2.Size = new System.Drawing.Size(134, 22);
            this.delusedToolStripMenuItem2.Text = "Delete";
            this.delusedToolStripMenuItem2.Click += new System.EventHandler(this.delusedToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(131, 6);
            // 
            // alignmentToolStripMenuItem
            // 
            this.alignmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leftOutsideAlignmentToolStripMenuItem,
            this.leftInsideAlignmentToolStripMenuItem,
            this.rightOutsideAlignmentToolStripMenuItem,
            this.rightInsideAlignmentToolStripMenuItem,
            this.toolStripMenuItem2,
            this.topOutsideAlignmentToolStripMenuItem,
            this.topInsideAlignmentToolStripMenuItem,
            this.bottomOutsideAlignmentToolStripMenuItem,
            this.bottomInsideAlignmentToolStripMenuItem});
            this.alignmentToolStripMenuItem.Name = "alignmentToolStripMenuItem";
            this.alignmentToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.alignmentToolStripMenuItem.Text = "Alignment";
            // 
            // leftOutsideAlignmentToolStripMenuItem
            // 
            this.leftOutsideAlignmentToolStripMenuItem.Name = "leftOutsideAlignmentToolStripMenuItem";
            this.leftOutsideAlignmentToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.leftOutsideAlignmentToolStripMenuItem.Text = "Outside of the left";
            this.leftOutsideAlignmentToolStripMenuItem.Click += new System.EventHandler(this.leftOutsideAlignmentToolStripMenuItem_Click);
            this.leftOutsideAlignmentToolStripMenuItem.MouseEnter += new System.EventHandler(this.leftOutsideAlignmentToolStripMenuItem_MouseEnter);
            // 
            // leftInsideAlignmentToolStripMenuItem
            // 
            this.leftInsideAlignmentToolStripMenuItem.Name = "leftInsideAlignmentToolStripMenuItem";
            this.leftInsideAlignmentToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.leftInsideAlignmentToolStripMenuItem.Text = "Inside of the left";
            this.leftInsideAlignmentToolStripMenuItem.Click += new System.EventHandler(this.leftInsideAlignmentToolStripMenuItem_Click);
            this.leftInsideAlignmentToolStripMenuItem.MouseEnter += new System.EventHandler(this.leftInsideAlignmentToolStripMenuItem_MouseEnter);
            // 
            // rightOutsideAlignmentToolStripMenuItem
            // 
            this.rightOutsideAlignmentToolStripMenuItem.Name = "rightOutsideAlignmentToolStripMenuItem";
            this.rightOutsideAlignmentToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.rightOutsideAlignmentToolStripMenuItem.Text = "Outside of the right";
            this.rightOutsideAlignmentToolStripMenuItem.Click += new System.EventHandler(this.rightOutsideAlignmentToolStripMenuItem_Click);
            this.rightOutsideAlignmentToolStripMenuItem.MouseEnter += new System.EventHandler(this.rightOutsideAlignmentToolStripMenuItem_MouseEnter);
            // 
            // rightInsideAlignmentToolStripMenuItem
            // 
            this.rightInsideAlignmentToolStripMenuItem.Name = "rightInsideAlignmentToolStripMenuItem";
            this.rightInsideAlignmentToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.rightInsideAlignmentToolStripMenuItem.Text = "Inside of the right";
            this.rightInsideAlignmentToolStripMenuItem.Click += new System.EventHandler(this.rightInsideAlignmentToolStripMenuItem_Click);
            this.rightInsideAlignmentToolStripMenuItem.MouseEnter += new System.EventHandler(this.rightInsideAlignmentToolStripMenuItem_MouseEnter);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(203, 6);
            // 
            // topOutsideAlignmentToolStripMenuItem
            // 
            this.topOutsideAlignmentToolStripMenuItem.Name = "topOutsideAlignmentToolStripMenuItem";
            this.topOutsideAlignmentToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.topOutsideAlignmentToolStripMenuItem.Text = "Outside of the top";
            this.topOutsideAlignmentToolStripMenuItem.Click += new System.EventHandler(this.topOutsideAlignmentToolStripMenuItem_Click);
            this.topOutsideAlignmentToolStripMenuItem.MouseEnter += new System.EventHandler(this.topOutsideAlignmentToolStripMenuItem_MouseEnter);
            // 
            // topInsideAlignmentToolStripMenuItem
            // 
            this.topInsideAlignmentToolStripMenuItem.Name = "topInsideAlignmentToolStripMenuItem";
            this.topInsideAlignmentToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.topInsideAlignmentToolStripMenuItem.Text = "Inside of the top";
            this.topInsideAlignmentToolStripMenuItem.Click += new System.EventHandler(this.topInsideAlignmentToolStripMenuItem_Click);
            this.topInsideAlignmentToolStripMenuItem.MouseEnter += new System.EventHandler(this.topInsideAlignmentToolStripMenuItem_MouseEnter);
            // 
            // bottomOutsideAlignmentToolStripMenuItem
            // 
            this.bottomOutsideAlignmentToolStripMenuItem.Name = "bottomOutsideAlignmentToolStripMenuItem";
            this.bottomOutsideAlignmentToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.bottomOutsideAlignmentToolStripMenuItem.Text = "Outside of the bottom";
            this.bottomOutsideAlignmentToolStripMenuItem.Click += new System.EventHandler(this.bottomOutsideAlignmentToolStripMenuItem_Click);
            this.bottomOutsideAlignmentToolStripMenuItem.MouseEnter += new System.EventHandler(this.bottomOutsideAlignmentToolStripMenuItem_MouseEnter);
            // 
            // bottomInsideAlignmentToolStripMenuItem
            // 
            this.bottomInsideAlignmentToolStripMenuItem.Name = "bottomInsideAlignmentToolStripMenuItem";
            this.bottomInsideAlignmentToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.bottomInsideAlignmentToolStripMenuItem.Text = "Inside of the bottom";
            this.bottomInsideAlignmentToolStripMenuItem.Click += new System.EventHandler(this.bottomInsideAlignmentToolStripMenuItem_Click);
            this.bottomInsideAlignmentToolStripMenuItem.MouseEnter += new System.EventHandler(this.bottomInsideAlignmentToolStripMenuItem_MouseEnter);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image File(png,bmp,jpg)|*.png;*.bmp;*.jpg;";
            this.openFileDialog.Multiselect = true;
            // 
            // ImagesetEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(225, 160);
            this.Name = "ImagesetEditControl";
            this.Size = new System.Drawing.Size(716, 430);
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
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.imageSetBoxContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.PictureBox imageSetBox;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripComboBox sizeSetToolStripComboBox;
        private System.Windows.Forms.ToolStripLabel canvasSizeToolStripLabel;
        private System.Windows.Forms.ListView usedListView;
        private System.Windows.Forms.ToolStrip usedToolStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem clearUsedToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripLabel nameToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox nameToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel positionToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox posToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel sizeToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox sizeToolStripTextBox;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader usedColumnHeader;
        private System.Windows.Forms.ToolStripMenuItem addImageMenuItem;
        private System.Windows.Forms.ContextMenuStrip imageSetBoxContextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem delusedToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel imageCountToolStripLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rimViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem alignmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftOutsideAlignmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftInsideAlignmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightOutsideAlignmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightInsideAlignmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem topOutsideAlignmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topInsideAlignmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottomOutsideAlignmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottomInsideAlignmentToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripMenuItem colorWorkspaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton amountMinusToolStripButton;
        private System.Windows.Forms.ToolStripTextBox amountSetToolStripTextBox;
        private System.Windows.Forms.ToolStripButton amountAddToolStripButton;
        private System.Windows.Forms.ToolStripDropDownButton autoTypesettingToolStripLabel;
        private System.Windows.Forms.ToolStripMenuItem horizontalLimitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalLimitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem newGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editGroupItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem moveToGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toGroupToolStripMenuItem;
    }
}
