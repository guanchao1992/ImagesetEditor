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

        #endregion Fields

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
                IImagesetExport export = null;

                switch (exportFileDialog.FilterIndex)
                {
                    case 1:
                        export = new TextExport(exportFileDialog.FileName);
                        break;
                    case 2:
                        export = new XmlExport(exportFileDialog.FileName);
                        break;
                    default:
                        break;
                }

                m_editControl.Export(export);
            }

            
        }

        #endregion Events

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

    public class ExportType {
        public string TypeName { get; set; }
    }
}
