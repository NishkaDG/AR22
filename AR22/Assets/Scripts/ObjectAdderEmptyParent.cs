using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAdderEmptyParent : MonoBehaviour
{

    [SerializeField] 
    private GameObject cube;
    
    [SerializeField] 
    private GameObject sphere;
    
    [SerializeField] 
    private GameObject cylinder;
    
    
    [SerializeField] 
    private Transform plane;
    // Start is called before the first frame update
    void Start()
    {
    	Debug.Log("Hello World");
    	GameObject gameObject = Instantiate(cube, plane);
        gameObject.AddComponent<MoveAround>();
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKeyDown(KeyCode.K))
	    {
		    Instantiate(sphere, new Vector3(1.0f, 1.0f, 1.0f), new Quaternion());
	    } else if (Input.GetMouseButtonDown(0))
	    {
			RaycastHit hit;
        	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        	if (Physics.Raycast(ray, out hit)) {
            	Vector3 point = hit.point;
            	Instantiate(cylinder, point, new Quaternion());
            	// Do something with the object that was hit by the raycast.
        	}
		    
	    }
    }
}
