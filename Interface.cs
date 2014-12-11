using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Media;

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

        public static void ShowUpdate(JEvent data)
        {
            if (IsServerFiltered(data.server) || IsEventFiltered(data.key))
                return;

            string monster = data.key.Split('.')[1].Split('.')[0]
                .Replace("Dragon_Head_Leader", "Rock_Dragon")
                .Replace("shtrs_Defense_System", "Avatar");
            if ((data.key.Contains("killed") || data.key.Contains("death"))
                && Settings.Default.showOnDeath)
            {
                if (Settings.Default.playSoundDeath)
                    new SoundPlayer().Play();
                _ShowNotification(
                    monster.Replace("_", " "),
                    "was killed in\n" + data.server + " " + data.realm,
                    false, monster); //FALSE FOR NOW
            }
            else if (data.key.Contains("new") && Settings.Default.showOnSpawn)
            {
                if (Settings.Default.playSoundSpawn)
                    new SoundPlayer().Play();
                _ShowNotification(
                    monster.Replace("_", " "),
                    "has spawned in\n" + data.server + " " + data.realm,
                    false, monster);
            }
            else if (data.key.Contains("many") && Settings.Default.showOnCount)
            {
                if (Settings.Default.soundOnCount)
                    new SoundPlayer().Play();
                _ShowNotification(
                    data.tokens["COUNT"] + " " + monster.Replace("_", " ") + "s",
                    "left in\n" + data.server + " " + data.realm,
                    false, monster);
            }
            else if (data.key.Contains("one") && Settings.Default.showOnCount)
            {
                if (Settings.Default.soundOnCount)
                    new SoundPlayer().Play();
                _ShowNotification(
                    "One " + monster.Replace("_", " "),
                    "left in\n" + data.server + " " + data.realm,
                    false, monster);
            }
        }

        private static bool IsEventFiltered(string e)
        {
            if ((e.Contains("Skull_Shrine") && !Settings.Default.showSkullShrine) ||
                (e.Contains("Cube_God") && !Settings.Default.showCubeGod) ||
                (e.Contains("Pentaract") && !Settings.Default.showPentaract) ||
                (e.Contains("Grand_Sphinx") && !Settings.Default.showGrandSphinx) ||
                (e.Contains("shtrs") && !Settings.Default.showAvatar) ||
                (e.Contains("Hermit_God") && !Settings.Default.showHermit) ||
                (e.Contains("Lord_of_the_Lost_Lands") && !Settings.Default.showLordoftheLostLands) ||
                (e.Contains("Ghost_Ship") && !Settings.Default.showGhostShip) ||
                (e.Contains("Rock_Dragon") && !Settings.Default.showRockDragon) ||
                (e.Contains("Red_Demon") && !Settings.Default.showRedDemon) ||
                (e.Contains("Ent_Ancient") && !Settings.Default.showEntAncient) ||
                (e.Contains("Ghost_King") && !Settings.Default.showGhostKing) ||
                (e.Contains("Cyclops_God") && !Settings.Default.showCyclopsGod) ||
                (e.Contains("Oasis_Giant") && !Settings.Default.showOasisGiant) ||
                (e.Contains("Phoenix_Lord") && !Settings.Default.showPhoenixLord) ||
                (e.Contains("Lich") && !Settings.Default.showLastLich))
                return true;
            return false;
        }

        private static bool IsServerFiltered(string server)
        {
            if ((server == "AsiaEast" && !Settings.Default.showAsiaEast) ||
                (server == "AsiaSouthEast" && !Settings.Default.showAsiaSouthEast) ||
                (server == "EUEast" && !Settings.Default.showEUEast) ||
                (server == "EUNorth" && !Settings.Default.showEUNorth) ||
                (server == "EUSouthWest" && !Settings.Default.showUSSouthWest) ||
                (server == "EUWest" && !Settings.Default.showEUWest) ||
                (server == "USEast" && !Settings.Default.showUSEast) ||
                (server == "USEast2" && !Settings.Default.showUSEast2) ||
                (server == "USEast3" && !Settings.Default.showUSEast3) ||
                (server == "USMidWest" && !Settings.Default.showUSMidWest) ||
                (server == "USMidWest2" && !Settings.Default.showUSMidWest2) ||
                (server == "USSouth" && !Settings.Default.showUSSouth) ||
                (server == "USSouth2" && !Settings.Default.showUSSouth2) ||
                (server == "USSouth3" && !Settings.Default.showUSSouth3) ||
                (server == "USSouthWest" && !Settings.Default.showUSSouthWest) ||
                (server == "USWest" && !Settings.Default.showUSWest) ||
                (server == "USWest2" && !Settings.Default.showUSWest2) ||
                (server == "USWest3" && !Settings.Default.showUSWest3))
                return true;
            return false;
        }

        private static void _ShowNotification(string Header, string Location, bool x, string Monster)
        {
            if (!n1.inUse)
            {
                n1.Notify(Header, Location, Monster, x, offset);
                offset += 75;
            }
            else if (!n2.inUse)
            {
                n2.Notify(Header, Location, Monster, x, offset);
                offset += 75;
            }
            else if (!n3.inUse)
            {
                n3.Notify(Header, Location, Monster, x, offset);
                offset += 75;
            }
        }

        public static void EndNotification(Notification n)
        {
            n1.Invoke(new MethodInvoker(() =>
            {
                if (n1.inUse)
                    n1.Location = new Point(n1.Location.X, n1.Location.Y + 75);
            }));

            n2.Invoke(new MethodInvoker(() =>
            {
                if (n2.inUse)
                    n2.Location = new Point(n2.Location.X, n2.Location.Y + 75);
            }));

            n3.Invoke(new MethodInvoker(() =>
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
