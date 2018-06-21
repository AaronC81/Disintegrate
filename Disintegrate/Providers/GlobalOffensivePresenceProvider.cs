using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CSGSI;
using Disintegrate.Configuration;
using Disintegrate.Customization;

namespace Disintegrate.Providers
{
    /// <summary>
    /// Provides presence info for Counter-Strike: Global Offensive.
    /// </summary>
    public class GlobalOffensivePresenceProvider : PresenceProvider
    {
        public GlobalOffensivePresenceProvider(PresenceApp app) : base(app) { }

        public override StateFrequency StateFrequency => StateFrequency.FastAsPossible;

        private GameStateListener _gameStateListener;

        const int NoStateSeconds = 5;
        private Timer _noStateTimer;

        public override void Start()
        {
            Safe(() =>
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
            });
        }

        /// <summary>
        /// Called when the <see cref="GameStateListener"/> receives a new state.
        /// </summary>
        /// <param name="gameState">The new game state.</param>
        public void NewGameState(GameState gameState)
        {
            Safe(() =>
            {
                var newState = new PresenceState();

                newState.ImageValue = new ImageBundle("logo", "CS:GO");

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

                newState.FieldValues["Kills"] = gameState.Player.MatchStats.Kills.ToString();
                newState.FieldValues["Deaths"] = gameState.Player.MatchStats.Deaths.ToString();
                newState.FieldValues["Assists"] = gameState.Player.MatchStats.Assists.ToString();
                newState.FieldValues["MVPs"] = gameState.Player.MatchStats.MVPs.ToString();
                newState.FieldValues["Map"] = gameState.Map.Name;
                newState.FieldValues["Mode"] = Utilities.GlobalOffensiveNaming.ModeNames[gameState.Map.Mode];

                // The Team and Score fields depend on your team
                switch (gameState.Player.Team)
                {
                    case CSGSI.Nodes.PlayerTeam.T:
                        newState.FieldValues["Team"] = "Terrorists";
                        newState.FieldValues["Score"] = $"T {gameState.Map.TeamT.Score} - {gameState.Map.TeamCT.Score} CT";
                        break;
                    case CSGSI.Nodes.PlayerTeam.CT:
                        newState.FieldValues["Team"] = "Counter-Terrorists";
                        newState.FieldValues["Score"] = $"CT {gameState.Map.TeamCT.Score} - {gameState.Map.TeamT.Score} T";
                        break;
                    default:
                        newState.FieldValues["Team"] = "Unknown team";
                        newState.FieldValues["Score"] = $"CT {gameState.Map.TeamCT.Score} - {gameState.Map.TeamT.Score} T";
                        break;
                }

                // If they want no icon, create a blank icon
                newState.IconValues["None"] = new ImageBundle("", "");

                // If they want a team icon, find out which team they're on
                if (gameState.Player.Team == CSGSI.Nodes.PlayerTeam.T)
                {
                    newState.IconValues["Team"] = new ImageBundle("t", "Terrorists");
                }
                else if (gameState.Player.Team == CSGSI.Nodes.PlayerTeam.CT)
                {
                    newState.IconValues["Team"] = new ImageBundle("ct", "Counter-Terrorists");
                }
                else
                {
                    newState.IconValues["Team"] = new ImageBundle("", "");
                }

                PushState(newState);
                _noStateTimer.Stop();
                _noStateTimer.Start();
            });
        }

        /// <summary>
        /// Called when the <see cref="GameStateListener"/> hasn't received a new state for
        /// <see cref="NoStateSeconds"/> seconds.
        /// </summary>
        public void NoGameState(object o, ElapsedEventArgs e)
        {
            Safe(() =>
            {
                PushState(new PresenceState("In menus", "")
                {
                    ImageValue = new ImageBundle("logo", "CS:GO")
                });
            });
        }

        public override void Stop()
        {
            Safe(() =>
            {
                _gameStateListener.Stop();
            });
        }
    }
}
