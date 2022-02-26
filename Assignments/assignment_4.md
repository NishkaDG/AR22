# Assignment 4 - Interaction techniques (graded)

**Date**: 20/02/2022

**Group members participating**: Rasmus Thorsøe, Sam Martin Vargas Giagnocavo and Nishka Dasgupta.

**Activity duration**: 7-8 hours

## Goal
- Further develop our AR app.
- Enable an expanded selection of objects for users to choose from.
- Enable customisation and animation of objects.

## Plan
The plan was for Sam to work on Exercise 1 (designing the catalogue for our posters), and for Nishka and Rasmus to do Exercises 2.1 (animation) and 2.2 (customisation)
respectively. For the catalogue we planned to keep it in its previous position (a horizontal bar near the bottom of the screen), accessible via a 
button with a shopping bag logo on it. For animation, we decided to have blinking text displaying the name of the selected poster. For customisation
we decided to let the user select if they wanted the texture of the poster to be matte or glossy.

## Results


### Exercise 1
[TODO: description]

### Exercise 2.1
For the animation we decided to go with blinking text displaying the name of the artwork that the user has placed from the catalogue. The name of the artwork is 
a 3DText object created as the child of each artwork. We then use a timer to periodically enable and disable it for as long as the toggle for animation is on. The code for the animation can be seen here: 

```c#
void Update()
    {
	    if(animated) {
			timer = timer + Time.deltaTime;
			if(timer >= 0.5) {
				this.transform.GetChild(0).gameObject.SetActive(true);
			}
			if(timer >= 1) {
				this.transform.GetChild(0).gameObject.SetActive(false);
				timer = 0;
			}
		}
		else {
		    this.transform.GetChild(0).gameObject.SetActive(false);
		}
	}
```

The script is attached to any piece of artwork the user has placed in the world. The "animated" boolean is toggled on and off when the corresponding button is clicked. 
The animation can be seen in the following video: 

INSERT VIDEO OF ANIMATION

### Exercise 2.2
For customisation we decided to let the user choose real-world texture of the object, i.e, should the artwork be matte or glossy. The code can be seen here: 
```c#
public void toggleGlossy()
    {
        Material mat = this.selectedObject.GetComponent<Renderer>().material;
        float currentGlossiness = mat.GetFloat("_Glossiness");

        if (currentGlossiness == 0.0)
        {
            mat.SetFloat("_Glossiness", 1);
            mat.SetFloat("_Metallic", 1);
        }
        else
        {
            mat.SetFloat("_Glossiness", 0);
            mat.SetFloat("_Metallic", 0);
        } 
    }
```

Here you can see the effect in the app, the poster on the left is maximum metallic/glossy, and the poster on the right is minimum metallic glossy. 
The effect is not quite what we had hoped for, and is not what we would expect to see in real life. This is probably due to our AR app not properly understanding the lighting in the real world, as the appearance of glossy/metallic surfaces are very dependent on the exact location, intensity and colour of light. 

INSERT VIDEO

## Conclusion
During this weeks exercises we have learned how to arrange the UI such that it works regardless of the users phone orientation.
We have created more interesting objects, and learned how to toggle states on and off for individual objects, and how to do simple animations in unity. 
We have also learned that the use of light (or materials that require light) in AR applications is not trivial. 

## References
- [Unity Docs](https://docs.unity3d.com/Manual/index.html)
