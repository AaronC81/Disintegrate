using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Disintegrate.Configuration;
using Dota2GSI;
using Disintegrate.Customization;

namespace Disintegrate.Providers
{
    /// <summary>
    /// Provides presence info for DOTA 2.
    /// </summary>
    public class Dota2PresenceProvider : PresenceProvider
    {
        public Dota2PresenceProvider(Preferences preferences)
        {
            Preferences = preferences;
        }

        public override string ProcessName => "dota2";
        public override string AppId => "457208839205289984";
        public override StateFrequency StateFrequency => StateFrequency.FastAsPossible;
        public override Configurator Configurator => new Configuration.Configurators.Dota2Configurator();
        public override Customizer Customizer => new Customization.Customizers.Dota2Customizer();

        private GameStateListener _gameStateListener;

        public Preferences Preferences { get; }

        const int NoStateSeconds = 5;
        private Timer _noStateTimer;
        
        public override void Start()
        {
            _gameStateListener = new GameStateListener(4000);
            _gameStateListener.NewGameState += NewGameState;
            _gameStateListener.Start();

            _noStateTimer = new Timer(NoStateSeconds * 1000)
            {
                Enabled = true,
                AutoReset = false
            };
            _noStateTimer.Elapsed += NoGameState;
        }

        /// <summary>
        /// Called when the <see cref="GameStateListener"/> receives a new state.
        /// </summary>
        /// <param name="gameState">The new game state.</param>
        public void NewGameState(GameState gameState)
        {
            string state, detail;

            /// <summary>
            /// Gets the value for a field with a given name.
            /// </summary>
            /// <returns></returns>
            string ValueForField(string fieldName)
            {
                switch (fieldName)
                {
                    case "Kills":
                        return gameState.Player.Kills.ToString();
                    case "Deaths":
                        return gameState.Player.Deaths.ToString();
                    case "Assists":
                        return gameState.Player.Assists.ToString();
                    case "Denies":
                        return gameState.Player.Denies.ToString();
                    case "LastHits":
                        return gameState.Player.LastHits.ToString();
                    case "Team":
                        return gameState.Player.Team.ToString();
                    case "Hero":
                        return Utilities.Dota2HeroNaming.MakeFriendlyName(gameState.Hero.Name);
                    case "Level":
                        return gameState.Hero.Level.ToString();
                    case "Gold":
                        return gameState.Player.Gold.ToString("N0");
                    default:
                        return "ERROR";
                }
            }

            if (gameState.Hero.Level == -1)
            {
                (detail, state) = ("Picking a hero", "");
            }
            else
            {
                (detail, state) = Preferences.FillFieldsByFunction(ValueForField);
            }

            var info = new PresenceInfo(state, detail)
            {
                LargeImageKey = "logo",
                LargeImageText = "DOTA 2",
            };

            if (Preferences.Icon == "Team")
            {
                if (gameState.Player.Team == Dota2GSI.Nodes.PlayerTeam.Dire)
                {
                    info.SmallImageKey = "dire";
                    info.SmallImageText = "Dire";
                }
                else if (gameState.Player.Team == Dota2GSI.Nodes.PlayerTeam.Radiant)
                {
                    info.SmallImageKey = "radiant";
                    info.SmallImageText = "Radiant";
                }
            }

            PushState(info);
            _noStateTimer.Stop();
            _noStateTimer.Start();
        }

        /// <summary>
        /// Called when the <see cref="GameStateListener"/> hasn't received a new state for
        /// <see cref="NoStateSeconds"/> seconds.
        /// </summary>
        public void NoGameState(object o, ElapsedEventArgs e)
        {
            PushState(new PresenceInfo("In menus", "")
            {
                LargeImageKey = "logo",
                LargeImageText = "DOTA 2",
            });
        }

        public override void Stop()
        {
            _gameStateListener.Stop();
        }
    }
}
