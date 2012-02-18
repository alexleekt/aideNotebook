using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace com.alexleekt.aideNotebook
{
    class PowerCfgController
    {
        public enum PowerSource {
          AC, DC
        };

        public static Int32[] PERCENTAGES = { 60, 80, 99, 100 };

        public static String APP_POWERCFG = "powercfg";
        public static String SUBGROUP_GUID_PROCESSOR_POWER_MANAGEMENT = "54533251-82be-4824-96c1-47b60b740d00";
        public static String SETTING_GUID_MAXIMUM_PROCESSOR_STATE = "bc5038f7-23e0-4960-96da-33abaf5935ec";

        private static PowerProfile _ActivePowerProfile;
        public static PowerProfile ActivePowerProfile
        {
            get
            {
                if (_ActivePowerProfile == null)
                {
                    _ActivePowerProfile = GetActiveScheme();
                }
                return _ActivePowerProfile;
            }
        }

        private static String ExecutePowerCfg(String args)
        {
            // http://stackoverflow.com/questions/206323/how-to-execute-command-line-in-c-get-std-out-results
            Process p = new Process();
            p.StartInfo.FileName = APP_POWERCFG;
            p.StartInfo.Arguments = args;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            String output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }

        public static PowerProfile GetActiveScheme()
        {
            // txt2re.com
            string txt = ExecutePowerCfg("-l");

            string re1 = ".*?";	// Non-greedy match on filler
            string re2 = "([A-Z0-9]{8}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{12})";	// SQL GUID 1
            string re3 = ".*?";	// Non-greedy match on filler
            string re4 = "((?:[a-z][a-z]+))";	// Word 1

            Regex r = new Regex(re1 + re2 + re3 + re4, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String guid = m.Groups[1].ToString();
                String name = m.Groups[2].ToString();
                return new PowerProfile(guid, name);
            }
            else
            {
                throw new Exception("GetActiveScheme(): Unable to extract Profile GUID and Name");
            }
            
        }

        public static void SetMaximumPowerState(PowerProfile pwrprf, PowerSource pwsrc, int percentage) 
        {
            if (pwrprf == null)
            {
                pwrprf = ActivePowerProfile;
            }

            //powercfg -setdcvalueindex [schemeguid] [subgroupguid] [settingguid] [setting]
            String args = String.Format("{0} {1} {2} {3} {4}", new String[] { 
                GetSetPowerSourceValueIndexParam(pwsrc), pwrprf.Guid, SUBGROUP_GUID_PROCESSOR_POWER_MANAGEMENT, SETTING_GUID_MAXIMUM_PROCESSOR_STATE, Convert.ToString(percentage) });
            ExecutePowerCfg(args);
            SetActiveScheme(pwrprf);
        }

        public static void SetActiveScheme(PowerProfile pwrprf)
        {
            String args =  String.Format("-setactive {0}", pwrprf.Guid);
            ExecutePowerCfg(args);
        }

        public static String GetSetPowerSourceValueIndexParam(PowerSource pwsrc)
        {
            switch (pwsrc)
            {
                case PowerSource.AC:
                    return "-SETACVALUEINDEX";
                case PowerSource.DC:
                    return "-SETDCVALUEINDEX";
            }
            return "";
        }

    }
}
