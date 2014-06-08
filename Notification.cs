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
using System.Threading;
using System.Resources;
using System.Media;

namespace EventNotifier
{
    public partial class Notification : Form
    {
        public bool inUse = false;

        public Notification()
        {
            InitializeComponent();
        }

        private void Closer_Tick(object sender, EventArgs e)
        {
            Closer.Stop();
            this.Hide();
            this.inUse = false;
            Interface.EndNotification(this);
        }

        public void Notify(string Header, string Location, string Event, string Monster, int Yoffset)
        {
            try
            {
                this.inUse = true;

                if ((Settings.Default.playSoundDeath && Event.Contains("Died")) || (Settings.Default.playSoundSpawn && Event.Contains("spawned")))
                {
                    SoundPlayer player = new SoundPlayer();
                    player.Play();
                }

                this.Invoke((MethodInvoker)delegate
                {
                    contactPIC.Image = (Bitmap)EventNotifier.Properties.Resources.ResourceManager.GetObject(Monster + (Event.Contains("Died") ? "X" : ""));

                    headerLBL.Text = Header;
                    //eventLBL.Text = Event;
                    eventLBL.Text = Location;
                    this.TopMost = true;
                    this.Location = new Point(SystemInformation.WorkingArea.Width - 258, SystemInformation.WorkingArea.Height - 75 - Yoffset);
                    Animation.AnimateWindow(this.Handle, 150, Animation.AnimateWindowFlags.AW_VER_NEGATIVE);
                    this.Show();

                    Closer.Interval = Settings.Default.duration;
                    Closer.Start();
                });
            }
            catch {}
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Load()");
        }

        public void CreateHandle()
        {
            base.CreateHandle();
        }
    }
}
