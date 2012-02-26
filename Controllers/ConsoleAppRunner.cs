using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace com.alexleekt.aideNotebook
{
    class ConsoleAppRunner
    {
        public static String Execute(String app, String args)
        {
            // http://stackoverflow.com/questions/206323/how-to-execute-command-line-in-c-get-std-out-results
            Process p = new Process();
            p.StartInfo.FileName = app;
            p.StartInfo.Arguments = args;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            String output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }
    }
}
