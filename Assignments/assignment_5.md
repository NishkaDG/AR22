# Assignment 5 (graded)

**Date**: 06/03/2022

**Group members participating**: Rasmus Thorsøe, Sam Martin Vargas Giagnocavo and Nishka Dasgupta.

**Activity duration**: 7-8 hours each

## Goal
- Create more interesting/meaningful objects
- Experiment with different interaction techniques

## Plan
The plan for this weeks exercises was for us to brainstorm ideas for interesting interactions. Then each of us would implement one of the interactions. 

## Results


### <ins>3 Finger Touch -> Swap poster</ins>
The user should be able to easily replace a poster with a different poster. To facilitate this, we have added an interaction using 3 simultaneous touches. If the user has selected an existing poster and then taps anywhere on the screen with 3 fingers, the poster will be swapped out for the next poster. The order of the posters is the same as in the object catalogue. The code for this can be seen here: The code is run on Update()

```c#
if (touches.Length == 3 
    && touches[0].phase == TouchPhase.Began 
    && touches[1].phase == TouchPhase.Began
    && touches[2].phase == TouchPhase.Began
    &&this.selectedObject)
{
    Renderer renderer = this.selectedObject.GetComponent<Renderer>();
    Material mat = renderer.material;
    int index = Array.FindIndex(materials, material =>
    {
        return mat.name.Contains(material.name); // Finds which material (index) the currently selected object has. 
    });
    if (index != -1)
    {
        int new_index = (index + 1) % materials.Length;
        renderer.material = materials[new_index];
        SelectObject(this.selectedObject); // reapplies the selection indicator
    }
}
```

## Conclusion
[TODO: conclusions of this weeks exercises]

## References
[TODO: used references]
