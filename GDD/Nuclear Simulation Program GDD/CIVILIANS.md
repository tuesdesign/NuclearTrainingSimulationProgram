Back: [[___READ ME - Project Documentation___]]

Here is a Civilian.
![[Pasted image 20241207022836.png]]

A civilian has several features of note.

# Animator
The animator defines the appearance of the character. The animator controls the mesh of the character and selects which animation to play (idle, walking). These animations are gathered from Mixamo. If you'd like to add more characters, copy the controller element to a new Avatar downloaded from Mixamo.

# Outline
Like many objects, civilians have a custom defined outline. The Civilian has a green outline.

# Nav Mesh Agent
This component defines how the character is able to walk around. 
![[Pasted image 20241207023542.png]]
A Nav Mesh is built around the walls and other unmoving (static) parts of the environment. The light blue parts are the sections where the character is able to walk. The Nav Mesh Agent system uses an algorithm to find a path between any points in this section. 

# Person Navigator

This component defines what we want the character to do. It is extensively documented in the file PersonNavigator.CS, but here are the important parts:
### Waypoints
This is a post of all [[POI]] objects and is gathered automatically.

### Acceptable Distance
The distance the agent must be from a [[POI]] before it will consider itself to have reached its destination. 

### Wait Time
The amount of time the agent will wait at one [[POI]] before choosing a new one to navigate to
