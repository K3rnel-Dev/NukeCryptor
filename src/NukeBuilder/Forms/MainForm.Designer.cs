namespace NukeBuilder
{
    partial class NukeMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NukeMain));
            this.GunaBorderless = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.GunaAnimator = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.CloseBox = new Guna.UI2.WinForms.Guna2ControlBox();
            this.MinimazeBox = new Guna.UI2.WinForms.Guna2ControlBox();
            this.SupportPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.AboutFormOpen = new Guna.UI2.WinForms.Guna2Button();
            this.BuildFormBtn = new Guna.UI2.WinForms.Guna2Button();
            this.OptionsFormBtn = new Guna.UI2.WinForms.Guna2Button();
            this.MainWindowBtn = new Guna.UI2.WinForms.Guna2Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.DateTimePrinter = new System.Windows.Forms.Label();
            this.MessageBoxEx = new Guna.UI2.WinForms.Guna2MessageDialog();
            this.SupportPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // GunaBorderless
            // 
            this.GunaBorderless.AnimateWindow = true;
            this.GunaBorderless.ContainerControl = this;
            this.GunaBorderless.DockIndicatorTransparencyValue = 0.6D;
            this.GunaBorderless.ResizeForm = false;
            this.GunaBorderless.ShadowColor = System.Drawing.Color.Aqua;
            this.GunaBorderless.TransparentWhileDrag = true;
            // 
            // GunaAnimator
            // 
            this.GunaAnimator.AnimationType = Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType.AW_HIDE;
            this.GunaAnimator.TargetForm = this;
            // 
            // CloseBox
            // 
            this.CloseBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBox.FillColor = System.Drawing.Color.Aquamarine;
            this.CloseBox.IconColor = System.Drawing.Color.White;
            this.CloseBox.Location = new System.Drawing.Point(647, 12);
            this.CloseBox.Name = "CloseBox";
            this.CloseBox.Size = new System.Drawing.Size(45, 29);
            this.CloseBox.TabIndex = 0;
            // 
            // MinimazeBox
            // 
            this.MinimazeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimazeBox.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.MinimazeBox.FillColor = System.Drawing.Color.Aquamarine;
            this.MinimazeBox.IconColor = System.Drawing.Color.White;
            this.MinimazeBox.Location = new System.Drawing.Point(596, 12);
            this.MinimazeBox.Name = "MinimazeBox";
            this.MinimazeBox.Size = new System.Drawing.Size(45, 29);
            this.MinimazeBox.TabIndex = 1;
            // 
            // SupportPanel
            // 
            this.SupportPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(37)))));
            this.SupportPanel.Controls.Add(this.AboutFormOpen);
            this.SupportPanel.Controls.Add(this.BuildFormBtn);
            this.SupportPanel.Controls.Add(this.OptionsFormBtn);
            this.SupportPanel.Controls.Add(this.MainWindowBtn);
            this.SupportPanel.Controls.Add(this.pictureBox1);
            this.SupportPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SupportPanel.Location = new System.Drawing.Point(0, 0);
            this.SupportPanel.Name = "SupportPanel";
            this.SupportPanel.Size = new System.Drawing.Size(105, 459);
            this.SupportPanel.TabIndex = 2;
            // 
            // AboutFormOpen
            // 
            this.AboutFormOpen.Animated = true;
            this.AboutFormOpen.AnimatedGIF = true;
            this.AboutFormOpen.BackColor = System.Drawing.Color.Transparent;
            this.AboutFormOpen.BorderColor = System.Drawing.Color.White;
            this.AboutFormOpen.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.AboutFormOpen.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.AboutFormOpen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.AboutFormOpen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.AboutFormOpen.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(37)))));
            this.AboutFormOpen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AboutFormOpen.ForeColor = System.Drawing.Color.White;
            this.AboutFormOpen.Image = global::NukeBuilder.Properties.Resources.AboutLogo;
            this.AboutFormOpen.ImageSize = new System.Drawing.Size(35, 35);
            this.AboutFormOpen.Location = new System.Drawing.Point(9, 289);
            this.AboutFormOpen.Name = "AboutFormOpen";
            this.AboutFormOpen.Size = new System.Drawing.Size(90, 55);
            this.AboutFormOpen.TabIndex = 12;
            this.AboutFormOpen.Click += new System.EventHandler(this.AboutFormOpen_Click);
            // 
            // BuildFormBtn
            // 
            this.BuildFormBtn.Animated = true;
            this.BuildFormBtn.AnimatedGIF = true;
            this.BuildFormBtn.BackColor = System.Drawing.Color.Transparent;
            this.BuildFormBtn.BorderColor = System.Drawing.Color.White;
            this.BuildFormBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.BuildFormBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.BuildFormBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.BuildFormBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.BuildFormBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(37)))));
            this.BuildFormBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BuildFormBtn.ForeColor = System.Drawing.Color.White;
            this.BuildFormBtn.Image = global::NukeBuilder.Properties.Resources.PictureBox7__2_;
            this.BuildFormBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.BuildFormBtn.Location = new System.Drawing.Point(8, 362);
            this.BuildFormBtn.Name = "BuildFormBtn";
            this.BuildFormBtn.Size = new System.Drawing.Size(90, 55);
            this.BuildFormBtn.TabIndex = 11;
            this.BuildFormBtn.Click += new System.EventHandler(this.BuildFormBtn_Click);
            // 
            // OptionsFormBtn
            // 
            this.OptionsFormBtn.Animated = true;
            this.OptionsFormBtn.AnimatedGIF = true;
            this.OptionsFormBtn.BackColor = System.Drawing.Color.Transparent;
            this.OptionsFormBtn.BorderColor = System.Drawing.Color.White;
            this.OptionsFormBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.OptionsFormBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.OptionsFormBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.OptionsFormBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.OptionsFormBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(37)))));
            this.OptionsFormBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.OptionsFormBtn.ForeColor = System.Drawing.Color.White;
            this.OptionsFormBtn.Image = global::NukeBuilder.Properties.Resources.PictureBox7__4_;
            this.OptionsFormBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.OptionsFormBtn.Location = new System.Drawing.Point(6, 223);
            this.OptionsFormBtn.Name = "OptionsFormBtn";
            this.OptionsFormBtn.Size = new System.Drawing.Size(90, 55);
            this.OptionsFormBtn.TabIndex = 10;
            this.OptionsFormBtn.Click += new System.EventHandler(this.OptionsFormBtn_Click);
            // 
            // MainWindowBtn
            // 
            this.MainWindowBtn.Animated = true;
            this.MainWindowBtn.AnimatedGIF = true;
            this.MainWindowBtn.BackColor = System.Drawing.Color.Transparent;
            this.MainWindowBtn.BorderColor = System.Drawing.Color.White;
            this.MainWindowBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.MainWindowBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.MainWindowBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.MainWindowBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.MainWindowBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(37)))));
            this.MainWindowBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MainWindowBtn.ForeColor = System.Drawing.Color.White;
            this.MainWindowBtn.Image = global::NukeBuilder.Properties.Resources.PictureBox7__3_;
            this.MainWindowBtn.ImageSize = new System.Drawing.Size(35, 35);
            this.MainWindowBtn.Location = new System.Drawing.Point(6, 141);
            this.MainWindowBtn.Name = "MainWindowBtn";
            this.MainWindowBtn.Size = new System.Drawing.Size(90, 55);
            this.MainWindowBtn.TabIndex = 9;
            this.MainWindowBtn.Click += new System.EventHandler(this.MainWindowBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NukeBuilder.Properties.Resources.MainLogo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(84, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MainPanel
            // 
            this.MainPanel.Location = new System.Drawing.Point(126, 80);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(561, 353);
            this.MainPanel.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(119, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 34);
            this.label1.TabIndex = 4;
            this.label1.Text = "NUKE CRYPTOR";
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Location = new System.Drawing.Point(121, 42);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(200, 10);
            this.guna2Separator1.TabIndex = 5;
            // 
            // DateTimePrinter
            // 
            this.DateTimePrinter.AutoSize = true;
            this.DateTimePrinter.BackColor = System.Drawing.Color.Transparent;
            this.DateTimePrinter.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.DateTimePrinter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.DateTimePrinter.Location = new System.Drawing.Point(123, 55);
            this.DateTimePrinter.Name = "DateTimePrinter";
            this.DateTimePrinter.Size = new System.Drawing.Size(0, 13);
            this.DateTimePrinter.TabIndex = 7;
            // 
            // MessageBoxEx
            // 
            this.MessageBoxEx.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            this.MessageBoxEx.Caption = "Build Info";
            this.MessageBoxEx.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
            this.MessageBoxEx.Parent = this;
            this.MessageBoxEx.Style = Guna.UI2.WinForms.MessageDialogStyle.Light;
            this.MessageBoxEx.Text = null;
            // 
            // NukeMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(704, 459);
            this.Controls.Add(this.DateTimePrinter);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.SupportPanel);
            this.Controls.Add(this.MinimazeBox);
            this.Controls.Add(this.CloseBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NukeMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nuke Cryptor - Main";
            this.Load += new System.EventHandler(this.NukeMain_Load);
            this.SupportPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm GunaBorderless;
        private Guna.UI2.WinForms.Guna2AnimateWindow GunaAnimator;
        private Guna.UI2.WinForms.Guna2ControlBox MinimazeBox;
        private Guna.UI2.WinForms.Guna2ControlBox CloseBox;
        private Guna.UI2.WinForms.Guna2Panel SupportPanel;
        private Guna.UI2.WinForms.Guna2Panel MainPanel;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.Label DateTimePrinter;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Button MainWindowBtn;
        private Guna.UI2.WinForms.Guna2Button BuildFormBtn;
        private Guna.UI2.WinForms.Guna2Button OptionsFormBtn;
        private Guna.UI2.WinForms.Guna2MessageDialog MessageBoxEx;
        private Guna.UI2.WinForms.Guna2Button AboutFormOpen;
    }
}

