using NukeBuilder.Algorithms.FormsHelper;
using NukeBuilder.Forms;
using System;
using System.Windows.Forms;

namespace NukeBuilder
{
    public partial class NukeMain : Form
    {
        private OptionsForm _optionsForm = new OptionsForm();
        
        private About _about = new About();

        private StubSettings _stubSettings;
        
        public NukeMain()
        {
            InitializeComponent();
        }

        #region Open Forms btn events

        private void LoadFormIntoPanel(Form form)
        {
            MainPanel.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;

            MainPanel.Controls.Add(form);
            form.Show();
        }

        private void MainWindowBtn_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(_stubSettings);
        }

        private void OptionsFormBtn_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(_optionsForm);
        }

        private void AboutFormOpen_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(_about);
        }

        private void BuildFormBtn_Click(object sender, EventArgs e)
        {
            MessageBoxEx.Text = _stubSettings.BuildEvent(); MessageBoxEx.Show();
        }

        #endregion

        #region Date Printer Region
        private void InitializeTimer()
        {
            Timer _timer;
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTimePrinter.Text = MainElementor.PrintDateInfo();
        }
        #endregion

        #region Form Load 
        private void NukeMain_Load(object sender, EventArgs e)
        {
            InitializeTimer();

            _stubSettings = new StubSettings(_optionsForm);

            LoadFormIntoPanel(_stubSettings);
        }
        #endregion

    }
}
