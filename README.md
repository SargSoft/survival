# Survival Game Project
A survival game focused on exploration and building, with a strong storyline, and expansive underwater / on land ecosystem.

## Table of Contents
1. [Team Members](https://github.com/sargasso-studios/general_testing#1-team-members)
2. [Getting Started](https://github.com/sargasso-studios/general_testing#2-getting-started)
3. [Style Guide](https://github.com/sargasso-studios/general_testing#3-style-guide)
	- [Naming](https://github.com/sargasso-studios/general_testing#31-naming)
	- [Language](https://github.com/sargasso-studios/general_testing#32-language)
	- [Declaration](https://github.com/sargasso-studios/general_testing#33-declaration)
	- [Spacing](https://github.com/sargasso-studios/general_testing#34-spacing)
	- [Commenting](https://github.com/sargasso-studios/general_testing#35-commenting)
	- [Bracing](https://github.com/sargasso-studios/general_testing#36-bracing)
	- [Layout](https://github.com/sargasso-studios/general_testing#37-layout)
	- [Directory](https://github.com/sargasso-studios/general_testing#38-directory)
		- [File Structure](https://github.com/sargasso-studios/general_testing#381-file-structure)
		- [Files Types](https://github.com/sargasso-studios/general_testing#382-file-types)
4. [Design](https://github.com/sargasso-studios/survival#4-design)
	- [Character](https://github.com/sargasso-studios/survival#41-character)
		- [Controls](https://github.com/sargasso-studios/survival#411-controls)
		- [Settings](https://github.com/sargasso-studios/survival#412-settings)
		- [Shaders](https://github.com/sargasso-studios/survival#413-shaders)
		- [Skills](https://github.com/sargasso-studios/survival#414-skills)
			- [Building](https://github.com/sargasso-studios/survival#4141-building)
			- [Crafting](https://github.com/sargasso-studios/survival#4142-crafting)
		- [UI](https://github.com/sargasso-studios/survival#415-ui)
		- [Visuals / Animations](https://github.com/sargasso-studios/survival#416-visuals--animations)
	- [Environment](https://github.com/sargasso-studios/survival#42-environment)
		- [AI / Fauna](https://github.com/sargasso-studios/survival#421-ai--fauna)
			- [Herbivores](https://github.com/sargasso-studios/survival#4211-herbivores)
				- [Small Herbivores](https://github.com/sargasso-studios/survival#42111-small-herbivores)
				- [Medium Herbivores](https://github.com/sargasso-studios/survival#42112-medium-herbivores)
				- [Large Herbivores](https://github.com/sargasso-studios/survival#42113-large-herbivores)
			- [Predators](https://github.com/sargasso-studios/survival#4212-predators)
				- [Small Predators](https://github.com/sargasso-studios/survival#42121-small-predators)
				- [Medium Predators](https://github.com/sargasso-studios/survival#42122-medium-predators)
				- [Large Predators](https://github.com/sargasso-studios/survival#42123-large-predators)
			- [Crustaceans](https://github.com/sargasso-studios/survival#4213-crustaceans)
				- [Small Crustaceans](https://github.com/sargasso-studios/survival#42131-small-crustaceans)
				- [Large Crustaceans](https://github.com/sargasso-studios/survival#42132-large-crustaceans)
		- [Audio](https://github.com/sargasso-studios/survival#422-audio)
			- [Music](https://github.com/sargasso-studios/survival#4221-music)
			- [Sounds](https://github.com/sargasso-studios/survival#4222-sounds)
		- [Landscape](https://github.com/sargasso-studios/survival#423-landscape)
		- [Plants](https://github.com/sargasso-studios/survival#424-plants)
	- [Main Menu](https://github.com/sargasso-studios/survival#43-main-menu)
		- [Background](https://github.com/sargasso-studios/survival#431-background)
		- [Buttons](https://github.com/sargasso-studios/survival#432-buttons)
		- [Music](https://github.com/sargasso-studios/survival#433-music)
		- [Options](https://github.com/sargasso-studios/survival#434-options)
	- [Story](https://github.com/sargasso-studios/survival#44-story)
		- [Backstory](https://github.com/sargasso-studios/survival#441-backstory)
		- [Ending](https://github.com/sargasso-studios/survival#442-ending)
		- [Journal](https://github.com/sargasso-studios/survival#443-journal)
		- [Story Arc](https://github.com/sargasso-studios/survival#444-story-arc)

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
This section is an outline of our preferred coding style, in order to improve readability/understandability, make sure that there is a clear structure to each script, and ensure a universal standard throughout all of the scripts.

### 3.1 Naming
- Use Camel Case for variables and parameters
- Use Pascal Case for functions, properties, events, and classes
- Use Pascal Case for files, and directories
- Do not use prefixes (e.g. m_ for private variables)
- Do use 'I' prefix for interfaces

### 3.2 Language
- Always use US spellings
- The only exception is 'MonoBehaviour' as this is the name of the class

**Good:**
```cs
string color = "red";
```
**Bad:**
```cs
string colour = "red";
```

### 3.3 Declaration
- Use one line per variable declaration, do not have multiple variables declared on a single line

**Good:**
```cs
string variable1;
string variable2;
string variable3;
```
**Bad:**
```cs
string variable1, variable2, variable3;
```

### 3.4 Spacing
- Use a single space after the comma between function arguments (example 1)
- Do not use a space between the opening parenthesis (example 1)
- Do not use a space between the function name and opening parenthesis (example 1)
- Do not use spaces inside brackets (example 2)
- Use a single space before and after an operator (example 3)
```cs
// example 1:

Console.WriteLine(argument1, argument2, argument3);
```
```cs
// example 2:

x = dataArray[index];
```
```cs
// example 3:

if (x == y)

while (x == y)

Console.WriteLine(x + y)
```

### 3.5 Commenting
- Double slash commenting (single line commenting) will be used rather than multi-line commenting
- A space will be left after the double slash and before the comment (example 1)
- Comments should **not** end with a period (example 1)
- The first letter of comments should always be capitalised (example 1)
- Variables purposes should be clear from their names, although a comment may be attached if appropriate
- All functions should have a comment that clearly describes their purpose, intention, and approach
- Additional comments may be used elsewhere where appropriate

```cs
// example 1

// Comment Here
```

### 3.6 Bracing
- Opening braces should be on the same line as the statements declaration
- Closing braces should be on their own line below the contents, unless there is an else statement which starts on the same line as the closing brace (example seen below)
- All contents inside braces should be indented by 1 tab more than the braces themselves

```cs
static void Function(string parameter1, int parameter2) {
    // Contents
}
```

```cs
if(someExpression) {
    doSomething();
} else {
    doSomethingElse();
}
```


### 3.7 Layout
- The first lines should use **using** to import the namespaces that will be used in the script
- Next, declare the class of the script (only one class per script)
- Inside that class declare the variables for the script (public then private) using Headers to separate them into groups, which will be displayed in the inspector window in unity
- Next will be void Start which will contain all of the code that will be initialised when the script is run
- After that is void Update, which will run once per frame
- Finally is the list of functions, the format of which is demonstrated in the below example, which follow the same format as the class they are within
- Leave blank lines between sections (as seen in the example below), and do **not** leave any other blank lines
- All of the comments in the example below should be removed in the actual scripts

```cs
using UnityEngine;
using System.Collections;

public class ClassName : MonoBehaviour {
	[Header("Title to describe variable group")]
	public string stringName;
	private float floatName;

	[Header("Title to describe variable group")]
	public float floatName;
	private string stringName;

    // Use this for initialization
    void Start () {
    	// Code here
    }
    
    // Update is called once per frame
    void Update () {
    	// Code here
    }

    // Functions here
    public void FunctionName() {
    	[Header("Title to describe variable group")]
    	private string stringName;
    	private float floatName;

    	// Main Code here
    }
}
```

### 3.8 Directory
This section outlines the expections in terms of how we structure our files, and also the file types that should be used.

#### 3.8.1 File Structure

```
Assets
+---Art
|	+---Materials
|	+---Models
|	+---Textures
+---Audio
|	+---Music
|	+---Sound
+---Code
|	+---Scripts
|		+---Environment
|		+---Framework
|		+---Tools
|		+---UI
|	+---Shaders
+---Docs
|	+---Concept Art
|	+---Marketing
|	+---Readme
|	+---Wiki
+---Level
|	+---Prefabs
|	+---Scenes
|	+---UI
+---Resources		# Configuration files, localisation text, other user files
```

#### 3.8.2 File Types

| Files         | File Type     |
|:-------------:|:-------------:|
| Music         | .WAV          |
| Sound         | .OGG          |
| Models        | .FBX          |
| Textures      | .PNG          |

## 4. Design
This section is an overview of the design of the game followed by a highly descriptive breakdown of all the individual elements of the game. Below is a visual representation of the game design overview.

![Mindmap](Assets/Docs/Readme/DesignMindmap.png)

### 4.1 Character
Below is a breakdown of all of the aspects relating to the character, including all of the requisites for each element.

#### 4.1.1 Controls
**Requisites:**

- Basic WASD movement
- Camera control with mouse (capsule should rotate along x-axis with mouse, but not along y-axis)
- Crouching, jumping, and running (each of which should have their own function)
- Swimming (should detect one camera is below water surface, and controls should adjust appropriately to simulate swimming)
- Using Unity's Input Manager & built-in multiplatform commands (e.g. 'Horizontal' rather than 'Mouse X')

#### 4.1.2 Settings
- Settings info

#### 4.1.3 Shaders
- Shaders info

#### 4.1.4 Skills
- Skills info

##### 4.1.4.1 Building
- Building info (UI, Recipes, Items Scripts & Assets)

##### 4.1.4.2 Crafting
- Crafting info (UI, Recipes, Items Scripts & Assets)

#### 4.1.5 UI
- UI info

#### 4.1.6 Visuals / Animations
- Visuals / Animations info

### 4.2 Environment
Below is a breakdown of all of the aspects relating to the environment, including all of the requisites for each element, and also initial concept art.

#### 4.2.1 AI / Fauna
This section will outline the overview of all the types of Fauna in the game, and their respective requisites.

![TreeDiagram](Assets/Docs/Readme/FaunaTreeDiagram.png)

##### 4.2.1.1 Herbivores
Herbivores intro text.

###### 4.2.1.1.1 Small Herbivores
**Small Shoaling Herbivores:**

![ShoalingHerbivoreAIFlowchart](Assets/Docs/Readme/ShoalingHerbivoreAIFlowchart.png)

- Build basic herbivore AI that paths around based on terrain, handles encounters with food,carnivores.
- Expand Herbivore AI to accommodate swarms (when >1 are in close proximity have them join together and consolidate logic).

**Small Scavenging Herbivoes:**

- Test build second species of herbivore with different behaviour (i.e. bottom feeders).

###### 4.2.1.1.2 Medium Herbivores
**Small Shoaling Herbivores:**

**Small Scavenging Herbivoes**

###### 4.2.1.1.3 Large Herbivores
**Small Roaming Herbivores:**

**Small Scavenging Herbivoes:**

##### 4.2.1.2 Predators
Preditores intro text.

###### 4.2.1.2.1 Small Predators
**Small Shoaling Preditors:**

###### 4.2.1.2.2 Medium Predators
**Medium Roaming Preditors:**

- Build basic carnivore AI that hunts herbivores when hungry, and otherwise patrols.

###### 4.2.1.2.3 Large Predators
**Large Roaming Preditors:**

##### 4.2.1.3 Crustaceans
Crustaceans intro text.

###### 4.2.1.3.1 Small Crustaceans
info

###### 4.2.1.3.2 Large Crustaceans
info

#### 4.2.2 Audio
- Audio info

##### 4.2.2.1 Music
- Music info

##### 4.2.2.2 Sounds
- Sounds info

#### 4.2.3 Landscape
- Landscape info

#### 4.2.4 Plants
**Requisites:**

- Build system for zoning that populates a specific area with plant life taking in to account terrain.
- Expand zoning system to have plants instantiated in clusters with variables dictating available food.

### 4.3 Main Menu
Below is a breakdown of all of the aspects relating to the main menu, including all of the requisites, and initial concept art.

#### 4.3.1 Background
- Background info

#### 4.3.2 Buttons
- Buttons info

#### 4.3.3 Music
- Music info

#### 4.3.4 Options
- Options info

### 4.4 Story
Below is a breakdown of all of the aspects relating to the story, including all of the major events, backstory, story chronology, and in game triggers for story advancement.

#### 4.4.1 Backstory
- Backstory info

#### 4.4.2 Ending
- Ending info

#### 4.4.3 Journal
- Journal info, ingame wiki with information on fauna / plants / environment / crafting / etc.

#### 4.4.4 Story Arc
- Story Arc info
