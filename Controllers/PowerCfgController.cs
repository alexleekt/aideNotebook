using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

using System.Windows.Forms;
using System.Globalization;

namespace com.alexleekt.aideNotebook
{
    class PowerCfgController
    {
        public enum PowerSource {
          AC, DC
        };

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

        public static String Query(String schemeGuid, String subGuid)
        {
            String args = "-query";
            if (String.IsNullOrEmpty(schemeGuid) == false)
            {
                args = String.Format("{0} {1}", args, schemeGuid);
            }
            if (String.IsNullOrEmpty(subGuid) == false)
            {
                args = String.Format("{0} {1}", args, subGuid);
            }

            return ExecutePowerCfg(args);
        }

        public static int GetMaximumPowerStatePercentage(PowerProfile pwrprf, PowerSource pwsrc)
        {
            if (pwrprf == null)
            {
                pwrprf = ActivePowerProfile;
            }

            String txt = Query(pwrprf.Guid, SUBGROUP_GUID_PROCESSOR_POWER_MANAGEMENT);
            string re1 = ".*?";	// Non-greedy match on filler
            string re2 = "(" + SETTING_GUID_MAXIMUM_PROCESSOR_STATE + ")";	// SQL GUID 1
            string re3 = ".*?";	// Non-greedy match on filler
            string re4 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re5 = ".*?";	// Non-greedy match on filler
            string re6 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re7 = ".*?";	// Non-greedy match on filler
            string re8 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re9 = ".*?";	// Non-greedy match on filler
            string re10 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re11 = ".*?";	// Non-greedy match on filler
            string re12 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re13 = ".*?";	// Non-greedy match on filler
            string re14 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re15 = ".*?";	// Non-greedy match on filler
            string re16 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re17 = ".*?";	// Non-greedy match on filler
            string re18 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re19 = ".*?";	// Non-greedy match on filler
            string re20 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re21 = ".*?";	// Non-greedy match on filler
            string re22 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re23 = ".*?";	// Non-greedy match on filler
            string re24 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re25 = ".*?";	// Non-greedy match on filler
            string re26 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re27 = ".*?";	// Non-greedy match on filler
            string re28 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re29 = ".*?";	// Non-greedy match on filler
            string re30 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re31 = ".*?";	// Non-greedy match on filler
            string re32 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re33 = ".*?";	// Non-greedy match on filler
            string re34 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re35 = ".*?";	// Non-greedy match on filler
            string re36 = "((?:[a-z][a-z]+))";	// AC
            string re37 = ".*?";	// Non-greedy match on filler
            string re38 = "((?:[a-z][a-z]*[0-9]+[a-z0-9]*))";	// AC value
            string re39 = ".*?";	// Non-greedy match on filler
            string re40 = "(?:[a-z][a-z]+)";	// Uninteresting: word
            string re41 = ".*?";	// Non-greedy match on filler
            string re42 = "((?:[a-z][a-z]+))";	// DC
            string re43 = ".*?";	// Non-greedy match on filler
            string re44 = "((?:[a-z][a-z]*[0-9]+[a-z0-9]*))";	// DC Value

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11 + re12 + re13 + re14 + re15 + re16 + re17 + re18 + re19 + re20 + re21 + re22 + re23 + re24 + re25 + re26 + re27 + re28 + re29 + re30 + re31 + re32 + re33 + re34 + re35 + re36 + re37 + re38 + re39 + re40 + re41 + re42 + re43 + re44, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String guidSettingsMaximumPowerState = m.Groups[1].ToString();
                String acKey = m.Groups[2].ToString();
                String acHexValue = m.Groups[3].ToString();
                int acDecValue = Int32.Parse(acHexValue.Substring(1), NumberStyles.HexNumber);
                String dcKey = m.Groups[4].ToString();
                String dcHexValue = m.Groups[5].ToString();
                int dcDecValue = Int32.Parse(dcHexValue.Substring(1), NumberStyles.HexNumber);
                if (pwsrc == PowerSource.AC)
                {
                    return acDecValue;
                }
                if (pwsrc == PowerSource.DC)
                {
                    return dcDecValue;
                }
            }
            return -1;
        }

        public static PowerSource GetCurrentPowerSource()
        {
            bool bAcPower = SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online;

            if (bAcPower)
            {
                return PowerSource.AC;
            }
            else
            {
                return PowerSource.DC;
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
