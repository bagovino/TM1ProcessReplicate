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
    public partial class TM1LoginDialog : Form
    {
        public string TM1Username { get; private set; }
        public System.Security.SecureString TM1Password { get; private set; }


        public TM1LoginDialog()
        {
            InitializeComponent();
            UsernameText.Text = Properties.Settings.Default.TM1Username;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            this.TM1Username = UsernameText.Text;

            if (PasswordText.Text.Length > 0)
            {
                unsafe
                {
                    fixed (char* pchars = PasswordText.Text.ToCharArray())
                    {
                        this.TM1Password = new System.Security.SecureString(pchars, PasswordText.Text.Length);
                        *pchars = '\0';
                    }
                }
            }
            else
            {
                this.TM1Password = new System.Security.SecureString();
            }

            PasswordText.Text = "";
            DialogResult = DialogResult.OK;
        }
    }
}
