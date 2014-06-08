namespace EventNotifier
{
    partial class Notification
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notification));
            this.panel1 = new System.Windows.Forms.Panel();
            this.contactPIC = new System.Windows.Forms.PictureBox();
            this.eventLBL = new System.Windows.Forms.Label();
            this.headerLBL = new System.Windows.Forms.Label();
            this.Closer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contactPIC)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.contactPIC);
            this.panel1.Controls.Add(this.eventLBL);
            this.panel1.Controls.Add(this.headerLBL);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 75);
            this.panel1.TabIndex = 0;
            // 
            // contactPIC
            // 
            this.contactPIC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(74)))), ((int)(((byte)(74)))));
            this.contactPIC.Image = ((System.Drawing.Image)(resources.GetObject("contactPIC.Image")));
            this.contactPIC.Location = new System.Drawing.Point(12, 12);
            this.contactPIC.Name = "contactPIC";
            this.contactPIC.Size = new System.Drawing.Size(50, 50);
            this.contactPIC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.contactPIC.TabIndex = 8;
            this.contactPIC.TabStop = false;
            this.contactPIC.WaitOnLoad = true;
            // 
            // eventLBL
            // 
            this.eventLBL.AutoSize = true;
            this.eventLBL.BackColor = System.Drawing.Color.Transparent;
            this.eventLBL.Font = new System.Drawing.Font("Verdana", 8.75F, System.Drawing.FontStyle.Bold);
            this.eventLBL.ForeColor = System.Drawing.Color.Gainsboro;
            this.eventLBL.Location = new System.Drawing.Point(71, 31);
            this.eventLBL.Name = "eventLBL";
            this.eventLBL.Size = new System.Drawing.Size(126, 14);
            this.eventLBL.TabIndex = 7;
            this.eventLBL.Text = "has just spawned.";
            // 
            // headerLBL
            // 
            this.headerLBL.AutoSize = true;
            this.headerLBL.BackColor = System.Drawing.Color.Transparent;
            this.headerLBL.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLBL.ForeColor = System.Drawing.Color.LightGreen;
            this.headerLBL.Location = new System.Drawing.Point(71, 8);
            this.headerLBL.Name = "headerLBL";
            this.headerLBL.Size = new System.Drawing.Size(67, 16);
            this.headerLBL.TabIndex = 6;
            this.headerLBL.Text = "Monster";
            // 
            // Closer
            // 
            this.Closer.Interval = 300;
            this.Closer.Tick += new System.EventHandler(this.Closer_Tick);
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 75);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Notification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Notification";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Notification_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contactPIC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label eventLBL;
        private System.Windows.Forms.Label headerLBL;
        private System.Windows.Forms.Timer Closer;
        private System.Windows.Forms.PictureBox contactPIC;
    }
}