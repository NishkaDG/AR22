using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RealObjectAdder : MonoBehaviour
{
    
    Camera camera;

    [SerializeField]
    GameObject realCube;
    
    [SerializeField]
    GameObject placeholder;
    GameObject activePlaceholder;

    [SerializeField]
    ARRaycastManager raycastManager;
    
    // GameObject we are going to create 
    // and its Rigidbody
    GameObject cubeObj;
    Rigidbody rigidBody;
    
    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;
        activePlaceholder = null;     
    }
    // Update is called once per frame
    void Update()
    {
        
        Ray ray;
        Touch touch;
        Vector3 raycastPos, placePos;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        
        // SHOULD WE POSITION THE PLACEHOLDER ?
        placePos = camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        ray = camera.ScreenPointToRay(placePos);

        if (raycastManager.Raycast(placePos, hits)) {
            raycastPos = ray.GetPoint(hits[0].distance);
             // We want to position it underneath the plane
            if (activePlaceholder) {
                Debug.Log("WE SHOULD MOVE THE PLACEHOLDER");
                activePlaceholder.transform.position = raycastPos;
            } else {
                Debug.Log("WE SHOULD CREATE THE PLACEHOLDER");
                activePlaceholder = Instantiate(placeholder, raycastPos, Quaternion.identity);
            }

            // PLACING CUBES
            if (Input.touchCount > 0) {

                touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began) {
                    
                    Instantiate(realCube, raycastPos, Quaternion.identity);
                    
                    /* ray = camera.ScreenPointToRay(touch.position); */
                    
                    /* if (raycastManager.Raycast(touch.position, hits)) { */
                    /*     raycastPos = ray.GetPoint(hits[0].distance); */
                        
                    /*     // We have to add an offset */
                    /*     /1* raycastPos.y += 2; *1/ */

                    /*     Instantiate(realCube, raycastPos, Quaternion.identity); */
                    /* } */
                }
            }
        } else if (activePlaceholder) {
            Destroy(activePlaceholder);
        }

    }
}
