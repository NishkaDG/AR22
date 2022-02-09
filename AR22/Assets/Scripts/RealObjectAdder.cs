using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	[SerializeField]
	public GameObject cubeToDrop;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Ray myRay;
		RaycastHit hit;
		Touch touch;
		for(int i = 0; i < Input.touchCount; i++) {
			touch = Input.GetTouch(i);
			if (touch.phase.Equals(TouchPhase.Began)) {
				myRay = Camera.main.ScreenPointToRay(touch.position);
				if(Physics.Raycast(myRay, out hit)) {
					//Vector3 dropPoint = hit.point + new Vector3(0, 0.1f, 0);
					Vector3 dropPoint = myRay.GetPoint(hit.distance);
					GameObject newCube = Instantiate(cubeToDrop, dropPoint, Quaternion.identity);
				}
			}
		}
        
    }
}
