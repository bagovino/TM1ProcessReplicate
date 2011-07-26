namespace ProcessReplicate
{
    partial class OptionsDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.AdminHostsText = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.OkayButton = new System.Windows.Forms.Button();
            this.CAMCredentialsGroup = new System.Windows.Forms.GroupBox();
            this.CAMUsername = new System.Windows.Forms.TextBox();
            this.CAMNamespace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TM1CredentialsGroup = new System.Windows.Forms.GroupBox();
            this.TM1Username = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TM1AdminHostsGroup = new System.Windows.Forms.GroupBox();
            this.CAMCredentialsGroup.SuspendLayout();
            this.TM1CredentialsGroup.SuspendLayout();
            this.TM1AdminHostsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TM1 Admin Hosts:";
            // 
            // AdminHostsText
            // 
            this.AdminHostsText.Location = new System.Drawing.Point(106, 19);
            this.AdminHostsText.Name = "AdminHostsText";
            this.AdminHostsText.Size = new System.Drawing.Size(328, 20);
            this.AdminHostsText.TabIndex = 1;
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(384, 186);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OkayButton
            // 
            this.OkayButton.Location = new System.Drawing.Point(303, 186);
            this.OkayButton.Name = "OkayButton";
            this.OkayButton.Size = new System.Drawing.Size(75, 23);
            this.OkayButton.TabIndex = 3;
            this.OkayButton.Text = "OK";
            this.OkayButton.UseVisualStyleBackColor = true;
            this.OkayButton.Click += new System.EventHandler(this.OkayButton_Click);
            // 
            // CAMCredentialsGroup
            // 
            this.CAMCredentialsGroup.Controls.Add(this.CAMUsername);
            this.CAMCredentialsGroup.Controls.Add(this.CAMNamespace);
            this.CAMCredentialsGroup.Controls.Add(this.label3);
            this.CAMCredentialsGroup.Controls.Add(this.label2);
            this.CAMCredentialsGroup.Location = new System.Drawing.Point(12, 128);
            this.CAMCredentialsGroup.Name = "CAMCredentialsGroup";
            this.CAMCredentialsGroup.Size = new System.Drawing.Size(447, 52);
            this.CAMCredentialsGroup.TabIndex = 4;
            this.CAMCredentialsGroup.TabStop = false;
            this.CAMCredentialsGroup.Text = "Default CAM Credentials";
            // 
            // CAMUsername
            // 
            this.CAMUsername.Location = new System.Drawing.Point(315, 17);
            this.CAMUsername.Name = "CAMUsername";
            this.CAMUsername.Size = new System.Drawing.Size(117, 20);
            this.CAMUsername.TabIndex = 3;
            // 
            // CAMNamespace
            // 
            this.CAMNamespace.Location = new System.Drawing.Point(104, 17);
            this.CAMNamespace.Name = "CAMNamespace";
            this.CAMNamespace.Size = new System.Drawing.Size(117, 20);
            this.CAMNamespace.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "CAM Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "CAM Namespace: ";
            // 
            // TM1CredentialsGroup
            // 
            this.TM1CredentialsGroup.Controls.Add(this.TM1Username);
            this.TM1CredentialsGroup.Controls.Add(this.label4);
            this.TM1CredentialsGroup.Location = new System.Drawing.Point(12, 70);
            this.TM1CredentialsGroup.Name = "TM1CredentialsGroup";
            this.TM1CredentialsGroup.Size = new System.Drawing.Size(447, 52);
            this.TM1CredentialsGroup.TabIndex = 5;
            this.TM1CredentialsGroup.TabStop = false;
            this.TM1CredentialsGroup.Text = "Default TM1 Credentials";
            // 
            // TM1Username
            // 
            this.TM1Username.Location = new System.Drawing.Point(104, 20);
            this.TM1Username.Name = "TM1Username";
            this.TM1Username.Size = new System.Drawing.Size(117, 20);
            this.TM1Username.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "TM1 Username:";
            // 
            // TM1AdminHostsGroup
            // 
            this.TM1AdminHostsGroup.Controls.Add(this.label1);
            this.TM1AdminHostsGroup.Controls.Add(this.AdminHostsText);
            this.TM1AdminHostsGroup.Location = new System.Drawing.Point(12, 12);
            this.TM1AdminHostsGroup.Name = "TM1AdminHostsGroup";
            this.TM1AdminHostsGroup.Size = new System.Drawing.Size(449, 52);
            this.TM1AdminHostsGroup.TabIndex = 6;
            this.TM1AdminHostsGroup.TabStop = false;
            this.TM1AdminHostsGroup.Text = "TM1 Admin Hosts";
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.OkayButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(468, 225);
            this.Controls.Add(this.CAMCredentialsGroup);
            this.Controls.Add(this.TM1AdminHostsGroup);
            this.Controls.Add(this.TM1CredentialsGroup);
            this.Controls.Add(this.OkayButton);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OptionsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.CAMCredentialsGroup.ResumeLayout(false);
            this.CAMCredentialsGroup.PerformLayout();
            this.TM1CredentialsGroup.ResumeLayout(false);
            this.TM1CredentialsGroup.PerformLayout();
            this.TM1AdminHostsGroup.ResumeLayout(false);
            this.TM1AdminHostsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AdminHostsText;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button OkayButton;
        private System.Windows.Forms.GroupBox CAMCredentialsGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CAMUsername;
        private System.Windows.Forms.TextBox CAMNamespace;
        private System.Windows.Forms.GroupBox TM1CredentialsGroup;
        private System.Windows.Forms.TextBox TM1Username;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox TM1AdminHostsGroup;
    }
}