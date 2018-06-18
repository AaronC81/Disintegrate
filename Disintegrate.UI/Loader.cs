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
        public static (List<LoadedProvider> providers, List<LoadError> errors) LoadAllProviders()
        {
            var providers = new List<LoadedProvider>();
            var errors = new List<LoadError>();

            // Determine provider path, creating it if it's been deleted for some reason
            var executablePath = Application.ExecutablePath;
            var appFolder = Path.GetDirectoryName(executablePath);
            var providersFolder = $"{appFolder}\\providers";
            Directory.CreateDirectory(providersFolder);

            foreach (var providerFile in Directory.GetFiles(providersFolder))
            {
                try
                {
                    var assembly = Assembly.LoadFile(providerFile);
                    var loadedProviders = assembly
                        .GetTypes()
                        .Where(t => t.BaseType == typeof(PresenceProvider))
                        .ToList();
                    var loadedConfigurators = assembly
                        .GetTypes()
                        .Where(t => t.BaseType == typeof(Configuration.Configurator))
                        .ToList();

                    if (loadedProviders.Count != 0)
                    {
                        throw new FileLoadException("DLL must contain exactly one PresenceProvider");
                    }
                    if (loadedConfigurators.Count != 0)
                    {
                        throw new FileLoadException("DLL must contain exactly one Configurator");
                    }

                    var theProvider = loadedProviders[0];
                    var theConfigurator = loadedProviders[1];

                    providers.Add(new LoadedProvider(theProvider, theConfigurator));
                }
                catch (Exception e)
                {
                    errors.Add(new LoadError(providerFile, e));
                }
            }

            return (providers, errors);
        }
    }

    /// <summary>
    /// Represents a successfully loaded provider and configurator.
    /// </summary>
    public class LoadedProvider
    {
        public LoadedProvider(Type provider, Type configurator)
        {
            Provider = provider;
            Configurator = configurator;
        }

        public Type Provider { get; }
        public Type Configurator { get; }
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

        public string File { get; }
        public Exception Error { get; }
    }
}
