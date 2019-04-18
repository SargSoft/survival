# Survival Game Project
A survival game focused on exploration and building, with a strong storyline, and expansive underwater / on land ecosystem.

## Table of Contents
1. [Team Members](https://github.com/sargasso-studios/general_testing#1-team-members)
2. [Getting Started](https://github.com/sargasso-studios/general_testing#2-getting-started)
3. [Style Guide](https://github.com/sargasso-studios/general_testing#3-style-guide)
	- [Naming](https://github.com/sargasso-studios/general_testing#31-naming)
	- [Spacing](https://github.com/sargasso-studios/general_testing#32-spacing)
	- [Commenting / Headers](https://github.com/sargasso-studios/general_testing#33-commenting--headers)
	- [Bracing](https://github.com/sargasso-studios/general_testing#34-bracing)
	- [Layout](https://github.com/sargasso-studios/general_testing#35-layout)
	- [File Structure](https://github.com/sargasso-studios/general_testing#36-file-structure)
4. [Design](https://github.com/sargasso-studios/general_testing#4-design)
	- [Environment Prototyping](https://github.com/sargasso-studios/general_testing#41-environment-prototyping)
	- [Character Control](https://github.com/sargasso-studios/general_testing#42-character-control)

## 1. Team Members
- Charlie Brown
- Chris Mimm
- Jack Thomas
- Nye Goodall
- Sadiq Adesanya

## 2. Getting Started
- Unity Version 2018.3.9 (found in 2018.x section of: https://unity3d.com/get-unity/download/archive)
- 3ds Max Version ...

## 3. Style Guide
This section is an outline of our preferred coding style, in order to make all of parts of the project very easy to read / understand, and make sure that there is a clear structure to each script.

### 3.1 Naming
- For all variable names use Camel Case
- For public variables simply use an appropriate, descriptive name
- For private variables use an appropriate, descriptive name, combine with "m_" as the prefix (e.g. m_variable)

### 3.2 Spacing
- For all function names use Camel Case

### 3.3 Commenting / Headers
- Use comments to describe the purpose of all of the variables
- Use comments to describe ...
- Use Headers to separate sections (e.g. [Header("Title")] )

### 3.4 Bracing
- Bracing stuff

### 3.5 Layout
- Variables at top (public, then private)
- Functions next
- Code that runs at start
- Code that runs per frame
- Image to show this?

### 3.6 File Structure
- File structure stuff

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
