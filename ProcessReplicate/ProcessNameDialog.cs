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
    public partial class ProcessNameDialog : Form
    {
        public string ProcessName { get; private set; }
    
        public ProcessNameDialog(string pname)
        {
            InitializeComponent();

            ProcessNameText.Text = pname;
        }

        private void OkayButton_Click(object sender, EventArgs e)
        {
            ProcessName = ProcessNameText.Text;
            this.Close();
        }
    }
}
