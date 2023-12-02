# Data Oriented Space Invaders
An assignment made for Futuregames course "Computer Technology".

## Controls
Use <kbd>A</kbd> and <kbd>D</kbd> to move.

## Features
Being my first time working with Data Oriented Programming, this project came with it's fair share of lessons.
I decided to focus my learning on Unity's Entity Component System (ECS) and profiler.
As such, I learnt to use unmanaged datatypes with distinguished entities, components and systems.
With the Job System, I used Burst Compilation to optimise the performance of various systems: player movement, invader movement, projectile spawning and projectile movement.
For greater consistency whilst profiling, I opted to spawn projectiles in large numbers at a fixed frequency.

- [x] Simple movement
- [x] Shooting
- [x] Waves of enemies

## Improvements
Having received the feedback that my code may not be structured in a way that takes advantage of ECS, I took a better look into best practices.
Namely, I learnt that I had carried over some bad habits from Object Oriented Programming, such as the use of queries to facilitate for loops which may not be taken advantage of by multithreading.
To ammend this, I replaced the usage of queries and for loops with Jobs in a selection of some of the least performant systems:
1. Projectile movement
2. Projectile spawner
3. Invader spawner
4. Player movement

An immediate sign that the scheduling of jobs was improving performance was the removal of some memory leaks.
I then proceeded to add collision detection and indetify and remove some accidental managed data type usage that was getting in the way of the Burst compiler.
In some places, I found it difficult to remove queries entirely.
In the case of the invader spawner system, I opted instead to use a combination of better query usage (by storing an entity query I required as a class member in OnCreate), and Jobs.
With all these optimisations in place, I found framerates to remain high even in the presence of an absurd number of enemies and projectiles.
Following with what I've learnt, there are some remaining optimisations that could be made, namely to the collision detection and invader movement.
I found difficulty in re-structuring both of these systems in a way that seemed optimal within my own time restrictions, although performance improvements at this scale seem less significant to gauge.

## Performance Tests
### 1st submission: ECS
Without collision detection.

100 enemies, 2020 projectiles — Approximately 180 FPS (7ms)  
200 enemies, 4020 projectiles — Approximately 120 FPS (8ms)  
400 enemies, 8020 projectiles — Approximately 60 FPS (8ms)  

### 2nd submission: ECS with multithreading and Burst
With collision detection.

100 enemies, 2020 projectiles — Approximately 240 FPS (4ms)  
200 enemies, 4020 projectiles — Approximately 220 FPS (4ms)  
400 enemies, 8020 projectiles — Approximately 190 FPS (5ms)
