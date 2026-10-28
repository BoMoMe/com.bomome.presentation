# FX And Game Juice

Version 1.0 | 2026-10-28 | Reusable design and authoring standards

This is the authoritative home for shared feedback principles. It is not a
requirement for a particular art style, genre, animation framework or vendor.
It adds documentation, not runtime functionality. A game's engineering standards
still govern architecture, dependencies, simulation changes and asset ownership.

## Read By Task
| Task | Read |
| --- | --- |
| Design or implement feedback | [Standards](Standards.md) |
| Author an effect or review a change | [Authoring and Review](AuthoringAndReview.md) |
| Understand evidence, examples and limitations | [Research and Case Studies](Research.md) |
| Integrate the Coffee reward-flight adapter | [Reward Flights](../RewardFlights.md) |

Read the affected feature guide and authored assets as well. Do not reread every
case study for a small tuning change; use the reference IDs when rationale matters.

## Ownership
| Layer | Owns |
| --- | --- |
| Presentation package | Shared principles, design vocabulary, checklists, evidence and package-specific contracts |
| Project composition | Installed-tool mapping, example wiring, input/sorting/canvas integration and project settings |
| Individual game | Motion personality, feedback vocabulary, semantic colors, intensity tiers, timings, sounds, budgets and reward rules |

Maintain one authoritative home. Consumers link here instead of copying the guide.
Never require a sibling source checkout. Pin a published package revision containing
these documents when distributing a consumer; an older package pin does not gain
new documentation automatically.

## Authority And Evidence
- MUST/MUST NOT: delivery, semantic correctness, lifecycle and accessibility rules.
- SHOULD: defaults; record a concrete reason for a different choice.
- MAY: optional techniques, chosen according to the game.
- Research findings are evidence with scope, not universal laws. Case-study lessons
  are our interpretation unless explicitly identified as developer statements.
- No document authorizes dependency changes, new global services, alterations to
  combat timing, a balance change or regeneration of authored assets.

Use a brief effect specification rather than a new manager or a long design
document for every button. A new project's style decisions belong beside its own
presentation feature, not in this shared guide.
