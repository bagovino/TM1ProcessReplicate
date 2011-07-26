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
    public partial class OptionsDialog : Form
    {
        public OptionsDialog(List<string> opts)
        {
            InitializeComponent();
            this.AdminHostsText.Text = opts[0];
            this.TM1Username.Text = opts[1];
            this.CAMNamespace.Text = opts[2];
            this.CAMUsername.Text = opts[3];
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OkayButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.TM1AdminHosts = AdminHostsText.Text;
            Properties.Settings.Default.TM1Username = TM1Username.Text;
            Properties.Settings.Default.CAMNamespace = CAMNamespace.Text;
            Properties.Settings.Default.CAMUsername = CAMUsername.Text;
            Properties.Settings.Default.Save();

            this.Close();
            this.Dispose();
        }
    }
}
