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
using System.Windows.Forms;
using System.Text;

namespace ProcessReplicate
{
    class ParameterEditListView : ListView
    {
        private int X = 0;
        private int Y = 0;
        private string SubItemText;
        private int SubItemSelected = 0;
        private ListViewItem SelectedItem;

        private System.Windows.Forms.TextBox EditBox = new System.Windows.Forms.TextBox();

        public ParameterEditListView(string cname)
        {
            ColumnHeader VariableHeader = new ColumnHeader();
            ColumnHeader TypeHeader = new ColumnHeader();
            ColumnHeader PromptHeader = new ColumnHeader();
            ColumnHeader ValueHeader = new ColumnHeader();

            this.Name = cname;

            VariableHeader.Text = "Parameter Variable";
            VariableHeader.Width = 110;

            TypeHeader.Text = "Parameter Type";
            TypeHeader.Width = 75;

            PromptHeader.Text = "Parameter Prompt";
            PromptHeader.Width = 325;

            ValueHeader.Text = "Parameter Value";
            ValueHeader.Width = 110;

            this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { VariableHeader, TypeHeader, PromptHeader, ValueHeader });
            this.Dock = DockStyle.Top;
            this.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.View = View.Details;
            this.LabelEdit = false;
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.Size = new System.Drawing.Size(592, 230);

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EditListView_MouseDown);
            this.DoubleClick += new System.EventHandler(this.EditListView_DoubleClick);
            this.GridLines = true;
            this.FullRowSelect = true;

            EditBox.Size = new System.Drawing.Size(0, 0);
            EditBox.Location = new System.Drawing.Point(0, 0);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.EditBox });
            EditBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditBox_EditOver);
            EditBox.LostFocus += new System.EventHandler(this.EditBox_FocusOver);
            EditBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            EditBox.BackColor = System.Drawing.Color.White;
            EditBox.BorderStyle = BorderStyle.Fixed3D;
            EditBox.Hide();
            EditBox.Text = "";
        }

        public void EditListView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            SelectedItem = this.GetItemAt(e.X, e.Y);
            X = e.X;
            Y = e.Y;
        }

        private void EditBox_EditOver(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SelectedItem.SubItems[SubItemSelected].Text = EditBox.Text;
                EditBox.Hide();
            }

            if (e.KeyChar == 27)
                EditBox.Hide();
        }

        private void EditBox_FocusOver(object sender, System.EventArgs e)
        {
            SelectedItem.SubItems[SubItemSelected].Text = EditBox.Text;
            EditBox.Hide();
        }

        public void EditListView_DoubleClick(object sender, System.EventArgs e)
        {
            // Find the subitem clicked
            int start = X;
            int spos = 0;
            int epos = 0;
            string colname;
            int rownum;

            for (int i = 0; i < this.Columns.Count; i++)
            {
                epos += this.Columns[i].Width; 
      
                if (start > spos && start < epos)
                {
                    SubItemSelected = i;
                    break;
                }

                spos = epos;     
            }

            SubItemText = SelectedItem.SubItems[SubItemSelected].Text;

            colname = this.Columns[SubItemSelected].Text;
            rownum = SelectedItem.Index;

            if (colname == "Parameter Value" && rownum < this.Items.Count)
            {
                System.Drawing.Rectangle r = new System.Drawing.Rectangle(spos, SelectedItem.Bounds.Y, epos, SelectedItem.Bounds.Bottom);
                EditBox.Size = new System.Drawing.Size(epos - spos, SelectedItem.Bounds.Bottom - SelectedItem.Bounds.Top);
                EditBox.Location = new System.Drawing.Point(spos, SelectedItem.Bounds.Y);
                EditBox.Show();
                EditBox.Text = SubItemText;
                EditBox.SelectAll();
                EditBox.Focus();
            }
        }
    }
}
