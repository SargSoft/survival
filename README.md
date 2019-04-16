# Survival Game Project
A survival game focused on exploration and building, with a strong storyline, and expansive underwater / on land ecosystem.

## Table of Contents
1. [Team Members](https://github.com/sargasso-studios/general_testing#1-team-members)
2. [Getting Started](https://github.com/sargasso-studios/general_testing#2-getting-started)
3. [Code Style Guide](https://github.com/sargasso-studios/general_testing#3-code-style-guide)
4. [Design](https://github.com/sargasso-studios/general_testing#4-design)

## 1. Team Members
- Charlie Brown
- Jack Thomas
- Nye Goodall
- Sadiq Adesanya

## 2. Getting Started
- Unity Version 2018.3.9f1
- 3ds Max Version ...

## 3. Code Style Guide
Section outlining the styling of the code.

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
