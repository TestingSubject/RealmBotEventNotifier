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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Media;

namespace EventNotifier
{
    public partial class frmMain : Form
    {
        bool loading = true;

        List<double> knownEvents = new List<double>();

        Thread _worker;

        public frmMain()
        {
            InitializeComponent();

            chkShowOnDeath.Checked = Settings.Default.showOnDeath;
            chkShowOnSpawn.Checked = Settings.Default.showOnSpawn;
            chkShowOnCount.Checked = Settings.Default.showOnCount;
            chkSoundDeath.Checked = Settings.Default.playSoundDeath;
            chkSoundSpawn.Checked = Settings.Default.playSoundSpawn;
            chkShowOnCount.Checked = Settings.Default.soundOnCount;

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
            chkRedDemon.Checked = Settings.Default.showRedDemon;
            chkEntAncient.Checked = Settings.Default.showEntAncient;
            chkGhostKing.Checked = Settings.Default.showGhostKing;
            chkCyclopsGod.Checked = Settings.Default.showCyclopsGod;
            chkOasisGiant.Checked = Settings.Default.showOasisGiant;
            chkPhoenixLord.Checked = Settings.Default.showPhoenixLord;
            chkRockDragon.Checked = Settings.Default.showRockDragon;

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

            _worker = new Thread(Fetch);
            _worker.IsBackground = true;
            _worker.Start();

            loading = false;
        }

        private void Fetch()
        {
            Log("Welcome!");
            WebClient wc = new WebClient();

            while (!this.Disposing)
            {
                try
                {
                    string[] events = wc.DownloadString("http://realmbot.xyz/data.json").Split('\n');

                    foreach (string e in events)
                    {
                        if (e.Length < 10)
                            continue;

                        JEvent data = JsonConvert.DeserializeObject<JEvent>(e);
                        if (e.Contains("tokens"))
                        {
                            string tokenData = e.Split(new string[] { "\"tokens\":" }, StringSplitOptions.None)[1];
                            tokenData = "[" + tokenData.Remove(tokenData.Length - 1) + "]";
                            JArray tokens = JArray.Parse(tokenData);

                            foreach (JObject o in tokens.Children<JObject>())
                                foreach (JProperty p in o.Properties())
                                    data.tokens[p.Name] = p.Value.ToString();
                        }

                        if (!knownEvents.Contains(data.time))
                        {
                            knownEvents.Add(data.time);
                            ShowUpdate(data);
                        }
                    }
                }
                catch
                {
                    Interface.ShowNotification("Connection failed!", "We couldn't connect to\nRealmBot. Waiting a bit...", false, "Kronks");
                    Thread.Sleep(15000);
                }

                Thread.Sleep(1000);
            }
        }

        private void Option_CheckedChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                Settings.Default.showOnDeath = chkShowOnDeath.Checked;
                Settings.Default.showOnSpawn = chkShowOnSpawn.Checked;
                Settings.Default.showOnCount = chkShowOnCount.Checked;
                Settings.Default.playSoundDeath = chkSoundDeath.Checked;
                Settings.Default.playSoundSpawn = chkSoundSpawn.Checked;
                Settings.Default.soundOnCount = chkSoundCount.Checked;

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
                Settings.Default.showRockDragon = chkRockDragon.Checked;
                Settings.Default.showRedDemon = chkRedDemon.Checked;
                Settings.Default.showCyclopsGod = chkCyclopsGod.Checked;
                Settings.Default.showGhostKing = chkGhostKing.Checked;
                Settings.Default.showOasisGiant = chkOasisGiant.Checked;
                Settings.Default.showEntAncient = chkEntAncient.Checked;
                Settings.Default.showPhoenixLord = chkPhoenixLord.Checked;

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
                    Settings.Default.duration = 3500;
                }
                finally
                {
                    Settings.Default.Save();
                }
            }
        }

        private void ShowUpdate(JEvent data)
        {
            if (IsServerFiltered(data.server) || IsEventFiltered(data.key))
                return;

            Log(data.key.Replace("stringlist", "").Replace(".", " ") + " : " + data.server + " - " + data.realm);

            string monster = data.key.Split('.')[1].Split('.')[0]
                .Replace("Dragon_Head_Leader", "Rock_Dragon")
                .Replace("shtrs_Defense-System", "Avatar");
            if ((data.key.Contains("killed") || data.key.Contains("death"))
                && Settings.Default.showOnDeath)
            {
                if (Settings.Default.playSoundDeath)
                    new SoundPlayer().Play();
                Interface.ShowNotification(
                    monster.Replace("_", " "),
                    "was killed in\n" + data.server + " " + data.realm,
                    false, monster); //FALSE FOR NOW
            }
            else if (data.key.Contains("new") && Settings.Default.showOnSpawn)
            {
                if (Settings.Default.playSoundSpawn)
                    new SoundPlayer().Play();
                Interface.ShowNotification(
                    monster.Replace("_", " "),
                    "has spawned in\n" + data.server + " " + data.realm,
                    false, monster);
            }
            else if (data.key.Contains("many") && Settings.Default.showOnCount)
            {
                if (Settings.Default.soundOnCount)
                    new SoundPlayer().Play();
                Interface.ShowNotification(
                    data.tokens["COUNT"] + " " + monster.Replace("_", " ") + "s",
                    "left in\n" + data.server + " " + data.realm,
                    false, monster);
            }
            else if (data.key.Contains("one") && Settings.Default.showOnCount)
            {
                if (Settings.Default.soundOnCount)
                    new SoundPlayer().Play();
                Interface.ShowNotification(
                    "One " + monster.Replace("_", " "),
                    "left in\n" + data.server + " " + data.realm,
                    false, monster);
            }
        }

        private void Log(string text)
        {
            if (!this.Disposing)
                this.Invoke(new MethodInvoker(() =>
                {
                    tbxLog.Text = "[" 
                        + DateTime.Now.ToShortTimeString() + "] "
                        + text + "\n" + tbxLog.Text;
                }));
        }

        private bool IsEventFiltered(string e)
        {
            if ((e == "Skull_Shrine" && !Settings.Default.showSkullShrine) ||
                (e == "Cube_God" && !Settings.Default.showCubeGod) ||
                (e == "Pentaract" && !Settings.Default.showPentaract) ||
                (e == "Grand_Sphinx" && !Settings.Default.showGrandSphinx) ||
                (e == "Avatar" && !Settings.Default.showAvatar) ||
                (e == "Hermit_God" && !Settings.Default.showHermit) ||
                (e == "Lord_of_the_Lost_Lands" && !Settings.Default.showLordoftheLostLands) ||
                (e == "Ghost_Ship" && !Settings.Default.showGhostShip) ||
                (e == "Rock_Dragon" && !Settings.Default.showRockDragon) ||
                (e == "Red_Demon" && !Settings.Default.showRedDemon) ||
                (e == "Ent_Ancient" && !Settings.Default.showEntAncient) ||
                (e == "Ghost_King" && !Settings.Default.showGhostKing) ||
                (e == "Cyclops_God" && !Settings.Default.showCyclopsGod) ||
                (e == "Oasis_Giant" && !Settings.Default.showOasisGiant) ||
                (e == "Phoenix_Lord" && !Settings.Default.showPhoenixLord) ||
                (e == "Lich" && !Settings.Default.showLastLich))
                return true;
            return false;
        }

        private bool IsServerFiltered(string server)
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

        private void btnToggleLog_Click(object sender, EventArgs e)
        {
            if (this.Width < 450)
                this.Width = 700;
            else
                this.Width = 430;
        }
    }
}
