using System;
using System.Windows.Forms;

namespace NukeBuilder
{
    internal static class Runtime
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NukeMain());
        }
    }
}
