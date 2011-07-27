/* 
    Copyright (C) 2011 Brian Agovino

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
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
