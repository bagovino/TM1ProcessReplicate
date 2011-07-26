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
    public partial class ParameterDialog : Form
    {
        ParameterEditListView ParamsListView = new ParameterEditListView("ParamsListView");
        public List<ProcessParameter> FinalParamValues = new List<ProcessParameter>();

        public ParameterDialog(List<ProcessParameter> ppsinfo, string pident)
        {
            int count = 0;
            string prompt = null;
            string value = null;
            string type = null;

            InitializeComponent();

            this.Text = pident + " Parameters";

            this.Controls.Add(ParamsListView);
            this.ParamsListView.Items.Clear();

            foreach (ProcessParameter ppinfo in ppsinfo)
            {
                ListViewItem i = new ListViewItem(ppinfo.Name);
                i.Name = ppinfo.Name + "_LVI";

                type = ppinfo.Type;
                i.SubItems.Add(type);

                prompt = ppinfo.Prompt;
                i.SubItems.Add(prompt);

                value = ppinfo.Value;
                i.SubItems.Add(value);

                this.ParamsListView.Items.Add(i);
                count = count + 1;
            }   
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void ParameterDialog_Load(object sender, EventArgs e)
        {
            
        }

        private void OkayBtn_Click(object sender, EventArgs e)
        {
            ProcessParameter ppi = null;
            string variable = null;
            string type = null;
            string prompt = null;
            string value = null;

            foreach (ListViewItem lvi in this.ParamsListView.Items)
            {
                variable = lvi.SubItems[0].Text;
                type = lvi.SubItems[1].Text;
                prompt = lvi.SubItems[2].Text;
                value = lvi.SubItems[3].Text;
                
                ppi = new ProcessParameter(variable, prompt, type, value);
                this.FinalParamValues.Add(ppi);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
