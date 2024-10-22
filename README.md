# Project 2: NPC

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)


### Student Info

-   Name: Jimmy Williams
-   Section: 5

## Simulation Design

You need to collect stars while under attack.

### Controls

-   _List all of the actions the player can have in your simulation_
    -  Player controls player with WSAD/Arrow keys

## _Agent 1 - Alien
These enemies wander around the edge of the screen and chase you if they get close

### Wander

Enemy will circle around screen and stay within a border

#### Steering Behaviors

- _List all behaviors used by this state_
  - wander
- Obstacles - asteroids
- Seperation - other wanderers
   
#### State Transistions

- _List all the ways this agent can transition to this state_
   - leaves range of player
   
### Chasing

**Objective:** Will slowly chase the player

#### Steering Behaviors

- _List all behaviors used by this state_
 - seek
- Obstacles - asteroids
- Seperation - other wanderers
   
#### State Transistions

- Enemy gets within range of player.

### Fleeing

**Objective:** Will run from player when they hurt them.

#### Steering Behaviors

- _List all behaviors used by this state_
 - Flee
- Obstacles - asteroids
- Seperation - other wanderers
   
#### State Transistions

- after hitting enemy

## _Agent 2 - Star

Will be collected by player

### Safe

**Objective:** Will wander randomly in space

#### Steering Behaviors

- _List all behaviors used by this state_
 - wander
- Obstacles - asteroids
- Seperation - other collectables
   
#### State Transistions

- _List all the ways this agent can transition to this state_
far enough away from the agent
   
### In Danger

**Objective:** Will evade player

#### Steering Behaviors

- _List all behaviors used by this state_
 - Flee
- Obstacles - asteroids
- Seperation - other enemies
   
#### State Transistions

- Get close to player.

### vacuum

**Objective:** Will slowly chase the player

#### Steering Behaviors

- _List all behaviors used by this state_
 - seek
- Obstacles - asteroids
- Seperation - other stars
   
#### State Transistions

- Player takes damage


## Sources

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_
https://www.nintendo.co.jp/networkservice_guideline/en/index.html - music
https://jitsukoan.itch.io/keeper - enemies
https://pixel-boy.itch.io/ninja-adventure-asset-pack - health
https://www.pngkit.com/downpic/u2w7r5o0o0q8u2q8_game-over-sprite-transparent/ - game over 
https://foozlecc.itch.io/void-main-ship - ship
https://deep-fold.itch.io/space-background-generator - background
https://www.mariowiki.com/Star_Bit - collectable
https://bulbapedia.bulbagarden.net/wiki/Minior_(Pokémon) - asteroid
## Make it Your Own

- _List out what you added to your game to make it different for you_
- _If you will add more agents or states make sure to list here and add it to the documention above_
- _If you will add your own assets make sure to list it here and add it to the Sources section
 - music
 - sprites from project 1
 - 3 states per agent
 - particle system
## Known Issues

_List any errors, lack of error checking, or specific information that I need to know to run your program_

### Requirements not completed

_If you did not complete a project requirement, notate that here_

