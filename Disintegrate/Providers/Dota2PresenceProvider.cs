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
        public Dota2PresenceProvider(PresenceApp app) : base(app) { }

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

                newState.ImageValue = new ImageBundle("logo", "DOTA 2");

                newState.FieldValues["Kills"] = gameState.Player.Kills.ToString();
                newState.FieldValues["Deaths"] = gameState.Player.Deaths.ToString();
                newState.FieldValues["Assists"] = gameState.Player.Assists.ToString();
                newState.FieldValues["Denies"] = gameState.Player.Denies.ToString();
                newState.FieldValues["LastHits"] = gameState.Player.LastHits.ToString();
                newState.FieldValues["Team"] = gameState.Player.Team.ToString();
                newState.FieldValues["Hero"] = Utilities.Dota2HeroNaming.MakeFriendlyName(gameState.Hero.Name);
                newState.FieldValues["Level"] = gameState.Hero.Level.ToString();
                newState.FieldValues["Gold"] = gameState.Player.Gold.ToString("N0");

                // If we're in a game but haven't picked yet
                if (gameState.Hero.Level == -1)
                {
                    newState.OverrideText = ("Picking a hero", "");
                }

                // If they want no icon, create a blank icon
                newState.IconValues["None"] = new ImageBundle("", "");

                // If they want a team icon, find out which team they're on
                if (gameState.Player.Team == Dota2GSI.Nodes.PlayerTeam.Dire)
                {
                    newState.IconValues["Team"] = new ImageBundle("dire", "Dire");
                }
                else if (gameState.Player.Team == Dota2GSI.Nodes.PlayerTeam.Radiant)
                {
                    newState.IconValues["Team"] = new ImageBundle("radiant", "Radiant");
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
                    ImageValue = new ImageBundle("logo", "DOTA 2")
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
