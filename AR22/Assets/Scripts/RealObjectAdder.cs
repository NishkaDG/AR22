using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealObjectAdder : MonoBehaviour
{
    
    Camera camera;

    // GameObject we are going to create 
    GameObject cubeObj;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;     
        cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubeObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        cubeObj.AddComponent<Rigidbody>();
        cubeObj.GetComponent<Collider>()
            .attachedRigidbody.useGravity = true;        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Raycast stuff
        Ray ray;
        RaycastHit hit;
        Vector3 raycastPos;
        
        // Input-related stuff
        Touch touch;
        
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began) {
                ray = camera.ScreenPointToRay(touch.position);
            
                if (Physics.Raycast(ray, out hit)) {
                    raycastPos = ray.GetPoint(hit.distance);
                    Instantiate(cubeObj, raycastPos, Quaternion.identity);
                }
            }
        }
    }
}
