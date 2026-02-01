# Worms-like (Unity) — Turn-Based 2D Prototype (WIP)

A small Unity prototype inspired by classic turn-based 2D artillery games.
The goal is to build a clean, modular foundation: destructible terrain, character control, and a scalable weapon/inventory system.

> Status: Work in progress. Systems are being iterated and may change as the architecture stabilizes.

## Current Features
- **Destructible 2D terrain at runtime**
  - Dynamic terrain damage (texture/alpha updates)
  - **Automatic collider regeneration** after terrain changes
- **Character movement**
  - Basic movement implemented and integrated into the gameplay loop

## In Progress
- **Weapons system**
  - Multiple weapon types (architecture in progress)
- **Inventory**
  - Inventory workflow + UI integration (work in progress)

## Planned / Next Steps
- Turn-based loop (turn start/end, actions per turn)
- Basic shooting/trajectory and damage application
- Simple AI / test dummy
- UX polishing: aiming, camera, turn UI, weapon selection flow

## Tech Notes (High Level)
- Terrain destruction updates the visual data and then rebuilds colliders to keep physics consistent.
- Weapon/inventory system is designed to avoid hardcoding logic per weapon type.

## How to Run
1. Clone the repository
2. Open the project in Unity (recommended: any recent LTS version)
3. Open the main scene and press Play

> If Unity version mismatch happens, check `ProjectSettings/ProjectVersion.txt`.

## Media
- GIF/Video: *(add later)*
- Screenshots: *(add later)*

## License
Specify a license if you plan to accept contributions.

## Contact
GitHub: https://github.com/SiryVovk
