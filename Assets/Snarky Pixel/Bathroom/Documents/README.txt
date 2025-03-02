
======================
        INDEX
======================

1.0 Thank You
2.0 Asset Overview
    2.1 What's to Know?
    2.2 Included Assets
    2.3 Bonus Assets
3.0 Included Scripts
    3.1 Interactions
    3.2 InteractiveObject
    3.3 InteractiveDoors
    3.4 Pushable
    3.5 DisplayUpdate
    3.6 FPSInputController
    3.7 MouseLook
4.0 Post-processing
    4.1 Backup Time
    4.2 Linear Color Space
    4.3 New Post-processing stack (Unity 2017.2 and above)
    4.4 Old Post-processing stack (any version)
5.0 Contact Me
    

=============
1.0 Thank You
=============
I would like to start off by thanking you for buying my very first non-free Unity asset! If you have any questions at all, please do not hesitate to contact me (at support@snarkypixel.com.) I will get back to you as soon as I can. I can provide support in English, German, Swedish and Finnish.

You can also e-mail me to request additional items or features be added to the pack if you need them and I will consider it, depending on the feature and its relevance to the pack.



==================
2.0 Asset Overview
==================
This bathroom asset is intended for a wide range of applications—including mobile and VR—and aims for realism, modularity and flexibility while staying conservative with polygons and textures. Let's have a better look at what this means in practice.

The materials, where possible, allow you to select their color by changing the Albedo color of the shader. This allows you to create variety and to fine-tune the materials to be just the way you like them. Likewise, wherever possible the objects have been UV-mapped to allow you to replace the materials. This means that if you do not like the look of the wooden material on the bathroom stalls for example, you can switch it out with any other tileable material, including any of the provided Base Materials. You can make them marble, aluminum or painted or anything else that suits your project. It also means that you can re-use the same materials elsewhere in your project and save even more video memory.

Important if your project requires you to atlas the textures is the fact that you will be able to combine a large percentage of your objects into a just few spaces on an atlas if they all share only a handful of materials. This allows you to save a great amount of video memory and draw calls without a big sacrifice in texture quality.

The objects themselves are conservative when it comes to topology (polygon count) and as such are well suited for mobile and VR applications. Note that the bottom faces of all objects have been retained to allow objects to be viewed from any angle, but can safely be removed if need be.


2.1 What's to know?
-------------------
The included demo scenes are optimized for a linear color space and use of the Unity post-processing stack with the provided profile. Please refer to section 4 of this document for information on setting it all up.

Due to the fact that materials are shared by multiple objects, you can't edit the material of one as is without without affecting the others. If you need to do that, you can create a new material sharing the same textures and apply it to your object. I wanted to keep materials to a minimum and as such did not create a new material for each object.

The bathroom stall-, cabinet- and countertop doors are all skinned and animated. All objects have at least a linear animation (for script interactions) and a natural animation for opening and closing of doors. Note that the wooden door assets are only props and are not skinned or animated. If you need an animated version, please e-mail me and I will skin and animate it as well.

Due to the modular and flexible nature of this set a large number of child objects will be present when done building your scene. I recommend combining them (where possible) into one mesh and to create a texture atlas in order to reduce the number of draw calls when used on mobile.

2.2 Included Assets
-------------------
Skinned and animated meshes:
* 4 cabinet variations (with doors, without doors, both with or without sink)
* Bathroom stall

Static meshes:
* Fluorescent ceiling lights
* Hand dryer
* Power outlet (EU model)
* Skylight ceiling
* Soap dispenser
* Toilet
* Roll of toilet paper
* Toilet paper roll holder
* 2 trash cans
* 2 wooden doors (64cm wide and 94cm wide)

2.3 Bonus Assets
----------------
This asset pack contains a TV display and a dinged up first aid kit which I included only to make the demo scene more interesting. These items are not officially part of the pack as they do not fit the theme. The display uses a simple script which replaces its material to update the display image and should not be used in production due to the inefficacy of doing so.



====================
3.0 Included Scripts
====================
Included in this asset pack are five sample scripts in addition to two standard Unity scripts (MouseLook and a slightly modified FPSInputController.) These scripts are only provided as a way of showing how you would typically interact with the animated objects and provide little more than that.

Let's take a closer look at the scripts and their parameters.

3.1 Interactions
----------------
This is the base script for all interactivity-related scripts in this asset pack. When the player hits a key it shoots a ray from the assigned camera and checks the length to see whether it hit an InteractiveObject. If it does hit an InteractiveObject it triggers its Action() function. This script would typically be assigned to your player or camera object.

Parameters:
Cam              Camera from which you want the ray to be cast.
Interact Key     Key which triggers the interaction.
Max Reach        Maximum distance from which to trigger an interaction. If using my assets (or other properly standardized ones) a value of 1 is one meter.

3.2 InteractiveObject
---------------------
This is an abstract class from which all interactive objects need to be derived. It should not be used directly and only exists to provide the Action interface, which derived members should override.

Parameters:
None

3.3 InteractiveDoors
--------------------
Through this script you can play an opening and closing animation when the object is interacted with. It lets you supply an animation prefix, name as well as to choose whether the object is allowed to animate or not.

Note: A parent object has to own the animator as the script looks for that. This will naturally be the case with any skinned model.

Parameters:
Prefix          Animation prefix, for example "Door|" (supplying this is optional.)
Open Anim       Opening animation.
Close Anim      Closing animation.
Locked          Whether or not the door is locked (if checked, the animation won't play.)

If your object has an animation called "Door|Open Smooth" for opening and "Door|Close Linear" for closing, you would put "Door|" in the prefix section, then "Open Smooth" and "Close Linear" respectively in the Open Anim and Close Anim boxes.

3.4 Pushable
------------
Simple script showing how to integrate the InteractiveObject script. When an object with a Rigidbody is interacted with it is pushed with force in the direction the camera is looking.

Parameters:
Push Force    Force with which to push the object when interacted with.

3.5 DisplayUpdate
------------------
This is part of the bonus assets and is used to change the material on the display every so often.

NOTE: *It is not recommended to use this in a production environment.* This is not an effective way of updating a screen, rather a spritesheet containing all the textures to be displayed and a function to look them up should be used instead.

Parameters:
Screens        Array of materials to switch between.
Timer          How frequently (in seconds) should the screen be changed?

3.6 FPSInputController
----------------------
This is a slightly modified version of an FPSInputController script provided by Unity Technologies. It takes three additional inputs which will be described below.

Requirements:
A character controller component has to be attached to the object.
An object in the scene tagged as Main Camera.

Parameters:
Speed          Speed with which to move the character controller.
Jump Speed     Character controller jump speed.
Gravity        World gravity affecting the object to which the FPSInputController is attached.

Added parameters:
Flashlight     A Light object to be turned on or off at the press of the F key.
TP Roll        GameObject to be instantiated and propelled from the character in the direction which the main camera looks when the user presses "Fire."
TP Speed       Force with which the TP Roll GameObject is propelled.

3.7 MouseLook
-------------
This is a standard Unity asset. Please refer to documentation within the source file.



===================
4.0 Post-Processing
===================
Included in this asset pack is a set of two post-processing profiles, one for each of the free Unity-provided Post-processing packages.

4.1 Backup Time
---------------
I would recommend you back up your project before importing any new asset and it is highly advisable to do so before adding a post-processing stack. Please do it if you want to be able to fully reverse the following steps. To back your project up, you can simply copy your entire project folder.

4.2 Linear Color Space
----------------------
In order for the effects to look as good as possible you should change your render pipeline color space from Gamma to Linear. If you want to read more on why this is the case, you can read about it here: https://docs.unity3d.com/Manual/LinearRendering-LinearOrGammaWorkflow.html

To change this, open the Edit menu and click on Project Settings then Player. Now, in the Inspector, under "Other Settings" locate the "Color Space" option and change it from Gamma to Linear. Done!

4.3 New Post-processing stack (Unity 2017.2 and above)
------------------------------------------------------

1.  In Unity, click on the "Window" drop-down menu at the top of your screen.
2.  Click "Package Manager." This should bring up the Unity package manager.
3.  Click the "All" button on the left-hand side of the package manager window.
4.  Find the "Post-processing" package and select it.
5.  Click the "Install" button in the top right corner of the window. Allow it to install all the necessary files.
6.  Select the camera with which you want to use the filter and click "Add Component" in the Inspector.
7.  Locate "Post Process Layer" under the Rendering category and add that.
8.  In the newly added component, click the drop-down menu for Layer and select "PostProcessing."
9.  At the top of the inspector panel for the camera you have selcted, set the camera's layer to "PostProcessing" as well.
10. With the camera still selected, click "Add Component" once more and add a "Post Process Volume" from the Rendering category.
11. Check the "Is Global" checkbox on the newly added component.
12. Drag and drop the provided profile into the Profile field of the component.
13. Done!

You can now expand the newly added subcomponents under Overrides if you want to modify the post-processing effects. You can also add new effects by clicking the "Add effect..." button at the bottom of this section.

4.4 Old Post-processing stack (any version)
-------------------------------------------

1. In Unity, click on the Window drop-down menu at the top of your screen, click General and then Asset Store.
2. Once the Unity Asset Store has opened up, type "Post Processing Stack" into the search bar and select the one from "Unity Technologies."
3. Click the Import button.
4. A "Import Unity Package" window should appear. Make sure everything in this window is checked and click the "Import" button in the bottom right of it.
5. At this point Unity may prompt you to back up your files before proceeding, but we already covered that in 4.1 so click "I Made A Backup. Go Ahead!" and allow it to install.
6. After it has installed its files, select the camera with which you want to use the filter and click "Add Component" in the Inspector.
7. Locate Post-Processing Behaviour under the Effects category and add that.
8. Drag and drop the provided profile into the Profile field of the component.
9. Done!

If you want to modify the post-processing behavior, go to the Post Processing folder in the root folder of this asset and select the post processing profile. You can then edit it to suit your needs in the Unity Inspector panel.



==============
5.0 Contact Me
==============

If you want to contact me for any reason at all you can write me an e-mail at support@snarkypixel.com and I will respond as soon as I receive it. I am very happy to receive any and all feedback and would love to hear from you!

You can write me in English, German, Swedish or Finnish.

