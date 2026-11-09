# ECS Bullet Hell 2D

A Unity DOTS prototype that separates player movement, enemy spawning, and bullet updates into ECS systems. It’s a small example of how authoring components and a SubScene feed a data-oriented gameplay loop.

## Run the prototype

1. Open the repository root in **Unity 2022.3.10f1**. Unity resolves **Entities**, **Entities Graphics**, and **Unity Physics** at version **1.0.16** from the [package manifest](Packages/manifest.json).
2. Open [SampleScene](Assets/Scenes/SampleScene.unity) and enter Play Mode. The level content is organized under [Level1SubScene](Assets/Scenes/SampleScene/Level1SubScene.unity).

## Code tour

| Area | Entry point |
| --- | --- |
| Player | [PlayerSystem.cs](Assets/Scripts/Player/PlayerSystem.cs) updates player behavior using ECS data. |
| Enemies | [EnemySpawnerSystem.cs](Assets/Scripts/Enemy/EnemySpawnerSystem.cs) and [EnemySystem.cs](Assets/Scripts/Enemy/EnemySystem.cs) cover spawning and enemy behavior. |
| Bullets | [BulletSystem.cs](Assets/Scripts/Bullets/BulletSystem.cs) and [BulletLifeTimeComponent.cs](Assets/Scripts/Bullets/BulletLifeTimeComponent.cs) are the starting points for bullet updates and lifetime data. |
