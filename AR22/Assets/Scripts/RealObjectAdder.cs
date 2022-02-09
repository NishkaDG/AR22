using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealObjectAdder : MonoBehaviour
{

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
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit)) {
                Vector3 point = hit.point;
                var thing = Instantiate(cubePrefab, point, new Quaternion());
                // Do something with the object that was hit by the raycast.
            }
        }
    }
}
