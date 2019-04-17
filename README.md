# Survival Game Project
A survival game focused on exploration and building, with a strong storyline, and expansive underwater / on land ecosystem.

## Table of Contents
1. [Team Members](https://github.com/sargasso-studios/general_testing#1-team-members)
2. [Getting Started](https://github.com/sargasso-studios/general_testing#2-getting-started)
3. [Code Style Guide](https://github.com/sargasso-studios/general_testing#3-code-style-guide)
	3.1 [Variables]
	3.2 [Functions]
	3.3 [Comments/Headers]
	3.4 [Layout]
4. [Design](https://github.com/sargasso-studios/general_testing#4-design)
	4.1 [Environment Prototyping]
	4.2 [Character Control]

## 1. Team Members
- Charlie Brown
- Jack Thomas
- Nye Goodall
- Sadiq Adesanya

## 2. Getting Started
- Unity Version 2018.3.9 (found in 2018.x section of: https://unity3d.com/get-unity/download/archive)
- 3ds Max Version ...

## 3. Code Style Guide
This section is an outline of our preferred coding style, in order to make all of parts of the project very easy to read / understand, and make sure that there is a clear structure to each script.

### 3.1 Variables
Stuff to do with variables.

### 3.2 Functions
Function text.

### 3.3 Comments/Headers
Stuff relating to comments & Headers.

### 3.4 Layout
Layout text.

## 4. Design
### 4.1 Environment Prototyping
A prototype of the ecosystem design.

#### Inital design
- Build system for zoning that populates a specific area with plant life taking in to account terrain.
- Expand zoning system to have plants instantiated in clusters with variables dictating available food.
- Build basic herbivore AI that paths around based on terrain, handles encounters with food,carnivores.
- Build basic carnivore AI that hunts herbivores when hungry, and otherwise patrols.

#### Further 
- Test build second species of herbivore with different behaviour (i.e. bottom feeders).
- Expand Herbivore AI to accommodate swarms (when >1 are in close proximity have them join together and consolidate logic).


### 4.2 Character Control
Prefabs of a simple character model (capsule) with functioning character controls

#### Inital design
- Basic WASD movement
- Ability to control the direction of the character with the mouse
- Add simple functions like crouch, jump, running

#### Further 
- Separating different controls into their own functions (e.g. crouching, jumping, running, etc.)
- Using Unity's Input Manager & built in multiplatform commands (e.g. 'Horizontal' rather than 'Mouse X')
- Implement swimming
