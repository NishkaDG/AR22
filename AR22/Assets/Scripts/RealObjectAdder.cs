using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

public class RealObjectAdder : MonoBehaviour
{
    
    Camera camera;

    [SerializeField]
    GameObject placeholder;
    
    private bool hasMoved;
    private bool checkForMovement;

    private Material materialToAdd;
    private GameObject selectedObject;
    
    [SerializeField]
    Renderer placeholderRenderer;
    private bool showingPlaceholder;

    [SerializeField]
    ARRaycastManager raycastManager;
    
    [SerializeField]
    Button placeItemButton;

    [SerializeField]
    Button deleteItemButton;

    [SerializeField]
    Button deleteAllButton;
    
    // Start is called before the first frame update
    void Start() {
        this.camera = Camera.main;
        this.materialToAdd = null;
        this.showingPlaceholder = false;
        this.placeholderRenderer.enabled = false;
        
        // Button textures
        this.placeItemButton.interactable = false;
        this.deleteItemButton.interactable = false;
        this.deleteAllButton.interactable = false;
    }

    public void ChangeObjectToAdd(Material material) {
        if (this.materialToAdd == material) {
            this.materialToAdd = null;
            placeItemButton.interactable = false;
        } else {
            this.materialToAdd = material;        
            placeItemButton.interactable = true;
        }
    }

    public void AddObject() {
        GameObject obj;
        Vector3 position;
        Quaternion rotation;
        
        if (showingPlaceholder && this.materialToAdd) {
            position = placeholder.transform.position;
            rotation = placeholder.transform.rotation;
            obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            obj.GetComponent<Renderer>().material = this.materialToAdd;
            obj.tag = "poster";
            
            this.deleteAllButton.interactable = true;
        }
    }
    
    public void DeleteSelectedObject() {
        if (this.selectedObject) {
            Destroy(this.selectedObject);
            this.deleteItemButton.interactable = false;
        }
    }
    
    public void DeleteAllObjects() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("poster");
        
        foreach (var obj in objs) {
            Destroy(obj);
        }
        
        this.selectedObject = null;
        this.deleteAllButton.interactable = false;
        this.deleteItemButton.interactable = false;
    }
    
    void UpdatePlaceholder() {
  
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        // Move the placeholder
        var placeHolderPosition = camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));

        if (raycastManager.Raycast(placeHolderPosition, hits)) {
            var hit = hits[0];
            if (hit.trackable is ARPlane plane) {
                placeholder.transform.position = hit.pose.position;
                placeholder.transform.rotation = hit.pose.rotation;
                placeholderRenderer.enabled = this.showingPlaceholder = true;
            }
        } else {
            placeholderRenderer.enabled = this.showingPlaceholder = false;
        }
        
    }
    
    void CheckUserInput() {
        Ray ray;
        RaycastHit hit;
        Touch[] touches;
		Vector2 currentPosition = transform.position;

        touches = Input.touches;

        if (touches.Length == 1 && touches[0].phase == TouchPhase.Began) {
            this.hasMoved = false;
            
            Debug.Log("Touch has been registered");

            // Selecting an object
            ray = Camera.main.ScreenPointToRay(touches[0].position);

            if (Physics.Raycast(ray, out hit)) {
                // We have selected the same object.
                // Are we deselecting or moving the object?
                if (this.selectedObject == hit.transform.gameObject) {
                    Debug.Log("This is the same object as before");
                    this.checkForMovement = true;
                // We have selected a different object,
                // no need to check for movement.
                } else {
                    Debug.Log("This is NOT the same object as before");
                    this.checkForMovement = false;
                    
                    // If there was an old selected object, change its color
                    if (this.selectedObject) {
                        this.selectedObject.GetComponent<Renderer>().material.color = Color.white;
                    }
                    
                    this.selectedObject = hit.transform.gameObject;
                    this.selectedObject.GetComponent<Renderer>().material.color = Color.yellow;
                    this.deleteItemButton.interactable = true;
                }
            } 

        }

        if (touches.Length == 1 && touches[0].phase == TouchPhase.Moved) {
            Debug.Log("Touch has moved");
            this.hasMoved = true;
        }

        if (touches.Length == 1 && this.checkForMovement && touches[0].phase == TouchPhase.Ended) {
            // Move object
            if (this.hasMoved) {
                Debug.Log("CHECKING IF A PLANE IS ON THE WAY OF MOVING");
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touches[0].position, hits)) {
                    if (hits[0].trackable is ARPlane) {
                        Debug.Log("THERE IS A PLANE");
                        this.selectedObject.transform.position = hits[0].pose.position;
                        this.selectedObject.transform.rotation = hits[0].pose.rotation;
                    }
                }
            // No movement was done, thus we deselect the object.
            } else {
                Debug.Log("Selected object should be deselected");
                this.selectedObject.GetComponent<Renderer>().material.color = Color.white;
                this.selectedObject = null;
                this.deleteItemButton.interactable = false;
            }
            this.checkForMovement = this.hasMoved = false;
        }
		if (touches.Length == 2 && this.checkForMovement && touches[0].phase == TouchPhase.Ended) {
			// Rotate object
            if (this.hasMoved) {
                Debug.Log("CHECKING IF A PLANE IS ON THE WAY OF MOVING");
				transform.Rotate( 5.0f * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime);
                /*float turnSpeed = 5;
				Vector2 moveTowards = Camera.main.ScreenToWorldPoint(touches[0].position);
				Vector2 movement = moveTowards - currentPosition;
				movement.Normalize();
				float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), turnSpeed * Time.deltaTime);*/
            // No movement was done, thus we deselect the object.
            } else {
                Debug.Log("Selected object should be deselected");
				this.selectedObject.GetComponent<Renderer>().material.color = Color.white;
                this.selectedObject = null;
                this.deleteItemButton.interactable = false;
            }
            
			
		}
    }

    // Update is called once per frame
    void Update() {
        UpdatePlaceholder();
        CheckUserInput();
    }
}
