using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Disintegrate.Configuration;
using Dota2GSI;

namespace Disintegrate.Providers
{
    /// <summary>
    /// Provides presence info for DOTA 2.
    /// </summary>
    public class Dota2PresenceProvider : PresenceProvider
    {
        public override string ProcessName => "dota2";
        public override string AppId => "457208839205289984";
        public override StateFrequency StateFrequency => StateFrequency.FastAsPossible;
        public override Configurator Configurator => new Configuration.Configurators.Dota2Configurator();

        private GameStateListener _gameStateListener;

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
            string state, heroName, detail;

            if (gameState.Hero.Level == -1)
            {
                state = "";
                detail = "Picking a hero";
            }
            else
            {
                state = $"{gameState.Player.Kills}/{gameState.Player.Deaths}/{gameState.Player.Assists}" +
                        $" - {gameState.Player.LastHits}LH/{gameState.Player.Denies}DN";

                heroName = Utilities.Dota2HeroNaming.MakeFriendlyName(gameState.Hero.Name);
                detail = $"{heroName} - Level {gameState.Hero.Level}";
            }

            var info = new PresenceInfo(state, detail)
            {
                LargeImageKey = "logo",
                LargeImageText = "DOTA 2",
            };

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
