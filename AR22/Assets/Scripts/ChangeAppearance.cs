using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAppearance : MonoBehaviour
{
    private bool animated = false;
	private float timer;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }
	
	void ToggleAnimated() {
		animated = !animated;
	}

    // Update is called once per frame
    void Update()
    {
		if(animated) {
			timer = timer + Time.deltaTime;
			if(timer >= 0.5) {
				this.GetComponent<Text>().enabled = true;
			}
			if(timer >= 1) {
				this.GetComponent<Text>().enabled = false;
				timer = 0;
			}
		}
		
		else {
			this.GetComponent<Text>().enabled = false;
		}
	}
}
