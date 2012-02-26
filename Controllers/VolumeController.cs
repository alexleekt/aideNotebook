using System;
using System.Collections.Generic;
using System.Text;

namespace com.alexleekt.aideNotebook
{
    class VolumeController
    {
        public static readonly String APP_NIRCMD = @"Resources\nircmd.exe";
        public static readonly int maxVolume = 65535;
        public static readonly int numVolumeIncrements = 15;
        public static readonly int valVolumeIncrement = maxVolume / numVolumeIncrements;

        private static String ExecuteNirCmd(String args)
        {
            return ConsoleAppRunner.Execute(APP_NIRCMD, args);
        }

        public static void IncreaseVolume()
        {
            ExecuteNirCmd("mutesysvolume 0");
            changeVolume(valVolumeIncrement);
        }

        public static void DecreaseVolume()
        {
            changeVolume(-valVolumeIncrement);
        }

        public static void Mute()
        {
            ExecuteNirCmd("mutesysvolume 1");
        }

        private static void changeVolume(int value)
        {
            ExecuteNirCmd("changesysvolume " + value);
        }
    }
}
