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
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace ProcessReplicate
{
    public partial class MainWindow : Form
    {
        private TM1App app;
        private int securitytype = 0;
        private BindingList<DataHelper> proclist = new BindingList<DataHelper>();

        private static readonly ILog LOG = LogManager.GetLogger("ProcessReplicate");

        public MainWindow()
        {
            InitializeComponent();

            XmlConfigurator.Configure(new System.IO.FileInfo("loggers.xml"));
        }

        private class DataHelper
        {
            public int? ProcessReplicate { get; set; }
            public string ProcessName { get; set; }
            public string ProcessSecurity { get; set; }
            public string ProcessDestName { get; set; }

            public DataHelper(int r, string p, string s, string d)
            {
                this.ProcessReplicate = r;
                this.ProcessName = p;
                this.ProcessSecurity = s;
                this.ProcessDestName = d;
            }
        }
        
        protected override void  OnFormClosing(FormClosingEventArgs e)
        {
 	        base.OnFormClosing(e);
            
            if (app != null)
            {
                app.Dispose();
            }

            Application.Exit();
        }

        private void SetMenuItem_Click(object sender, EventArgs e)
        {
            List<string> opts = new List<string>();

            opts.Add(Properties.Settings.Default.TM1AdminHosts);
            opts.Add(Properties.Settings.Default.TM1Username);
            opts.Add(Properties.Settings.Default.CAMNamespace);
            opts.Add(Properties.Settings.Default.CAMUsername);

            OptionsDialog dlg = new OptionsDialog(opts);
            dlg.ShowDialog();
        }   

        private void ConnectMenuItem_Click(object sender, EventArgs e)
        {
            app = new TM1App(Properties.Settings.Default.TM1AdminHosts, "");
            app.SetVisualControls(this.statusStrip1, this.ToolStripRepStatusLabel, this.ToolStripReplicateProcess.ProgressBar);
            SourceServers.Items.Clear();
            SourceServers.Items.AddRange(app.GetServers().ToArray());
            SourceServers.Enabled = true;
        }

        private void DisconnectMenuItem_Click(object sender, EventArgs e)
        {
            if (app != null)
            {
                app.LogoutFromAllServers();
            }

            ToolStripRepStatusLabel.Text = "Ready.";
            statusStrip1.Refresh();

            SourceServers.SelectedIndex = -1;
            DestServers.SelectedIndex = -1;
            DestServers.Enabled = false;
            ToolStripReplicateProcess.ProgressBar.Value = 0;
            ReplicateMenuItem.Enabled = false;
            ClearMenuItem.Enabled = false;
            ProcessGridView.Rows.Clear();
        }

        

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (app != null)
            {
                app.Dispose();
            }

            Application.Exit();
        }

        private void SourceServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string serv;

            if (SourceServers.SelectedIndex == -1)
            {
                return;
            }

            serv = SourceServers.SelectedItem.ToString();

            if (!this.DoServerLogin(serv))
            {
                SourceServers.SelectedIndex = -1;
                ProcessGridView.Rows.Clear();
                DestServers.Enabled = false;
                MessageBox.Show("Error while attempting to log into " + serv + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClearMenuItem.Enabled = true;

            ToolStripRepStatusLabel.Text = "Retreiving processes...";
            statusStrip1.Refresh();

            List <string> procs = app.GetProcessesOnServer(serv);

            proclist.Clear();

            foreach (string proc in procs)
            {
                proclist.Add(new DataHelper(0, proc, "None", "Create/Overwrite"));
            }

            ProcessGridView.DataSource = proclist;
            ToolStripRepStatusLabel.Text = "Ready.";
            statusStrip1.Refresh();

           DestServers.Items.Clear();

            for (int i = 0; i < SourceServers.Items.Count; i++)
            {
                if (!SourceServers.Items[i].Equals(SourceServers.SelectedItem))
                {
                    DestServers.Items.Add(SourceServers.Items[i]);
                }
            }

            DestServers.Enabled = true;
            MessageBox.Show("Successfully logged into " + serv + ".", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ProcessGridView.Focus();
        }

        private void DestServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string serv;

            if (DestServers.SelectedIndex == -1)
            {
                return;
            }

            serv = DestServers.SelectedItem.ToString();

            if (!this.DoServerLogin(serv))
            {
                MessageBox.Show("Error while attempting to log into " + serv + ".", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ReplicateMenuItem.Enabled = false;
            }
            else
            {
                MessageBox.Show("Successfully logged into " + serv + ".", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReplicateMenuItem.Enabled = true;
            }

            ProcessGridView.Focus();
        }

        private void ReplicateMenuItem_Click(object sender, EventArgs e)
        {
            string source;
            string dest = "";
            string proc;
            StringBuilder proclist = new StringBuilder();
            int destname = 0;

            try
            {

                if (MessageBox.Show("Begin process replication?", "Replicate Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                // Force an end to the edit in case the user didn't click off of any control within the DataGridView
                ProcessGridView.EndEdit();

                foreach (DataGridViewRow row in ProcessGridView.Rows)
                {
                    if (Convert.ToInt32(row.Cells[0].Value) == 1)
                    {
                        source = SourceServers.SelectedItem.ToString();
                        dest = DestServers.SelectedItem.ToString();
                        proc = row.Cells[1].Value.ToString();

                        switch (row.Cells[2].Value.ToString())
                        {
                            case "None":
                                this.securitytype = 0;
                                break;
                            case "Overwrite":
                                this.securitytype = 1;
                                break;
                            case "Preserve":
                                this.securitytype = 2;
                                break;
                            default:
                                this.securitytype = 0;
                                break;
                        }

                        switch (row.Cells[3].Value.ToString())
                        {
                            case "Rename":
                                destname = 1;
                                break;
                            case "Create/Overwrite":
                            default:
                                destname = 0;
                                break;
                        }

                        if (!app.ReplicateProcess(source, dest, proc, this.securitytype, destname))
                        {
                            ToolStripRepStatusLabel.Text = "Replication error.";
                            statusStrip1.Refresh();

                            MessageBox.Show("Error while attempting to replicate process " + proc + " to " + dest + ".", "Replication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            proclist.Append(proc + ", ");
                        }
                    }
                }

                proclist = proclist.Remove(proclist.Length - 2, 2);

                ToolStripRepStatusLabel.Text = "Replication complete.";
                statusStrip1.Refresh();
                MessageBox.Show(proclist.ToString() + " successfully replicated to " + dest + ".", "Replication Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       private  void ProcessGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            string version =  Assembly.GetExecutingAssembly().GetName().Version.ToString(); 

            MessageBox.Show("ProcessReplicate v" + version + "\nWritten by Brian Agovino, 2011", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool DoServerLogin(string serv)
        {
            TM1LoginDialog tlg;
            CAMLoginDialog clg;

            int authentype;

            authentype = app.GetAuthenticationType(serv);

            switch (authentype)
            {
                case 1:
                    if (!app.CheckServerLogin(serv))
                    {
                        tlg = new TM1LoginDialog();
                        tlg.ShowDialog();

                        if (tlg.DialogResult == DialogResult.OK)
                        {
                            app.SetTM1LoginCredentials(tlg.TM1Username, tlg.TM1Password);

                            ToolStripRepStatusLabel.Text = "Logging into server...";
                            statusStrip1.Refresh();

                            if (!app.LoginToServer(serv, authentype))
                            {
                                return false;
                            }

                            ToolStripRepStatusLabel.Text = "Ready.";
                            statusStrip1.Refresh();

                            return true;
                        }
                        else
                        {
                            tlg.Dispose();
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                case 4:
                    if (!app.CheckServerLogin(serv))
                    {
                        clg = new CAMLoginDialog();
                        clg.ShowDialog();

                        if (clg.DialogResult == DialogResult.OK)
                        {
                            app.SetCAMLoginCredentials(clg.CAMNamespace, clg.CAMUsername, clg.CAMPassword);

                            ToolStripRepStatusLabel.Text = "Logging into server...";
                            statusStrip1.Refresh();

                            if (!app.LoginToServer(serv, authentype))
                            {
                                return false;
                            }

                            ToolStripRepStatusLabel.Text = "Ready.";
                            statusStrip1.Refresh();

                            return true;
                        }
                        else
                        {
                            clg.Dispose();
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                default:
                    return false;
            }
        }

        private void ClearMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ProcessGridView.Rows)
            {
                row.Cells[0].Value = 0;
                row.Cells[2].Value = "None";
            }
        }

    }
}
