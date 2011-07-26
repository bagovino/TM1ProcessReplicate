using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProcessReplicate
{
    public partial class CAMLoginDialog : Form
    {
        public string CAMNamespace { get; private set; }
        public string CAMUsername { get; private set; }
        public System.Security.SecureString CAMPassword { get; private set; }

        public CAMLoginDialog()
        {
            InitializeComponent();
            NamespaceText.Text = Properties.Settings.Default.CAMNamespace;
            UsernameText.Text = Properties.Settings.Default.CAMUsername;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            this.CAMNamespace = this.NamespaceText.Text;
            this.CAMUsername = this.UsernameText.Text;

            if (PasswordText.Text.Length > 0)
            {
                unsafe
                {
                    fixed (char* pchars = PasswordText.Text.ToCharArray())
                    {
                        this.CAMPassword = new System.Security.SecureString(pchars, PasswordText.Text.Length);
                        *pchars = '\0';
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a password.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PasswordText.Text = "";
            DialogResult = DialogResult.OK;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
    }
}
