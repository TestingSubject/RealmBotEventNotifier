using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace EventNotifier
{
    public static class Interface
    {
        [DllImport("USER32.DLL")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("USER32.DLL")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int smIndex);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static IntPtr Parent = IntPtr.Zero;

        public static int offset = 0;
        public static Notification n1;
        public static Notification n2;
        public static Notification n3;

        public static void ShowNotification(string Header, string Location, string Event, string Monster)
        {
            if (n1.inUse == false)
                n1.Notify(Header, Location, Event, Monster, offset);
            else if (n2.inUse == false)
                n2.Notify(Header, Location, Event, Monster, offset);
            else if (n3.inUse == false)
                n3.Notify(Header, Location, Event, Monster, offset);
            //else
                //MessageBox.Show("all in use");

            offset += 75;
        }

        public static void EndNotification(Notification n)
        {
            n1.Invoke(new MethodInvoker(delegate()
            {
                if (n1.inUse)
                    n1.Location = new Point(n1.Location.X, n1.Location.Y + 75);
            }));

            n2.Invoke(new MethodInvoker(delegate()
            {
                if (n2.inUse)
                    n2.Location = new Point(n2.Location.X, n2.Location.Y + 75);
            }));

            n3.Invoke(new MethodInvoker(delegate()
            {
                if (n3.inUse)
                    n3.Location = new Point(n3.Location.X, n3.Location.Y + 75);
            }));
            offset -= 75;
        }

        public static int Width()
        {
            RECT rct;
            GetWindowRect(Parent, out rct);
            return rct.Right - rct.Left - 16;
        }

        public static int Height()
        {
            RECT rct;
            GetWindowRect(Parent, out rct);
            return rct.Bottom - rct.Top - 38;
        }
    }
}
