# Bomome Presentation (Beta 0.2.0)

## Features
- Concurrent reward flights: independent pooled Coffee effects, profiles, targets,
  batch completion, visual limits and inspector/direct-call triggers. See
  [Reward Flights](Documentation~/RewardFlights.md) for setup and usage.
- FeedbackChannel: an asset identifier and lightweight synchronous dispatch, with
  same-channel recursion protection and development-only hierarchy tracing.
- FeedbackButtonTransmitter: observes Button.onClick without replacing existing
  click behavior. Subscribes only while enabled and respects interactability.
- Optional FeedbackChannelReceiver: caches channel-to-Feel-player mappings,
  unsubscribes on disable, and supports an unscaled minimum playback interval.
- Channel inspector: on-demand saved scene/prefab usage search and loaded mappings.

## FX And Game Juice Guidelines
See [FX And Game Juice](Documentation~/GameJuice/README.md) for shared design
standards, authoring/review checklists, research and case studies. These are
tool-agnostic guidelines, not additional runtime features or a prescribed art style.
Consumer projects own their tool integration, tuning, visual language and budgets.

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

The package is published at https://github.com/BoMoMe/com.bomome.presentation.git.
Pin a published revision in consumer manifests; local source references are only
for development. The optional reward-flight adapter requires Coffee UI Particle,
LeanPool and BOMOME_COFFEE_REWARD_FLIGHTS on each supported target. Core feedback
channels do not acquire these dependencies.
Tests cover channel dispatch, recursion/exception recovery and button lifecycle.
Integration/device validation is still required for each consumer.
