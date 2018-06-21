# Disintegrate
This project is the core of Disintegrate. It contains most of the logic which
makes Disintegrate work, with UI tasks handled by `Disintegrate.UI` instead.

## Presences
There are seven key classes in Disintegrate. There used to be four, but 5.0.0 added
three more to make each class smaller and simpler, improving overall code usage.

### `PresenceState`
This class is just a bundle of data which can be used in a Rich Presence.
It does not actually have any kind of layout for use in Discord.

### `PresenceInfo`
This class represents a single Rich Presence description in Discord. It contains
information like the description and image. It uses data from a `PresenceState`
to populate the description.

### `PresenceFormatter`
This class converts a `PresenceState` to a `PresenceInfo` using the customizer
information for an app.

### `PresenceProvider`
This class is the base class which must be inherited to add a new game to
Disintegrate. Its purpose is to hook into the game, generate `PresenceState`
instances with relevant info, and then call `PushState(PresenceState)` to
broadcast to the program that there is a new state available.

There are two main kinds of provider, determined by the value of the object's `StateFrequency` property.

  - Providers with `TimeControlled` push new states at steadily timed
    intervals, for example every 2 seconds. Every state pushed by the provider
    will be sent to Discord.
  - Providers with `FastAsPossible` push new states chaotically, or at *very*
    fast timed intervals, for example every 0.5 seconds. Only one state every
    few seconds is actually sent to Discord to avoid the API becoming 
    overwhelmed and rejecting future states.

When a provider does anything, it should be done within a call to `Safe`. For example:

```
public override void Start() {
	Safe(() => {
		// Setup
	})
}
```

This enables logging and crash recovery.

### `PresenceRelay`
This class wraps a `PresenceProvider` and sends its state on to Discord based
on its `StateFrequency`.

### `PresenceManager`
This class instantiates, starts, and stops relays accordingly when game
processes are launched or closed. The `PresenceManager` periodically checks
for new processes, and if any match a supported game, a relay and provider
are instantiated and started.

### `PresenceApp`
Each instance of this class represents a new app in Disintegrate. There are
only two instances currently used, one for DOTA 2 and one for CS:GO. Each instance
holds a reference to that classes formatters, configurators, customizers, and other
important information.

## Configurators
Configurators are classes which handle first-time setup of providers. Every
provider has a *Configure* button in the Disintegrate UI, and clicking it will
execute the `Configure()` method on the provider's configurator. The button is
disabled if `IsConfigured()` returns `true`.

## Customizers
Customizers specify the fields and settings available for a particular app. Each
'layout' made from a customizer is called a preference. The user's preference
dictates how a `PresenceState` is translated to a `PresenceInfo`.