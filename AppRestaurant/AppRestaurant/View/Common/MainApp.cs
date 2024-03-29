﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Siticone.Desktop.UI.WinForms;
using System.IO;

namespace AppRestaurant.View.Common
{
    public partial class MainApp : Form
    {
        private SiticoneButton currentBtn;
        private Image activeImage;
        private Image disableImage;
        private Image defaultImage;
        private Form currentChildForm;
        private Timer timer;
        private Simulation simulation;
        private Setting setting;
        private Monitoring monitoring;
        private Inventory inventory;
        private Booking booking;

        public MainApp(Simulation simulation, Setting setting, Monitoring monitoring, Inventory inventory, Booking booking)
        {
            this.simulation = simulation;
            this.setting = setting;
            this.monitoring = monitoring;
            this.inventory = inventory;
            this.booking = booking;
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Tick);
            timer.Start();
            defaultImage = (Image)pictureBox2.Image.Clone();
            Text = string.Empty;
            ControlBox = false;
            DoubleBuffered = true;
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
        }

        private void Tick(object sender, EventArgs e)
        {
            string date = DateTime.Now.Date.ToLongDateString();
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;

            string time = "";

            if (hour < 10)
            {
                time += "0" + hour;
            }
            else
            {
                time += hour;
            }
            time += ":";

            if (minute < 10)
            {
                time += "0" + minute;
            }
            else
            {
                time += minute;
            }
            time += ":";

            if (second < 10)
            {
                time += "0" + second;
            }
            else
            {
                time += second;
            }
            siticoneHtmlLabel2.Text = date;
            siticoneHtmlLabel1.Text = time;
        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(63, 81, 181);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(1, 135, 144);
            public static Color color6 = Color.FromArgb(24, 161, 251);
            public static Color color7 = Color.FromArgb(255, 152, 0);
        }

        private void ActivateButton(object senderBtn, Color color, string currentImage)
        {
            if (senderBtn != null)
            {
                string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string exeDir = Path.GetDirectoryName(exePath);
                DirectoryInfo binDir = Directory.GetParent(exeDir);
                binDir = Directory.GetParent(binDir.FullName);

                string currentImagePath = binDir.FullName + "\\Resources\\Icons\\" + currentImage;

                currentBtn = (SiticoneButton)senderBtn;
                activeImage = System.Drawing.Bitmap.FromFile(currentImagePath);

                currentBtn.CheckedState.FillColor = Color.FromArgb(37, 36, 81);
                currentBtn.CheckedState.ForeColor = color;
                currentBtn.CheckedState.CustomBorderColor = color;
                currentBtn.CheckedState.Image = activeImage;
                pictureBox2.Image = activeImage;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                disableImage = (Image)currentBtn.Image.Clone();
                currentBtn.CheckedState.FillColor = Color.FromArgb(31, 30, 68);
                currentBtn.CheckedState.ForeColor = Color.White;
                currentBtn.CheckedState.CustomBorderColor = Color.FromArgb(31, 30, 68);
                currentBtn.CheckedState.Image = disableImage;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(childForm);
            panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            siticoneHtmlLabel6.Text = childForm.Text;
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Maximize(object sender, EventArgs e)
        {
            //WindowState = FormWindowState.Maximized;
            //iconButton10.Visible = true;
        }

        private void Normal(object sender, EventArgs e)
        {
            //WindowState = FormWindowState.Normal;
            //iconButton7.Visible = true;
            //iconButton10.Visible = false;
        }

        private void Minimize(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Simulation(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1, "gripfire_32px_2.png");
            OpenChildForm(simulation);
        }

        private void Monitoring(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2, "heart_with_pulse_32px_2.png");
            OpenChildForm(monitoring);
        }

        private void Setting(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3, "settings_32px_2.png");
            OpenChildForm(setting);
        }

        private void Inventory(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4, "shop_32px_2.png");
            OpenChildForm(inventory);
        }

        private void Booking(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5, "bookmark_32px_2.png");
            OpenChildForm(booking);
        }

        private void Help(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color6, "help_32px_2.png");
            OpenChildForm(new Help());
        }

        private void SignIn(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color7, "login_32px_2.png");
            OpenChildForm(new SignIn());
        }

        private void Home(object sender, EventArgs e)
        {
            currentChildForm.Hide();
            pictureBox2.Image = defaultImage;
            siticoneHtmlLabel6.Text = "Home";
            Reset();
        }

        private void Reset()
        {
            DisableButton();
        }

        // Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
    }
}
