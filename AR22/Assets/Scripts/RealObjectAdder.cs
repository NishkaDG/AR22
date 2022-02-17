using System;
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
    
    [SerializeField]
    Renderer placeholderRenderer;

    [SerializeField]
    ARRaycastManager raycastManager;

    [SerializeField] 
    private ButtonHandler callback;


    // Start is called before the first frame update
    void Start() {
        camera = Camera.main;
        Debug.Log("test");
    }
    
    public void AddObject(Vector3 position, Quaternion rotation) {
        if (placeholderRenderer.enabled) {
            GameObject s1 = Instantiate(prefab, position, rotation);
			s1.tag = "art";
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        // Move the placeholder
        var placeHolderPosition = camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));

        if (raycastManager.Raycast(placeHolderPosition, hits)) {
            var hit = hits[0];
            if (hit.trackable is ARPlane plane)
            {
                placeholderRenderer.enabled = true;
                placeholder.transform.position = hit.pose.position;
                placeholder.transform.rotation = hit.pose.rotation;
            }
            
            // check for input to place object
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase != TouchPhase.Began)
                {
                    return;
                }
                AddObject(placeholder.transform.position, placeholder.transform.rotation);
                callback.FinishedAdding();
                placeholderRenderer.enabled = false;
            }
        } else {
            placeholderRenderer.enabled = false;
        }
    }
}
