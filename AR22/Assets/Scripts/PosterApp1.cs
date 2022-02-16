using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

public class PosterApp1 : MonoBehaviour
{
    
    public void DeleteAll() {
		var objects = GameObject.FindObjectsOfType(GameObject);
		for(o : GameObject in objects) {
			Destroy(o);
		}
	}
}
