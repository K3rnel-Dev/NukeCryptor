namespace NukeBuilder.Forms
{
    partial class About
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.AuthorLink = new System.Windows.Forms.Label();
            this.GithubLnk = new System.Windows.Forms.Label();
            this.AboutGroupBox = new Guna.UI2.WinForms.Guna2GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AboutProgram = new System.Windows.Forms.Label();
            this.RepositoryLnk = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.AboutGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NukeBuilder.Properties.Resources.SoftwareAuthor;
            this.pictureBox1.Location = new System.Drawing.Point(35, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 137);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // AuthorLink
            // 
            this.AuthorLink.AutoSize = true;
            this.AuthorLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AuthorLink.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AuthorLink.ForeColor = System.Drawing.Color.Cyan;
            this.AuthorLink.Location = new System.Drawing.Point(31, 179);
            this.AuthorLink.Name = "AuthorLink";
            this.AuthorLink.Size = new System.Drawing.Size(150, 20);
            this.AuthorLink.TabIndex = 1;
            this.AuthorLink.Text = "Author: K3rnel-Dev";
            // 
            // GithubLnk
            // 
            this.GithubLnk.AutoSize = true;
            this.GithubLnk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GithubLnk.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GithubLnk.ForeColor = System.Drawing.Color.Cyan;
            this.GithubLnk.Location = new System.Drawing.Point(47, 210);
            this.GithubLnk.Name = "GithubLnk";
            this.GithubLnk.Size = new System.Drawing.Size(115, 20);
            this.GithubLnk.TabIndex = 2;
            this.GithubLnk.Text = "Github (Click) ";
            this.GithubLnk.Click += new System.EventHandler(this.GithubLnk_Click);
            // 
            // AboutGroupBox
            // 
            this.AboutGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.AboutGroupBox.BorderColor = System.Drawing.Color.Cyan;
            this.AboutGroupBox.Controls.Add(this.label1);
            this.AboutGroupBox.Controls.Add(this.AboutProgram);
            this.AboutGroupBox.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.AboutGroupBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(37)))));
            this.AboutGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AboutGroupBox.ForeColor = System.Drawing.Color.White;
            this.AboutGroupBox.Location = new System.Drawing.Point(191, 29);
            this.AboutGroupBox.Name = "AboutGroupBox";
            this.AboutGroupBox.Size = new System.Drawing.Size(342, 181);
            this.AboutGroupBox.TabIndex = 3;
            this.AboutGroupBox.Text = "About ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(337, 40);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nuke Cryptor - A Simple Example of an Executable \r\nFile Protector to Evade Antivi" +
    "rus Software\r\n";
            // 
            // AboutProgram
            // 
            this.AboutProgram.AutoSize = true;
            this.AboutProgram.Font = new System.Drawing.Font("Franklin Gothic Medium", 10.25F);
            this.AboutProgram.ForeColor = System.Drawing.Color.White;
            this.AboutProgram.Location = new System.Drawing.Point(6, 43);
            this.AboutProgram.Name = "AboutProgram";
            this.AboutProgram.Size = new System.Drawing.Size(307, 72);
            this.AboutProgram.TabIndex = 4;
            this.AboutProgram.Text = "Everything is provided for informational purposes \r\nand the author of the softwar" +
    "e  is not responsible \r\nfor the illegal use of this software. Everything is \r\npr" +
    "ovided for  informational purposes.";
            // 
            // RepositoryLnk
            // 
            this.RepositoryLnk.AutoSize = true;
            this.RepositoryLnk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RepositoryLnk.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RepositoryLnk.ForeColor = System.Drawing.Color.Cyan;
            this.RepositoryLnk.Location = new System.Drawing.Point(12, 240);
            this.RepositoryLnk.Name = "RepositoryLnk";
            this.RepositoryLnk.Size = new System.Drawing.Size(199, 20);
            this.RepositoryLnk.TabIndex = 4;
            this.RepositoryLnk.Text = "Github-Repository (Click) ";
            this.RepositoryLnk.Click += new System.EventHandler(this.RepositoryLnk_Click);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(545, 314);
            this.Controls.Add(this.RepositoryLnk);
            this.Controls.Add(this.AboutGroupBox);
            this.Controls.Add(this.GithubLnk);
            this.Controls.Add(this.AuthorLink);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "About";
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.AboutGroupBox.ResumeLayout(false);
            this.AboutGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label AuthorLink;
        private System.Windows.Forms.Label GithubLnk;
        private Guna.UI2.WinForms.Guna2GroupBox AboutGroupBox;
        private System.Windows.Forms.Label AboutProgram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label RepositoryLnk;
    }
}