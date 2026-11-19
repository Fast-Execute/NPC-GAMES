# NPC-GAMES Architecture

## The consciousness model

Each NPC is represented as an individual agent with:

1. **Identity** — name, role and persistent personality traits.
2. **Memory** — observations and information personally encountered.
3. **Knowledge** — a changing estimate of what the NPC understands.
4. **Trust** — how willing the NPC is to accept information.
5. **Social graph** — explicit relationships to other NPC nodes.
6. **World model** — beliefs formed from memories and information received.

## Information flow

Player/world event -> originating NPC -> social links -> transformed memory -> further propagation.

The important design rule is that information is not copied perfectly. Personality and trust can alter how an event is remembered and communicated.

## Future milestones

- Persistent save/load
- Long-term memory compression
- NPC goals and autonomous schedules
- Relationship graph with positive/negative edges
- Belief confidence and contradictory beliefs
- Local and global knowledge layers
- Procedural cultures and languages
- Server-side shared consciousness
- LLM-driven dialogue with bounded memory
- Retrieval from curated external knowledge sources
- Multiplayer player-to-NPC information effects
- Emergent factions, religions, economies and politics
- Simulation replay/debugging tools

## Design principle

**NPCs should not know what the game designer knows. They should know what their experience and information network gives them.**
