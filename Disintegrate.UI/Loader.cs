using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disintegrate.UI
{
    /// <summary>
    /// Responsible for loading provider DLLs.
    /// </summary>
    public static class Loader
    {
        /// <summary>
        /// Returns a list of <see cref="PresenceProvider"/>-based types loaded from the 
        /// DLLs of the 'providers' folder in the app folder.
        /// </summary>
        public static (List<Type> types, List<LoadError> errors) LoadAllProviders()
        {
            var types = new List<Type>();
            var errors = new List<LoadError>();

            var executablePath = Application.ExecutablePath;
            var appFolder = Path.GetDirectoryName(executablePath);
            var providersFolder = $"appFolder\\providers";

            foreach (var providerFile in Directory.GetFiles(providersFolder))
            {
                try
                {
                    var assembly = Assembly.LoadFile(providerFile);
                    var providers = assembly
                        .GetTypes()
                        .Where(t => t.BaseType == typeof(PresenceProvider))
                        .ToList();

                    if (providers.Count == 0)
                    {
                        throw new FileLoadException("DLL contains no PresenceProvider classes");
                    }

                    types.AddRange(providers);
                }
                catch (Exception e)
                {
                    errors.Add(new LoadError(providerFile, e));
                }
            }

            return (types, errors);
        }
    }

    /// <summary>
    /// Represents a provider DLL load error.
    /// </summary>
    public class LoadError
    {
        public LoadError(string file, Exception error)
        {
            File = file;
            Error = error;
        }

        public string File { get; set; }
        public Exception Error { get; set; }
    }
}
