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
            TimerHud.Start();
        }
        
        // Sets the window to be foreground
        [DllImport("User32")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

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
            hkm.RegisterGlobalHotKey(this.Name, this.Handle, Keys.Oemtilde, HotkeyManager.MOD_ALT);
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
                toggleHudVisibility();
            }
        }

        #endregion

        private System.Windows.Forms.Timer _timerHud;
        public System.Windows.Forms.Timer TimerHud
        {
            get
            {
                if (_timerHud == null)
                {
                    _timerHud = new System.Windows.Forms.Timer();
                    _timerHud.Tick += new EventHandler(hudTimer_Tick);
                    _timerHud.Interval = Properties.Settings.Default.HUD_Timeout;
                }
                return _timerHud;
            }
        }

        private System.Windows.Forms.Timer _timerExtraControl;
        public System.Windows.Forms.Timer TimerExtraControl
        {
            get
            {
                if (_timerExtraControl == null)
                {
                    _timerExtraControl = new System.Windows.Forms.Timer();
                    _timerExtraControl.Tick += new EventHandler(timerExtraControl_Tick);
                    _timerExtraControl.Interval = Properties.Settings.Default.ExtraControl_Timeout;
                }
                return _timerExtraControl;
            }
        }

        private void repaintCpuMaxFrequencyPercentageLabels(int percentage, String description)
        {
            this.lblPercentage.Text = String.Format("{0}%", percentage);
            this.lblDescription.Text = description;
        }

        private void toggleHudVisibility()
        {
            if (this.Visible)
            {
                this.Visible = false;
                TimerHud.Stop();
            }
            else
            {
                TimerHud.Stop();
                this.Visible = true;
                SetForegroundWindow(this.Handle);
                TimerHud.Start();
            }
        }

        private void hudTimer_Tick(object sender, EventArgs e)
        {
            toggleHudVisibility();
        }

        void timerExtraControl_Tick(object sender, EventArgs e)
        {
            this.pbExtraControlLogo.Visible = false;
            this.lblExtraControlText.Visible = false;
        }

        private void repaintExtraControlLogoAndText(Image logo, String text)
        {
            TimerExtraControl.Stop();

            this.pbExtraControlLogo.Image = logo;
            this.lblExtraControlText.Text = text;

            this.pbExtraControlLogo.Visible = true;
            this.lblExtraControlText.Visible = true;

            TimerExtraControl.Start();
        }

        private void hudKeyDownHandler(object sender, KeyEventArgs e)
        {
            TimerHud.Stop();
            switch (e.KeyCode)
            {
                case Keys.Space: //change the cpu cap speed
                    KeyValuePair<int, String> nextCpuMaxFrequencyPercentage = FrequencyController.GetNextFrequencyPercentage(PowerCfgController.GetMaximumPowerStatePercentage(null, PowerCfgController.GetCurrentPowerSource()));
                    PowerCfgController.SetMaximumPowerState(null, PowerCfgController.GetCurrentPowerSource(), nextCpuMaxFrequencyPercentage.Key);
                    repaintCpuMaxFrequencyPercentageLabels(nextCpuMaxFrequencyPercentage.Key, nextCpuMaxFrequencyPercentage.Value);
                    break;
                case Keys.Right: //brightness up
                case Keys.OemCloseBrackets:
                    repaintExtraControlLogoAndText(
                        global::com.alexleekt.aideNotebook.Properties.Resources._512px_brightness,
                        "+");
                    BrightnessController.IncreaseBrightness();
                    break;
                case Keys.Left: //brightness down
                case Keys.OemOpenBrackets:
                    repaintExtraControlLogoAndText(
                        global::com.alexleekt.aideNotebook.Properties.Resources._512px_brightness,
                        "-");
                    BrightnessController.DecreaseBrightness();
                    break;
                case Keys.Up: //volume up
                case Keys.Oemplus:
                    repaintExtraControlLogoAndText(
                        global::com.alexleekt.aideNotebook.Properties.Resources._512px_volume,
                        "+");
                    VolumeController.IncreaseVolume();
                    break;
                case Keys.Down: //volume down
                case Keys.OemMinus:
                    repaintExtraControlLogoAndText(
                        global::com.alexleekt.aideNotebook.Properties.Resources._512px_volume,
                        "-");
                    VolumeController.DecreaseVolume();
                    break;
                case Keys.M: //mute
                case Keys.D0:
                    repaintExtraControlLogoAndText(
                        global::com.alexleekt.aideNotebook.Properties.Resources._512px_voume_mute,
                        "Mute");
                    VolumeController.Mute();
                    break;
            }
            TimerHud.Start();
        }

    }
}
    