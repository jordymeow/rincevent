using System;
using System.Windows.Forms;

namespace Meow.FR.Rincevent.Core.Gui
{
    static class Program
    {
        [STAThread]
        static void Main(string [] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.GetLength(0) > 0)
                Application.Run(new FrmMain(args[0]));
            else
                Application.Run(new FrmMain());
        }
    }
}