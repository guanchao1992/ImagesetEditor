using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageSetEditor.EditControl
{
    public partial class ImagesetEditControl : UserControl, IDisposable, IRedraw
    {
        #region Fields

        /// <summary>
        /// 当前鼠标状态
        /// </summary>
        enum MouseStatus
        {
            Normal, /// 正常
            Drag,   /// 拖拽图片
            Select, /// 选择图片
        };

        private enum SortTypes
        {
            Name = 0,
            NameReverse = 1,
            Size = 2,
            SizeReverse = 3,
            Invert = 4,
        };

        /// <summary>
        /// 当前文档路径
        /// </summary>
        private string m_documentPath;

        /// <summary>
        /// 当前文档是否被修改
        /// </summary>
        private bool m_modify;

        /// <summary>
        /// 画布
        /// </summary>
        private Canvas m_canvas;

        /// <summary>
        /// 当前选中的图片
        /// </summary>
        private SubImage m_select;

        /// <summary>
        /// 当前选中的图片组
        /// </summary>
        private List<SubImage> m_selects;

        /// <summary>
        /// 操作开始时鼠标起始位置
        /// </summary>
        private Point m_beginMousePos;

        /// <summary>
        /// 当前鼠标位置
        /// </summary>
        private Point m_curMousePos;

        /// <summary>
        /// 当前鼠标状态
        /// </summary>
        private MouseStatus m_MouseStatus;

        /// <summary>
        /// 鼠标在已选择的图片范围内
        /// </summary>
        private bool m_inSelects;

        /// <summary>
        /// 列表视图节点操作锁
        /// </summary>
        /// 防止修改节点选择状态时对事件的调用
        private bool m_listViewNodeLock;

        /// <summary>
        /// 菜单中显示图片名称的项
        /// </summary>
        private List<ToolStripMenuItem> m_menuImageViewItem;

        /// <summary>
        /// 停靠辅助
        /// </summary>
        private DockAid m_dockAid;

        #endregion Fields

        #region Methods

        public void Open(string fileName)
        {
        }

        public void Close()
        {
        }

        public void Save(string fileName)
        {
        }

        public string GetCurrentName()
        {
            return "";
        }

        private void imageSetBoxUpdate()
        {
            imageSetBox.Invalidate();
        }

        public void Redraw()
        {
            imageSetBoxUpdate();
        }

        private void imageCountUpdate()
        {
            imageCountToolStripLabel.Text = "共 " + usedListView.Items.Count.ToString() + " 个图片";
        }

        private void SetSelect(SubImage select)
        {
            if (select == null)
            {
                nameToolStripTextBox.Text = "不可用";
                rectToolStripTextBox.Text = "不可用";
                sizeToolStripTextBox.Text = "不可用";
                nameToolStripTextBox.ReadOnly = true;
            }
            else
            {
                nameToolStripTextBox.Text = select.Name;
                rectToolStripTextBox.Text =
                    String.Format("{0},{1}", select.Position.X, select.Position.Y);
                sizeToolStripTextBox.Text =
                    String.Format("{0},{1}", select.Size.Width, select.Size.Height);
                nameToolStripTextBox.ReadOnly = false;
            }

            m_select = select;

            m_dockAid.SetImage(select);
        }

        public void SetCursor(Cursor cursor)
        {
            imageSetBox.Cursor = cursor;
        }

        /// <summary>
        /// 根据两点获得一个矩形范围
        /// </summary>
        static private Rectangle GetRectangleArea(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y));
        }

        static private Rectangle GetRectangleArea(Point p1, Point p2, Point offset)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X) + offset.X,
                Math.Min(p1.Y, p2.Y) + offset.Y,
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y));
        }

        private class CompareName : IComparer
        {
            public int Compare(object x, object y)
            {
                return CompareOriginal(x, y);
            }

            public static int CompareOriginal(object x, object y)
            {
                SubImage imageX = (SubImage)x;
                SubImage imageY = (SubImage)y;

                return String.CompareOrdinal(imageX.Name, imageY.Name);
            }
        }

        private class CompareNameReverse : IComparer
        {
            public int Compare(object x, object y)
            {
                return CompareName.CompareOriginal(x, y) * -1;
            }
        }

        private class CompareSize : IComparer
        {
            public int Compare(object x, object y)
            {
                return CompareOriginal(x, y);
            }

            public static int CompareOriginal(object x, object y)
            {
                SubImage imageX = (SubImage)x;
                SubImage imageY = (SubImage)y;

                int areaX = imageX.Size.Width * imageX.Size.Height;
                int areaY = imageY.Size.Width * imageY.Size.Height;

                if (areaX > areaY && imageX.Size.Width > imageY.Size.Width)
                {
                    return 1;
                }

                if (imageX.Size == imageY.Size)
                {
                    return String.CompareOrdinal(imageX.Name, imageY.Name);
                }

                return -1;
            }
        }

        private class CompareSizeReverse : IComparer
        {
            public int Compare(object x, object y)
            {
                return CompareSize.CompareOriginal(x, y) * -1;
            }
        }

        private void SortItems(SortTypes type, IList items)
        {
            ArrayList list = new ArrayList();

            if (type == SortTypes.Invert)
            {
                for (int i = items.Count - 1; i >= 0; --i)
                {
                    list.Add(((ListViewItem)items[i]).Tag);
                }
            }
            else
            {
                foreach (ListViewItem item in items)
                {
                    list.Add(item.Tag);
                }
            }

            IComparer sorter = null;

            switch (type)
            {
                case SortTypes.Name:
                    sorter = new CompareName();
                    break;
                case SortTypes.NameReverse:
                    sorter = new CompareNameReverse();
                    break;
                case SortTypes.Size:
                    sorter = new CompareSize();
                    break;
                case SortTypes.SizeReverse:
                    sorter = new CompareSizeReverse();
                    break;
                case SortTypes.Invert:
                    break;
                default:
                    break;
            };

            if (sorter != null)
                list.Sort((IComparer)sorter);

            for (int i = 0; i != list.Count; ++i)
            {
                SubImage image = (SubImage)list[i];
                ListViewItem item = (ListViewItem)items[i];
                item.Text = image.Name;
                item.Tag = image;
                image.BindItem = item;
            }

            imageSetBoxUpdate();
        }

        private void ClearSelects()
        {
            if (m_selects.Count != 0)
            {
                m_listViewNodeLock = true;

                foreach (SubImage image in m_selects)
                {
                    image.BindItem.Selected = false;
                }

                m_listViewNodeLock = false;

                m_selects.Clear();

                SetSelect(null);
            }
        }

        private void SelectTop()
        {
            ClearSelects();

            for (int i = usedListView.Items.Count - 1; i >= 0; --i)
            {
                SubImage image = (SubImage)usedListView.Items[i].Tag;

                if (image.Rectangle.Contains(m_curMousePos.X + m_canvas.ViewPos.X, m_curMousePos.Y + m_canvas.ViewPos.Y))
                {
                    m_listViewNodeLock = true;

                    usedListView.Items[i].Selected = true;

                    m_listViewNodeLock = false;

                    m_selects.Add(image);

                    SetSelect(image);

                    break;
                }
            }
        }

        #endregion Methods

        #region Properties
        #endregion Properties

        #region Events

        private void ImagesetEditControl_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
        }

        private void imageSetBox_SizeChanged(object sender, EventArgs e)
        {
            m_canvas.ViewSize = imageSetBox.Size;

            /// 可视范围比画布大
            if (m_canvas.ViewSize.Height >= m_canvas.Size.Height)
            {
                vScrollBar.Visible = false;
            }
            else
            {
                vScrollBar.Maximum = m_canvas.Size.Height;
                vScrollBar.LargeChange = m_canvas.ViewSize.Height;
                vScrollBar.Visible = true;
            }

            if (m_canvas.ViewSize.Width >= m_canvas.Size.Width)
            {
                hScrollBar.Visible = false;
            }
            else
            {
                hScrollBar.Maximum = m_canvas.Size.Width;
                hScrollBar.LargeChange = m_canvas.ViewSize.Width;
                hScrollBar.Visible = true;
            }

        }

        private void imageSetBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                switch (m_MouseStatus)
                {
                    case MouseStatus.Drag:
                        {
                            m_MouseStatus = MouseStatus.Normal;

                            Point offset = new Point(
                                m_curMousePos.X - m_beginMousePos.X,
                                m_curMousePos.Y - m_beginMousePos.Y);

                            foreach (SubImage image in m_selects)
                            {
                                image.Position = new Point(
                                    image.Position.X + offset.X,
                                    image.Position.Y + offset.Y);
                            }

                            if (m_select != null)
                            {
                                SetSelect(m_select);
                            }

                            imageSetBoxUpdate();
                        }
                        break;
                    case MouseStatus.Select:
                        {
                            m_MouseStatus = MouseStatus.Normal;

                            if (m_beginMousePos == m_curMousePos)
                            {
                                /// 光标位置不变点击一次则只选取最顶层的图片
                                SelectTop();
                            }
                            else
                            {
                                /// 拖动一个矩形区域
                                Rectangle selectArea = GetRectangleArea(
                                    m_beginMousePos,
                                    m_curMousePos,
                                    m_canvas.ViewPos);

                                foreach (ListViewItem item in usedListView.Items)
                                {
                                    SubImage image = (SubImage)item.Tag;

                                    m_listViewNodeLock = true;

                                    if (selectArea.IntersectsWith(image.Rectangle))
                                    {
                                        item.Selected = true;
                                        m_selects.Add(image);
                                    }

                                    m_listViewNodeLock = false;
                                }

                                if (m_selects.Count == 1)
                                {
                                    SetSelect(m_selects.First());
                                }
                            }

                            imageSetBoxUpdate();
                        }
                        break;
                    default:
                        if (m_dockAid.InArrowButton(e.X, e.Y) && m_dockAid.OnClick(e.X, e.Y))
                        {
                            imageSetBoxUpdate();
                            m_dockAid.SetImage(m_select);
                        }
                        break;
                }
            }
        }

        private void imageSetBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (m_inSelects)
                {
                    m_MouseStatus = MouseStatus.Drag;
                    m_beginMousePos = e.Location;
                    m_curMousePos = e.Location;

                    m_dockAid.SetImage(null);
                }
                else
                {
                    if (m_dockAid.InArrowButton(e.X, e.Y))
                    {
                        return;
                    }

                    m_MouseStatus = MouseStatus.Select;
                    m_beginMousePos = e.Location;
                    m_curMousePos = e.Location;

                    ClearSelects();
                }
            }
        }

        private void imageSetBox_MouseMove(object sender, MouseEventArgs e)
        {
            m_curMousePos = e.Location;

            if (m_MouseStatus != MouseStatus.Normal)
            {
                imageSetBoxUpdate();

                return;
            }

            m_inSelects = false;

            if (m_dockAid.OnMouseMove(e.X, e.Y))
            {
                imageSetBox.Cursor = Cursors.Hand;
                return;
            }

            /// 鼠标移动到已经选择的图片里时变更光标
            

            foreach (SubImage image in m_selects)
            {
                if (image.Rectangle.Contains(new Point(
                    e.X + m_canvas.ViewPos.X,
                    e.Y + m_canvas.ViewPos.Y)))
                {
                    m_inSelects = true;
                    break;
                }
            }

            if (m_inSelects)
            {
                imageSetBox.Cursor = Cursors.SizeAll;
            }
            else
            {
                imageSetBox.Cursor = Cursors.Default;
            }
        }

        private void imageSetBoxContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            /*
            if (m_inSelects == false)
            {
                SelectTop();

                imageSetBoxUpdate();
            }
            */

            /// 使菜单显示鼠标位置顶层十个图片名称

            int menuViewItem = 0;

            for (int i = usedListView.Items.Count - 1; i >= 0; --i)
            {
                SubImage image = (SubImage)usedListView.Items[i].Tag;

                if (image.Rectangle.Contains(m_curMousePos.X + m_canvas.ViewPos.X, m_curMousePos.Y + m_canvas.ViewPos.Y))
                {
                    m_menuImageViewItem[menuViewItem].Text = (menuViewItem + 1).ToString() + ")  " + image.Name;
                    m_menuImageViewItem[menuViewItem].Tag = image;
                    m_menuImageViewItem[menuViewItem].Visible = true;
                    m_menuImageViewItem[menuViewItem].Enabled = true;

                    menuViewItem++;

                    if (menuViewItem == m_menuImageViewItem.Count)
                    {
                        break;
                    }
                }
            }

            for (; menuViewItem != m_menuImageViewItem.Count; ++menuViewItem)
            {
                m_menuImageViewItem[menuViewItem].Text = "";
                m_menuImageViewItem[menuViewItem].Visible = false;
                m_menuImageViewItem[menuViewItem].Tag = null;
            }

            if (m_menuImageViewItem[0].Tag == null)
            {
                m_menuImageViewItem[0].Text = "未选中图片";
                m_menuImageViewItem[0].Visible = true;
                m_menuImageViewItem[0].Enabled = false;
            }
        }

        private void imagenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSelects();

            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            SubImage image = (SubImage)item.Tag;

            m_listViewNodeLock = true;

            image.BindItem.Selected = true;

            SetSelect(image);

            m_selects.Clear();
            m_selects.Add(image);

            m_listViewNodeLock = true;

            imageSetBoxUpdate();
        }

        private void imageSetBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void imageSetBox_Paint(object sender, PaintEventArgs e)
        {
            m_canvas.Begin(e.Graphics);

            foreach (ListViewItem item in usedListView.Items)
            {
                if (m_MouseStatus == MouseStatus.Drag)
                {
                    if (item.Selected)
                        continue;
                }

                SubImage image = (SubImage)item.Tag;
                m_canvas.DrawImage(image);
            }

            if (m_MouseStatus == MouseStatus.Drag)
            {
                Point offset = new Point(
                    m_curMousePos.X - m_beginMousePos.X,
                    m_curMousePos.Y - m_beginMousePos.Y);

                foreach (SubImage image in m_selects)
                {
                    m_canvas.DrawImage(image, offset);
                }

                foreach (SubImage image in m_selects)
                {
                    m_canvas.DrawImageArea(image, offset);
                }
            }
            else
            {
                foreach (SubImage image in m_selects)
                {
                    m_canvas.DrawImageArea(image);
                }
            }

            if (m_MouseStatus == MouseStatus.Select)
            {
                Rectangle selectArea =
                    GetRectangleArea(m_beginMousePos, m_curMousePos, m_canvas.ViewPos);

                m_canvas.DrawRectangle(selectArea);
            }

            m_dockAid.Draw(m_canvas);

            m_canvas.End();
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            m_canvas.ViewPos = new Point(hScrollBar.Value, vScrollBar.Value);

            imageSetBoxUpdate();
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            m_canvas.ViewPos = new Point(hScrollBar.Value, vScrollBar.Value);

            imageSetBoxUpdate();
        }

        private void addImageMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (usedListView.Items.Count + openFileDialog.FileNames.Count() > 500)
                {
                    MessageBox.Show("添加这些图片后超过图片数量限制，你编辑图片总数不能超过500。", "超过数量上限", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (string file in openFileDialog.FileNames)
                {
                    ListViewItem newItem = new ListViewItem();
                    SubImage newImage = new SubImage(file);
                    newImage.BindItem = newItem;
                    newItem.Tag = newImage;
                    newItem.Text = newImage.Name;
                    newImage.Position =
                        new Point(m_canvas.ViewPos.X, m_canvas.ViewPos.Y);
                    usedListView.Items.Add(newItem);
                }

                imageCountUpdate();

                imageSetBoxUpdate();
            }
        }

        private void clearUsedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usedListView.Items.Count == 0)
                return;
            if (DialogResult.OK == MessageBox.Show("删除所有？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                SetSelect(null);
                foreach (ListViewItem item in usedListView.Items)
                {
                    ((SubImage)item.Tag).Dispose();
                    item.Tag = null;
                    item.Remove();
                }
            }

            imageCountUpdate();

            imageSetBoxUpdate();
        }

        private void delusedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usedListView.SelectedItems.Count == 0)
                return;
            if (DialogResult.OK == MessageBox.Show("确定从列表中删除所选的 " + usedListView.SelectedItems.Count + " 个图片？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                m_listViewNodeLock = true;

                foreach (ListViewItem item in usedListView.SelectedItems)
                {
                    ((SubImage)item.Tag).Dispose();
                    item.Tag = null;
                    item.Remove();
                }

                m_listViewNodeLock = false;

                SetSelect(null);
                m_selects.Clear();
            }

            imageCountUpdate();

            imageSetBoxUpdate();
        }

        private void sizeSetToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = int.Parse(((ToolStripComboBox)sender).Text.Split('*')[0]);

            m_canvas.Size = new Size(n, n);

            m_canvas.ViewPos = new Point(0, 0);
            hScrollBar.Value = 0;
            vScrollBar.Value = 0;

            imageSetBox_SizeChanged(null, null);

            imageSetBoxUpdate();
        }

        private void nameToolStripTextBox_Leave(object sender, EventArgs e)
        {
            if (m_select != null)
                nameToolStripTextBox.Text = m_select.Name;
        }

        private void nameToolStripTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (nameToolStripTextBox.Text.Length == 0)
                {
                    nameToolStripTextBox.Text = m_select.Name;
                    return;
                }
                m_select.Name = nameToolStripTextBox.Text;
                m_select.BindItem.Text = m_select.Name;
            }
        }

        private void usedSelectSortNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usedListView.SelectedItems.Count <= 1)
            {
                SortItems(SortTypes.Name, usedListView.Items);
            }
            else
            {
                SortItems(SortTypes.Name, usedListView.SelectedItems);
            }
        }

        private void usedSelectSortNameReverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usedListView.SelectedItems.Count <= 1)
            {
                SortItems(SortTypes.NameReverse, usedListView.Items);
            }
            else
            {
                SortItems(SortTypes.NameReverse, usedListView.SelectedItems);
            }
        }

        private void usedSelectSortSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usedListView.SelectedItems.Count <= 1)
            {
                SortItems(SortTypes.Size, usedListView.Items);
            }
            else
            {
                SortItems(SortTypes.Size, usedListView.SelectedItems);
            }
        }

        private void usedSelectSortSizeReverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usedListView.SelectedItems.Count <= 1)
            {
                SortItems(SortTypes.SizeReverse, usedListView.Items);
            }
            else
            {
                SortItems(SortTypes.SizeReverse, usedListView.SelectedItems);
            }
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usedListView.SelectedItems.Count <= 1)
            {
                SortItems(SortTypes.Invert, usedListView.Items);
            }
            else
            {
                SortItems(SortTypes.Invert, usedListView.SelectedItems);
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in usedListView.Items)
            {
                item.Selected = true;
            }
        }

        private void usedListView_SizeChanged(object sender, EventArgs e)
        {
            usedColumnHeader.Width = usedListView.Width;
        }

        private void usedListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_listViewNodeLock)
                return;

            int n = usedListView.SelectedItems.Count;

            if (n != 1)
            {
                SetSelect(null);
            }
            else
            {
                SetSelect((SubImage)usedListView.SelectedItems[0].Tag);
            }

            m_selects.Clear();

            foreach (ListViewItem item in usedListView.SelectedItems)
            {
                m_selects.Add((SubImage)item.Tag);
            }

            imageSetBoxUpdate();
        }

        #endregion Events

        #region Constructors

        public ImagesetEditControl()
        {
            m_canvas = new Canvas();

            m_selects = new List<SubImage>();

            m_menuImageViewItem = new List<ToolStripMenuItem>();

            m_MouseStatus = MouseStatus.Normal;

            InitializeComponent();

            m_dockAid = new DockAid(usedListView.Items, m_canvas, this);

            m_menuImageViewItem.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
                imagename01ToolStripMenuItem,
                imagename02ToolStripMenuItem,
                imagename03ToolStripMenuItem,
                imagename04ToolStripMenuItem,
                imagename05ToolStripMenuItem,
                imagename06ToolStripMenuItem,
                imagename07ToolStripMenuItem,
                imagename08ToolStripMenuItem,
                imagename09ToolStripMenuItem,
                imagename10ToolStripMenuItem,
            });

            SetSelect(null);

            sizeSetToolStripComboBox.SelectedIndex = 3;
        }

        #endregion Constructors
    }

    internal interface IRedraw
    {
        void Redraw();
    }


    /// <summary>
    /// 画布对象
    /// </summary>
    internal class Canvas
    {
        #region Fields

        /// <summary>
        /// 画布位图绘图接口
        /// </summary>
        private Graphics m_viewGraph;

        /// <summary>
        /// 画布位图尺寸
        /// </summary>
        private Size m_canvasSize;

        /// <summary>
        /// 当前查看位置
        /// </summary>
        private Point m_viewPosition;

        /// <summary>
        /// 查看范围
        /// </summary>
        private Size m_viewSize;

        /// <summary>
        /// 整体范围
        /// </summary>
        private Size m_size;

        private Pen m_dashedPen;

        private Pen m_blackPen;

        private Pen m_whitePen;

        private Pen m_greenPen;

        #endregion Fields

        #region Methods

        /// <summary>
        /// 判断一个图片是否可以被看到
        /// </summary>
        /// <returns></returns>
        private bool IsView(SubImage image)
        {
            return new Rectangle(m_viewPosition, m_viewSize).IntersectsWith(image.Rectangle);
        }

        public void Begin(Graphics viewGraph)
        {
            m_viewGraph = viewGraph;
            m_viewGraph.FillRectangle(
                m_whitePen.Brush,
                -m_viewPosition.X,
                -m_viewPosition.Y,
                m_size.Width,
                m_size.Height);
        }

        public void End()
        {
            m_viewGraph.DrawRectangle(
                m_dashedPen,
                -m_viewPosition.X,
                -m_viewPosition.Y,
                m_size.Width,
                m_size.Height);
        }

        public void DrawImage(SubImage image, Point offset)
        {
            m_viewGraph.DrawImage(
                image.Image,
                image.Position.X - m_viewPosition.X + offset.X,
                image.Position.Y - m_viewPosition.Y + offset.Y,
                image.Size.Width,
                image.Size.Height);
        }

        public void DrawImage(SubImage image)
        {
            if (IsView(image) == false)
                return;
            m_viewGraph.DrawImage(
                image.Image,
                image.Position.X - m_viewPosition.X,
                image.Position.Y - m_viewPosition.Y,
                image.Size.Width,
                image.Size.Height);
        }

        public void DrawImageArea(SubImage image)
        {
            DrawSmallBox(image.Position.X, image.Position.Y);
            DrawSmallBox(image.Position.X + image.Size.Width, image.Position.Y);
            DrawSmallBox(image.Position.X + image.Size.Width, image.Position.Y + image.Size.Height);
            DrawSmallBox(image.Position.X, image.Position.Y + image.Size.Height);

            m_viewGraph.DrawRectangle(
                m_dashedPen,
                image.Position.X - m_viewPosition.X,
                image.Position.Y - m_viewPosition.Y,
                image.Size.Width,
                image.Size.Height);
        }

        public void DrawImageArea(SubImage image, Point offset)
        {
            Point p = new Point(image.Position.X + offset.X, image.Position.Y + offset.Y);

            DrawSmallBox(p.X, p.Y);
            DrawSmallBox(p.X + image.Size.Width, p.Y);
            DrawSmallBox(p.X + image.Size.Width, p.Y + image.Size.Height);
            DrawSmallBox(p.X, p.Y + image.Size.Height);

            m_viewGraph.DrawRectangle(
                m_dashedPen,
                p.X - m_viewPosition.X,
                p.Y - m_viewPosition.Y,
                image.Size.Width,
                image.Size.Height);
        }

        public void DrawRectangle(Rectangle rect)
        {
            m_viewGraph.DrawRectangle(
                m_dashedPen,
                rect.X - m_viewPosition.X,
                rect.Y - m_viewPosition.Y,
                rect.Width, rect.Height);
        }

        public void DrawSmallBox(int x, int y)
        {
            m_viewGraph.FillRectangle(
                m_blackPen.Brush, x - 3 - m_viewPosition.X, y - 3 - m_viewPosition.Y, 6, 6);
        }

        public void DrawDockLine(int x0, int y0, int x1, int y1)
        {
            m_viewGraph.DrawLine(
                m_greenPen, x0 - m_viewPosition.X, y0 - m_viewPosition.Y, x1 - m_viewPosition.X, y1 - m_viewPosition.Y);
        }

        #endregion Methods

        #region Properties

        public Graphics Graphics
        {
            get { return m_viewGraph; }
        }

        public Point ViewPos
        {
            get { return m_viewPosition; }
            set { m_viewPosition = value; }
        }

        public Size ViewSize
        {
            get { return m_viewSize; }
            set { m_viewSize = value; }
        }

        public Size Size
        {
            get { return m_size; }
            set { m_size = value; }
        }

        #endregion Properties

        #region Constructors

        public Canvas()
        {
            m_viewGraph = null;

            m_dashedPen = new Pen(Color.Gray);

            m_dashedPen.DashStyle = DashStyle.DashDot;

            m_blackPen = new Pen(Color.Black);

            m_whitePen = new Pen(Color.White);

            m_greenPen = new Pen(Color.Green);

            m_greenPen.DashStyle = DashStyle.DashDot;
        }

        #endregion Constructors
    };

    /// <summary>
    /// 停靠辅助
    /// </summary>
    internal class DockAid
    {
        #region Fields

        private const int Direction_Upper = 0;

        private const int Direction_UpperLeft = 1;

        private const int Direction_UpperRight = 2;

        private const int Direction_Lower = 3;

        private const int Direction_LowerLeft = 4;

        private const int Direction_LowerRight = 5;

        private const int Direction_Left = 6;

        private const int Direction_Right = 7;

        private const int Direction_Nums = 8;

        private int m_selectArrow;

        private SubImage[] m_arrow;

        private SubImage m_select;

        private ListView.ListViewItemCollection m_items;

        private Canvas m_canvas;

        private IRedraw m_redraw;

        private List<int> m_leftContactEdge;

        private List<int> m_upperContactEdge;

        private List<int> m_rightContactEdge;

        private List<int> m_lowerContactEdge;

        #endregion Fields

        #region Methods

        /// <summary>
        /// 设置当前需要停靠的图片
        /// </summary>
        public void SetImage(SubImage image)
        {
            m_select = image;

            m_selectArrow = -1;

            if (image == null)
            {
                m_leftContactEdge.Clear();
                m_upperContactEdge.Clear();
                m_rightContactEdge.Clear();
                m_lowerContactEdge.Clear();
                return;
            }

            ContactBegin();

            foreach (ListViewItem item in m_items)
            {
                SubImage img = (SubImage)item.Tag;
                Contact(image, img);
            }

            ContactEnd();
        }

        public void Draw(Canvas canvas)
        {
            if (m_select == null)
            {
                return;
            }

            m_arrow[Direction_Upper].Position
                = new Point(m_select.Position.X + m_select.Size.Width / 2 - m_arrow[Direction_Upper].Size.Width / 2, m_select.Position.Y - m_arrow[Direction_Upper].Size.Height / 2);
            m_arrow[Direction_UpperLeft].Position
                = new Point(m_select.Position.X - m_arrow[Direction_UpperLeft].Size.Width / 2, m_select.Position.Y - m_arrow[Direction_UpperLeft].Size.Height / 2);
            m_arrow[Direction_UpperRight].Position
                = new Point(m_select.Position.X + m_select.Size.Width - m_arrow[Direction_UpperRight].Size.Width / 2, m_select.Position.Y - m_arrow[Direction_UpperRight].Size.Height / 2);
            m_arrow[Direction_Lower].Position
                = new Point(m_select.Position.X + m_select.Size.Width / 2 - m_arrow[Direction_Lower].Size.Width / 2, m_select.Position.Y + m_select.Size.Height - m_arrow[Direction_Lower].Size.Height / 2);
            m_arrow[Direction_LowerLeft].Position
                = new Point(m_select.Position.X - m_arrow[Direction_LowerLeft].Size.Width / 2, m_select.Position.Y + m_select.Size.Height - m_arrow[Direction_LowerLeft].Size.Height / 2);
            m_arrow[Direction_LowerRight].Position
                = new Point(m_select.Position.X + m_select.Size.Width - m_arrow[Direction_LowerRight].Size.Width / 2, m_select.Position.Y + m_select.Size.Height - m_arrow[Direction_LowerRight].Size.Height / 2);
            m_arrow[Direction_Left].Position
                = new Point(m_select.Position.X - m_arrow[Direction_Left].Size.Width / 2, m_select.Position.Y + m_select.Size.Height / 2 - m_arrow[Direction_Left].Size.Height / 2);
            m_arrow[Direction_Right].Position
                = new Point(m_select.Position.X + m_select.Size.Width - m_arrow[Direction_Right].Size.Width / 2, m_select.Position.Y + m_select.Size.Height / 2 - m_arrow[Direction_Right].Size.Height / 2);

            if (m_upperContactEdge.Count > 0)
                canvas.DrawImage(m_arrow[Direction_Upper]);

            if (m_upperContactEdge.Count > 0 && m_leftContactEdge.Count > 0)
                canvas.DrawImage(m_arrow[Direction_UpperLeft]);

            if (m_upperContactEdge.Count > 0 && m_rightContactEdge.Count > 0)
                canvas.DrawImage(m_arrow[Direction_UpperRight]);

            if (m_lowerContactEdge.Count > 0)
                canvas.DrawImage(m_arrow[Direction_Lower]);

            if (m_lowerContactEdge.Count > 0 && m_leftContactEdge.Count > 0)
                canvas.DrawImage(m_arrow[Direction_LowerLeft]);

            if (m_lowerContactEdge.Count > 0 && m_rightContactEdge.Count > 0)
                canvas.DrawImage(m_arrow[Direction_LowerRight]);

            if (m_leftContactEdge.Count > 0)
                canvas.DrawImage(m_arrow[Direction_Left]);

            if (m_rightContactEdge.Count > 0)
                canvas.DrawImage(m_arrow[Direction_Right]);

            switch (m_selectArrow)
            {
                case Direction_Upper:
                    if (m_upperContactEdge.Count > 0)
                    {
                        canvas.DrawDockLine(0, m_upperContactEdge.Last(), canvas.Size.Width, m_upperContactEdge.Last());
                    }
                    break;
                case Direction_UpperLeft:
                    if (m_upperContactEdge.Count > 0 && m_leftContactEdge.Count > 0)
                    {
                        canvas.DrawDockLine(0, m_upperContactEdge.Last(), m_leftContactEdge.Last(), m_upperContactEdge.Last());
                        canvas.DrawDockLine(m_leftContactEdge.Last(), 0, m_leftContactEdge.Last(), m_upperContactEdge.Last());
                    }
                    break;
                case Direction_UpperRight:
                    if (m_upperContactEdge.Count > 0 && m_rightContactEdge.Count > 0)
                    {
                        canvas.DrawDockLine(m_rightContactEdge.First(), m_upperContactEdge.Last(), canvas.Size.Width, m_upperContactEdge.Last());
                        canvas.DrawDockLine(m_rightContactEdge.First(), 0, m_rightContactEdge.First(), m_upperContactEdge.Last());
                    }
                    break;
                case Direction_Lower:
                    if (m_lowerContactEdge.Count > 0)
                    {
                        canvas.DrawDockLine(0, m_lowerContactEdge.First(), canvas.Size.Width, m_lowerContactEdge.First());
                    }
                    break;
                case Direction_LowerLeft:
                    if (m_lowerContactEdge.Count > 0 && m_leftContactEdge.Count > 0)
                    {
                        canvas.DrawDockLine(0, m_lowerContactEdge.First(), m_leftContactEdge.Last(), m_lowerContactEdge.First());
                        canvas.DrawDockLine(m_leftContactEdge.Last(), m_lowerContactEdge.First(), m_leftContactEdge.Last(), canvas.Size.Height);
                    }
                    break;
                case Direction_LowerRight:
                    if (m_lowerContactEdge.Count > 0 && m_rightContactEdge.Count > 0)
                    {
                        canvas.DrawDockLine(m_rightContactEdge.First(), m_lowerContactEdge.First(), canvas.Size.Width, m_lowerContactEdge.First());
                        canvas.DrawDockLine(m_rightContactEdge.First(), m_lowerContactEdge.First(), m_rightContactEdge.First(), canvas.Size.Height);
                    }
                    break;
                case Direction_Left:
                    if (m_leftContactEdge.Count > 0)
                    {
                        canvas.DrawDockLine(m_leftContactEdge.Last(), 0, m_leftContactEdge.Last(), canvas.Size.Height);
                    }
                    break;
                case Direction_Right:
                    if (m_rightContactEdge.Count > 0)
                    {
                        canvas.DrawDockLine(m_rightContactEdge.First(), 0, m_rightContactEdge.First(), canvas.Size.Height);
                    }
                    break;
                default:
                    break;
            };

        }

        public bool InArrowButton(int x, int y)
        {
            foreach (SubImage image in m_arrow)
            {
                if (image.Rectangle.Contains(x + m_canvas.ViewPos.X, y + m_canvas.ViewPos.Y))
                {
                    return true;
                }
            }

            return false;
        }

        public bool OnMouseMove(int x, int y)
        {
            if (m_select == null)
            {
                return false;
            }

            int lastSelectArrow = m_selectArrow;

            m_selectArrow = -1;

            for (int i = 0; i != m_arrow.Count(); ++i)
            {
                if (m_arrow[i].Rectangle.Contains(x + m_canvas.ViewPos.X, y + m_canvas.ViewPos.Y))
                {
                    m_selectArrow = i;
                    break;
                }
            }

            if (lastSelectArrow != m_selectArrow)
            {
                m_redraw.Redraw();
            }

            return m_selectArrow != -1;
        }

        public bool OnClick(int x, int y)
        {
            bool handled = false;
            switch (m_selectArrow)
            {
                case Direction_Upper:
                    if (m_upperContactEdge.Count > 0)
                    {
                        m_select.Position = new Point(m_select.Position.X, m_upperContactEdge.Last() - m_select.Size.Height);
                        handled = true;
                    }
                    break;
                case Direction_UpperLeft:
                    if (m_upperContactEdge.Count > 0 && m_leftContactEdge.Count > 0)
                    {
                        m_select.Position = new Point(m_leftContactEdge.Last() - m_select.Size.Width, m_upperContactEdge.Last() - m_select.Size.Height);
                        handled = true;
                    }
                    break;
                case Direction_UpperRight:
                    if (m_upperContactEdge.Count > 0 && m_rightContactEdge.Count > 0)
                    {
                        m_select.Position = new Point(m_rightContactEdge.First(), m_upperContactEdge.Last() - m_select.Size.Height);
                        handled = true;
                    }
                    break;
                case Direction_Lower:
                    if (m_lowerContactEdge.Count > 0)
                    {
                        m_select.Position = new Point(m_select.Position.X, m_lowerContactEdge.First());
                        handled = true;
                    }
                    break;
                case Direction_LowerLeft:
                    if (m_lowerContactEdge.Count > 0 && m_leftContactEdge.Count > 0)
                    {
                        m_select.Position = new Point(m_leftContactEdge.Last() - m_select.Size.Width, m_lowerContactEdge.First());
                        handled = true;
                    }
                    break;
                case Direction_LowerRight:
                    if (m_lowerContactEdge.Count > 0 && m_rightContactEdge.Count > 0)
                    {
                        m_select.Position = new Point(m_rightContactEdge.First(), m_lowerContactEdge.First());
                        handled = true;
                    }
                    break;
                case Direction_Left:
                    if (m_leftContactEdge.Count > 0)
                    {
                        m_select.Position = new Point(m_leftContactEdge.Last() - m_select.Size.Width, m_select.Position.Y);
                        handled = true;
                    }
                    break;
                case Direction_Right:
                    if (m_rightContactEdge.Count > 0)
                    {
                        m_select.Position = new Point(m_rightContactEdge.First(), m_select.Position.Y);
                        handled = true;
                    }
                    break;
                default:
                    break;
            };
            return handled;
        }

        private void ContactBegin()
        {
            m_leftContactEdge.Clear();
            m_upperContactEdge.Clear();
            m_rightContactEdge.Clear();
            m_lowerContactEdge.Clear();

            /*
            m_leftContactEdge.Add(m_canvas.Size.Width);
            m_upperContactEdge.Add(m_canvas.Size.Height);
            m_rightContactEdge.Add(0);
            m_lowerContactEdge.Add(0);*/
        }

        private void Contact(SubImage imageA, SubImage imageB)
        {
            if (imageA == imageB)
            {
                return;
            }

            if (imageA.Rectangle.IntersectsWith(imageB.Rectangle))
            {
                /*
                // 相交左上角的点则添加左和顶边
                if (imageA.Rectangle.Contains(imageB.Position))
                {
                    m_leftContactEdge.Add(imageB.Position.X);
                    m_upperContactEdge.Add(imageB.Position.Y);
                }

                // 相交右上角的点则添加右和顶边
                if (imageA.Rectangle.Contains(imageB.Position.X + imageB.Size.Width, imageB.Position.Y))
                {
                    m_upperContactEdge.Add(imageB.Position.Y);
                    m_rightContactEdge.Add(imageB.Position.X + imageB.Size.Width);
                }

                // 相交左下角的点则添加左和底边
                if (imageA.Rectangle.Contains(imageB.Position.X, imageB.Position.Y + imageB.Size.Height))
                {
                    m_leftContactEdge.Add(imageB.Position.X);
                    m_lowerContactEdge.Add(imageB.Position.Y + imageB.Size.Height);
                }

                // 相交右下角的点则添加右和底边
                if (imageA.Rectangle.Contains(imageB.Position.X + imageB.Size.Width, imageB.Position.Y + imageB.Size.Height))
                {
                    m_rightContactEdge.Add(imageB.Position.X + imageB.Size.Width);
                    m_lowerContactEdge.Add(imageB.Position.Y + imageB.Size.Height);
                }
                */
                m_leftContactEdge.Add(imageB.Position.X);
                m_upperContactEdge.Add(imageB.Position.Y);
                m_rightContactEdge.Add(imageB.Position.X + imageB.Size.Width);
                m_lowerContactEdge.Add(imageB.Position.Y + imageB.Size.Height);
            }
        }

        private void ContactEnd()
        {
            m_leftContactEdge.Sort();
            m_upperContactEdge.Sort();
            m_rightContactEdge.Sort();
            m_lowerContactEdge.Sort();
        }

        #endregion Methods

        #region Properties
        #endregion Properties

        #region Constructors

        public DockAid(ListView.ListViewItemCollection items, Canvas canvas, IRedraw redraw)
        {
            m_arrow = new SubImage[] {
                new SubImage(@"icons\arrow_upper.png"),
                new SubImage(@"icons\arrow_upper_left.png"),
                new SubImage(@"icons\arrow_upper_right.png"),
                new SubImage(@"icons\arrow_lower.png"),
                new SubImage(@"icons\arrow_lower_left.png"),
                new SubImage(@"icons\arrow_lower_right.png"),
                new SubImage(@"icons\arrow_left.png"),
                new SubImage(@"icons\arrow_right.png"),
            };

            m_items = items;

            m_canvas = canvas;

            m_redraw = redraw;

            m_leftContactEdge = new List<int>();

            m_upperContactEdge = new List<int>();

            m_rightContactEdge = new List<int>();

            m_lowerContactEdge = new List<int>();
        }

        #endregion Constructors
    };

    internal class SubImage : IDisposable
    {

        #region Fields

        private Point m_position;
        private Bitmap m_image;
        private Rectangle m_rect;
        private string m_filePath;
        private string m_name;
        private ListViewItem m_bindItem;

        #endregion Fields

        #region Methods

        /// <summary>
        /// 检查是否与另一个对象相交
        /// </summary>
        /// <returns></returns>
        public bool Intersect(SubImage image)
        {
            Rectangle rect = new Rectangle(image.Position, image.Size);

            return Rectangle.IntersectsWith(rect);
        }

        public bool Intersect(Rectangle rect)
        {
            return Rectangle.IntersectsWith(rect);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Free managed objects.  
            }

            // Free unmanaged objects
            if (m_image != null)
            {
                m_image.Dispose();
                m_image = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // Ensure that the destructor is not called  
            GC.SuppressFinalize(this);
        }


        #endregion Methods

        #region Properties

        public Point Position
        {
            get { return m_position; }
            set { m_position = value; m_rect = new Rectangle(this.Position, this.Size); }
        }

        public Size Size
        {
            get { return m_image.Size; }
        }

        public Bitmap Image
        {
            get { return m_image; }
        }

        public string FilePath
        {
            get { return m_filePath; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public ListViewItem BindItem
        {
            get { return m_bindItem; }
            set { m_bindItem = value; }
        }

        public Rectangle Rectangle
        {
            get { return m_rect; }
        }

        #endregion Properties

        #region Constructors

        public SubImage(string file)
        {
            m_position = new Point(0, 0);
            m_image = new Bitmap(file);
            m_filePath = file;
            m_name = m_filePath.Split('\\').Last().Split('.').First();
            m_bindItem = null;
            m_rect = new Rectangle(this.Position, this.Size);
        }

        #endregion Constructors
    };
}
