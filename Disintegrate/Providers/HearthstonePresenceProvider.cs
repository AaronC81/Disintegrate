using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Disintegrate.Providers
{
    public class HearthstonePresenceProvider : PresenceProvider
    {
        // TODO: Quitting during a game doesn't leave a 'complete' tag, so when you relaunch the provider thinks you're in that game still
        // Could be fixed by only reading logs *after* the point at which you started (use timestamps cos line no's could change), or just deleting logs (bad)

        public HearthstonePresenceProvider(PresenceApp app) : base(app) { }

        public override StateFrequency StateFrequency => StateFrequency.TimeControlled;

        private Thread _watcher;
        private PresenceState _currentState = new PresenceState();

        public override void Start()
        {
            Safe(() =>
            {
                var logPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)}\Hearthstone\Logs\Power.log";

                _watcher = new Thread(() =>
                {
                    while (true)
                    {
                        string content;
                        try
                        {
                            // This complicated way of opening a file still works if it's locked
                            using (var file = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                using (var stream = new StreamReader(file))
                                {
                                    content = stream.ReadToEnd();
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Hearthstone log is locked");
                            continue;
                        }
                        LogChanged(content);
                        Thread.Sleep(5000);
                    }
                });

                _watcher.Start();
            });
        }



        public void LogChanged(string content)
        {
            Safe(() =>
            {
                // This regex will match game information such as players and the type of game
                var gameInfoRegex = new Regex(@"^.*GameState.DebugPrintGame\(\) - (.*)\=(.*)$");

                // This regex will match a log line signalling that the game is over
                var gameEndRegex = new Regex(@"^.*GameState.DebugPrintPower\(\) -\s*TAG_CHANGE Entity=GameEntity tag=STATE value=COMPLETE\s*$");

                foreach (var line in content.Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None))
                {
                    var gameInfoMatch = gameInfoRegex.Match(line);
                    if (gameInfoMatch.Success)
                    {
                        var (key, value) = (gameInfoMatch.Groups[1].Value, gameInfoMatch.Groups[2].Value);
                        switch (key)
                        {
                            // Handle stuff
                            case "GameType":
                                _currentState.FieldValues["GameType"] = Utilities.HearthstoneNaming.GameTypeNames[value];
                                break;
                            case "FormatType":
                                _currentState.FieldValues["Format"] = Utilities.HearthstoneNaming.FormatTypeNames[value];
                                break;
                            default:
                                break;
                        }
                        continue;
                    }

                    var gameEndMatch = gameEndRegex.Match(line);
                    if (gameEndMatch.Success)
                    {
                        _currentState.FieldValues.Remove("GameType");
                        _currentState.FieldValues.Remove("Format");
                    }
                }

                _currentState.IconValues["None"] = new ImageBundle("", "");
                _currentState.ImageValue = new ImageBundle("logo", "Hearthstone");

                // If we've collected basic game information, broadcast the state
                if (_currentState.FieldValues.ContainsKey("GameType") && _currentState.FieldValues.ContainsKey("Format"))
                {
                    PushState(_currentState);
                }
                else
                {
                    _currentState.OverrideText = ("In menus", "");
                    PushState(_currentState);
                    _currentState.OverrideText = null;
                }
            });
        }

        public override void Stop()
        {
            Safe(() =>
            {
                _watcher.Abort();
            });
        }
    }
}
