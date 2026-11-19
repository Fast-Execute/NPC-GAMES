# NPC-GAMES — Master Progress Tracker

**Project:** NPC-GAMES — Internet Consciousness  
**Target:** 3D third-person Windows PC game (Unity / C#)  
**Tracking rule:** Elimination method. When a task is implemented and its stated verification standard is met, move it to **Completed Work** and remove it from the **Active Development Index**. Never renumber the remaining canonical items. Do not count code as runtime-verified until it has been opened and tested in Unity.

## Overall progress

<text>Project vision and early prototype foundations: established. The first two 3D prototypes have source-code implementations, but neither has yet been verified inside the Unity Editor.</text>

**3D roadmap implementation estimate: 22%**  
`[████░░░░░░░░░░░░░░]` 22%

This is a planning estimate across the nine numbered 3D development stages (3D-001 through 3D-009), not a claim that a finished game is 22% feature-complete. Two stages have implementation work committed; Unity verification remains outstanding. The earlier HTML/JavaScript foundation is recorded separately.

| Measure | Status |
|---|---:|
| 3D stages with implementation work committed | 2 / 9 |
| 3D prototype source files | 6 |
| Unity Editor compile/play test | Not yet performed |
| Windows executable | Not yet built |

## Completed Work — removed from the active index

### Foundation — completed
- Established the NPC-GAMES concept: each NPC is an individual node in a shared, evolving information network.
- Created the dependency-free HTML/CSS/JavaScript simulation foundation.
- Added NPC identities, personalities, memories, shared knowledge, rumours, information propagation and player influence.
- Added the initial life-cycle, death, reincarnation and generational civilization simulation concepts.
- Documented the long-term architecture and design principles.

### 3D-001 — First settlement scaffold — implementation committed
- Selected Unity/C# as the intended 3D Windows PC technology.
- Added runtime-generated settlement layout: square, houses, Memory Archive, Community School and Open Market.
- Added a third-person player capsule and CharacterController movement.
- Added mouse-controlled follow camera.
- Added ten named NPCs with occupations and autonomous wandering.
- Added proximity interaction using E.
- Added Unity setup and Windows build instructions.

### 3D-002 — Living settlement systems — implementation committed
- Added camera-relative movement and character turning.
- Added camera collision handling.
- Added accelerated 24-hour world clock.
- Added daylight, sun, ambient-light and fog transitions.
- Added night-time street-lamp activation.
- Added NPC home, work and public-square routines.
- Added personality-specific NPC responses, basic trust/mood changes and interaction memory.
- Added roads, trees and street-lamp landmarks.

**Verification caveat:** The source files were fetched again and basic delimiter/structure checks passed. This is not a C# compilation or Unity playtest. These stages remain pending hands-on Unity verification.

## Active Development Index

The numbering below is canonical. Completed items are not repeated here and remaining numbers must never be renumbered.

### 3D-001V — Verify the first playable build
- [ ] Create/open a Unity 6 LTS 3D Core project.
- [ ] Import the repository Assets folder.
- [ ] Create a scene and attach NpcGames3DBootstrap.
- [ ] Set Active Input Handling to Both or Input Manager (Old), if required.
- [ ] Compile without errors and enter Play mode.
- [ ] Verify movement, camera, NPC wandering and E interaction.
- [ ] Record Unity version, test result and any defects.

### 3D-002V — Verify living settlement systems
- [ ] Verify day/night lighting and street lamps.
- [ ] Verify NPC schedules at different times.
- [ ] Verify personality dialogue and interaction memory.
- [ ] Verify camera collision around settlement objects.
- [ ] Fix all runtime errors and record test evidence.

### 3D-003 — Human characters and animation
- [ ] Replace placeholder capsules with rigged human characters.
- [ ] Add idle, walk, run and talk animation states.
- [ ] Add distinct clothing, silhouettes and visual identities.
- [ ] Add a basic character animation controller.

### 3D-004 — World exploration
- [ ] Add terrain and a larger navigable settlement.
- [ ] Add doors, enterable buildings and interiors.
- [ ] Add interactable objects and contextual prompts.
- [ ] Add environmental audio and spatial ambience.

### 3D-005 — Autonomous NPC intelligence
- [ ] Separate NPC identity/data from visual GameObjects.
- [ ] Add perception and bounded knowledge.
- [ ] Add needs, goals, schedules and decision-making.
- [ ] Add relationships, trust, family links and social memory.
- [ ] Add witness → memory → conversation → rumour → reputation propagation.

### 3D-006 — Persistent civilization
- [ ] Connect NPCs to a persistent world data model.
- [ ] Save and load NPC identities, memories and relationships.
- [ ] Persist households, descendants, property, institutions and world events.
- [ ] Ensure player death/reincarnation does not reset civilization history.

### 3D-007 — Dialogue and Meeting Icons
- [ ] Build an in-game dialogue interface.
- [ ] Add historical Meeting Icon simulations with clearly bounded historical knowledge.
- [ ] Add debates, player training interactions and NPC interpretation.
- [ ] Ensure dialogue changes with personality, memory, context and relationship.

### 3D-008 — World scale and network
- [ ] Add navigation/pathfinding for complex environments.
- [ ] Add crowd and simulation performance controls.
- [ ] Design world streaming and server-authoritative simulation.
- [ ] Evaluate optional multiplayer and player-as-network-node systems.

### 3D-009 — Windows release
- [ ] Profile CPU, GPU and memory on target PC hardware.
- [ ] Add settings, accessibility and save management.
- [ ] Create repeatable Windows build configuration.
- [ ] Build and test a Windows executable.
- [ ] Package release notes and installation instructions.

## Progress update rules
1. A task is completed only when the work exists in the repository and its relevant verification is recorded.
2. Move completed tasks out of the Active Development Index into Completed Work; preserve canonical IDs and titles.
3. Update the overall percentage only when a milestone's status changes. Show implementation and verification separately.
4. Never claim Unity compilation, playability or an executable until it has actually been tested/built.
5. Each progress report must state: current milestone, percentage, newly completed work, active next task and remaining index.

## Current next exact item

**3D-001V — Verify the first playable build.** The next practical step is to open the project in Unity 6 LTS, import Assets, create the bootstrap scene and test Play mode.