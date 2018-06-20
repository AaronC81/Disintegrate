# THIS IS NOW OUT-OF-DATE.

# Disintegrate
This project is the core of Disintegrate. It contains most of the logic which
makes Disintegrate work, with UI tasks handled by `Disintegrate.UI` instead.

## Presences
There are four key classes in Disintegrate.

### `PresenceInfo`
This class represents a single Rich Presence 'state' in Discord. It contains
information like the description and image. This class is really just a data
container, and doesn't do anything on its own.

### `PresenceProvider`
This class is the base class which must be inherited to add a new game to
Disintegrate. Its purpose is to hook into the game, generate `PresenceInfo`
instances with relevant info, and then call `PushState(PresenceInfo)` to
broadcast to the program that there is a new state available.

There are two main kinds of provider, determined by the value of the object's `StateFrequency` property.

  - Providers with `TimeControlled` push new states at steadily timed
    intervals, for example every 2 seconds. Every state pushed by the provider
    will be sent to Discord.
  - Providers with `FastAsPossible` push new states chaotically, or at *very*
    fast timed intervals, for example every 0.5 seconds. Only one state every
    few seconds is actually sent to Discord to avoid the API becoming 
    overwhelmed and rejecting future states.

### `PresenceRelay`
This class wraps a `PresenceProvider` and sends its state on to Discord based
on its `StateFrequency`.

### `PresenceManager`
This class instantiates, starts, and stops relays accordingly when game
processes are launched or closed. The `PresenceManager` periodically checks
for new processes, and if any match a supported game, a relay and provider
are instantiated and started.

## Configurators
Configurators are classes which handle first-time setup of providers. Every
provider has a *Configure* button in the Disintegrate UI, and clicking it will
execute the `Configure()` method on the provider's configurator. The button is
disabled if `IsConfigured()` returns `true`.