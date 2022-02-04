# Assignment 1 - Unity basics (ungraded)

**Date**: [TODO: date]

**Group members participating**: Rasmus Thorsøe and Sam Martin Vargas Giagnocavo (Luis?).

**Activity duration**: [TODO: Insert hours spend]

## Goal
- Form a group.
- Create Gitlab repository to store our code and reports.
- Understand some of Unity's basic functionality (such as inserting objects, scripting, interaction and colliders).
- Build a large portion of what can become our AR application.

## Plan
To have a better undestanding of how Unity works we decided not to split the workload for this assignment. Each member would have to do the exercises and once finished we would start writing the report.

## Results

Exercise 1 and 2 were completed during the TA session, and we both started doing exercise 3. We both completed exercise 3 seperately, as planned. 

### <ins>Exercise 3.1</ins>
The first step was creating a new scene called *Assignment 1*. This was done by selecting *File → New Scene* in the menu bar.

Once the scene was created we added a 3D plane and a 3D Cube to the scene. This could be done by clicking on the *+* button located in the Hierarchy pane. An empty GameObject called *BasicBehaviour* was also added. Unity's preview view now displays the following:
![preview](media/assignment_1/preview.png)

Next, we created a cube prefab (inside the Prefabs folder) from the 3D Cube we previously inserted in the scene.

Finally, we created the `ObjectAdder.cs` script that would display a *Hello World* message and place a cube prefab on top of the plane. Since the prefab had to be instantiated, a *SerializeField* was added so that we could point the cube prefab to the script.
```c#
public class ObjectAdder : MonoBehaviour
{

    [SerializeField]
    public Transform prefab;

    void Start()
    {
        float planeY;
        GameObject plane;
        Vector3 cubeSize, cubePos, planePos;
        // Retrieve the plane from the scene
        plane = GameObject.Find("Plane");

        // Get the plane's position
        planePos = plane.transform.position;

        // Get the plane's Y value
        planeY = planePos.y    

        // Get the prefab's size
        cubeSize = prefab.Find("Cube").GetComponent<MeshRenderer>().bounds.size;
        
        // The cube is on top of the plane now...
        cubePos = new Vector3(0, planeY, 0);
        // But we still need to add an offset (half of the cube's Y value)
        cubePos += new Vector3(0, cubeSize.y / 2, 0);
        
        // We create the prefab :D
        Instantiate(prefab, cubePos, Quaternion.identity);
    }

    void Update() {}
}
```

To properly position the prefab on top of the plane (without cutting through it), an offset had to be added in the `Instantiate` function. **We later found out that this is not ideal way of doing it**. If we were to use an empty *GameObject* as a parent for our cube and apply the offset there, we would not need to modify the code at all. Instead we could just edit the position of the cube, in relation to the empty parent, inside the prefab. This makes it far easier to change the position of the cube, This technique is especially relevant when you have a number of object you want to place in relation to eachother, then you can easily iterate different ideas in the editor, and not have to change anything in the script. 

The Start() code can then be simplified to this: 

```c#
void Start()
    {
        float planeY;
        GameObject plane;
        Vector3 cubeSize, cubePos, planePos;
        // Retrieve the plane from the scene
        plane = GameObject.Find("Plane");

        // Get the plane's position
        planePos = plane.transform.position;

        // Get the plane's Y value
        planeY = planePos.y    
        
        // The cube is on the plane now...
        cubePos = new Vector3(0, planeY, 0);

        // We create the prefab :D
        Instantiate(prefab, cubePos, Quaternion.identity);
    }
```

The hierarchy in the prefab is as follows: 
![empty_cube_parent](media/assignment_1/empty_cube_parent.png)

### <ins>Exercise 3.2</ins>
For this exercise we created a simple material (Ground) inside of the *Materials* folder in our project.

Initially we assigned the material a red color by changing the *Albedo* value in the inspector pane. After that we assigned it to the plane. This was the result:
![red_plane](media/assignment_1/red_plane.png)

Once that was done, we searched for a texture to add to our new material. We created a *Textures* folder and saved a dirt texture in it. After that we dragged the image to the *Albedo* value in the inspector view of our new material. The tiling in the material was slightly modified to make it look better. An example of the plane using a dirt texture can be seen below.
![dirt_plane](media/assignment_1/dirt_plane.png)

### <ins>Exercise 3.3</ins>
We have chosen a simple capsule as our `midair` object, and a sphere as our `raycast` object. The objects are placed as follows: 
```c#
void Update()
{
    Ray ray;
    RaycastHit hit;
    
    if (Input.GetKeyDown(KeyCode.K)) {
        Instantiate(midairPrefab, midairPos, Quaternion.identity, parent.transform);
    } else if (Input.GetKeyDown(KeyCode.Mouse0)) {
        ray = camera.ScreenPointToRay(Input.mousePosition);
    
        if (Physics.Raycast(ray, out hit)) {
            raycastPos = ray.GetPoint(hit.distance);
            raycastPos.y += raycastSize.y / 2;
            Instantiate(raycastPrefab, raycastPos, Quaternion.identity, parent.transform);
        }
    }
}
```
In 3.1 we created the cube inside Start(), but in 3.1 we created the raycast and midair objects inside Update(). This makes sense, since Start() is only run once per object, when the object is created, and we only wanted the one cube, and we wanted it immediately. Update() is run once per frame, and should be used for the behaviour of the object, in this case the behaviour of the object is to place the `midair` and ``raycast` objects on demand. 

INSERT GIF?

### <ins>Exercise 3.4</ins>
With the `MoveAround.cs` script created from the Project pane, we add the following line at the end of the *Start* function in `ObjectAdder.cs`.
```c#
GameObject.Find("Cube").AddComponent<MoveAround>();
```
Once we have checked that the script does indeed load alongside the cube. We modify the script to make the cube move around.

```c#
void Update()
{
    float x, y, z;
    x = transform.rotation.x;
    y = transform.rotation.y;
    z = transform.rotation.z;
    if (Input.GetKey(KeyCode.W))
    {
        transform.position += new Vector3(0, 0, 0.1f);
    }
    else if (Input.GetKey(KeyCode.A))
    {
        transform.position -= new Vector3(0.1f, 0, 0);
    }
    else if (Input.GetKey(KeyCode.S))
    {
        transform.position -= new Vector3(0, 0, 0.1f);
    }
    else if (Input.GetKey(KeyCode.D))
    {
        transform.position += new Vector3(0.1f, 0, 0);
    }
    else if (Input.GetKey(KeyCode.Q))
    {
        transform.RotateAround(transform.position, -transform.up, Time.deltaTime * 90f);
    }
    else if (Input.GetKey(KeyCode.E))
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
    }
}
```

The GIF below shows a small example of the cube's movement.
![movement](media/assignment_1/movement.gif)

### <ins>Exercise 3.5</ins>
When we removed the Collider from the 3D Plane, the raycast no longer hits the 3D Plane. In general, Raycast shoots a ray from a point towards a direction, registering any objects hit along the way, only objects with a collider are registered. When we then added a RigidBody to our `midair` prefab, we observed that our `midair` capsule started falling down through the plane. If we readded the Collider to the 3D Plane, we observed that the `midair` capsule fell onto the plane, and if multiple `midair` capsules were added, the capsules would collide and start rolling away from eachother. The RigidBody component adds basic physics properties to an object, like gravity.

We also tried changing the collider on our `midair` prefab to be of a different shape. This caused the `midair` capsules to behave as if they were of that shape. For example, when we reduced the height of the Capsule Collider from 2 to 1, we observed that the capsule would go halfway through the plane.

![half_capsule](media/assignment_1/half_capsule.png)

### <ins>Exercise 3.6</ins>

In the `midair` prefab we added a 3D Text Object above the sphere, however the text is always facing directly "forwards", potentially making it rather difficult to read if viewed from a sharp angle.

![text_flat](media/assignment_1/text_flat.png)

To solve this, we rotated the text to always face the camera position using the lookAt() function, however the text is facing directly away from the camera. To solve this we rotated the text 180 degrees (from the result of the lookAt() function) around the y axis. 

![text_correct](media/assignment_1/text_correct.png)


### <ins>Exercise 3.7</ins>
Fortunately "cleaning up" the project was not needed since we followed the suggestions that were given to us at the beginning of the TA session. Our folder structure is as follows:
![folder_structure](media/assignment_1/folder_structure.png)

However, we did have to modify the code so that new objects would be added to an empty parent object. This was achieved by creating a *GameObject* inside of `ObjectAdder.cs`, initializing it and using its transform value to instantiate the new objects. A simplified version of the code can be seen below.
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAdder : MonoBehaviour
{

    // Variables...
    
    private GameObject parent;

    void Start()
    {
        // Variables...
        
        // Create empty parent
        parent = new GameObject("Generated Objects");

        // More code...
        
        Instantiate(prefab, cubePos, Quaternion.identity, parent.transform);
        
        // More code...
    }

    // Update is called once per frame
    void Update()
    {
        // More code...
        
        if (Input.GetKeyDown(KeyCode.K)) {
            Instantiate(midairPrefab, midairPos, Quaternion.identity, parent.transform);
        } else if (Input.GetKeyDown(KeyCode.Mouse0)) {
            // More code...
        }
    }
}
```
### <ins>Exercise 4.1</ins>
Ideas?

## Conclusion
[TODO: conclusions of this weeks exercises]

## References
- [Unity Docs](https://docs.unity3d.com/Manual/index.html)
