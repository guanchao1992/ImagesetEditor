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
using System.IO;

namespace ImagesetEditor
{
    public partial class MainForm : Form
    {
        #region Fields

        private ImagesetEditControl m_editControl;

        private Localization m_local;

        private XmlDocument m_configDoc;

        private string m_lastExportFile = "";

        private int m_lastExportFilterIndex;

        private string m_docPath = "";

        private List<string> m_recentFiles;

        #endregion Fields

        #region Strings

        private string m_strNewProject = "New project";

        private string m_strNewProjectAndClear = "Create new project and clear current images?";

        private string m_strExportedTo = "Exported to #FILENAME#";

        private string m_strExportToLastPosition = "Export to last Position #FILENAME#";

        private string m_strExportedToLastPosition = "Exported to last Position #FILENAME#";

        private string m_strSavedTo = "Saved to #FILENAME#";

        private string m_strSavedError = "Failed to save";

        private string m_strOpenProject = "Open project";

        private string m_strOpenVersionMismatch = "Project version mismatch, try to open it?#N##N#To be opened project version: #OPEN-PROJECT-VER#, Created from the #OPEN-PROJECT-CREATED-VER# version of the editor。#N##N#Editor can open project version: #CURRENT-PROJECT-VER#";

        private string m_strOpenImageFailed = "One or more images failed to load, they will be replaced with a red frame, they may be in use by another program or has been removed from its original location.";

        private string m_strOpenProjectFailed = "Failed to open #FILENAME#";

        private string m_strStatusReady = "Ready";

        private string m_strChangeLanguageCaption = "Change language";

        private string m_strChangeLanguage = "Change the interface language must restart the program.";

        private string m_strFileNotExist = "File does not exist.";

        #endregion Strings

        #region Methods

        public static string GetVersion()
        {
            return "0.2.2";
        }

        public static string GetProjectVersion()
        {
            return "0.2";
        }

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

        private string GetConfig(string name, string attribute, string defaultValue)
        {
            XmlNode node = m_configDoc.DocumentElement.SelectSingleNode(name);
            if (node == null)
                return defaultValue;
            XmlNode att = node.Attributes.GetNamedItem(attribute);
            if (att == null)
                return defaultValue;
            return att.Value;
        }

        private void SetConfig(string name, string attribute, string value)
        {
            XmlNode node = m_configDoc.DocumentElement.SelectSingleNode(name);
            if (node == null)
                node = m_configDoc.DocumentElement.AppendChild(m_configDoc.CreateNode(XmlNodeType.Element, name, ""));
            XmlNode att = node.Attributes.GetNamedItem(attribute);
            if (att == null)
                att = node.Attributes.Append(m_configDoc.CreateAttribute(attribute));
            att.Value = value;
        }

        private void OpenFile(string fileName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                string projectVer = xmlDoc.DocumentElement.Attributes.GetNamedItem("ProjectVersion").Value;

                if (projectVer != GetProjectVersion())
                {
                    if (DialogResult.Cancel ==
                        MessageBox.Show(
                        m_strOpenVersionMismatch
                        .Replace("#N#", "\n")
                        .Replace("#OPEN-PROJECT-VER#", projectVer)
                        .Replace("#OPEN-PROJECT-CREATED-VER#", xmlDoc.DocumentElement.Attributes.GetNamedItem("Version").Value)
                        .Replace("#CURRENT-PROJECT-VER#", GetProjectVersion()),
                        m_strOpenProject, MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        return;
                    }
                }

                m_editControl.ClearImages();

                string projectDir = ProjectExport.GetFolder(fileName);

                List<string> loadFaild = new List<string>();

                m_editControl.CanvasSize = new Size(
                    int.Parse(xmlDoc.DocumentElement.Attributes.GetNamedItem("Width").Value),
                    int.Parse(xmlDoc.DocumentElement.Attributes.GetNamedItem("Height").Value)
                    );

                string[] colorParams =
                    xmlDoc.DocumentElement.Attributes.GetNamedItem("Background").Value.Split(',');

                if (colorParams.Count() == 3)
                {
                    m_editControl.ColorWorkSpace = Color.FromArgb(
                        int.Parse(colorParams[0]),
                        int.Parse(colorParams[1]),
                        int.Parse(colorParams[2]));
                }
                else
                {
                    m_editControl.ColorWorkSpace = Color.White;
                }

                m_editControl.RimView =
                    xmlDoc.DocumentElement.Attributes.GetNamedItem("RimView").Value == "True";

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
                    string text = m_strOpenImageFailed;

                    foreach (string s in loadFaild)
                    {
                        text = text + "\n\n" + s;
                    }

                    MessageBox.Show(text, m_strOpenProject, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                m_editControl.Redraw();

                m_docPath = fileName;

                toolStripStatusLabel.Text = m_strStatusReady;

                AddRecentFiles(fileName);
            }
            catch
            {
                MessageBox.Show(m_strOpenProjectFailed.Replace("#FILENAME#", fileName),
                    m_strOpenProject, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRecentFiles(string path)
        {
            this.Text = "ImagesetEditor - " + path.Split('\\').Last();

            for (int i = 0; i != m_recentFiles.Count();)
            {
                if (m_recentFiles[i] == path)
                {
                    m_recentFiles.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }

            m_recentFiles.Insert(0, path);

            while (m_recentFiles.Count() > 10)
            {
                m_recentFiles.RemoveAt(m_recentFiles.Count() - 1);
            }
        }

        private void UpdateLocalization(Localization local)
        {
            // Menu

            // file

            fileToolStripMenuItem.Text = local.GetName("Forms.MainForm.Menu.File.01");

            newToolStripMenuItem.Text = local.GetName("Forms.MainForm.Menu.File.02");

            openToolStripMenuItem.Text = local.GetName("Forms.MainForm.Menu.File.03");

            saveToolStripMenuItem.Text = local.GetName("Forms.MainForm.Menu.File.04");

            saveAsToolStripMenuItem.Text = local.GetName("Forms.MainForm.Menu.File.05");

            exportToolStripMenuItem.Text = local.GetName("Forms.MainForm.Menu.File.06");

            recentFilesToolStripMenuItem.Text = local.GetName("Forms.MainForm.Menu.File.07");

            exitToolStripMenuItem.Text = local.GetName("Forms.MainForm.Menu.File.08");

            // help

            helpToolStripMenuItem.Text = local.GetName("Forms.MainForm.Menu.Help.01");

            languageToolStripMenuItem.Text = local.GetName("Forms.MainForm.Menu.Help.02");

            aboutToolStripMenuItem.Text = local.GetName("Forms.MainForm.Menu.Help.03");

            // Messages

            m_strNewProject = local.GetName("Forms.MainForm.Messages.new-project");

            m_strNewProjectAndClear = local.GetName("Forms.MainForm.Messages.new-project-and-clear");

            m_strExportedTo = local.GetName("Forms.MainForm.Messages.exported-to");

            m_strExportToLastPosition = local.GetName("Forms.MainForm.Messages.export-to-last-position");

            m_strExportedToLastPosition = local.GetName("Forms.MainForm.Messages.exported-to-last-position");

            m_strSavedTo = local.GetName("Forms.MainForm.Messages.saved-to");

            m_strSavedError = local.GetName("Forms.MainForm.Messages.saved-error");

            m_strOpenProject = local.GetName("Forms.MainForm.Messages.open-project");

            m_strOpenVersionMismatch = local.GetName("Forms.MainForm.Messages.open-version-mismatch");

            m_strOpenImageFailed = local.GetName("Forms.MainForm.Messages.open-image-failed");

            m_strOpenProjectFailed = local.GetName("Forms.MainForm.Messages.open-project-failed");

            m_strStatusReady = local.GetName("Forms.MainForm.Messages.status-ready");

            m_strChangeLanguageCaption = local.GetName("Forms.MainForm.Messages.change-language-caption");

            m_strChangeLanguage = local.GetName("Forms.MainForm.Messages.change-language");

            m_strFileNotExist = local.GetName("Forms.MainForm.Messages.file-not-exist");

            toolStripStatusLabel.Text = m_strStatusReady;
        }

        #endregion Methods

        #region Events

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutForm(m_local)).ShowDialog();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportFileDialog.FileName = "";

            if (DialogResult.OK == exportFileDialog.ShowDialog())
            {
                ExportToFile(exportFileDialog.FileName, exportFileDialog.FilterIndex);

                toolStripStatusLabel.Text = 
                    m_strExportedTo.Replace("#FILENAME#", exportFileDialog.FileName);

                m_lastExportFile = exportFileDialog.FileName;

                m_lastExportFilterIndex = exportFileDialog.FilterIndex;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_editControl.ImageCount != 0)
            {
                if (DialogResult.Cancel == MessageBox.Show(
                    m_strNewProjectAndClear,
                    m_strNewProject, 
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Question))
                {
                    return;
                }
            }

            this.Text = "ImagesetEditor";

            m_docPath = "";

            m_lastExportFile = "";

            m_editControl.ClearImages();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                OpenFile(openFileDialog.FileName);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "";

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                try
                {
                    IImagesetExport export = new ProjectExport(saveFileDialog.FileName, this);

                    m_editControl.Export(export);

                    toolStripStatusLabel.Text = 
                        m_strSavedTo.Replace("#FILENAME#", saveFileDialog.FileName);

                    AddRecentFiles(saveFileDialog.FileName);
                }
                catch (SystemException exc)
                {
                    MessageBox.Show(exc.Message, m_strSavedError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                m_docPath = saveFileDialog.FileName;
            }
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (m_lastExportFile != "")
            {
                exportLastFileToolStripMenuItem.Text = 
                    m_strExportToLastPosition.Replace("#FILENAME#", m_lastExportFile);
                exportLastFileToolStripMenuItem.Visible = true;
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
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                try
                {
                    IImagesetExport export = new ProjectExport(m_docPath, this);

                    m_editControl.Export(export);

                    toolStripStatusLabel.Text = m_strSavedTo.Replace("#FILENAME#", m_docPath);
                }
                catch (SystemException exc)
                {
                    MessageBox.Show(exc.Message, m_strSavedError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                toolStripStatusLabel.Text = m_strExportedToLastPosition.Replace("#FILENAME#", m_lastExportFile);
            }
        }

        private void languageSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            if (item.Checked == true)
            {
                return;
            }

            MessageBox.Show(m_strChangeLanguage, m_strChangeLanguageCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (sender == defaultToolStripMenuItem)
            {
                SetConfig("Language", "Value", "English");
            }
            else
            {
                SetConfig("Language", "Value", item.Text);
            }

            foreach (ToolStripMenuItem i in languageToolStripMenuItem.DropDownItems)
            {
                i.Checked = false;
            }

            item.Checked = true;
        }

        private void recentFilesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            recentFilesToolStripMenuItem.DropDownItems.Clear();
            foreach (string s in m_recentFiles)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(s);
                item.Click += recentFileToolStripMenuItem_Click;
                recentFilesToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void recentFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = ((ToolStripMenuItem)sender).Text;

            if (File.Exists(fileName))
            {
                OpenFile(fileName);
            }
            else
            {
                for (int i = 0; i != m_recentFiles.Count(); )
                {
                    if (m_recentFiles[i] == fileName)
                    {
                        m_recentFiles.RemoveAt(i);
                    }
                    else
                    {
                        ++i;
                    }
                }

                MessageBox.Show(m_strOpenProjectFailed.Replace("#FILENAME#", fileName), m_strFileNotExist, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo("Localization");

            foreach (FileInfo file in dirInfo.GetFiles("*.xml"))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(file.Name.Split('.').First());

                item.Click += languageSelectToolStripMenuItem_Click;

                languageToolStripMenuItem.DropDownItems.Add(item);
            }

            string lanName = GetConfig("Language", "Value", "English");

            if (lanName == "English")
            {
                defaultToolStripMenuItem.Checked = true;
            }
            else
            {
                foreach (ToolStripMenuItem item in languageToolStripMenuItem.DropDownItems)
                {
                    if (item.Text == lanName)
                    {
                        item.Checked = true;
                        break;
                    }
                }
            }

            int i = 0;

            while(true)
            {
                string path = GetConfig("RecentFiles", "n" + i.ToString(), "");

                if (path == "")
                {
                    break;
                }

                m_recentFiles.Add(path);

                ++i;
            }
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i != m_recentFiles.Count; ++i)
            {
                SetConfig("RecentFiles", "n" + i.ToString(), m_recentFiles[i]);
            }

            m_configDoc.Save("config.xml");
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
            m_recentFiles = new List<string>();

            InitializeComponent();

            try
            {
                m_configDoc = new XmlDocument();
                m_configDoc.Load("config.xml");
            }
            catch
            {
                m_configDoc = new XmlDocument();
                m_configDoc.AppendChild(m_configDoc.CreateElement("Config"));
            }

            if (GetConfig("Language", "Value", "English") != "English")
            {
                try
                {
                    m_local = new Localization(@"localization\" +
                        GetConfig("Language", "Value", "English") + ".xml");

                    UpdateLocalization(m_local);
                }
                catch
                {
                    m_local = null;
                }
            }
            else
            {
                m_local = null;
            }

            m_editControl = new ImagesetEditControl(m_local);

            m_editControl.Dock = DockStyle.Fill;

            this.Controls.Add(m_editControl);

            m_editControl.BringToFront();
        }

        #endregion Constructors
    }
}
