using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventNotifier
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Interface.n1 = new Notification();
            Interface.n2 = new Notification();
            Interface.n3 = new Notification();

            Interface.n1.CreateHandle();
            Interface.n2.CreateHandle();
            Interface.n3.CreateHandle();

            Interface.ShowNotification("Welcome!", "EventNotifier has started.", "EventNotifier has started.", "KrazyShank");

            //Interface.n1.Show();
            //Interface.n2.Show();
            //Interface.n3.Show();

            Application.Run(new frmMain());
        }
    }
}
