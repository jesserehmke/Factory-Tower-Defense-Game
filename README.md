# Factory Tower Defense

A Tower Defense game where you have to fight off different enemies that try to break into the player's factory and reach the player.

The player can use the factory to build a production chain to manufacture components for building a defense system.
## Status

This project is in an early phase of development.

## Vision

I started this game as my next bigger project. I want this game to have an original feeling. To realize that, I try to give the player as much freedom in choosing a defense strategy as possible.

I want this project to be better structured, so I try to map out my plans, write cleaner code, and make use of Git's backup system.

## Technologies

- C#
- Visual Studio
- Blender
- Unity 3D

## Core Mechanics

Elements of the game that bring the biggest technical complications.

### Pathfinding

The first bigger mechanic that I am trying to develop is a pathfinding algorithm. I want the enemies to register which tiles are free to pass and find the fastest way using those free tiles.

I want to write the algorithm with as little help as possible because I think pathfinding and really understanding how it works is a great technical exercise.

First, I wrote an algorithm that only considers the four directly surrounding (not diagonally adjacent) tiles as possible next steps. The algorithm ended up being rather similar to Dijkstra's algorithm.

Since I wanted the enemies to be able to take diagonal paths as well, I decided to write a new version.

I found the A* algorithm to be very interesting, so I tried to implement an own version of that approach. I think I got fairly close to a working A* algorithm, but I am still running into some issues that I am trying to solve.

## Screenshots

The following screenshots show the most recent version of the project.

You can see:
- A laser that is reflected by mirrors and beams at the enemy.
- A UI for constructing new buildings.
- Transportation belts that can be placed via the UI and move items around.
- An enemy (orange ball) that moves around an obstacle.
- A shredder that shreds enemies into resources (the enemies are probably going to be something like robots).

![laser](screenshots/laser_beaming_enemy.png)

![shredder](screenshots/shredder+transportation_belt.png)