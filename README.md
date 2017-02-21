# UFPS-Unity3d
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

The repository contains working C# code for different components of a FPS game built using Unity3d and UFPS with a Survivial theme .
The screenshots folder and readme file for each component contain steps to understand the the functionality .

##UFPS 
UFPS is a professional FPS base platform for Unity. One of the longest-running and most popular titles of the Unity Asset Store, it’s known for smooth controls and fluid, realtime-generated camera and weapon motion

## Following game logic is covered in each package :

### 1) AddForceNSpawn -
Add Force and Spawn new GameObject OnTriggerStay

### 2) BatteryPickupandTorchController-
The code contains two packages one each for picking up batteries by the player and torch controller

### 3) BulletParticle -  
This package contains code for spawning custom  bullet particles when hit with the object using Raycasting and Particle system.

### 4) ChangeGunAccuracy - 
Contains logic for decreasing gun accuracy by adding shake with time when player's health decrease

### 5) CODGhostEffect - 
This package shows COD Ghost like popup effect when player hits a collectible

### 6) InventoryController -

The package contains InventoryController.cs which is a Singleton Manager class. To use anywhere in scripts just call
InventoryController.instance.anyFunctionName() .This makes all the related logic to go inside just one controller class which can be reused .

It handles following scenarios : 
Item pick-up:
- Item pick-up with E.
- Max carry for items is 2x for each item.
- After 2x pick-up show gui "Inventory full".

UI - inventory:
- Pressing Q shows inventory(fade in). Inventory fades out after 5 seconds.
- When inventory is open pressing E consumes food.

### 7) PauseMenu - 
This script shows a pause menu when Esc key is pressed. A button "resume game" does same on click and hides pause menu. 
The mouse cursor becomes visible when game is in pause state , applies to Unity 5.2 .

### 8) RotateCameraUFPS  - 
 The package contain solution to rotate camera using UFPS , but the rotation is abrupt/sudden 

### 8) SurvivalScript  - 
The package contains logic and assets to show a survival game aspect where the Helath , Thirst and Temperature is affected and corresponding gui is changed .
OnTriggerEnter of Food , Water or Fire normalizes the corresponsidng survial property

### 8) ThermalController  - 

Fire/Firestarter/Wood:
- When close to object tag "fireplace" by raycast press "E" to spawn prefab.
- Spawns(turn on) prefab(fire) and collider for heat/temperature.
- Spawning only turns-on disabled object. 
- For this a simplified approach of 2 cubes, one on top of another is used, top cube will turn on.

UI - frost/heat:
- Frost counter is hidden and value is min 0 - max 60.
- Frost icon shows when counter is less than 20.
- When frost is 0 health lowers by 5hp per second.
- Frost goes down by 0.5 per second when not near heat source.  
- When near heat source "frost" goes up by 8 units per second to max 60. 
- When "frost" is 60 "heat icon" disappears. 

### 8) WeaponSwicthAndDashboard  - 
Package contains logic for showing a Weapon Dashboard and changing gun using UFPS .4


## Meta

Divyanshu – [@LinkedIn](https://linkedin.com/in/softxide) – divyanshu17x@gmail.com

Distributed under the MIT license. See ``LICENSE`` for more information.








