# Disintegrate
Disintegrate adds **Discord Rich Presence to CS:GO and DOTA 2.** For more
information, see [disint.cc](disint.cc).

## Project Structure
The Disintegrate solution has three projects:
  - `Disintegrate` is the main project, containing classes for creating and
    dispatching providers.
  - `Disintegrate.UI` contains the tray icon and user interface. It is the
    entry point for the application once installed.
  - `Disintegrate.Setup` is a Visual Studio Setup project for the installer.
    You may need to install [this extension](https://marketplace.visualstudio.com/items?itemName=VisualStudioProductTeam.MicrosoftVisualStudio2017InstallerProjects) to edit this.
    
`Disintegrate.UI` and `Disintegrate.Setup` are relatively simple, but `Disintegrate` is much more complex and as such has its own README in its project root.