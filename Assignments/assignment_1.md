# Assignment 1 - Unity basics (ungraded)

**Date**: 02/02/2022

**Group members participating**: Rasmus Thorsøe, Sam Martin Vargas Giagnocavo

**Activity duration**: 5 hours per person 

## Goal
The goal of this weeks exercises was to form a group, create a gitlab repository for our code and reports, and to familiarize ourselves with using Unity. 

## Plan
For this assignment, we decided to do the exercises sequentially. We also decided to both do exercise 3 seperately, so that we both could familiarize ourselves with Unity. For exercise 4 we plan to do a brainstorming session together. 

## Results
Exercise 1 and 2 were completed during the TA session, and we both started doing exercise 3. We both completed exercise 3 seperately, as planned. 

Exercise 3.1 - Explain the differences between altering your script and adding a parent object: If you alter your script to place the cube nicely on the 3d plane, ther everytime the cube changes, you will have to edit the script and recompile. If you instead use an empty parent object in your prefab, you can in the editor specify that the cube should have a y of 0.5, which will be relative to the empty object. In the script you can then simply Instantiate the cube at the planes coordinate, substantially simplifying the script. This makes it far easier to change the position of the cube, This technique is especially relevant, when you have a number of object you want to place in relation to eachother, then you can easily iterate different ideas in the editor, and not have to change anything in the script. 

Exercise 3.3 - Explain the diference between Start() and Update(): Start() is only run once per object, when the object is created. You should use Start() to initialize your object. Update is run once per frame, and should be used for the behaviour of the object. 

Exercvise 3.5.1 -  Explain what happens when you remove the collider from the 3D plane: With the collider removed, the raycast no longer hits the #d Plance. In general, Raycast shoots a ray from a point towards a direction, registering any objects hit along the way, only objects with a collider are registered.

Exercise 3.5.2 - Explain what happens when you add a rigid body to the midar prefab: Rigid body adds physics to an objects, in this case the midair objects start fallign through the plane, since the plane does not have a collider. If the collider is added back to the plane, the midair object does not fall through. If multiple midair objects are created, they push eachother around. 

Ecercise 3.5.3 - Explain what happens when you change the collider: When the collider is changed, the object behaves as if it has the form specified by the collider, regardless of the visual shape of the object. For example, if the collider is changed from a Sphere Collider with radius 0.5, to a radius 0.25, the object will sink halfway through the plane.

Exercise 3.6.1 - Explain how the text looks when you place objects at different locations: The text looks 2D, as if it has no depth. The text is always facing directly "forwards", this causes it to seem like the text is plastered onto the screen, rather than being a part of the virtual world. 

Exercise 3.6.2 - Explain how the text looks (after making the text always face the camera): The text now looks like it has depth, and appears to be a part of the virtual world. The text was initially facing in the opposite direction, which was fixed by setting the x-scale to be negative, causing the text to flip along the x axis. 



## Conclusion
[TODO: conclusions of this weeks exercises]

## References
[TODO: used references]
