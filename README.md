# Disintegrate
Disintegrate adds **Discord Rich Presence to CS:GO and DOTA 2.** For more
information, see [disint.cc](disint.cc).

## Project Structure
The Disintegrate solution has two projects:
  - `Disintegrate` is the main project, containing classes for creating and
    dispatching providers.
  - `Disintegrate.UI` contains the tray icon and user interface. It is the
    entry point for the application once installed.
    
`Disintegrate.UI` is relatively simple, but `Disintegrate` is much more complex
and as such has its own README in its project root.