using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

public class PosterApp1 : MonoBehaviour
{
    
    public void DeleteAll() {
		GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);
		for (int i = 0; i < GameObjects.Length; i++)
		{
			Destroy(GameObjects[i]);
		}
	}
}
