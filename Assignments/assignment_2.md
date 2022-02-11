# Assignment 2 - AR Foundation basics (ungraded)

**Date**: 11/02/2022

**Group members participating**: Rasmus Thorsøe, Sam Martin Vargas Giagnocavo, Nishka Dasgupta

**Activity duration**: [TODO: Insert hours spend]

## Goal
- Begin building a phone app.
- Learn basic Unity functionality such as scene creation, plane detection, object tracking.
- Create a mobile app that can add an object tethered to a plane in the physical world.

## Plan
As in the previous week, we initially decided that each member should work on all exercises independently, to facilitate learning. 

## Results
Exercise 1 was completed by all of us in the TA session. Over the course of the week we each worked independently on the remaining exercises and
made different degrees of progress. On Friday we decided to split the remaining coding work between Sam and Rasmus, with Nishka and Sam
working on the report.

### <ins>Exercise 1.2</ins>
After building the application as a phone app, it shows approximate planes being tracked in the physical world, e.g a bed or a windowsill. In low light conditions it does not do
well with planes that are close to the camera, e.g the laptop keyboard a few inches away. On occasion the application will appear to track planes
where none exist, e.g in mid-air.

### <ins>Exercise 2.1</ins>

For this exercise we created a cube object with the given parameters (side length 0.2, rigidbody with gravity enabled). We then made it a prefab
and deleted the object.
The cube object appears to have sides of length slightly less than 30cm, when compared against various objects in the physical world, both near and far 
from the camera. As the side of the cube prefab was set to 0.2 units in unity, we can assume that 1 unit in unity corresponds to a little over 1 metre
in the physical world.

When we enter a cube at the same position as an existing cube, the cubes explosively repel each other and are flung to different corners of the space.

### <ins>Exercise 2.2</ins>
[Explain why your objects fall through the ground.

Try to place an object on top of another object. Explain what happens and why?


Explain the difference between the two different raycast functions (Unity's build-in and AR Foundation's)?
- When will you use each function?
- Can you combine them in a meaningful way?
- If you want to use gravity in your project, how can you change the design of the debug planes to look nicer?]

In this exercise, we began to use the AR Foundation raycast. This initially caused our cubes to fall through the ground. 
The gravity feature of the cube prefab was enabled. Further, within the rigidbody feature, collision detection was set to discrete, i.e, at specific
discrete time intervals, the object would move a fixed number of units, according to the force applied on it (in this case, gravity). This meant that
the game may not be able to detect the collision at all since the cube only changed position directly without passing through intermediate points (the
ground). On disabling rigidbody, the object no longer falls through the ground.

Now, on adding an object on top of the current object, the new object simply applies just in front of the old object. 

The Unity built-in raycast function tracks objects in the physical world, whereas AR Foundation's raycast tracks trackable AR objects that may not
exist in the physical world.


### <ins>Exercise 2.4</ins>
[Optional: why we chose a particular indication marker]

### <ins>Exercise 2.5</ins>
[Explain and demonstrate the objects you have created.]

### <ins>Exercise 3.1</ins>
[Explain what happens when you move the physical image.]

### <ins>Exercise 3.2</ins>
[Explain: how does the two MO's look compared to each other? Why do they look this way?]

## Conclusion
During this assignment we learned the basics of mixing the virtual world with the physical world in Unity. These concepts will be vital to our final project.

We also further refined our final project idea.

## References
- [Unity Docs](https://docs.unity3d.com/Manual/index.html)
