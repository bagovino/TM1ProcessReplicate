namespace ProcessReplicate
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OperationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.DisconnectMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ClearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ReplicateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SourceServers = new System.Windows.Forms.ComboBox();
            this.DestServers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ToolStripRepStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripReplicateProcess = new System.Windows.Forms.ToolStripProgressBar();
            this.ProcessGridView = new System.Windows.Forms.DataGridView();
            this.ProcessReplicateColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProcessNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessSecurityColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ProcessDestNameColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.OperationsMenuItem,
            this.OptionsMenuItem,
            this.AboutMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(604, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "MainMenu";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(35, 20);
            this.FileMenuItem.Text = "&File";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(92, 22);
            this.ExitMenuItem.Text = "E&xit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // OperationsMenuItem
            // 
            this.OperationsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectMenuItem,
            this.toolStripSeparator1,
            this.DisconnectMenuItem,
            this.toolStripSeparator2,
            this.ClearMenuItem,
            this.toolStripSeparator3,
            this.ReplicateMenuItem});
            this.OperationsMenuItem.Name = "OperationsMenuItem";
            this.OperationsMenuItem.Size = new System.Drawing.Size(72, 20);
            this.OperationsMenuItem.Text = "Ope&rations";
            // 
            // ConnectMenuItem
            // 
            this.ConnectMenuItem.Name = "ConnectMenuItem";
            this.ConnectMenuItem.Size = new System.Drawing.Size(213, 22);
            this.ConnectMenuItem.Text = "&Connect Admin Hosts";
            this.ConnectMenuItem.Click += new System.EventHandler(this.ConnectMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(210, 6);
            // 
            // DisconnectMenuItem
            // 
            this.DisconnectMenuItem.Name = "DisconnectMenuItem";
            this.DisconnectMenuItem.Size = new System.Drawing.Size(213, 22);
            this.DisconnectMenuItem.Text = "&Disconnect All";
            this.DisconnectMenuItem.Click += new System.EventHandler(this.DisconnectMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(210, 6);
            // 
            // ClearMenuItem
            // 
            this.ClearMenuItem.Enabled = false;
            this.ClearMenuItem.Name = "ClearMenuItem";
            this.ClearMenuItem.Size = new System.Drawing.Size(213, 22);
            this.ClearMenuItem.Text = "C&lear Selected Processes";
            this.ClearMenuItem.Click += new System.EventHandler(this.ClearMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(210, 6);
            // 
            // ReplicateMenuItem
            // 
            this.ReplicateMenuItem.Enabled = false;
            this.ReplicateMenuItem.Name = "ReplicateMenuItem";
            this.ReplicateMenuItem.Size = new System.Drawing.Size(213, 22);
            this.ReplicateMenuItem.Text = "&Replicate Selected Processes";
            this.ReplicateMenuItem.Click += new System.EventHandler(this.ReplicateMenuItem_Click);
            // 
            // OptionsMenuItem
            // 
            this.OptionsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetMenuItem});
            this.OptionsMenuItem.Name = "OptionsMenuItem";
            this.OptionsMenuItem.Size = new System.Drawing.Size(56, 20);
            this.OptionsMenuItem.Text = "&Options";
            // 
            // SetMenuItem
            // 
            this.SetMenuItem.Name = "SetMenuItem";
            this.SetMenuItem.Size = new System.Drawing.Size(113, 22);
            this.SetMenuItem.Text = "&Settings";
            this.SetMenuItem.Click += new System.EventHandler(this.SetMenuItem_Click);
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(48, 20);
            this.AboutMenuItem.Text = "&About";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // SourceServers
            // 
            this.SourceServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SourceServers.Enabled = false;
            this.SourceServers.FormattingEnabled = true;
            this.SourceServers.Location = new System.Drawing.Point(96, 35);
            this.SourceServers.Name = "SourceServers";
            this.SourceServers.Size = new System.Drawing.Size(148, 21);
            this.SourceServers.TabIndex = 1;
            this.SourceServers.SelectedIndexChanged += new System.EventHandler(this.SourceServers_SelectedIndexChanged);
            // 
            // DestServers
            // 
            this.DestServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DestServers.Enabled = false;
            this.DestServers.FormattingEnabled = true;
            this.DestServers.Location = new System.Drawing.Point(363, 35);
            this.DestServers.Name = "DestServers";
            this.DestServers.Size = new System.Drawing.Size(148, 21);
            this.DestServers.TabIndex = 3;
            this.DestServers.SelectedIndexChanged += new System.EventHandler(this.DestServers_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Source Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Processes:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Destination Server:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripRepStatusLabel,
            this.ToolStripReplicateProcess});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 411);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(604, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ToolStripRepStatusLabel
            // 
            this.ToolStripRepStatusLabel.Name = "ToolStripRepStatusLabel";
            this.ToolStripRepStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.ToolStripRepStatusLabel.Spring = true;
            this.ToolStripRepStatusLabel.Text = "Ready.";
            // 
            // ToolStripReplicateProcess
            // 
            this.ToolStripReplicateProcess.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ToolStripReplicateProcess.Name = "ToolStripReplicateProcess";
            this.ToolStripReplicateProcess.Size = new System.Drawing.Size(158, 16);
            this.ToolStripReplicateProcess.Step = 3;
            // 
            // ProcessGridView
            // 
            this.ProcessGridView.AllowUserToAddRows = false;
            this.ProcessGridView.AllowUserToDeleteRows = false;
            this.ProcessGridView.AllowUserToResizeRows = false;
            this.ProcessGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.ProcessGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ProcessGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProcessGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProcessReplicateColumn,
            this.ProcessNameColumn,
            this.ProcessSecurityColumn,
            this.ProcessDestNameColumn});
            this.ProcessGridView.Location = new System.Drawing.Point(95, 62);
            this.ProcessGridView.Name = "ProcessGridView";
            this.ProcessGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.ProcessGridView.RowHeadersVisible = false;
            this.ProcessGridView.Size = new System.Drawing.Size(497, 340);
            this.ProcessGridView.TabIndex = 15;
            this.ProcessGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ProcessGridView_CellMouseUp);
            // 
            // ProcessReplicateColumn
            // 
            this.ProcessReplicateColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProcessReplicateColumn.DataPropertyName = "ProcessReplicate";
            this.ProcessReplicateColumn.HeaderText = "";
            this.ProcessReplicateColumn.Name = "ProcessReplicateColumn";
            this.ProcessReplicateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ProcessReplicateColumn.Width = 25;
            // 
            // ProcessNameColumn
            // 
            this.ProcessNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProcessNameColumn.DataPropertyName = "ProcessName";
            this.ProcessNameColumn.FillWeight = 175F;
            this.ProcessNameColumn.HeaderText = "Process Name";
            this.ProcessNameColumn.Name = "ProcessNameColumn";
            this.ProcessNameColumn.ReadOnly = true;
            this.ProcessNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProcessNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProcessSecurityColumn
            // 
            this.ProcessSecurityColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProcessSecurityColumn.DataPropertyName = "ProcessSecurity";
            this.ProcessSecurityColumn.FillWeight = 75F;
            this.ProcessSecurityColumn.HeaderText = "Process Security";
            this.ProcessSecurityColumn.Items.AddRange(new object[] {
            "None",
            "Overwrite",
            "Preserve"});
            this.ProcessSecurityColumn.Name = "ProcessSecurityColumn";
            // 
            // ProcessDestNameColumn
            // 
            this.ProcessDestNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProcessDestNameColumn.DataPropertyName = "ProcessDestName";
            this.ProcessDestNameColumn.HeaderText = "Destination Process";
            this.ProcessDestNameColumn.Items.AddRange(new object[] {
            "Create/Overwrite",
            "Rename"});
            this.ProcessDestNameColumn.Name = "ProcessDestNameColumn";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 433);
            this.Controls.Add(this.ProcessGridView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DestServers);
            this.Controls.Add(this.SourceServers);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TM1 Process Replicate";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ComboBox SourceServers;
        private System.Windows.Forms.ComboBox DestServers;
        private System.Windows.Forms.ToolStripMenuItem ConnectMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OptionsMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ToolStripRepStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar ToolStripReplicateProcess;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem SetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OperationsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisconnectMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ReplicateMenuItem;
        private System.Windows.Forms.DataGridView ProcessGridView;
        private System.Windows.Forms.ToolStripMenuItem ClearMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ProcessReplicateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessNameColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ProcessSecurityColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ProcessDestNameColumn;
    }
}

