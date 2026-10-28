# Feedback Design Standards

Version 1.0 | 2026-10-28

## 1. Purpose Before Technique
Every effect MUST have an identifiable role: acknowledge input, explain state,
preview a consequence, confirm an outcome, express material/identity, guide attention
or celebrate importance. Decoration MAY support atmosphere but must not impersonate
a gameplay signal. Removing an effect should reveal which role it served.

Fix unreliable interaction before decorating it. Hit areas, drag ownership, snapping,
input buffering and timing leniency belong to the interaction/game rules owner.
They MUST NOT be silently changed as an FX task. See R1 and C5 in [Research](Research.md).

## 2. Meaning And Semantic Honesty
Keep these meanings distinct: available, valid, targeted, selected, recommended,
in progress, completed and rejected. Valid MUST NOT imply strategically optimal.
A merge or attachment preview does not endorse that choice. A speculative action
MUST NOT play authoritative success feedback before validation.

Visual treatment SHOULD identify the event/source and its significance, not merely
look impressive. Distinguish materials and actions through shape, motion, timing
and sound as well as color. Preserve identity across skins and quality levels.
Do not rely on color, audio or haptics alone for essential information.

Reward ceremony MUST reflect real outcomes. Do not create false rarity, misleading
near-misses or success-like cues for losses. A count of decorative icons is not the
actual amount granted. Collection callbacks are never an economy authority.

## 3. A Game-Specific Feedback Vocabulary
The game SHOULD record a compact style contract:
- Intended feel, with a few descriptive qualities and explicit anti-goals.
- Meanings of major colors, shapes, sounds and haptic patterns.
- Routine, meaningful and exceptional intensity tiers.
- Motion/easing families, material behavior and escalation limits.
- Frequency expectations and accelerated/reduced-motion alternatives.
- Context priorities and measured platform budgets.

Shared standards do not prescribe bouncy buttons, realistic physics, a particular
palette or a mascot. Reuse semantic families rather than random effects per screen.
An upgrade can transform an object's state; an ordinary tap should not resemble
a major unlock. Repeated actions must remain comfortable at their expected frequency.

## 4. Responsiveness And Choreography
Separate four beats: acknowledgment, optional anticipation, consequence/peak, and
settle/tail. Acknowledgment SHOULD begin at the next available presentation
opportunity, without an artificial wait for decorative assets or a long wind-up.
Do not promise a universal latency number across platforms; measure on target devices.

Anticipation is appropriate when it communicates a forthcoming action or builds a
rare payoff. It MUST NOT postpone an ordinary interaction merely for spectacle.
Align the visual peak with the meaningful event, not an unrelated callback.

Overlap compatible beats. A settling tail may continue while the next interaction
becomes available. Input blocking MUST have an explicit reason, owner and bounded
end condition; a particle lifetime must not decide when gameplay becomes legal.

Animate purposefully: onset, acceleration, peak, overshoot and decay are separate
choices. Do not apply the same elastic or symmetric ease-in/ease-out to everything.
Continuous rotation usually needs constant angular speed and a seamless loop;
breathing, drifting and pulsing may use easing. Inspect loop boundaries and reset.

Prototype timing with simple shapes before expensive assets. Specify seconds or
normalized progress meaningfully; do not transplant a frame count from another
game as a universal preset. Reference timings remain observations or starting points.

## 5. Readability And Effect Anatomy
Give the effect a readable primary action or shape. Secondary particles support
it; tertiary detail must not be necessary to understand the event. Test without
secondary detail and at actual phone size.

The strongest contrast, saturation, motion and sound SHOULD support the highest
priority current information. Decorative brightness must not hide warnings,
targets, reward identity or the next decision. Area, timing and direction of
gameplay-affecting visuals MUST not misrepresent their actual behavior.

Separate an activation burst from a persistent state. Sustained buffs, selection
and ongoing production SHOULD settle into quieter localized indicators. A state
must remain understandable after its introductory flourish ends. Background motion
must not compete with text or ordinary navigation.

Tie visible cause to consequence: contact to impact, source to travel, destination
to collection. Preview and actual outcomes must agree, including when layout moves
or a destination belongs to a different canvas/camera.

## 6. Context, Accumulation And Repetition
Design local and shared feedback separately. Many local sparks may coexist;
full-screen flashes, camera motion, audio, haptics and dominant overlays need
explicit concurrency and priority limits.

Each effect family MUST define its retrigger policy: restart, blend, accumulate,
coalesce, queue with a bound, or ignore. Never accumulate unlimited queues.
Suppressing low-priority decoration must not suppress essential information.

Frequent feedback SHOULD be short and restrained. Rare achievements may use more
anticipation, contrast and recovery. Review repeated use, not just the first
playback. Offer acceleration/dismissal for repeated ceremonies where appropriate,
without losing the result summary or actual rewards.

Bound variation in pitch, timing, position and scale. Preserve recognizable
meaning; randomness must not obscure identity, change gameplay or escalate forever.
Where audio is tonal, consider a bounded musical phrase rather than an unbounded
pitch ramp. A climax needs contrast; continuous climax removes that contrast.

## 7. Audio, Haptics And Camera
Synchronize audiovisual and tactile accents to the same semantic event. Match
material, intensity and sharpness deliberately. Silence and quieter intervals
are valid parts of the composition.

Audio MUST route through the project's settings/channels, with appropriate voice
limits, variation and priority. Several simultaneous sounds must not produce
uncontrolled loudness. A limiter alone does not make an intelligible mix; protect
important cues and evaluate the result on phone speakers and headphones.

Haptics MUST be optional, respect applicable settings and have a fallback where
unsupported. Strength controls SHOULD be provided where supported and appropriate.
Avoid a vibration per particle or long buzzing for routine actions.

Camera shake MUST have bounds and an accessibility alternative. Prefer local
motion when it conveys the event adequately. Hit-stop, slow motion and camera
control can affect input, timers and simulation: they require explicit game-level
ownership and approval. Never globally freeze a busy multiplayer/army simulation
as a default per-hit feedback technique.

## 8. Presentation Is Not Authority
Gameplay/economy services validate and apply outcomes. Presentation renders supplied
state and reports visual outcomes. It MUST NOT grant currency, decide wins, deduct
costs or depend on every particle arriving for correctness.

Define behavior for completion, rejection, expiration, cancellation, missing
targets, pooling, scene changes and application interruption. A visual completion
does not imply a successful transaction; cancellation does not undo a valid reward.

Use explicit serialized relationships or existing injected services. Cross-system
notifications may use the established event bus and a project-owned adapter;
a known view's local command need not go through a bus. Do not create a second
presentation singleton, economy, event bus or generic manager without a real need.

## 9. Authoring And Lifecycle
Use installed tools suited to the behavior. Authored sequences, procedural runtime
targets, character clips and simulation are different tasks. This shared guide
does not mandate Feel, LeanTween, Coffee, Rive, Spine or DOTween as dependencies.
A project's integration guide identifies its actual tools and extension points.

Designer controls MUST have meaningful names, units, constraints and tooltips.
Group references, behavior, timing, visuals, feedback and diagnostics. Expose
useful decisions, not every implementation detail. Avoid unexplained tuning
literals and conflicting sources of truth.

Keep shared config assets read-only during play. Document override precedence.
Reuse prefabs/variants and profile assets; avoid copies for data-only differences.
Generated content becomes authored content: inspect and patch it, never rerun a
generator to replace designer edits. Creation must refuse unintended overwrite.

Define time mode, restart, interruption and cancellation. Stop only owned
animations and audio. Reset pooled transforms, colors, listeners and state.
Do not let a UI refresh restart a celebration or interrupt an upgrade count-up.

## 10. Accessibility And Performance
Reduced-motion/effects variants MUST preserve essential meaning. Replace a long
travel path with a shorter confirmation/destination response when appropriate;
do not simply remove all informational feedback. Avoid unnecessary camera motion,
repetitive background animation and large-area flashes. Assess photosensitivity
separately; reduced motion is not a complete photosensitivity safeguard.

Respect independent sound, music and haptic settings. Text/result summaries must
remain understandable without animation, color, audio or tactile output alone.

Budget simultaneous systems, screen coverage, transparency layers, material
changes, CPU simulation, allocations and frame time, not just particle count.
Pooling does not eliminate GPU overdraw. Review low-end hardware and stable frame
pacing. A quality reduction must preserve critical telegraphs and state cues.

## 11. Verification And Claims
Review the effect in isolation, normal play, crowded play and repeated use.
Use A/B or layer-removal comparisons when choosing techniques. Check mute,
reduced motion, interruption, time scale changes, scene exit and pooled reuse.

Evaluate clarity, perceived responsiveness, repetition fatigue and task completion
separately from aesthetic preference. Do not infer retention, revenue or universal
engagement benefits from an attractive animation or a different game's case study.
Record observed results, device/context and unresolved limitations.
