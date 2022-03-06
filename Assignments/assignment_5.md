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

### <ins>Tracked Image Marker Interaction</ins>
#### Adding new light sources to the scene.


<table>
  <colgroup>
  <colgroup span="2">
  <colgroup span="2">
  <tr>
    <th colspan="3">Input techniques</th>
    <th colspan="3">Output techniques</th>
  </tr>
  <tr>
    <th>Tracked image</th>
    <th>Relation</th>
    <th>Other</th>
    <th>Change appearance</th>
    <th>Animate object</th>
    <th>Other</th>
  </tr>
  <tr>
    <td>✓</td>
    <td>X</td>
    <td>X</td>
    <td>✓</td>
    <td>X</td>
    <td>X</td>
  </tr>
</table>

For our app we thought an ideal use of an image marker would be to add new light sources to the scene. The user would be able to move these light sources around and see how each poster reacts to the light.

In this case, we would be able to reuse most of the code from *Assignment 2* but we would have look for a lamp prefab in the Unity Asset Store. We settled for this [one](https://assetstore.unity.com/packages/3d/props/interior/lamp-model-110960). After a few modifications to the original prefab we were ready to add it to the scene.

Following the steps from *Assignment 2* we added an *AR Tracked Image Manager* to the *AR Session Origin* in our scene (we had removed it previously), and we would then assign our *Reference Image Library* to it.

The code used for this interaction is identical to the one in *Assignment 2* since the lighting processing is handled by Unity. A demonstration can be seen below:

<center>
<img src="media/assignment_5/lamp.gif" height="450" />
</center>


### <ins>Poster animation tied to proximity</ins>
#### Rotation triggered when the user comes close to a poster.

<table>
  <colgroup>
  <colgroup span="2">
  <colgroup span="2">
  <tr>
    <th colspan="3">Input techniques</th>
    <th colspan="3">Output techniques</th>
  </tr>
  <tr>
    <th>Tracked image</th>
    <th>Relation</th>
    <th>Other</th>
    <th>Change appearance</th>
    <th>Animate object</th>
    <th>Other</th>
  </tr>
  <tr>
    <td>X</td>
    <td>✓</td>
    <td>X</td>
    <td>X</td>
    <td>✓</td>
    <td>X</td>
  </tr>
</table>

<center>
  <img src="media/assignment_5/distance.gif" height="450" />
</center>

### <ins>3 Finger Touch → Swap poster</ins>
The user should be able to easily replace a poster with a different poster. To facilitate this, we have added an interaction using 3 simultaneous touches. If the user has selected an existing poster and then taps anywhere on the screen with 3 fingers, the poster will be swapped out for the next poster. The order of the posters is the same as in the object catalogue. The code for this can be seen here:

```c#
// Code inside the Update() function

if (touches.Length == 3 
    && touches[0].phase == TouchPhase.Began 
    && touches[1].phase == TouchPhase.Began
    && touches[2].phase == TouchPhase.Began
    && this.selectedObject)
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

<center>
  <img src="media/assignment_5/3-tap.gif" height="450" />
</center>

## Conclusion
[TODO: conclusions of this weeks exercises]

## References
- [Unity Docs](https://docs.unity3d.com/Manual/index.html)
