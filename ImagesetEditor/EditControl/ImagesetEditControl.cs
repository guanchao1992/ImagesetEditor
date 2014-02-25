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

namespace ImageSetEditor
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
        /// 画布
        /// </summary>
        private Canvas m_canvas;

        /// <summary>
        /// 选中对象
        /// </summary>
        private SubImageSelects m_select;

        
        /// <summary>
        /// 显示信息的图片
        /// </summary>
        private SubImage m_viewInfoImage;

        /*
        /// <summary>
        /// 当前选中的图片组
        /// </summary>
        private List<SubImage> m_selects;
        */

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
        /// 停靠辅助
        /// </summary>
        private DockAid m_dockAid;

        /// <summary>
        /// 对齐预览线段
        /// </summary>
        private Point m_alignmentPreviewLineBegin;

        private Point m_alignmentPreviewLineEnd;

        /// <summary>
        /// 用于排序对齐边的列表
        /// </summary>
        private List<int> m_alignmentArray;

        #endregion Fields

        #region Methods

        public void AddImage(string fileName)
        {
            ListViewItem newItem = new ListViewItem();

            Bitmap image = null;

            try
            {

                image = new Bitmap(fileName);
            }
            catch
            {
                throw new SystemException(fileName + " 加载失败");
            }

            SubImage newImage = new SubImage(image);

            newImage.FilePath = fileName;

            newImage.BindItem = newItem;

            newItem.Tag = newImage;

            newImage.SetDefaultName();

            newItem.Text = newImage.Name;

            newImage.Position =
                new Point(m_canvas.ViewPos.X, m_canvas.ViewPos.Y);

            usedListView.Items.Add(newItem);

            ImageCountUpdate();
        }

        public bool AddImage(string fileName, string name, Point position, Size size)
        {
            bool result = true;

            ListViewItem newItem = new ListViewItem();

            Image image = null;

            try
            {
                image = new Bitmap(fileName);
            }
            catch
            {
                image = CreateErrorImage(size);
                result = false;
            }

            SubImage newImage = new SubImage(image);

            newImage.FilePath = fileName;

            newImage.Name = name;

            newImage.Position = position;

            newImage.Size = size;

            newImage.BindItem = newItem;

            newItem.Tag = newImage;

            newItem.Text = newImage.Name;
           
            usedListView.Items.Add(newItem);

            ImageCountUpdate();

            return result;
        }

        public void ClearImages()
        {
            SetViewInfo(null);

            m_select.Selects.Clear();

            foreach (ListViewItem item in usedListView.Items)
            {
                ((SubImage)item.Tag).Dispose();
                item.Tag = null;
                item.Remove();
            }

            ImageCountUpdate();

            ImageSetBoxUpdate();
        }

        public void Export(IImagesetExport saver)
        {
            saver.OnExportBegin(m_canvas);

            foreach (ListViewItem item in usedListView.Items)
            {
                saver.OnExportImage((SubImage)item.Tag);
            }

            ExportImage(saver.OnExportEnd());
        }

        private void ExportImage(string filePath)
        {
            if (filePath == null || filePath == "")
                return;

            Bitmap outputImage = new Bitmap(
                m_canvas.Size.Width, 
                m_canvas.Size.Height, 
                PixelFormat.Format32bppArgb);

            Graphics outputGraph = Graphics.FromImage(outputImage);

            outputGraph.CompositingQuality = CompositingQuality.HighQuality;
            outputGraph.SmoothingMode = SmoothingMode.HighQuality;
            outputGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            outputGraph.Clear(Color.Transparent);

            foreach (ListViewItem item in usedListView.Items)
            {
                SubImage image = (SubImage)item.Tag;
                outputGraph.DrawImage(image.Image,
                        new Rectangle(image.Position.X, image.Position.Y, image.Size.Width, image.Size.Height),
                        new Rectangle(0, 0, image.Size.Width, image.Size.Height), 
                        GraphicsUnit.Pixel);
            }

            outputImage.Save(filePath, outputImage.RawFormat);
        }

        private void ImageSetBoxUpdate()
        {
            imageSetBox.Invalidate();
        }

        public void Redraw()
        {
            ImageSetBoxUpdate();
        }

        private void ImageCountUpdate()
        {
            imageCountToolStripLabel.Text = "共 " + usedListView.Items.Count.ToString() + " 个图片";
        }

        private void SetViewInfo(SubImage image)
        {
            if (image == null)
            {
                nameToolStripTextBox.Text = "不可用";

                posToolStripTextBox.Text = "不可用";

                sizeToolStripTextBox.Text = "不可用";

                nameToolStripTextBox.ReadOnly = true;

                posToolStripTextBox.ReadOnly = true;
            }
            else
            {
                nameToolStripTextBox.Text = image.Name;

                posToolStripTextBox.Text =
                    String.Format("{0},{1}", image.Position.X, image.Position.Y);

                sizeToolStripTextBox.Text =
                    String.Format("{0},{1}", image.Size.Width, image.Size.Height);

                nameToolStripTextBox.ReadOnly = false;

                posToolStripTextBox.ReadOnly = false;
            }

            m_viewInfoImage = image;
        }

        private Image CreateErrorImage(Size size)
        {
            Bitmap image = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);

            Graphics graph = Graphics.FromImage(image);

            Pen redPen = new Pen(Color.Red);

            redPen.DashStyle = DashStyle.DashDot;

            graph.DrawRectangle(redPen, 0, 0, size.Width - 1, size.Height - 1);

            graph.DrawLine(redPen, 0, 0, size.Width - 1, size.Height - 1);

            graph.DrawLine(redPen, size.Width - 1, 0, 0, size.Height - 1);

            return image;
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

            ImageSetBoxUpdate();
        }

        private void ClearSelects()
        {
            if (m_select.Selects.Count != 0)
            {
                m_listViewNodeLock = true;

                foreach (SubImage image in m_select.Selects)
                {
                    image.BindItem.Selected = false;
                }

                m_listViewNodeLock = false;

                m_select.Selects.Clear();

                SetViewInfo(null);
            }

            m_dockAid.SetImage(null);
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

                    SetViewInfo(image);

                    m_select.Selects.Add(image);

                    m_select.UpdateRim();

                    m_dockAid.SetImage(m_select);

                    break;
                }
            }
        }

        #endregion Methods

        #region Properties

        public Size CanvasSize
        {
            get { return m_canvas.Size; }
            set
            {
                sizeSetToolStripComboBox.Text =
                  value.Width.ToString() + "*" + value.Height.ToString();
                sizeSetToolStripComboBox_SelectedIndexChanged(null, null);
            }
        }

        public int ImageCount
        {
            get { return usedListView.Items.Count; }
        }

        public bool RimView
        {
            get { return rimViewToolStripMenuItem.Checked; }
            set { rimViewToolStripMenuItem.Checked = value; }
        }

        public Color ColorWorkSpace
        {
            get { return m_canvas.ColorWorkSpace; }
            set { 
                m_canvas.ColorWorkSpace = value;
                colorWorkspaceToolStripMenuItem.BackColor = value;
                colorDialog.Color = value;
                ImageSetBoxUpdate();
            }
        }

        #endregion Properties

        #region Events

        private void imageSetBox_SizeChanged(object sender, EventArgs e)
        {
            m_canvas.ViewSize = imageSetBox.Size;

            /// 可视范围比画布大
            if (m_canvas.ViewSize.Height >= m_canvas.Size.Height)
            {
                vScrollBar.Visible = false;
                m_canvas.ViewPos = new Point(m_canvas.ViewPos.X, 0);
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
                m_canvas.ViewPos = new Point(0, m_canvas.ViewPos.Y);
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

                            m_select.SetMove(
                                m_curMousePos.X - m_beginMousePos.X,
                                m_curMousePos.Y - m_beginMousePos.Y);

                            m_dockAid.SetImage(m_select);

                            if (m_select != null)
                            {
                                SetViewInfo(m_viewInfoImage);
                            }

                            ImageSetBoxUpdate();
                        }
                        break;
                    case MouseStatus.Select:
                        {
                            m_MouseStatus = MouseStatus.Normal;

                            /// 拖动的矩形区域
                            Rectangle selectArea = GetRectangleArea(
                                m_beginMousePos,
                                m_curMousePos,
                                m_canvas.ViewPos);

                            /// 当拖动一个很小的矩形区域时，视为点击了一下
                            if (selectArea.Width <= 5 && selectArea.Height <= 5)
                            {
                                m_beginMousePos = m_curMousePos;
                            }

                            if (m_beginMousePos == m_curMousePos)
                            {
                                /// 光标位置不变点击一次则只选取最顶层的图片
                                SelectTop();
                            }
                            else
                            {

                                foreach (ListViewItem item in usedListView.Items)
                                {
                                    SubImage image = (SubImage)item.Tag;

                                    m_listViewNodeLock = true;

                                    if (selectArea.IntersectsWith(image.Rectangle))
                                    {
                                        item.Selected = true;
                                        m_select.Selects.Add(image);
                                    }

                                    m_listViewNodeLock = false;
                                }

                                m_select.UpdateRim();

                                m_dockAid.SetImage(m_select);

                                if (m_select.Selects.Count == 1)
                                {
                                    SetViewInfo(m_select.Selects.First());
                                }
                            }

                            ImageSetBoxUpdate();
                        }
                        break;
                    default:
                        if (m_dockAid.InArrowButton(e.X, e.Y) && m_dockAid.OnClick(e.X, e.Y))
                        {
                            m_dockAid.SetImage(m_select);
                            ImageSetBoxUpdate();
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
                ImageSetBoxUpdate();

                return;
            }

            m_inSelects = false;

            if (m_dockAid.OnMouseMove(e.X, e.Y))
            {
                imageSetBox.Cursor = Cursors.Hand;
                return;
            }

            /// 鼠标移动到已经选择的图片里时变更光标

            foreach (SubImage image in m_select.Selects)
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

        private void imagenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSelects();

            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            SubImage image = (SubImage)item.Tag;

            m_listViewNodeLock = true;

            image.BindItem.Selected = true;

            SetViewInfo(image);

            m_select.Selects.Clear();
            m_select.Selects.Add(image);

            m_listViewNodeLock = true;

            ImageSetBoxUpdate();
        }

        private void imageSetBox_Paint(object sender, PaintEventArgs e)
        {
            m_canvas.Begin(e.Graphics);

            /// 绘制图片内容和他们的边框
            foreach (ListViewItem item in usedListView.Items)
            {
                if (m_MouseStatus == MouseStatus.Drag)
                {
                    if (item.Selected)
                        continue;
                }

                SubImage image = (SubImage)item.Tag;

                m_canvas.DrawImage(image);

                if (rimViewToolStripMenuItem.Checked)
                    m_canvas.DrawImageRim(image, m_canvas.RimPen);
            }

            /// 绘制选中项
            if (m_MouseStatus == MouseStatus.Drag)
            {
                Point offset = new Point(
                    m_curMousePos.X - m_beginMousePos.X,
                    m_curMousePos.Y - m_beginMousePos.Y);

                foreach (SubImage image in m_select.Selects)
                {
                    m_canvas.DrawImage(image, offset);
                }

                foreach (SubImage image in m_select.Selects)
                {
                    m_canvas.DrawImageArea(image, m_canvas.SelectPen, offset);
                }

                if (m_select.Selects.Count > 1)
                {
                    m_canvas.DrawImageRim(m_select, m_canvas.SelectPen, offset);
                }
            }
            else
            {
                foreach (SubImage image in m_select.Selects)
                {
                    m_canvas.DrawImageArea(image, m_canvas.SelectPen);
                }

                if (m_select.Selects.Count > 1)
                {
                    m_canvas.DrawImageRim(m_select, m_canvas.SelectPen);
                }
            }

            /// 绘制鼠标框选的区域
            if (m_MouseStatus == MouseStatus.Select)
            {
                Rectangle selectArea =
                    GetRectangleArea(m_beginMousePos, m_curMousePos, m_canvas.ViewPos);

                m_canvas.DrawRectangle(selectArea);
            }

            m_canvas.DrawRim();

            if (m_alignmentPreviewLineBegin != m_alignmentPreviewLineEnd)
            {
                m_canvas.DrawDockLine(
                    m_alignmentPreviewLineBegin.X,
                    m_alignmentPreviewLineBegin.Y,
                    m_alignmentPreviewLineEnd.X,
                    m_alignmentPreviewLineEnd.Y);
            }

            m_dockAid.Draw(m_canvas);

            m_canvas.End();
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            m_canvas.ViewPos = new Point(hScrollBar.Value, vScrollBar.Value);

            ImageSetBoxUpdate();
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            m_canvas.ViewPos = new Point(hScrollBar.Value, vScrollBar.Value);

            ImageSetBoxUpdate();
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
                    AddImage(file);
                }

                ImageSetBoxUpdate();

                ImageCountUpdate();
            }
        }

        private void clearUsedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usedListView.Items.Count == 0)
                return;
            if (DialogResult.OK == MessageBox.Show("删除所有？", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                ClearImages();
            }
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

                SetViewInfo(null);
                m_select.Selects.Clear();
            }

            ImageCountUpdate();

            ImageSetBoxUpdate();
        }

        private void sizeSetToolStripComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sizeSetToolStripComboBox_SelectedIndexChanged(sender, e);
            }
        }

        private void sizeSetToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] sizeParam = sizeSetToolStripComboBox.Text.Split('*');

                m_canvas.Size = new Size(int.Parse(sizeParam[0]), int.Parse(sizeParam[1]));
            }
            catch
            {
                MessageBox.Show("无效的文本格式", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sizeSetToolStripComboBox.Text = 
                    m_canvas.Size.Width.ToString() + "*" + m_canvas.Size.Height.ToString();
                return;
            }

            m_canvas.ViewPos = new Point(0, 0);
            hScrollBar.Value = 0;
            vScrollBar.Value = 0;

            imageSetBox_SizeChanged(null, null);

            ImageSetBoxUpdate();
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

        private void posToolStripTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (posToolStripTextBox.Text.Length == 0)
                {
                    posToolStripTextBox.Text = posToolStripTextBox.Text =
                    String.Format("{0},{1}", m_select.Position.X, m_select.Position.Y); ;
                    return;
                }

                string[] strArray = posToolStripTextBox.Text.Split(',');

                try
                {
                    if (strArray.Count() != 2)
                    {
                        throw new SystemException("无效的参数");
                    }

                    m_select.Position =
                        new Point(int.Parse(strArray[0]), int.Parse(strArray[1]));

                    ImageSetBoxUpdate();
                }
                catch (SystemException exc)
                {
                    MessageBox.Show(exc.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    posToolStripTextBox.Text = 
                        String.Format("{0},{1}", m_select.Position.X, m_select.Position.Y); ;
                }
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

            if (n == 1)
            {
                SetViewInfo((SubImage)usedListView.SelectedItems[0].Tag);
            }
            else
            {
                SetViewInfo(null);
            }

            m_select.Selects.Clear();

            foreach (ListViewItem item in usedListView.SelectedItems)
            {
                m_select.Selects.Add((SubImage)item.Tag);
            }

            m_select.UpdateRim();

            ImageSetBoxUpdate();
        }

        private void rimViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rimViewToolStripMenuItem.Checked = !rimViewToolStripMenuItem.Checked;

            ImageSetBoxUpdate();
        }

        private void imageSetBoxContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            alignmentToolStripMenuItem.Enabled = (m_select.Selects.Count > 1);
        }

        private void imageSetBoxContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            m_alignmentPreviewLineEnd = m_alignmentPreviewLineBegin;
        }

        private void leftOutsideAlignmentToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            m_alignmentArray.Clear();

            foreach (SubImage image in m_select.Selects)
            {
                m_alignmentArray.Add(image.Position.X);
            }

            m_alignmentArray.Sort();

            m_alignmentPreviewLineBegin = new Point(m_alignmentArray.First(), 0);

            m_alignmentPreviewLineEnd = new Point(m_alignmentArray.First(), m_canvas.Size.Height);

            leftOutsideAlignmentToolStripMenuItem.Tag = m_alignmentArray.First();

            ImageSetBoxUpdate();
        }

        private void leftOutsideAlignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (SubImage image in m_select.Selects)
            {
                image.Position = new Point(
                    (int)leftOutsideAlignmentToolStripMenuItem.Tag, image.Position.Y);
            }

            m_select.UpdateRim();

            ImageSetBoxUpdate();
        }

        private void leftInsideAlignmentToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            m_alignmentArray.Clear();

            foreach (SubImage image in m_select.Selects)
            {
                m_alignmentArray.Add(image.Position.X);
            }

            m_alignmentArray.Sort();

            m_alignmentPreviewLineBegin = new Point(m_alignmentArray.Last(), 0);

            m_alignmentPreviewLineEnd = new Point(m_alignmentArray.Last(), m_canvas.Size.Height);

            leftInsideAlignmentToolStripMenuItem.Tag = m_alignmentArray.Last();

            ImageSetBoxUpdate();
        }

        private void leftInsideAlignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (SubImage image in m_select.Selects)
            {
                image.Position = new Point(
                    (int)leftInsideAlignmentToolStripMenuItem.Tag, image.Position.Y);
            }

            m_select.UpdateRim();

            ImageSetBoxUpdate();
        }

        private void rightOutsideAlignmentToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            m_alignmentArray.Clear();

            foreach (SubImage image in m_select.Selects)
            {
                m_alignmentArray.Add(image.Position.X + image.Size.Width);
            }

            m_alignmentArray.Sort();

            m_alignmentPreviewLineBegin = new Point(m_alignmentArray.Last(), 0);

            m_alignmentPreviewLineEnd = new Point(m_alignmentArray.Last(), m_canvas.Size.Height);

            rightOutsideAlignmentToolStripMenuItem.Tag = m_alignmentArray.Last();

            ImageSetBoxUpdate();
        }

        private void rightOutsideAlignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (SubImage image in m_select.Selects)
            {
                image.Position = new Point(
                    (int)rightOutsideAlignmentToolStripMenuItem.Tag - image.Size.Width, image.Position.Y);
            }

            m_select.UpdateRim();

            ImageSetBoxUpdate();
        }

        private void rightInsideAlignmentToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            m_alignmentArray.Clear();

            foreach (SubImage image in m_select.Selects)
            {
                m_alignmentArray.Add(image.Position.X + image.Size.Width);
            }

            m_alignmentArray.Sort();

            m_alignmentPreviewLineBegin = new Point(m_alignmentArray.First(), 0);

            m_alignmentPreviewLineEnd = new Point(m_alignmentArray.First(), m_canvas.Size.Height);

            rightInsideAlignmentToolStripMenuItem.Tag = m_alignmentArray.First();

            ImageSetBoxUpdate();
        }

        private void rightInsideAlignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (SubImage image in m_select.Selects)
            {
                image.Position = new Point(
                    (int)rightInsideAlignmentToolStripMenuItem.Tag - image.Size.Width, image.Position.Y);
            }

            m_select.UpdateRim();

            ImageSetBoxUpdate();
        }

        private void topOutsideAlignmentToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            m_alignmentArray.Clear();

            foreach (SubImage image in m_select.Selects)
            {
                m_alignmentArray.Add(image.Position.Y);
            }

            m_alignmentArray.Sort();

            m_alignmentPreviewLineBegin = new Point(0, m_alignmentArray.First());

            m_alignmentPreviewLineEnd = new Point(m_canvas.Size.Width, m_alignmentArray.First());

            topOutsideAlignmentToolStripMenuItem.Tag = m_alignmentArray.First();

            ImageSetBoxUpdate();
        }
        
        private void topOutsideAlignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (SubImage image in m_select.Selects)
            {
                image.Position = new Point(
                    image.Position.X, (int)topOutsideAlignmentToolStripMenuItem.Tag);
            }

            m_select.UpdateRim();

            ImageSetBoxUpdate();
        }

        private void topInsideAlignmentToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            m_alignmentArray.Clear();

            foreach (SubImage image in m_select.Selects)
            {
                m_alignmentArray.Add(image.Position.Y);
            }

            m_alignmentArray.Sort();

            m_alignmentPreviewLineBegin = new Point(0, m_alignmentArray.Last());

            m_alignmentPreviewLineEnd = new Point(m_canvas.Size.Width, m_alignmentArray.Last());

            topInsideAlignmentToolStripMenuItem.Tag = m_alignmentArray.Last();

            ImageSetBoxUpdate();
        }

        private void topInsideAlignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (SubImage image in m_select.Selects)
            {
                image.Position = new Point(
                    image.Position.X, (int)topInsideAlignmentToolStripMenuItem.Tag);
            }

            m_select.UpdateRim();

            ImageSetBoxUpdate();
        }

        private void bottomOutsideAlignmentToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            m_alignmentArray.Clear();

            foreach (SubImage image in m_select.Selects)
            {
                m_alignmentArray.Add(image.Position.Y + image.Size.Height);
            }

            m_alignmentArray.Sort();

            m_alignmentPreviewLineBegin = new Point(0, m_alignmentArray.Last());

            m_alignmentPreviewLineEnd = new Point(m_canvas.Size.Width, m_alignmentArray.Last());

            bottomOutsideAlignmentToolStripMenuItem.Tag = m_alignmentArray.Last();

            ImageSetBoxUpdate();
        }

        private void bottomOutsideAlignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (SubImage image in m_select.Selects)
            {
                image.Position = new Point(
                    image.Position.X, (int)bottomOutsideAlignmentToolStripMenuItem.Tag - image.Size.Height);
            }

            m_select.UpdateRim();

            ImageSetBoxUpdate();
        }

        private void bottomInsideAlignmentToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            m_alignmentArray.Clear();

            foreach (SubImage image in m_select.Selects)
            {
                m_alignmentArray.Add(image.Position.Y + image.Size.Height);
            }

            m_alignmentArray.Sort();

            m_alignmentPreviewLineBegin = new Point(0, m_alignmentArray.First());

            m_alignmentPreviewLineEnd = new Point(m_canvas.Size.Width, m_alignmentArray.First());

            bottomInsideAlignmentToolStripMenuItem.Tag = m_alignmentArray.First();

            ImageSetBoxUpdate();
        }

        private void bottomInsideAlignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (SubImage image in m_select.Selects)
            {
                image.Position = new Point(
                    image.Position.X, (int)bottomInsideAlignmentToolStripMenuItem.Tag - image.Size.Height);
            }

            m_select.UpdateRim();

            ImageSetBoxUpdate();
        }

        private void colorWorkspaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.colorDialog.ShowDialog())
            {
                colorWorkspaceToolStripMenuItem.BackColor = colorDialog.Color;

                m_canvas.ColorWorkSpace = colorDialog.Color;

                ImageSetBoxUpdate();
            }
        }

        private void colorWorkspaceToolStripMenuItem_BackColorChanged(object sender, EventArgs e)
        {
            if (colorWorkspaceToolStripMenuItem.BackColor.GetBrightness() * 240 < 90)
            {
                colorWorkspaceToolStripMenuItem.ForeColor = Color.White;
            }
            else
            {
                colorWorkspaceToolStripMenuItem.ForeColor = Color.Black;
            }
        }

        private void imageSetBox_Click(object sender, EventArgs e)
        {
            usedListView.Select();
        }

        #endregion Events

        #region Constructors

        public ImagesetEditControl()
        {
            m_canvas = new Canvas();

            m_select = new SubImageSelects();

            m_alignmentArray = new List<int>();

            m_MouseStatus = MouseStatus.Normal;

            InitializeComponent();

            m_dockAid = new DockAid(usedListView.Items, m_canvas, this);

            SetViewInfo(null);

            sizeSetToolStripComboBox.SelectedIndex = 3;
        }

        #endregion Constructors     
    }

    public interface IImage
    {
        #region Properties

        Point Position { get; }

        Size Size { get; }

        string FilePath { get; }

        string Name { get; }

        #endregion Properties
    }

    public interface ICanvas
    {
        #region Properties

        Size Size { get; }

        Color ColorWorkSpace { get; }

        #endregion Properties
    }

    public interface IImagesetExport
    {
        #region Methods

        void OnExportBegin(ICanvas canvas);

        void OnExportImage(IImage image);

        string OnExportEnd();

        #endregion Methods
    }

    internal interface IRedraw
    {
        #region Methods

        void Redraw();

        #endregion Methods
    }

    /// <summary>
    /// 画布对象
    /// </summary>
    internal class Canvas : ICanvas
    {
        #region Fields

        /// <summary>
        /// 画布位图绘图接口
        /// </summary>
        private Graphics m_viewGraph;

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

        private Pen m_selectPen;

        private Pen m_blackPen;

        private Pen m_workSpacePen;

        private Pen m_greenPen;

        private Pen m_rimPen;

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
                m_workSpacePen.Brush,
                -m_viewPosition.X,
                -m_viewPosition.Y,
                m_size.Width,
                m_size.Height);
        }

        public void DrawRim()
        {
            m_viewGraph.DrawRectangle(
                m_rimPen,
                -m_viewPosition.X,
                -m_viewPosition.Y,
                m_size.Width,
                m_size.Height);
        }

        public void End()
        {
            
        }

        public void DrawImage(SubImage image, Point offset)
        {
            image.Draw(m_viewGraph,
                image.Position.X - m_viewPosition.X + offset.X,
                image.Position.Y - m_viewPosition.Y + offset.Y,
                image.Size.Width,
                image.Size.Height);
        }

        public void DrawImage(SubImage image)
        {
            if (IsView(image) == false)
                return;
            image.Draw(m_viewGraph, 
                image.Position.X - m_viewPosition.X,
                image.Position.Y - m_viewPosition.Y,
                image.Size.Width,
                image.Size.Height);
        }

        public void DrawImageRim(SubImage image, Pen pen)
        {
            m_viewGraph.DrawRectangle(
                pen,
                image.Position.X - m_viewPosition.X,
                image.Position.Y - m_viewPosition.Y,
                image.Size.Width - 1,
                image.Size.Height - 1);
        }

        public void DrawImageRim(SubImage image, Pen pen, Point offset)
        {
            m_viewGraph.DrawRectangle(
                pen,
                image.Position.X - m_viewPosition.X + offset.X,
                image.Position.Y - m_viewPosition.Y + offset.Y,
                image.Size.Width - 1,
                image.Size.Height - 1);
        }

        public void DrawImageArea(SubImage image, Pen pen)
        {
            DrawSmallBox(image.Position.X, image.Position.Y);
            DrawSmallBox(image.Position.X + image.Size.Width, image.Position.Y);
            DrawSmallBox(image.Position.X + image.Size.Width, image.Position.Y + image.Size.Height);
            DrawSmallBox(image.Position.X, image.Position.Y + image.Size.Height);

            m_viewGraph.DrawRectangle(
                pen,
                image.Position.X - m_viewPosition.X,
                image.Position.Y - m_viewPosition.Y,
                image.Size.Width - 1,
                image.Size.Height - 1);
        }

        public void DrawImageArea(SubImage image, Pen pen, Point offset)
        {
            Point p = new Point(image.Position.X + offset.X, image.Position.Y + offset.Y);

            DrawSmallBox(p.X, p.Y);
            DrawSmallBox(p.X + image.Size.Width, p.Y);
            DrawSmallBox(p.X + image.Size.Width, p.Y + image.Size.Height);
            DrawSmallBox(p.X, p.Y + image.Size.Height);
            
            m_viewGraph.DrawRectangle(
                pen,
                p.X - m_viewPosition.X,
                p.Y - m_viewPosition.Y,
                image.Size.Width - 1,
                image.Size.Height - 1);
        }

        public void DrawRectangle(Rectangle rect)
        {
            m_viewGraph.DrawRectangle(
                SelectPen,
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

        public Color ColorWorkSpace
        {
            get { return m_workSpacePen.Color; }
            set { m_workSpacePen.Color = value; }
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

        public Pen SelectPen
        {
            get { return m_selectPen; }
        }

        public Pen RimPen
        {
            get { return m_rimPen; }
        }

        public Pen GreenPen
        {
            get { return m_greenPen; }
        }

        public Pen BlackPen
        {
            get { return m_blackPen; }
        }

        #endregion Properties

        #region Constructors

        public Canvas()
        {
            m_viewGraph = null;

            m_selectPen = new Pen(Color.Gray);

            //m_selectPen.DashStyle = DashStyle.DashDot;

            m_blackPen = new Pen(Color.Black);

            m_workSpacePen = new Pen(Color.White);

            m_greenPen = new Pen(Color.Green);

            //m_greenPen.DashStyle = DashStyle.DashDot;

            m_rimPen = new Pen(Color.FromArgb(180, 180, 180));

            m_rimPen.DashStyle = DashStyle.Dash;
        }

        #endregion Constructors
    };

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

        private Arrow[] m_arrow;

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
                if (item.Selected)
                    continue;
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

            if (m_arrow[Direction_Upper].Visible = (m_upperContactEdge.Count > 0))
            {
                canvas.DrawImage(m_arrow[Direction_Upper]);
            }

            if (m_arrow[Direction_UpperLeft].Visible = (m_upperContactEdge.Count > 0 && m_leftContactEdge.Count > 0))
                canvas.DrawImage(m_arrow[Direction_UpperLeft]);

            if (m_arrow[Direction_UpperRight].Visible = (m_upperContactEdge.Count > 0 && m_rightContactEdge.Count > 0))
                canvas.DrawImage(m_arrow[Direction_UpperRight]);

            if (m_arrow[Direction_Lower].Visible = (m_lowerContactEdge.Count > 0))
                canvas.DrawImage(m_arrow[Direction_Lower]);

            if (m_arrow[Direction_LowerLeft].Visible = (m_lowerContactEdge.Count > 0 && m_leftContactEdge.Count > 0))
                canvas.DrawImage(m_arrow[Direction_LowerLeft]);

            if (m_arrow[Direction_LowerRight].Visible = (m_lowerContactEdge.Count > 0 && m_rightContactEdge.Count > 0))
                canvas.DrawImage(m_arrow[Direction_LowerRight]);

            if (m_arrow[Direction_Left].Visible = (m_leftContactEdge.Count > 0))
                canvas.DrawImage(m_arrow[Direction_Left]);

            if (m_arrow[Direction_Right].Visible = (m_rightContactEdge.Count > 0))
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
                if (m_arrow[i].Visible == false)
                    continue;
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
        }

        private void Contact(SubImage imageA, SubImage imageB)
        {
            if (imageA == null || imageB == null)
            {
                return;
            }

            if (imageA.Rectangle.IntersectsWith(imageB.Rectangle))
            {
                m_leftContactEdge.Add(imageB.Position.X);
                m_upperContactEdge.Add(imageB.Position.Y);
                m_rightContactEdge.Add(imageB.Position.X + imageB.Size.Width);
                m_lowerContactEdge.Add(imageB.Position.Y + imageB.Size.Height);
            }
        }

        private void ContactEnd()
        {
            if (m_select.Position.X < 0)
                m_rightContactEdge.Add(0);
            if (m_select.Position.Y < 0)
                m_lowerContactEdge.Add(0);
            if (m_select.Position.X + m_select.Size.Width > m_canvas.Size.Width)
                m_leftContactEdge.Add(m_canvas.Size.Width);
            if (m_select.Position.Y + m_select.Size.Height > m_canvas.Size.Height)
                m_upperContactEdge.Add(m_canvas.Size.Height);

            m_leftContactEdge.Sort();
            m_upperContactEdge.Sort();
            m_rightContactEdge.Sort();
            m_lowerContactEdge.Sort();
        }

        #endregion Methods

        #region Constructors

        public DockAid(ListView.ListViewItemCollection items, Canvas canvas, IRedraw redraw)
        {
            m_arrow = new Arrow[] {
                new Arrow(@"icons\arrow_upper.png"),
                new Arrow(@"icons\arrow_upper_left.png"),
                new Arrow(@"icons\arrow_upper_right.png"),
                new Arrow(@"icons\arrow_lower.png"),
                new Arrow(@"icons\arrow_lower_left.png"),
                new Arrow(@"icons\arrow_lower_right.png"),
                new Arrow(@"icons\arrow_left.png"),
                new Arrow(@"icons\arrow_right.png"),
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

    internal class SubImage : IImage, IDisposable
    {
        #region Fields

        protected Point m_position;

        protected Size m_size;

        protected Rectangle m_rect;

        private Image m_image;

        private string m_filePath;

        private string m_name;

        private ListViewItem m_bindItem;

        #endregion Fields

        #region Methods

        public virtual void Draw(Graphics graph, int x, int y, int width, int height)
        {
            graph.DrawImage(m_image, x, y, width, height);
        }

        public void SetDefaultName()
        {
            m_name = m_filePath.Split('\\').Last().Split('.').First();
        }

        public void SetImage(string file)
        {
            if (m_image != null)
            {
                m_image.Dispose();
                m_image = null;
            }

            m_image = new Bitmap(file);
        }

        public void SetImage(Image image)
        {
            if (m_image != null)
            {
                m_image.Dispose();
                m_image = null;
            }

            m_image = image;
        }

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

        virtual public Point Position
        {
            get { return m_position; }
            set { m_position = value; m_rect = new Rectangle(this.Position, this.Size); }
        }

        virtual public Size Size
        {
            get { return m_size; }
            set { m_size = value; }
        }

        public Image Image
        {
            get { return m_image; }
        }

        public string FilePath
        {
            get { return m_filePath; }
            set { m_filePath = value; }
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

        public SubImage(string fileName)
        {
            m_position = new Point(0, 0);

            m_image = new Bitmap(fileName);

            m_size = m_image.Size;

            m_filePath = "";

            SetDefaultName();

            m_bindItem = null;

            m_rect = new Rectangle(this.Position, this.Size);
        }

        public SubImage(Image image)
        {
            m_position = new Point(0, 0);

            m_image = image;

            if (image != null)
                m_size = m_image.Size;

            m_filePath = "";

            m_bindItem = null;

            m_rect = new Rectangle(this.Position, this.Size);
        }

        #endregion Constructors
    };

    internal class Arrow : SubImage
    {
        #region Properties

        public bool Visible { get; set; }

        #endregion Properties

        #region Constructors

        public Arrow(string fileName) 
            : base(fileName)
        {
        }

        #endregion Constructors
    }

    internal class SubImageSelects : SubImage
    {
        #region Fields

        private List<SubImage> m_selects;

        #endregion Fields

        #region Properties

        public List<SubImage> Selects
        {
            get { return m_selects; }
        }

        public override Point Position
        {
            get { return m_position; }
            set 
            { 
                SetMove(value.X - m_position.X, value.Y - m_position.Y);
                m_rect = new Rectangle(this.Position, this.Size);
            }
        }

        public override Size Size
        {
            get { return m_size; }
            set { m_size = value; }
        }

        #endregion Properties

        #region Methods

        public override void Draw(Graphics graph, int x, int y, int width, int height)
        {
            
        }


        public void SetMove(int x, int y)
        {
            m_position.X += x;
            m_position.Y += y;

            foreach (SubImage image in m_selects)
            {
                image.Position =
                    new Point(image.Position.X + x, image.Position.Y + y);
            }

            m_rect.Offset(x, y);
        }

        public void UpdateRim()
        {
            if (m_selects.Count == 0)
                return;

            m_position.X = m_selects.First().Position.X;
            m_position.Y = m_selects.First().Position.Y;

            foreach (SubImage image in m_selects)
            {
                m_position.X = Math.Min(image.Position.X, m_position.X);
                m_position.Y = Math.Min(image.Position.Y, m_position.Y);
            }

            m_size.Width = 0;
            m_size.Height = 0;

            foreach (SubImage image in m_selects)
            {
                m_size.Width = Math.Max(image.Position.X + image.Size.Width - m_position.X, m_size.Width);
                m_size.Height = Math.Max(image.Position.Y + image.Size.Height - m_position.Y, m_size.Height);
            }

            m_rect = new Rectangle(this.Position, this.Size);
        }

        #endregion Methods

        #region Constructors

        public SubImageSelects()
            : base((Image)null)
        {
            m_selects = new List<SubImage>();
        }

        #endregion Constructors
    }
}
