using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

public class PosterApp1 : MonoBehaviour
{
    
    public void DeleteAll() {
		GameObject[] go = GameObject.FindGameObjectsWithTag("art");
		for (int i = 0; i < go.Length; i++)
		{
			Destroy(go[i]);
		}
	}
}
