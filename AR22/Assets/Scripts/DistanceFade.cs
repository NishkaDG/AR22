using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class DistanceFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	    
    }

    // Update is called once per frame
    void Update()
    {
		GameObject[] go = GameObject.FindGameObjectsWithTag("poster");
		Vector3 campos = Camera.main.transform.position;
		for (int i = 0; i < go.Length; i++) {
			GameObject obj = go[i];
			var dist = Vector3.Distance(obj.transform.position, campos);
			if(dist < 0.7) {
				obj.transform.RotateAround(obj.transform.position, obj.transform.up, Time.deltaTime * 90f);
			}
		}
    }
}
