using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RealObjectAdder : MonoBehaviour
{
    
    Camera camera;

    [SerializeField]
    GameObject prefab;
    
    [SerializeField]
    GameObject placeholder;
    GameObject activePlaceholder;

    [SerializeField]
    ARRaycastManager raycastManager;
    
    // GameObject we are going to create 
    // and its Rigidbody
    GameObject cubeObj;
    Rigidbody rigidBody;
    
    Vector3 raycastPos, placePos;
    
    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;
        activePlaceholder = null;     
    }
    
    public void AddCube() {
        if (activePlaceholder) {
            Instantiate(prefab, raycastPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        
        // SHOULD WE POSITION THE PLACEHOLDER ?
        placePos = camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        ray = camera.ScreenPointToRay(placePos);

        if (raycastManager.Raycast(placePos, hits)) {
            raycastPos = ray.GetPoint(hits[0].distance);
             // We want to position it underneath the plane
            if (activePlaceholder) {
                activePlaceholder.transform.position = raycastPos;
            } else {
                activePlaceholder = Instantiate(placeholder, raycastPos, Quaternion.identity);
            }
        } else if (activePlaceholder) {
            Destroy(activePlaceholder);
        }

    }
}
