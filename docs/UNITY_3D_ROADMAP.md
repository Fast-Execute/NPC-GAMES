# NPC-GAMES — 3D PC Game Roadmap

## Progress tracking

See [MASTER_PROGRESS.md](MASTER_PROGRESS.md) for the canonical elimination-based progress index, completed-work register, percentages and next exact task.

## Product direction
NPC-GAMES is being developed as a **3D third-person Windows PC game**. The existing HTML/JavaScript build remains a rapid simulation prototype and design reference. The intended playable client is Unity with C#.

## Why Unity
- C# gameplay code and a mature 3D editor.
- Character controllers, physics, animation, lighting, terrain and audio.
- Windows desktop builds.
- A practical path from a small playable settlement to larger streamed worlds.

## Prototype 3D-001 — First playable vertical slice
The first vertical slice is deliberately small and generated at runtime:
- Third-person player capsule with WASD movement, Shift sprint and Space jump.
- Mouse-controlled follow camera; Escape releases the cursor and left-click locks it again.
- Settlement square, houses, archive, school and market.
- Ten visible NPCs with names and occupations.
- Simple autonomous wandering within each NPC's home radius.
- Approach an NPC and press E to receive an interaction line.

This is a greybox foundation. Capsules and cubes are temporary placeholders, not final character art or architecture.

## Open the prototype in Unity
1. Install Unity Hub and a Unity 6 LTS editor with Windows Build Support.
2. Create a new **3D Core** project.
3. Copy this repository's Assets folder into the Unity project's root.
4. Open the project and wait for Unity to finish importing the Assets folder.
5. Select **NPC-GAMES → Build First Playable Scene** from the Unity menu. This creates and saves `Assets/Scenes/NPC-GAMES-First-Playable.unity`, attaches the bootstrap component and adds the scene to Build Settings.
6. Press Play. The settlement and player are created automatically.

The repository currently supplies the Unity `Assets` source folder rather than a complete Unity project shell. Create a Unity 6 LTS 3D Core project first, then copy the repository's `Assets` folder into that project. Unity will generate its own `ProjectSettings`, package manifest and asset metadata on first import.

The scripts use Unity's built-in Input axes. If the project is configured for the new Input System only, set **Active Input Handling** to **Both** or **Input Manager (Old)** in Player settings, then restart the editor.

## Build a Windows PC executable
1. Open File → Build Profiles (or Build Settings in older editor versions).
2. Select Windows as the target platform.
3. Add the playable scene to the build scene list.
4. Select Build and choose an output folder.
5. Run the generated Windows executable on a PC.

## Controls
| Input | Action |
|---|---|
| W / A / S / D | Move |
| Mouse | Rotate camera |
| Left Shift | Run |
| Space | Jump |
| E | Interact with a nearby NPC |
| Escape | Release mouse cursor |
| Left mouse button | Lock mouse cursor again |

## Development principles
- Keep NPC identity, memory, beliefs, relationships and knowledge separate from rendering and animation.
- The player client should not become the authority for the entire civilization. Persistent world simulation should ultimately run server-side.
- NPCs should only act on information available through perception, memory and social communication.
- A death should alter families, relationships and inheritance without deleting the world's history.
- The first 3D prototype uses placeholder geometry and simple wandering. It does not yet include NavMesh pathfinding, animation rigs, saving, multiplayer, procedural terrain or the full JavaScript civilization simulation.

## Prototype 3D-002 — Living settlement systems
- Camera-relative player movement: character turns toward the direction of travel.
- Third-person camera collision to reduce clipping through walls and props.
- Accelerated 24-hour world clock with sun angle, daylight, ambient light and fog changes.
- NPC daily routine destinations: home in the early morning and evening, work during the day, and the public square after work.
- NPC personality-specific conversation responses, simple trust changes and a short interaction memory.
- Roads, tree clusters and street lamps add spatial landmarks to the settlement.

This is still a greybox simulation: NPC movement uses simple steering, not Unity NavMesh; buildings are not enterable; NPC schedules and memories are local runtime state and are not saved yet.

## Next milestones
1. Replace capsules with rigged characters, animation controller and expressive idle/walk/run states.
2. Add terrain, roads, interiors, doors, interactable objects and day/night cycle.
3. Add NPC perception, schedules, needs, goals, relationships and memory.
4. Connect the civilization simulation to a persistent data model and save/load.
5. Add dialogue UI and contextual Meeting Icons.
6. Add optimized crowds, navigation, world streaming and optional multiplayer architecture.
7. Profile performance and package repeatable Windows builds.
