using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

public class RealObjectInteractor : MonoBehaviour
{
	

	void Update()
	{
		if (Input.touchCount > 0) {
			
			Touch touch = Input.GetTouch(0);
			if (touch.phase != TouchPhase.Began)
			{
				return;
			}

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(touch.position);
			if (Physics.Raycast(ray, out hit)) {
				Vector3 point = hit.point;
				Debug.Log("hit object");
				Debug.Log(hit.transform.gameObject.name);
			}
		}
	}
}
