# Research And Case Studies

Compiled 2026-10-28 | Supporting evidence, not runtime requirements

This summarizes published material reviewed during guideline design. Developer
statements describe their production context; they are not universal prescriptions.
Qualitative case analyses and observational studies are not controlled evidence
of retention or revenue. Historical timing statements are not current-version
frame measurements. We have not independently benchmarked these games or watched
every linked talk in full. No claim below depends on doing so.

## Research Findings

### R1. Game Feel Extends Beyond FX
Pichlmair and Johansen's survey analyzes over 200 sources and distinguishes
physicality, amplification and support. This helps separate coherent object
behavior, expressive feedback and support for player intention.
**Implication:** fix unreliable interaction before decorating it. Changes to input
leniency and physics are separate gameplay decisions, not implicit FX authority.
[Designing Game Feel: A Survey](https://arxiv.org/abs/2011.09201)

### R2. There Is No Universal More-Is-Better Rule
An action-RPG study with 3,018 participants compared no, medium, high and extreme
juiciness. Its abstract reports worse outcomes for none and extreme relative to
medium/high across play time, experience, motivation and performance.
**Limit:** this concerns the tested implementation/context, not a transferable
intensity threshold, proof of retention improvement or a causal explanation for
every successful game. The full publisher article was access-restricted during
review; this summary is limited to its public abstract.
[The Effects of Juiciness in an Action RPG](https://www.sciencedirect.com/science/article/pii/S1875952118300879)

### R3. Turn Vague Taste Into Observable Questions
Hicks and colleagues derive a framework from 17 developer responses and refine it
through qualitative analyses of Candy Crush Saga and Downwell. It considers
confirmatory/multimodal/relevant feedback, thematic coherence and game-state
communication. This is a design-analysis framework, not an experimental validation
of individual effects.
**Implication:** review semantics, emphasis and context rather than using
"make it juicier" as a specification.
[Good Game Feel (DiGRA 2018)](https://dl.digra.org/index.php/dl/article/download/936/936/933)

### R4. Impact Depends On More Than A Hit Particle
Lin and colleagues investigate impact feedback using player-review analysis and
comparisons across selected action games. Their findings highlight hit-stop,
sound coherence and camera control as potentially influential.
**Limit:** the methodology does not prove a particular technique will improve our
game. Global hit-stop is especially unsuitable as an automatic recommendation for
many simultaneous combatants.
[What Features Influence Impact Feel?](https://arxiv.org/abs/2208.06155)

### R5. Timing Can Be Prototyped Independently Of Detail
VFX Apprentice recommends geometric block-ins and distinguishes wind-up, climax
and falloff, as well as immediate impulses versus sustained rhythms.
**Implication:** settle the temporal shape before expensive shader/texture work.
Immediate feedback need not include anticipation; a gentle tail can follow a sharp
peak. This does not establish universal duration, BPM or easing prescriptions.
[The Soul of Effects: Timing in VFX](https://www.vfxapprentice.com/blog/the-soul-of-effects-what-is-timing-in-vfx)

## Production Case Studies

### C1. Peggle Blast: Repeated Hits As Musical Material
RJ Mattingly describes coordinating peg-hit notes with the current music's key
and scale through Wwise music callbacks. The design also accommodated memory and
implementation constraints.
**Our interpretation:** make repetition coherent. Collection sounds can form a
bounded phrase with a resolving final accent instead of escalating loudness or
pitch forever. A musical progression is optional; it is not a requirement to add
Wwise or to synchronize every game sound to background music.
[Developer breakdown](https://www.audiogang.org/peggle-blast-peg-hits-and-the-music-system/)
[Related PopCap GDC session](https://www.gdcvault.com/play/1020240/Peggle-2-Live-Orchestra-Meets)

### C2. Hearthstone: Visual Feedback Must Not Endorse A Strategy
Blizzard's effects artist describes Magnetic needing to communicate legal
attachment, destination and imminent attachment without implying that attaching
was always the better option. The article also describes replacing a heavy
persistent full-screen treatment with a localized hero effect for Glorious Gloop,
and reserving elaborate sequences for significant events.
**Our interpretation:** distinguish valid from recommended; separate activation
bursts from persistent state; give exceptional consequences more room than routine
actions. Apply this to merge previews and support effects without changing rules.
[Blizzard: The Art Behind THE SCIENCE](https://hearthstone.blizzard.com/en-us/news/22552047)

### C3. Hearthstone Packs: Storyboard The Ceremony
Art director Ben Thompson describes pack-opening production using concepts,
storyboards, effects and in-game testing. In a separate interview he discusses
balancing ceremony with opening many packs and player-controlled reveal order.
**Our interpretation:** a reward sequence has beats and pacing, not just a particle
prefab. Make identity/results readable and define acceleration for repeat use.
Do not borrow monetization pressure, misleading rarity or arbitrary historical
timing as a universal preset.
[Art director interview: production](https://toucharcade.com/2017/04/04/designing-hearthstone-card-packs-animations-iterations-ungoro-and-more-with-art-director-ben-thompson)
[Art director interview: ceremony and repetition](https://www.pcgamer.com/behind-the-addictive-psychology-and-seductive-art-of-loot-boxes/)

### C4. Dead Cells: A Coordinated Impact And Iteration Workflow
A 2017 interview quotes Motion Twin describing critical hits as a brief freeze,
slowdown, blood spray and a specific impact sound. It also describes an animation
pipeline that made timing adjustments and re-export practical.
**Our interpretation:** coordinate contact, sound, reaction and recovery; make
iteration affordable. A historical single-frame statement is not a measurement of
the current release. Do not copy global slowdown into a busy army simulation.
[Developer interview](https://www.gamedeveloper.com/design/designing-each-of-the-50-weapons-in-i-dead-cells-i-to-feel-distinctive)

### C5. Celeste: Respect Intention Before Decorating Response
Maddy Thorson documents coyote time, input buffering and corner correction as
deliberate allowances for player intention.
**Our interpretation:** difficult interaction and satisfying response are distinct
from decorative effects. For a placement game, inspect touch/drag/snap behavior
before adding pickup FX. These allowances modify game behavior and require the
appropriate owner and approval.
[Celeste & Forgiveness](https://www.maddymakesgames.com/articles/celeste_and_forgiveness/index.html)

### C6. Duolingo: Presentation Reacts And Can Be Interrupted
Duolingo documents separate character-pose and mouth states, audio synchronization
and stopping speech when a user completes the interaction early.
**Our interpretation:** define independent layers and interruption intentionally.
Reward sequences, countups and characters should yield cleanly to navigation.
This is an adjacent interactive-product example, not evidence about game retention,
and not a mandate to add Rive.
[Duolingo's animation architecture](https://blog.duolingo.com/world-character-visemes/)

### C7. Dense Combat Audio: Importance Before Event Count
Treyarch describes Black Ops 6's contextual priority system emphasizing relevant
threat sounds over less useful activity. Overwatch's audio team similarly frames
its GDC session around supporting play through sound in a crowded multiplayer
context.
**Our interpretation:** preserve useful cues while bounding the mix; an army should
feel numerous without treating every attack as an equally dominant sound. Do not
infer that a volume limiter alone solves readability, or that a published design
description proves flawless behavior in every version.
[Treyarch audio-team breakdown](https://www.callofduty.com/blog/2024/10/call-of-duty-black-ops-6-dynamic-soundscape-audio-intel)
[Blizzard GDC session](https://www.gdcvault.com/play/1023010/Overwatch-The-Elusive-Goal-Play)

### C8. Candy Crush And Downwell: Common Questions, Different Styles
The R3 paper examines cascading audiovisual feedback in Candy Crush and the
attention-directing use of a restrained palette in Downwell. Its analysis also
raises overwhelm under rapidly repeated exaggerated feedback.
**Our interpretation:** standardize meaning, context and validation, not a single
palette or level of bounciness. These are qualitative observations, not causal
player-outcome tests. See R3 for the source and methodology.

## Accessibility And Mobile References

### A1. Motion And Readability
Xbox Accessibility Guideline 117 discusses distracting UI motion and camera
effects; 110 discusses configurable haptics and alternative channels; 118 addresses
photosensitivity.
**Implication:** meaningful low-motion alternatives, optional haptics and separate
flash review belong in design. These are guidelines, not a compliance certification
or a complete test procedure for every platform.
[Motion](https://learn.microsoft.com/en-us/xbox/accessibility/xbox-accessibility-guidelines/117)
[Haptics](https://learn.microsoft.com/en-us/xbox/accessibility/xbox-accessibility-guidelines/110)
[Photosensitivity](https://learn.microsoft.com/en-us/xbox/accessibility/xbox-accessibility-guidelines/118)

### A2. Purposeful Haptics
Apple recommends consistent causal meanings, synchronization, restraint and optional
haptics. Android distinguishes clear/rich/buzzy effects and warns about overuse and
device support.
**Implication:** map tactile feedback to an event, not every particle; test fallback
and actual devices. Platform API recommendations do not prescribe a Unity plugin.
[Apple](https://developer.apple.com/design/human-interface-guidelines/playing-haptics)
[Android](https://developer.android.com/develop/ui/views/haptics/haptics-principles)

### A3. Mobile Cost Is Not Just Particle Count
Android's graphics guide warns about overlapping transparency and overdraw.
Unity's profiling guidance distinguishes render construction from actual GPU
timing measurements.
**Implication:** measure coverage, overlapping layers, materials, CPU and frame time.
Pooling is a lifecycle optimization, not proof that a visual is inexpensive.
[Android materials](https://developer.android.com/games/optimize/materials)
[Unity profiling](https://unity.com/how-to/best-practices-for-profiling-game-performance)

## Practical References
- [Juice It or Lose It](https://www.gdcvault.com/play/1016789/Juice-It-or-Lose):
  incremental embellishment demonstration; not an instruction to maximize intensity.
- [Deepnight interactive game-feel demo](https://deepnight.net/games/game-feel/):
  Enter/Start toggles individual features. Useful for comparative learning, not a
  substitute for testing the target game or device.

Use these as reference material. Do not copy art, audio, commercial assets or
unverified implementation details. Recheck links and claims when extending the
research; record version/date/method when quoting a measured timing.
