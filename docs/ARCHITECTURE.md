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


## Life, Death and Reincarnation

The player is a persistent consciousness node rather than a disposable avatar.

**Life cycle:** birth -> aging -> relationships -> experience -> death -> legacy -> reincarnation.

Death does not reset the simulation. NPCs retain memories of the deceased player, possessions and discoveries can enter the world, and the next incarnation begins with only fragments of previous-life memory.

The reincarnation system deliberately avoids perfect memory transfer. This creates mystery, continuity and emergent interpretation.

## Meeting Icons / Training Icons

Historical figures can be represented as bounded historical simulations. The initial prototype includes Aleister Crowley, Marie Curie, Albert Einstein, Socrates and Nikola Tesla.

An Icon has:
- historical era and knowledge boundary
- characteristic speech style
- associated ideas and works
- contextual worldview
- encounter history

An NPC or player does not receive the Icon's identity. They receive an **interpretation of an encounter**. Different personalities can therefore leave the same meeting with different beliefs.

The long-term Icon system should support a large historical catalogue while distinguishing historical evidence from fictional dialogue generated for gameplay.

## Generational simulation

NPCs should eventually age through visible life stages. Their relationships, careers, beliefs and memories change over time. When an NPC dies, descendants and social networks can carry forward parts of that individual's legacy.

**Core principle:** death changes the state of the world; it does not erase the world.


## Generational World Engine — Prototype 002

The simulation now gives each seeded NPC an age, life stage, family identity, child list and living/deceased state.

- Life stages: infant, child, youth, adult, elder and very old.
- The player can advance the world by one calendar year independently of the hourly world tick.
- NPCs age together; some adults may form the next generation.
- Descendants inherit a family connection and a starting trace of family memory, not a full copy of a parent's identity.
- NPCs can die in old age. Their community receives a remembrance, while their record remains visible as deceased.
- Children can grow into adult occupations and develop their own knowledge.

This is an early stochastic prototype, not yet a complete demographic model. Future work should add guardianship, partners, pregnancy/birth rules, inherited assets, schooling, individual schedules, illness, mortality distributions, family trees, visual age stages and save/load persistence.
