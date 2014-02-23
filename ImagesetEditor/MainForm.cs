using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ImageSetEditor
{
    public partial class MainForm : Form
    {
        #region Fields

        private ImagesetEditControl m_editControl;

        private string m_lastExportFile = "";

        private int m_lastExportFilterIndex;

        private string m_docPath = "";

        #endregion Fields

        #region Methods

        private void ExportToFile(string fileName, int typeIndex)
        {
            IImagesetExport export = null;

                switch (typeIndex)
                {
                    case 1:
                        export = new TextExport(fileName);
                        break;
                    case 2:
                        export = new XmlExport(fileName);
                        break;
                    default:
                        break;
                }

                m_editControl.Export(export);
        }

        #endregion Methods

        #region Events

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutForm()).ShowDialog();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportFileDialog.FileName = "";

            if (DialogResult.OK == exportFileDialog.ShowDialog())
            {
                ExportToFile(exportFileDialog.FileName, exportFileDialog.FilterIndex);

                toolStripStatusLabel.Text = "已导出至 " + exportFileDialog.FileName;

                m_lastExportFile = exportFileDialog.FileName;

                m_lastExportFilterIndex = openFileDialog.FilterIndex;
            }
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_editControl.ImageCount != 0)
            {
                if (DialogResult.Cancel == MessageBox.Show(
                    "新建项目并且清空当前图片组？", 
                    "新建项目", 
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Question))
                {
                    return;
                }
            }

            m_lastExportFile = "";

            m_editControl.ClearImages();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                m_editControl.ClearImages();

                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(openFileDialog.FileName);

                string projectDir = ProjectExport.GetFolder(openFileDialog.FileName);

                List<string> loadFaild = new List<string>();

                m_editControl.CanvasSizeIndex = 
                    int.Parse(xmlDoc.DocumentElement.Attributes.GetNamedItem("SizeIndex").Value);

                m_editControl.RimView =
                    xmlDoc.DocumentElement.Attributes.GetNamedItem("RimView").Value == "True";

                exportFileDialog.InitialDirectory =
                    xmlDoc.DocumentElement.Attributes.GetNamedItem("LastExportFile").Value;

                m_lastExportFile =
                    xmlDoc.DocumentElement.Attributes.GetNamedItem("LastExportFile").Value;

                m_lastExportFilterIndex =
                    int.Parse(xmlDoc.DocumentElement.Attributes.GetNamedItem("LastExportFilterIndex").Value);

                foreach (XmlNode n in xmlDoc.DocumentElement)
                {
                    if (m_editControl.AddImage(
                        n.Attributes.GetNamedItem("Source").Value.Replace("$(ProjectDir)", projectDir),
                        n.Attributes.GetNamedItem("Name").Value,
                        new Point(
                            int.Parse(n.Attributes.GetNamedItem("XPos").Value),
                            int.Parse(n.Attributes.GetNamedItem("YPos").Value)
                            ),
                        new Size(
                            int.Parse(n.Attributes.GetNamedItem("Width").Value),
                            int.Parse(n.Attributes.GetNamedItem("Height").Value)
                            )
                        ) == false)
                    {
                        loadFaild.Add(n.Attributes.GetNamedItem("Source").Value.Replace("$(ProjectDir)", projectDir));
                    }
                }

                if (loadFaild.Count != 0)
                {
                    string text = "一个或多个图片未能加载，将使用红色线框代替它们，他们可能被其他程序占用或者已经从原来的位置移除。";

                    foreach (string s in loadFaild)
                    {
                        text = text + "\n\n" + s;
                    }

                    MessageBox.Show(text, "加载图片失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                m_editControl.Redraw();

                m_docPath = openFileDialog.FileName;

                toolStripStatusLabel.Text = "就绪";
            }
        }

        private void saveToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "";

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                try
                {
                    IImagesetExport export = new ProjectExport(saveFileDialog.FileName, this);

                    m_editControl.Export(export);

                    toolStripStatusLabel.Text = "已保存至 " + saveFileDialog.FileName;
                }
                catch (SystemException exc)
                {
                    MessageBox.Show(exc.Message, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                m_docPath = saveFileDialog.FileName;
            }
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (m_lastExportFile != "")
            {
                exportLastFileToolStripMenuItem.Text = "导出到最后导出位置 " + m_lastExportFile;
                exportLastFileToolStripMenuItem.Visible = true;
                toolStripStatusLabel.Text = "已导出至 " + m_lastExportFile;
            }
            else
            {
                exportLastFileToolStripMenuItem.Visible = false;
                toolStripStatusLabel.Text = "";
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_docPath == "")
            {
                exportToolStripMenuItem_Click(sender, e);
            }
            else
            {
                try
                {
                    IImagesetExport export = new ProjectExport(m_docPath, this);

                    m_editControl.Export(export);

                    toolStripStatusLabel.Text = "已保存至 " + m_docPath;
                }
                catch (SystemException exc)
                {
                    MessageBox.Show(exc.Message, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exportLastFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_lastExportFile == "")
            {
                exportToolStripMenuItem_Click(sender, e);
            }
            else
            {
                ExportToFile(m_lastExportFile, m_lastExportFilterIndex);

                toolStripStatusLabel.Text = "已导出至上一次的位置 " + m_lastExportFile;
            }
        }

        #endregion Events
  
        #region Properties

        public SaveFileDialog SaveFileDialog
        {
            get { return saveFileDialog; }
        }

        public SaveFileDialog ExportFileDialog
        {
            get { return exportFileDialog; }
        }

        public OpenFileDialog OpenFileDialog
        {
            get { return openFileDialog; }
        }

        public ImagesetEditControl EditControl
        {
            get { return m_editControl; }
        }

        public string LastExportFile
        {
            get { return m_lastExportFile; }
        }

        public int LastExportFilterIndex
        {
            get { return m_lastExportFilterIndex; }
        }

        #endregion Properties

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            m_editControl = new ImagesetEditControl();

            m_editControl.Dock = DockStyle.Fill;

            this.Controls.Add(m_editControl);

            m_editControl.BringToFront();
        }

        #endregion Constructors
    }
}
