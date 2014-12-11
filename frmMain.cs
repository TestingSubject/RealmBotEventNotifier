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
using System.Net.Sockets;

namespace EventNotifier
{
    public partial class frmMain : Form
    {
        enum Packets : int
        {
            WELCOME = 0,
            EVENT = 1
        }
        bool loading = true;
        TcpClient _client;

        public frmMain()
        {
            InitializeComponent();
            LoadSettings();
            ShowInfoPage();

            _client = new TcpClient();
            _client.BeginConnect(
                IPAddress.Parse("178.62.186.72"),
                7565, ConnectCallback, _client);

            loading = false;
        }

        void Reconnect()
        {
            _client.Close();
            _client = new TcpClient();
            _client.BeginConnect(
                IPAddress.Parse("178.62.186.72"),
                7565, ConnectCallback, _client);
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                _client.EndConnect(ar);
                NetworkStream ns = _client.GetStream();
                byte[] buffer = new byte[_client.ReceiveBufferSize];
                ns.BeginRead(
                    buffer, 0, buffer.Length,
                    ReadCallback, buffer);
                Log("Connected to RealmBot!");
            }
            catch
            {
                Log("Connect failed, retrying.");
                Reconnect();
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            NetworkStream ns = _client.GetStream();
            byte[] buffer = (byte[])ar.AsyncState;

            try
            {
                int read = ns.EndRead(ar);

                if (read > 0)
                {
                    //Log("\nRead " + read + " bytes.");
                    int id = BitConverter.ToInt16(buffer, 0);
                    string msg = Encoding.ASCII.GetString(buffer, 3, read - 3);
                    msg = msg.Replace("\r", "").Replace("\n", "");

                    if (id == (int)Packets.EVENT)
                    {
                        JEvent data = JsonConvert.DeserializeObject<JEvent>(msg);
                        if (msg.Contains("tokens"))
                        {
                            string tokenData = msg.Split(new string[] { "\"tokens\":" }, StringSplitOptions.None)[1];
                            tokenData = "[" + tokenData.Remove(tokenData.Length - 1) + "]";
                            JArray tokens = JArray.Parse(tokenData);

                            foreach (JObject o in tokens.Children<JObject>())
                                foreach (JProperty p in o.Properties())
                                    data.tokens[p.Name] = p.Value.ToString();
                        }

                        Log(data.key.Replace("stringlist", "")
                            .Replace(".", " ")
                            .Replace("0", "")
                            .Replace("1", "")
                            + " " + data.server + " " + data.realm);

                        Interface.ShowUpdate(data);
                    }
                }

                buffer = new byte[buffer.Length];
                //Log("trying to read more");
                ns.BeginRead(buffer, 0, buffer.Length, ReadCallback, buffer);
            }
            catch 
            { 
                Log("Reading data from RealmBot failed! Disconnecting.");
                Reconnect();
            }
        }

        public void ShowInfoPage()
        {
            this.SuspendLayout();
            WebBrowser webStart = new WebBrowser();
            PNLlist.Controls.Add(webStart);
            webStart.Visible = false;
            webStart.ScriptErrorsSuppressed = true;
            webStart.ScrollBarsEnabled = false;
            webStart.Dock = DockStyle.Fill;
            webStart.BringToFront();

            Button btnClose = new Button();
            this.Controls.Add(btnClose);
            btnClose.Visible = false;
            btnClose.Size = new Size(PNLlist.Width - 6, 35);
            btnClose.Location = new Point(15, PNLlist.Height - 39 + 39);
            btnClose.FlatAppearance.BorderColor = Color.Gray;
            btnClose.BackColor = Color.FromArgb(64, 64, 64);
            btnClose.ForeColor = Color.WhiteSmoke;
            btnClose.Text = "Close";
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.BringToFront();
            this.ResumeLayout();

            btnClose.Click += (e, s) =>
            {
                PNLlist.Controls.Remove(webStart);
                this.Controls.Remove(btnClose);
            };
            webStart.Navigated += (e, s) =>
            {
                if (!webStart.Document.ToString().ToLower().Contains("checking your"))
                {
                    webStart.Show();
                    btnClose.Show();
                }
            };
            webStart.Navigate("http://www.realmbot.xyz/hello.html");
        }

        public void LoadSettings()
        {
            this.SuspendLayout();
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

            numDuration.Value = Settings.Default.duration;
            this.ResumeLayout();
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
            Settings.Default.duration = (int)numDuration.Value;
            Settings.Default.Save();
        }

        private void Log(string text)
        {
            if (!this.Disposing)
                this.Invoke(new MethodInvoker(() =>
                {
                    tbxLog.Text = text + "\n" + tbxLog.Text;
                }));
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Animation.AnimateWindow(this.Handle, 300, Animation.AnimateWindowFlags.AW_HOR_POSITIVE);
            PNLfooter.Refresh();
        }

        private Point _mouseLocation;
        private void PNLcontent_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseLocation = new Point(-e.X, -e.Y);
        }

        private void PNLcontent_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(_mouseLocation.X, _mouseLocation.Y);
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
                this.Width = 640;
            else
                this.Width = 370;
        }
    }
}
