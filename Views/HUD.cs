using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32;

namespace com.alexleekt.aideNotebook
{
    public partial class HUD : Form
    {
        #region Moveable Borderless Window
        //ref: http://www.codeproject.com/KB/cs/csharpmovewindow.aspx

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
       
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        HotkeyManager hkm;

        PowerCfgController.PowerSource powersource;
        KeyValuePair<int, String> currentCpuMaxFrequencyPercentage;
        
        public HUD()
        {
            hkm = new HotkeyManager();
            InitializeComponent();

            // listen to power mode changes
            powersource = PowerCfgController.GetCurrentPowerSource();
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);

            setupCurrentCpuMaxFrequencyPercentageLabels();
        }

        private void setupCurrentCpuMaxFrequencyPercentageLabels()
        {
            int percentage = PowerCfgController.GetMaximumPowerStatePercentage(null, powersource);
            String description = FrequencyController.GetDescription(percentage);
            currentCpuMaxFrequencyPercentage = new KeyValuePair<int, string>(percentage, description);

            repaintCpuMaxFrequencyPercentageLabels(percentage, description);
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            PowerCfgController.PowerSource newPowersource = PowerCfgController.GetCurrentPowerSource();
            if (powersource != newPowersource)
            {
                powersource = newPowersource;
                setupCurrentCpuMaxFrequencyPercentageLabels();
            }
        }

        #region global hotkey hooking

        private void HUD_Load(object sender, EventArgs e)
        {
            hkm.RegisterGlobalHotKey(this.Name, this.Handle, Keys.Space, HotkeyManager.MOD_SHIFT);
        }

        private void HUD_FormClosed(object sender, FormClosedEventArgs e)
        {
            hkm.UnregisterGlobalHotKey(this.Handle);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // let the base class process the message
            base.WndProc(ref m);

            // if this is a WM_HOTKEY message, notify the parent object
            const int WM_HOTKEY = 0x312;
            if (m.Msg == WM_HOTKEY)
            {
                doSwitchNextCpuMaxFrequencyPercentage();
            }
        }

        #endregion

        private System.Windows.Forms.Timer _timer;
        public System.Windows.Forms.Timer Timer
        {
            get
            {
                if (_timer == null)
                {
                    _timer = new System.Windows.Forms.Timer();
                    _timer.Tick += new EventHandler(hudTimer_Tick);
                    _timer.Interval = Properties.Settings.Default.HUD_Timeout;
                }
                return _timer;
            }
        }

        private void doSwitchNextCpuMaxFrequencyPercentage()
        {
            if (this.Visible)
            {
                KeyValuePair<int, String> nextCpuMaxFrequencyPercentage = FrequencyController.GetNextFrequencyPercentage(PowerCfgController.GetMaximumPowerStatePercentage(null, PowerCfgController.GetCurrentPowerSource()));
                PowerCfgController.SetMaximumPowerState(null, PowerCfgController.GetCurrentPowerSource(), nextCpuMaxFrequencyPercentage.Key);
                
                currentCpuMaxFrequencyPercentage = nextCpuMaxFrequencyPercentage;
            }

            repaintCpuMaxFrequencyPercentageLabels(currentCpuMaxFrequencyPercentage.Key, currentCpuMaxFrequencyPercentage.Value);
        }

        private void repaintCpuMaxFrequencyPercentageLabels(int percentage, String description)
        {
            Timer.Stop();
            this.Visible = true;
            this.lblPercentage.Text = String.Format("{0}%", percentage);
            this.lblDescription.Text = description;
            Timer.Start();
        }

        void hudTimer_Tick(object sender, EventArgs e)
        {
            this.Visible = false;
            Timer.Stop();
        }

        private void lblPercentage_Click(object sender, EventArgs e)
        {
            doSwitchNextCpuMaxFrequencyPercentage();
        }

    }
}
    