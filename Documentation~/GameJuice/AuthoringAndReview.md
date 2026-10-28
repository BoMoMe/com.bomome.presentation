# Effect Brief And Review

2026-10-28 | Use the sections relevant to the change; do not create a document for every button.

## Short Effect Brief
Attach this to the feature guide, task or profile notes for a substantial effect.
Reuse the game style contract instead of copying it.

| Field | Record |
| --- | --- |
| Intent | What the player should understand or feel; semantic event |
| Owner | Gameplay outcome owner, presentation owner and affected assets |
| Context | Frequency, concurrent events, screen/phase, target devices |
| Meaning | Available/valid/selected/success/etc.; any risk of suggesting a strategy |
| Priority | Intensity tier, competing cues, what may be coalesced or suppressed |
| Timeline | Onset, anticipation if any, peak, tail; when input is available |
| Spatial behavior | Source/target, canvas/camera, sorting and input/raycast constraints |
| Channels | Visual, audio, haptic; what remains when each is unavailable |
| Retrigger | Restart/blend/coalesce/etc.; queue and concurrency limits |
| Lifecycle | Complete, interrupt, cancel, disable, target loss, scene exit and pooled reset |
| Authoring | Existing tool/profile/prefab, tuning units and override precedence |
| Budget | Peak systems/screen coverage/voices and measured frame-time target |
| Evidence | Reference ID, hypothesis, test result and limitations |

Timings and budgets are game/device choices, not universal values supplied here.
Do not add a config field, class or global service for every row.

## Suggested Workflow
1. Inspect the current feature, relevant package capability, authored assets and
   game style contract. Identify which values belong to a profile versus behavior.
2. State the semantic event and expected frequency before selecting a technique.
3. Block in the primary motion/shape. Tune timing and spatial relationships first.
4. Layer audio, optional haptics and supporting detail around the same peak.
5. Establish bounds, interruption and low-motion behavior before final polish.
6. Test normal play and worst-case overlap; remove layers that do not improve it.
7. Preserve authored changes, document the narrow result and outstanding validation.

Use slow playback or frame stepping to diagnose, then judge at real speed.
A paused screenshot proves composition, not responsiveness or sound synchronization.

## Design Review Checklist
- [ ] Cause, state change and outcome are readable at actual play size.
- [ ] Available, valid and strategically recommended are not conflated.
- [ ] Importance matches the event; routine actions do not resemble rare rewards.
- [ ] Primary shape/movement works without tertiary particles or glow.
- [ ] Persistent state remains legible after the activation burst.
- [ ] Normal and busy screens preserve warnings, targets, text and the next action.
- [ ] Onset feels responsive; tail/anticipation do not impose unnecessary waiting.
- [ ] Audio, contact, visual peak and optional haptic agree.
- [ ] Repetition is comfortable at expected frequency, not just once in a demo.
- [ ] Pitch/random variation and celebration escalation have bounds.
- [ ] Rejection explains the result without punitive spectacle for routine mistakes.
- [ ] Loop seams and unintended easing/pauses have been checked.
- [ ] Result significance and rarity are represented honestly.

## Functional Review Checklist
- [ ] Authoritative game/economy state is independent of visual completion.
- [ ] Repeated triggers follow an explicit bounded policy.
- [ ] Skip/accelerate does not double-grant, lose information or leave input locked.
- [ ] Disabled/missing targets, scene changes and cancellation settle safely.
- [ ] Moving targets and supported canvas/camera modes remain aligned.
- [ ] Owned tweens/listeners/audio stop or reset without affecting unrelated work.
- [ ] Pooled reuse resets all modified visual and interaction state.
- [ ] UI refresh does not restart reveals or cancel countups.
- [ ] Time mode is deliberate; simulation changes were explicitly authorized.
- [ ] Existing safe-area, sorting, input and settings systems are respected.

## Accessibility And Performance Review
- [ ] Core information survives mute, missing haptics and reduced effects.
- [ ] Reduced motion uses an understandable replacement, not information deletion.
- [ ] Flashes and camera motion received a separate accessibility review.
- [ ] Voice/haptic/screen-wide effects have priority and concurrency limits.
- [ ] Quality reductions preserve essential cues.
- [ ] GPU transparency coverage and material changes were inspected.
- [ ] CPU simulation, allocations and frame pacing were measured on target hardware.
- [ ] Normal, overlapping and repeated-use cases were tested.
- [ ] Evidence distinguishes compile checks, runtime checks and actual device review.

## Comparison And Test Card
Compare: baseline, primary motion only, motion plus audio, supporting particles,
full combination, reduced variant. Order comparisons fairly where practical;
do not assume the newest or busiest version is best.

Ask players what happened, where the consequence went, whether input registered,
and what they would do next before asking which version looks nicer.
For repeated-use tests, record interruptions/skip behavior and fatigue as well as
preference. Do not add telemetry or new debug UI without the project's normal
scope, privacy and build-guard approvals.

## Brief Examples (Illustrative, Not Shipped Features)
| Interaction | Primary read | Supporting treatment | Constraint |
| --- | --- | --- | --- |
| Place an object | Clear valid preview then stable placement | Small settle and contact sound | Preview does not bypass validation |
| Merge | Two sources become one identifiable stronger state | Converging motion, peak accent | Valid merge is not always a strategic recommendation |
| Collect mixed rewards | Distinct items reach their respective destinations | Bounded burst, stagger, target pulse | Visual counts do not grant or equal currency amounts |
| Persistent buff | Quiet identifiable state on its owner | Activation accent then lower intensity | Must remain clear without a full-screen loop |
| Repeated combat hit | Local contact/direction and reaction | Material-specific impact sound/spark | No default global hit-stop per troop |
| Rank/unlock | Readable before/after identity | Rare celebratory peak | Repeated ceremonies can accelerate without losing results |

These examples do not authorize changes to existing balance, input or scene flow.
