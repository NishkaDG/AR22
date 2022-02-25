using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class ChangeAppearance : MonoBehaviour
{
    private bool animated = true;
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
	    Debug.Log("change appearance");
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
}
