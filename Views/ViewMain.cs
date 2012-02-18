using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Threading;
using System.Runtime.InteropServices;

namespace com.alexleekt.aideNotebook
{
    public partial class ViewMain : Form
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

        
        public ViewMain()
        {
            InitializeComponent();
            PowerCfgController.SetMaximumPowerState(null, PowerCfgController.PowerSource.AC, 100);
            PowerCfgController.SetMaximumPowerState(null, PowerCfgController.PowerSource.DC, 60);
            hkm = new HotkeyManager();
        }

        private void redrawCpuSpeed()
        {
            ManagementObject Mo = new ManagementObject("Win32_Processor.DeviceID='CPU0'");
            uint sp = (uint)(Mo["CurrentClockSpeed"]);
            Mo.Dispose();
            this.label2.Text = sp + "MHz";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            redrawCpuSpeed();
        }

        HotkeyManager hkm;

        private void Form1_Load(object sender, EventArgs e)
        {
            hkm.RegisterGlobalHotKey(this.Name, this.Handle, Keys.Space, HotkeyManager.MOD_SHIFT);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
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
                this.label1.Text = "detected";
            }
        }

    }
}
