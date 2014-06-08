using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace EventNotifier
{
    public partial class frmMain : Form
    {
        bool loading = true;

        string[] lastEvents = { "['null','null','null','null','null'],", "['null','null','null','null','null'],", "['null','null','null','null','null']," };

        Thread worker;

        public frmMain()
        {
            InitializeComponent();

            chkShowOnDeath.Checked = Settings.Default.showOnDeath;
            chkShowOnSpawn.Checked = Settings.Default.showOnSpawn;
            chkSoundDeath.Checked = Settings.Default.playSoundDeath;
            chkSoundSpawn.Checked = Settings.Default.playSoundSpawn;

            chkSkullShrine.Checked = Settings.Default.showSkullShrine;
            chkCubeGod.Checked = Settings.Default.showCubeGod;
            chkPentaract.Checked = Settings.Default.showPentaract;
            chkGrandSphinx.Checked = Settings.Default.showGrandSphinx;
            chkAvatar.Checked = Settings.Default.showAvatar;
            chkHermit.Checked = Settings.Default.showHermit;
            chkLordoftheLostLands.Checked = Settings.Default.showLordoftheLostLands;
            chkCrystal.Checked = Settings.Default.showCrystal;
            chkGhostShip.Checked = Settings.Default.showGhostShip;
            chkLastLich.Checked = Settings.Default.showLastLich;

            chkAsiaEast.Checked = Settings.Default.showAsiaEast;
            chkAsiaSouthEast.Checked = Settings.Default.showAsiaSouthEast;
            chkEUEast.Checked = Settings.Default.showEUEast;
            chkEUNorth.Checked = Settings.Default.showEUNorth;
            chkEUSouthWest.Checked = Settings.Default.showEUSouthWest;
            chkEUWest.Checked = Settings.Default.showEUWest;
            chkUSEast.Checked = Settings.Default.showUSEast;
            chkUSEast2.Checked = Settings.Default.showUSEast2;
            chkUSEast3.Checked = Settings.Default.showUSEast3;
            chkUSMidWest.Checked = Settings.Default.showUSMidWest;
            chkUSMidWest2.Checked = Settings.Default.showUSMidWest2;
            chkUSNorthWest.Checked = Settings.Default.showUSNorthWest;
            chkUSSouth.Checked = Settings.Default.showUSSouth;
            chkUSSouth2.Checked = Settings.Default.showUSSouth2;
            chkUSSouth3.Checked = Settings.Default.showUSSouth3;
            chkUSSouthWest.Checked = Settings.Default.showUSSouthWest;
            chkUSWest.Checked = Settings.Default.showUSWest;
            chkUSWest2.Checked = Settings.Default.showUSWest2;
            chkUSWest3.Checked = Settings.Default.showUSWest3;

            tbxDuration.Text = Settings.Default.duration.ToString();

            worker = new Thread(Update);
            worker.IsBackground = true;
            worker.Start();

            loading = false;
        }

        private void Update()
        {
            WebClient wc = new WebClient();

            while (!this.Disposing)
            {
                string[] events = wc.DownloadString("http://realmbay.com/data.txt").Split('\n');
                    try
                    {
                        string[] eventData1 = events[0].Replace("[", "").Replace("]", "").Replace("'", "").Split(',').Skip(1).ToArray();

                        if (lastEvents[0] != events[0])
                        {
                            ShowUpdate(eventData1[0], eventData1[1], eventData1[2], eventData1[3], 0);
                            lastEvents[0] = events[0];
                        }
                    }
                    catch { }

                Thread.Sleep(1000);
            }
        }

        private void Option_CheckedChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                Settings.Default.showOnDeath = chkShowOnDeath.Checked;
                Settings.Default.showOnSpawn = chkShowOnSpawn.Checked;
                Settings.Default.playSoundDeath = chkSoundDeath.Checked;
                Settings.Default.playSoundSpawn = chkSoundSpawn.Checked;

                Settings.Default.showSkullShrine = chkSkullShrine.Checked;
                Settings.Default.showCubeGod = chkCubeGod.Checked;
                Settings.Default.showPentaract = chkPentaract.Checked;
                Settings.Default.showGrandSphinx = chkGrandSphinx.Checked;
                Settings.Default.showAvatar = chkAvatar.Checked;
                Settings.Default.showHermit = chkHermit.Checked;
                Settings.Default.showCrystal = chkCrystal.Checked;
                Settings.Default.showGhostShip = chkGhostShip.Checked;
                Settings.Default.showLordoftheLostLands = chkLordoftheLostLands.Checked;
                Settings.Default.showLastLich = chkLastLich.Checked;

                Settings.Default.showAsiaEast = chkAsiaEast.Checked;
                Settings.Default.showAsiaSouthEast = chkAsiaSouthEast.Checked;
                Settings.Default.showEUEast = chkEUEast.Checked;
                Settings.Default.showEUNorth = chkEUNorth.Checked;
                Settings.Default.showEUSouthWest = chkEUSouthWest.Checked;
                Settings.Default.showEUWest = chkEUWest.Checked;
                Settings.Default.showUSEast = chkUSEast.Checked;
                Settings.Default.showUSEast2 = chkUSEast2.Checked;
                Settings.Default.showUSEast3 = chkUSEast3.Checked;
                Settings.Default.showUSMidWest = chkUSMidWest.Checked;
                Settings.Default.showUSMidWest2 = chkUSMidWest2.Checked;
                Settings.Default.showUSNorthWest = chkUSNorthWest.Checked;
                Settings.Default.showUSSouth = chkUSSouth.Checked;
                Settings.Default.showUSSouth2 = chkUSSouth2.Checked;
                Settings.Default.showUSSouth3 = chkUSSouth3.Checked;
                Settings.Default.showUSSouthWest = chkUSSouthWest.Checked;
                Settings.Default.showUSWest = chkUSWest.Checked;
                Settings.Default.showUSWest2 = chkUSWest2.Checked;
                Settings.Default.showUSWest3 = chkUSWest3.Checked;
                

                Settings.Default.Save();
            }
        }

        private void tbxDuration_TextChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                try
                {
                    Settings.Default.duration = int.Parse(tbxDuration.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid input. Please enter an integer only.");
                    Settings.Default.duration = 1500;
                }
                finally
                {
                    Settings.Default.Save();
                }
            }
        }

        public void ShowUpdate(string monster, string info, string server, string realm, int offset)
        {
            if ((info == "Died" && Settings.Default.showOnDeath != true) || (info == "Just Spawned" && Settings.Default.showOnSpawn != true))
                return;

            if ((monster == "Skull_Shrine" && !Settings.Default.showSkullShrine) ||
                (monster == "Cube_God" && !Settings.Default.showCubeGod) ||
                (monster == "Pentaract" && !Settings.Default.showPentaract) ||
                (monster == "Grand_Sphinx" && !Settings.Default.showGrandSphinx) ||
                (monster == "Avatar" && !Settings.Default.showAvatar) ||
                (monster == "Hermit_God" && !Settings.Default.showHermit) ||
                (monster == "Lord_of_the_Lost_Lands" && !Settings.Default.showLordoftheLostLands) ||
                (monster == "Crystal" && !Settings.Default.showCrystal) ||
                (monster == "Ghost_Ship" && !Settings.Default.showGhostShip) ||
                (monster == "Lich" && !Settings.Default.showLastLich))
                return;

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
                return;

            Interface.ShowNotification(monster.Replace('_', ' ').Replace("Lich", "Last Lich"), " " + server + "\n " + realm, "has " + info.Replace("Is there", "been found") + ".\nOn " + server + " in " + realm + ".", monster);
            Console.WriteLine(monster);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Animation.AnimateWindow(this.Handle, 300, Animation.AnimateWindowFlags.AW_HOR_POSITIVE);
            PNLfooter.Refresh();
        }

        public Point mouseLocation;
        private void PNLcontent_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void PNLcontent_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseLocation.X, mouseLocation.Y);
                Location = new Point(mousePos.X, mousePos.Y);
            }
        }

        private void LBL_MouseOver(object sender, MouseEventArgs e)
        {
            Label s = (Label)sender;
            s.BackColor = Color.DimGray;
        }

        private void LBL_MouseLeave(object sender, EventArgs e)
        {
            Label s = (Label)sender;
            s.BackColor = Color.Transparent;
        }

        private void BTNclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BTNminimise_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
