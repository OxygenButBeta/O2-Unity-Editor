using System;
using System.Diagnostics;

namespace o2.Runtime.ScriptGeneration.lib.Formatter {
    public static class o2Format {
        public static string RunDotNetFormat(string filePath) {
            try
            {
                var process = new Process();
                process.StartInfo.FileName = "dotnet";
                process.StartInfo.Arguments = "format " + filePath;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return output;
            }
            catch (Exception ex)
            {
                throw new Exception("Error running dotnet format: " + ex.Message);
            }
        }
    }
}