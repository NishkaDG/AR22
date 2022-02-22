using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

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
				this.GetComponent<Button>().enabled = true;
			}
			if(timer >= 1) {
				this.GetComponent<Button>().enabled = false;
				timer = 0;
			}
		}
		
		else {
			this.GetComponent<Button>().enabled = false;
		}
	}
}
