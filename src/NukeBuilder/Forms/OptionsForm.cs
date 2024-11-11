using System.Windows.Forms;

namespace NukeBuilder.Forms
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }
        #region Checkbox Checkers
        public bool IsAutorunEnabled;
        public bool IsAntiAnalysisEnabled;
        public bool IsRunAdminEnabled;
        public bool isHideFileEnabled;
        public bool IsMutexControlEnabled;
        public bool IsMeltFileEnabled;


        private void AutorunChk_CheckedChanged(object sender, System.EventArgs e)
        {
            IsAutorunEnabled = AutorunChk.Checked;
        }

        private void AntiAnalysis_Chk_CheckedChanged(object sender, System.EventArgs e)
        {
            IsAntiAnalysisEnabled = AntiAnalysis_Chk.Checked;
        }
        private void HideFile_Chk_CheckedChanged(object sender, System.EventArgs e)
        {
            isHideFileEnabled = HideFile_Chk.Checked;
        }

        private void RunAsAdmin_Chk_CheckedChanged(object sender, System.EventArgs e)
        {
            IsRunAdminEnabled = RunAsAdmin_Chk.Checked;
        }


        private void MutexControl_Chk_CheckedChanged(object sender, System.EventArgs e)
        {
            IsMutexControlEnabled = MutexControl_Chk.Checked;
        }

        private void MeltFile_Chk_CheckedChanged(object sender, System.EventArgs e)
        {
            IsMeltFileEnabled = MeltFile_Chk.Checked;
        }
        #endregion
    }
}
