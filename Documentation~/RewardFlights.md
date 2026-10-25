# Reward Flights

## Ownership And Dependencies
This is visual presentation, not a reward or currency service. The core
Bomome.Presentation assembly owns flight/batch completion contracts only.
Bomome.Presentation.Coffee implements motion with Coffee UI Particle and
pools effect prefabs with LeanPool. It never reads or writes wallet balances.

Install Coffee UI Particle (validated with 4.14.1), LeanPool (assembly LeanPool)
and uGUI. Add BOMOME_COFFEE_REWARD_FLIGHTS to scripting defines for each supported
build target. The optional assembly stays excluded without this define; projects
using only feedback channels need neither vendor. Your calling asmdef must
reference Bomome.Presentation and Bomome.Presentation.Coffee.

Feel is optional. Hook the exposed UnityEvents to authored Feel players if desired.
No vendor code, art, sounds, global services, save keys or gameplay bus is included.

## Author A Profile
1. Make a UI effect prefab with a RectTransform and UIParticle.
2. Add one child ParticleSystem (with a UI-compatible material), and a separate
   child UIParticleAttractor. The attractor must not be the destination bar itself:
   every spawned flight needs its own attractor.
3. Add CoffeeRewardFlightEffect to the root. Assign UIParticle, the counted
   ParticleSystem and its attractor. Put all three inside this prefab.
4. Author the icon using the ParticleSystem texture-sheet Sprite mode. Its material
   must use the corresponding sprite texture. Author size, speed, scatter and
   lifetime on this prefab; do not duplicate them in the profile.
5. Author attraction movement, delay (fraction of lifetime), speed and arrival
   radius on the attractor. Use non-looping particles and no automatic emission.
   The adapter enforces manual emission, unscaled simulation and no loop per use.
   Raise ParticleSystemRenderer.maxParticleSize for UI-space sizes (the example
   uses 100); the default camera-relative clamp can shrink icons to tiny dots.
6. Create > Bomome > Presentation > Reward Flight Profile. Assign this prefab,
   default/max visual counts and a timeout longer than the particle lifetime.

Use a distinct prefab/profile for each art treatment. Reuse a profile across
different sources and destinations. Do not mutate shared profile assets at runtime.
The counted particle system must not use looping, extra emission or subemitters
that add counted particles; decorative particles should be separate effects.
The example uses UIParticle scale 1, local simulation, a 3.2-second lifetime,
20%-lifetime attraction delay, Smooth movement and a 15-unit maximum speed.

## Place A Presenter And Targets
Put RewardFlightPresenter on a stretched RectTransform below a Canvas. Place
its effects layer after ordinary content so icons appear in front, but below
modals if required. It has no raycast graphic and does not block buttons.
The scene owns its lifetime; disable/destroy cancels its flights.

Add RewardFlightTarget at each currency/item destination. Its RectTransform pivot
is the collection point. It follows the target's current position, including
layout changes during flight. Use onCollected for an arrival pulse/SFX and
onFlightCollected for a successful flight finish. Control accumulating sound
concurrency in your audio system; this package does not normalize audio.

For screen-space camera and world-space canvases, assign the root Canvas camera.
RectTransform sources and targets are converted through their own root Canvas
camera into the presenter's canvas plane. For a world-space source, use your
world camera to calculate a screen point and pass a RewardFlightRequest.
Off-screen positions are allowed; NaN and infinity are rejected.

## Direct Calls
```csharp
using Bomome.Presentation.RewardFlights;

// Count means decorative icons, not the amount granted.
RewardFlightHandle flight = presenter.Play(coinsProfile, chestRect, coinsTarget, 12);
flight.Finished += result => Debug.Log(result.Status);
flight.Cancel(); // Optional: only cancels this flight.
```

Play returns a handle immediately. Check IsDone before subscribing if a request
could be rejected synchronously. Status distinguishes Completed (all collected),
Expired, Cancelled, TargetLost and Rejected. CollectedCount records actual arrivals.

```csharp
var source = RewardFlightPresenter.ScreenPosition(chestRect);
var batch = presenter.PlayBatch(new[]
{
    new RewardFlightRequest(coinsProfile, source, coinsTarget, 12),
    new RewardFlightRequest(gemsProfile, source, gemsTarget, 8, 0.12f),
    new RewardFlightRequest(tokensProfile, source, tokensTarget, 8, 0.24f)
}, result =>
{
    // Always settles, even if one effect expired or its target disappeared.
    Debug.Log($"Visuals settled; all collected: {result.AllCollected}");
});
```

PlayBatch registers the completion callback before sealing the batch, including
empty or immediately rejected batches. batch.Cancel() cancels only its flights.
Several batches may overlap without retargeting each other's particles.

## Inspector And Event Calls
RewardFlightTrigger contains a presenter, source and list of profile/target/count/
delay entries. Wire Button.onClick or another UnityEvent to its Play method.
It cancels its own outstanding batches on disable; it does not cancel unrelated
flights. onFinished means settled, not necessarily collected. onAllCollected means
every flight completed successfully.

A game-specific message-bus adapter may subscribe to reward presentation events
and call the presenter/trigger. Store and dispose that adapter's subscriptions.
Do not put round/chest/economy message dependencies in the library.

## Limits, Pooling And Interruption
- The presenter caps concurrent flights (including delayed starts) and total
  reserved visual particles. Counts are clamped to profile and remaining budgets.
- No available capacity, invalid references or a disabled presenter reject a
  request immediately. No queue silently grows and no live flight is recycled.
- An inactive/destroyed target ends with TargetLost. A vanished active effect
  cancels rather than respawning. Particle expiration and timeout are terminal.
- Disable cancels all owned flights and returns surviving effects to LeanPool.
  Delayed flights reserve capacity but do not spawn effects until their start.
- Coffee arrival callbacks only accumulate counts. User feedback executes on the
  presenter tick, outside Coffee's simulation loop, avoiding pooled reuse inside it.
- State and internal listeners are cleared before pooling. No per-coin GameObjects
  or per-coin Update loops are added.
- Time scale zero does not stop this presentation. This is distinct from pausing
  the Unity Editor itself. Profile timeouts and start delays use unscaled time.
- User completion callbacks are isolated so one exception cannot strand a batch.
  Never grant rewards from a collection callback: presentation can be skipped.

## Validation
Core tests cover once-only settlement, mixed outcomes, cancellation and empty/
rejected batches. Consumer integration checks must also cover real Coffee rendering,
pool reuse, simultaneous targets, moving targets, canvas conversion, visual budgets,
scene exit and time scale zero. Tests do not replace Android profiling.
