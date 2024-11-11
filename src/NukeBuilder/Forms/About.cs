using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NukeBuilder.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void GithubLnk_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/k3rnel-dev");
        }

        private void RepositoryLnk_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/k3rnel-dev/NukeCryptor");
        }
    }
}
