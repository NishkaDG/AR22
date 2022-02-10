using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RealObjectAdder : MonoBehaviour
{
    
    [SerializeField]
    ARRaycastManager m_RaycastManager;

    [SerializeField] 
    private GameObject cubePrefab;
    
    [SerializeField] 
    private GameObject marker;
    
    [SerializeField] 
    private Transform parent;
    
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
            {
                return;
            }
            Instantiate(cubePrefab, marker.transform.position, marker.transform.rotation, parent);
        }
    }
}
