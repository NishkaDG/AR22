using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealObjectAdder : MonoBehaviour
{
    
    Camera camera;

    [SerializeField]
    GameObject realCube;
    
    // GameObject we are going to create 
    // and its Rigidbody
    GameObject cubeObj;
    Rigidbody rigidBody;
    
    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;
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
                    /* cubeObj = GameObject.CreatePrimitive(PrimitiveType.Cube); */
                    /* // Scale of (0.2, 0.2, 0.2) */
                    /* cubeObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); */

                    /* /1* // Add Rigidbody with gravity enabled *1/ */ 
                    /* rigidBody = cubeObj.AddComponent<Rigidbody>(); */
                    /* rigidBody.useGravity = true; */
                    
                    // We have to add an offset
                    raycastPos.y += 2;

                    /* Instantiate(cubeObj, raycastPos, Quaternion.identity); */
                    Instantiate(realCube, raycastPos, Quaternion.identity);
                }
            }
        }
    }
}
