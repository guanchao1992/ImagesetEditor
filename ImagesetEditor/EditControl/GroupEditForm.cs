using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImagesetEditor
{
    public partial class GroupEditForm : Form
    {
        #region Fields

        ListViewGroup m_editGroup;

        #endregion Fields

        #region Strings

        private string m_strErrorCaption = "Error";

        #endregion Strings

        #region Methods

        public void UpdateLocalization(Localization local)
        {
            label1.Text = local.GetName("GroupEditForm.01");
            label2.Text = local.GetName("GroupEditForm.02");
            okButton.Text = local.GetName("GroupEditForm.03");
            cancelButton.Text = local.GetName("GroupEditForm.04");

            m_strErrorCaption = local.GetName("Control.Messages.error-caption");
        }

        #endregion Methods

        #region Events

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                GroupExpression exp = new GroupExpression(expressionTextBox.Text);
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message, m_strErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            m_editGroup.Header = expressionTextBox.Text.Split(':').First();
            m_editGroup.Tag = expressionTextBox.Text;
            Close();
        }

        private void GroupEditForm_Load(object sender, EventArgs e)
        {
            if (m_editGroup.Tag == null)
                expressionTextBox.Text = m_editGroup.Header;
            else
                expressionTextBox.Text = (string)m_editGroup.Tag;
        }

        #endregion Events

        #region Constructors

        public GroupEditForm(string text, ListViewGroup group, Localization local = null)
        {
            InitializeComponent();

            this.Text = text;

            this.m_editGroup = group;

            if (local != null)
                UpdateLocalization(local);
        }

        #endregion Constructors
    }
}
