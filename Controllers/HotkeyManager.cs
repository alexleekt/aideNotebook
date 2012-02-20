using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace com.alexleekt.aideNotebook
{
    class HotkeyManager
    {
        #region C# Global Hotkeys

        /*
         * ref: http://www.dotnet2themax.com/ShowContent.aspx?ID=103cca7a-0323-47eb-b210-c2bb7075ba78&Lang=cs
         */

        //Windows API functions and constants
        [DllImport("user32", SetLastError = true)]
        private static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);
        [DllImport("user32", SetLastError = true)]
        private static extern int UnregisterHotKey(IntPtr hwnd, int id);
        [DllImport("kernel32", SetLastError = true)]
        private static extern short GlobalAddAtom(string lpString);
        [DllImport("kernel32", SetLastError = true)]
        private static extern short GlobalDeleteAtom(short nAtom);

        public const int MOD_NONE = 0;
        public const int MOD_ALT = 1;
        public const int MOD_CTRL = 2;
        public const int MOD_SHIFT = 4;
        public const int MOD_WIN = 8;

        // the id for the hotkey
        private short hotkeyID;

        // register a global hot key
        public void RegisterGlobalHotKey(string name, IntPtr handle, Keys hotkey, int modifiers)
        {
            try
            {
                // use the GlobalAddAtom API to get a unique ID (as suggested by MSDN docs)

                string atomName = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString("X8") + name;
                hotkeyID = GlobalAddAtom(atomName);
                if (hotkeyID == 0)
                {
                    throw new Exception("Unable to generate unique hotkey ID. Error code: " +
                       Marshal.GetLastWin32Error().ToString());
                }

                // register the hotkey, throw if any error
                if (RegisterHotKey(handle, hotkeyID, modifiers, (int)hotkey) == 0)
                {
                    throw new Exception("Unable to register hotkey. Error code: " + Marshal.GetLastWin32Error().ToString());
                }
            }
            catch (Exception e)
            {
                // clean up if hotkey registration failed
                UnregisterGlobalHotKey(handle);
            }
        }

        // unregister a global hotkey
        public void UnregisterGlobalHotKey(IntPtr handle)
        {
            if (this.hotkeyID != 0)
            {
                UnregisterHotKey(handle, hotkeyID);
                // clean up the atom list
                GlobalDeleteAtom(hotkeyID);
                hotkeyID = 0;
            }
        }
        #endregion
    }
}
