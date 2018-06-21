using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disintegrate
{
    public static class Crash
    {
        /// <summary>
        /// Writes a crash log to disk.
        /// </summary>
        public static void WriteLog(string content)
        {
            // Create crash directory if it doesn't exist
            var folder = Path.GetDirectoryName(Application.ExecutablePath);
            Directory.CreateDirectory($"{folder}/crash");

            var fileName = $"crash-{DateTimeOffset.Now.ToUnixTimeSeconds()}.txt";
            File.WriteAllText($"{folder}/crash/{fileName}", content);
        }
    }
}
