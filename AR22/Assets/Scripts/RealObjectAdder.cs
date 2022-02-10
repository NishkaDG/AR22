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
            List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
            if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
            {
                var hit = m_Hits[0];
                if (hit.trackable is ARPlane plane)
                {
                    Debug.Log($"Hit a plane");
                    Vector3 point = hit.pose.position;
                    var thing = Instantiate(cubePrefab, point, new Quaternion(), parent);
                }
                // Do something with the object that was hit by the raycast.
            }
        }
    }
}
