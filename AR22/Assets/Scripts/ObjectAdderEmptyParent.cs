using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAdderEmptyParent : MonoBehaviour
{

    [SerializeField] 
    private GameObject myPrefab;
    
    [SerializeField] 
    private Transform plane;
    // Start is called before the first frame update
    void Start()
    {
    	Debug.Log("Hello World");
    	Instantiate(myPrefab, plane);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
