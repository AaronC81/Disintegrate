using CSGSI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disintegrate;
using System.Timers;

namespace GlobalOffensiveProvider
{
    public class GlobalOffensivePresenceProvider : PresenceProvider
    {
        public override string ProcessName => "csgo";
        public override string AppId => "457492341318746113";
        public override StateFrequency StateFrequency => StateFrequency.FastAsPossible;

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
            // The game broadcasts a '-1' state when started
            if (gameState.Player.MatchStats.Deaths == -1)
            {
                return;
            }

            // gameState.Player refers to the player we're watching, so if we're dead, 
            // it's the person we're spectating
            // If the player isn't us, reset the timer but don't update any presence info
            if (gameState.Player.SteamID != gameState.Provider.SteamID)
            {
                _noStateTimer.Stop();
                _noStateTimer.Start();

                return;
            }

            var state = $"{gameState.Player.MatchStats.Kills}/{gameState.Player.MatchStats.Assists}/{gameState.Player.MatchStats.Deaths}" +
                        $" - {gameState.Player.MatchStats.MVPs} MVPs";

            var detail = $"Score: T {gameState.Map.TeamT.Score} - {gameState.Map.TeamCT.Score} CT";

            var activity = gameState.Player.Activity;
            
            var info = new PresenceInfo(state, detail)
            {
                LargeImageKey = "logo",
                LargeImageText = "Counter-Strike: Global Offensive"
            };

            if (gameState.Player.Team == CSGSI.Nodes.PlayerTeam.T)
            {
                info.SmallImageKey = "t";
                info.SmallImageText = "Terrorists";
            }
            else if (gameState.Player.Team == CSGSI.Nodes.PlayerTeam.CT)
            {
                info.SmallImageKey = "ct";
                info.SmallImageText = "Counter-Terrorists";
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
            var info = new PresenceInfo("In menus", "")
            {
                LargeImageKey = "logo",
                LargeImageText = "Counter-Strike: Global Offensive"
            };

            PushState(info);
        }

        public override void Stop()
        {
            _gameStateListener.Stop();
        }
    }
}
