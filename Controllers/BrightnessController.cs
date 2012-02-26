using System;
using System.Collections.Generic;
using System.Text;

namespace com.alexleekt.aideNotebook
{
    class BrightnessController
    {
        public static readonly String APP_NIRCMD = @"Resources\nircmd.exe";
        public static readonly int maxBrightness = 100;
        public static readonly int numBrightnessIncrements = 14;
        public static readonly int valBrightnessIncrement = maxBrightness / numBrightnessIncrements;

        private static String ExecuteNirCmd(String args)
        {
            return ConsoleAppRunner.Execute(APP_NIRCMD, args);
        }

        public static void IncreaseBrightness()
        {
            ExecuteNirCmd("changebrightness " + valBrightnessIncrement);
        }

        public static void DecreaseBrightness()
        {
            ExecuteNirCmd("changebrightness -" + valBrightnessIncrement);
        }
    }
}
