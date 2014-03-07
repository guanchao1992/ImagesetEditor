using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagesetEditor
{
    public partial class AboutForm : Form
    {
        #region Methods
        private void UpdateLocalization(Localization local)
        {
            this.Text = local.GetName("Forms.AboutForm.01");
            okButton.Text = local.GetName("Forms.AboutForm.02");
            toProjectWebButton.Text = local.GetName("Forms.AboutForm.03");
        }

        #endregion Methods

        #region Events

        private void AboutForm_Load(object sender, EventArgs e)
        {
            label1.Text = label1.Text.Replace("#VER#", MainForm.GetVersion());
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toProjectWebButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/frimin/ImagesetEditor");
        }

        #endregion Events

        #region Constructors

        public AboutForm(Localization local)
        {
            InitializeComponent();

            if (local != null)
            {
                UpdateLocalization(local);
            }
        }

        #endregion Constructors 
    }
}
