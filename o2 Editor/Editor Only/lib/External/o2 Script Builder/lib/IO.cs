using System;
using System.IO;
using System.Text;
using o2.Runtime.ScriptGeneration.lib.Formatter;

namespace o2.Runtime.ScriptGeneration.lib {
    public static class IO {
        public static void SaveToFile(this ICSBuilder builder, string filePath) {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Unsupported file path.");

            File.WriteAllText(filePath, builder.Build());
            o2Format.RunDotNetFormat(filePath);
        }
    }
}