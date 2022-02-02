using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAdderScript : MonoBehaviour
{

    [SerializeField] 
    private GameObject myPrefab;
    
    [SerializeField] 
    private Transform plane;
    // Start is called before the first frame update
    void Start()
    {
    	Debug.Log("Hello World");
    	Instantiate(myPrefab, new Vector3(plane.position.x, plane.position.y + 0.5f, plane.position.y), new Quaternion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
