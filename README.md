# Bomome Presentation (Beta 0.1.0)

## Features
- FeedbackChannel: an asset identifier and lightweight synchronous dispatch, with
  same-channel recursion protection and development-only hierarchy tracing.
- FeedbackButtonTransmitter: observes Button.onClick without replacing existing
  click behavior. Subscribes only while enabled and respects interactability.
- Optional FeedbackChannelReceiver: caches channel-to-Feel-player mappings,
  unsubscribes on disable, and supports an unscaled minimum playback interval.
- Channel inspector: on-demand saved scene/prefab usage search and loaded mappings.

Core assembly Bomome.Presentation depends only on Unity and uGUI. It owns no
wallet, audio mixer, progression, scene policy, saves, singleton or gameplay bus.
Use these channels for authored presentation, not authoritative gameplay events.
Multiple enabled receivers may intentionally listen to one channel. Only direct
same-channel recursive dispatch is suppressed; exceptions propagate to the caller.

## Optional Feel Integration
Import licensed Feel/MMF separately. The supported integration expects its
MMF_Player in the MoreMountains.Tools assembly (the existing Bomome setup).
Enable BOMOME_FEEL in scripting defines for every target using the adapter.
Without that define the adapter assembly is excluded. No vendor code, clips or
commercial art is redistributed by this package. Other Feel assembly layouts
require adapting the adapter assembly reference explicitly.

## Setup
Create a channel with Create > Bomome > Feedbacks > Channel. Assign it to a
FeedbackButtonTransmitter on a Button. On a scene-owned receiver, add the same
channel and a Feel player, then author the clips/effects there. Receiver
lifetime determines routing lifetime. Keep global UI receivers on a persistent
root only when cross-scene feedback is intended.

Configure ordinary commands via Button.onClick as before. A feedback failure must
not be used as success/failure of a purchase or game command.

## Migration
Extracted from BattleTroops with original script GUIDs, namespaces, serialized
field names and MovedFrom assembly attributes. Existing channel assets and scene
receiver mappings remain consumer-owned. Remove the old source files before
installing this package; do not duplicate their GUIDs or types.

ProtoFactory owns the neutral setup example. BattleTroops owns its audio mappings,
clips, skins and gameplay feedback controllers. The UIEffect-to-Feel adapter is
not part of this extraction.

This package currently uses sibling file references during beta. It is not yet
published. Replace them with a released repository reference before distribution.
Tests cover channel dispatch, recursion/exception recovery and button lifecycle.
Integration/device validation is still required for each consumer.
