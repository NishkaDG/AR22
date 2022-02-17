using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

public class RealObjectInteractor : MonoBehaviour
{
	private Nullable<Vector3> currentposition;
	private GameObject currentObject;

	[SerializeField] 
	private GameObject selectionIndicator;

	void Update()
	{
		if (Input.touchCount > 0) {
			
			Touch touch = Input.GetTouch(0);
			
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(touch.position);
			if (!Physics.Raycast(ray, out hit))
			{
				return;
			}

			switch (touch.phase)
			{
				case TouchPhase.Began:
				{
					// Remove children from existing selection, only 1 selected item at a time
					if (currentObject != null)
					{
						foreach (Transform child in currentObject.transform)
						{
							Destroy(child.gameObject);
						}
					}

					if (currentObject == hit.transform.gameObject)
					{
						currentObject = null;
						currentposition = null;
						return;
					}

					currentposition = hit.point;
					currentObject = hit.transform.gameObject;
					var bounds = currentObject.GetComponent<Renderer>().bounds;
					var indicator = Instantiate(selectionIndicator, hit.transform);
					Vector3 indicatorScaleOffset = new Vector3(0.1f, 0.1f, 0.1f);
					Vector3 indicatorTranslateOffset = new Vector3(0f, -1f, 0f);
					indicator.transform.localScale = indicator.transform.localScale + indicatorScaleOffset;
					indicator.transform.localPosition = indicator.transform.localPosition + indicatorTranslateOffset;
					Debug.Log(currentObject.name);
					Debug.Log(bounds.ToString());
					break;	
				}
				case TouchPhase.Moved:
				{
					break;
				}
				case TouchPhase.Canceled:
				case TouchPhase.Ended:
				{
					currentObject = null;
					currentposition = null;
					break;
				}
			}
			


			
		}
	}
}
