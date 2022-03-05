using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class RealObjectAdder : MonoBehaviour
{
    
    Camera camera;

    [SerializeField]
    private GameObject placeholder;
    
    private bool hasMoved;
    private bool checkForMovement;
    private Vector2[] lastTwoFingerPosition;

    private int currentMaterial;
    private String currentName;
    
    private GameObject selectedObject;

    [SerializeField] 
    private GameObject posterPrefab;
    
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
    
    [SerializeField]
    Button animateButton;
    
    [SerializeField]
    Button displayButton;

    [SerializeField]
    Button catalogButton;
    
    [SerializeField]
    ARTrackedImageManager trackedImageManager;
    
    [SerializeField]
    GameObject lampPrefab;    
	
    private void EnableButton(Button btn) {
        btn.interactable = true;
    }

    private void DisableButton(Button btn) {
        btn.interactable = false;
    }

    private void ToggleButton(Button btn) {
        btn.interactable = !btn.IsInteractable();
    }
    
    public void ToggleCatalogue(GameObject catalogue) {
        catalogue.SetActive(!catalogue.activeSelf);
    }
    
    private void SelectObject(GameObject obj) {
        // If there was an old selected object, change its color
        if (this.selectedObject) {
            this.selectedObject.GetComponent<Renderer>().material.color = Color.white;
        }
        
        this.selectedObject = obj;
        
        if (this.selectedObject) {
            this.selectedObject.GetComponent<Renderer>().material.color = Color.yellow;
            Debug.Log("An object has been selected");
            EnableButton(this.deleteItemButton);
            EnableButton(this.displayButton);
            EnableButton(this.animateButton);
        } else {
            Debug.Log("An object has been deselected");
            DisableButton(this.deleteItemButton);
            DisableButton(this.displayButton);
            DisableButton(this.animateButton);
        }
    }

    public void ChangeCurrentMaterial(int matIdx) {
        if (this.currentMaterial == matIdx) {
            this.currentMaterial = -1;
            DisableButton(this.placeItemButton);
        } else {
            this.currentMaterial = matIdx;
            EnableButton(this.placeItemButton);
        }
    }

    public void ChangeCurrentName(int posterIdx)
    {
        Debug.Log("We change the name to: " + posterIdx);
        string actualName = Catalogue.posternames[posterIdx];
        if (this.currentName == actualName) {
            Debug.Log("Deny change");
            this.currentName = null;
        } else {
            Debug.Log("Confirm change");
            this.currentName = actualName;
        }
    }

    public void AddObject() {
        if (showingPlaceholder && this.currentMaterial != -1) {
            Vector3 position = placeholder.transform.position;
            Quaternion rotation = placeholder.transform.rotation;
            GameObject obj = Instantiate(posterPrefab, position, rotation);
            obj.GetComponent<Renderer>().material = Catalogue.materials[this.currentMaterial];
            obj.GetComponentInChildren<TextMesh>().text = this.currentName;
            obj.tag = "poster";
            EnableButton(this.deleteAllButton);
        }
    }
    
    public void DeleteSelectedObject() {
        int numPosters;
        
        numPosters = GameObject.FindGameObjectsWithTag("poster").Length;
        if (this.selectedObject) {
            Destroy(this.selectedObject);
            DisableButton(this.deleteItemButton);
            numPosters -= 1;
        }
        
        // An alternative would be to use DestroyImmediate to get
        // the correct number of objects within the same function.
        if (numPosters == 0) {
            DisableButton(this.deleteAllButton);
        }
    }
	
    public void DeleteAllObjects() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("poster");
        
        foreach (var obj in objs) {
            Destroy(obj);
        }
        
        SelectObject(null);
        DisableButton(this.deleteAllButton);
        DisableButton(this.deleteItemButton);
    }

    private float CalcSlope(Vector2 v1, Vector2 v2) {
        return (v2.y - v1.y) / (v2.x - v1.x);
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
        Vector2[] currTwoFingerPosition;

        touches = Input.touches;
        

        // One finger touches the screen. Is the user selecting, deselecting or moving?
        if (touches.Length == 1 && touches[0].phase == TouchPhase.Began) {
            this.hasMoved = false;
            this.lastTwoFingerPosition = null;
            
            ray = Camera.main.ScreenPointToRay(touches[0].position);

            // Is the finger pointing at something?
            if (Physics.Raycast(ray, out hit)) {
                // We have selected the same object.
                // Are we deselecting or moving the object?
                if (this.selectedObject == hit.transform.gameObject) {
                    this.checkForMovement = true;
                // We have selected a different object,
                // no need to check for movement.
                } else {
                    this.checkForMovement = false;
                    SelectObject(hit.transform.gameObject);
                }
            } 

        }

        // If the user pointed at an already selected object, check for movement and move the object.
        if (touches.Length == 1 && this.checkForMovement && touches[0].phase == TouchPhase.Moved && this.selectedObject) {
            this.hasMoved = true;
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (raycastManager.Raycast(touches[0].position, hits)) {
                if (hits[0].trackable is ARPlane) {
                    this.selectedObject.transform.position = hits[0].pose.position;
                    this.selectedObject.transform.rotation = hits[0].pose.rotation;
                }
            }
        }

        // If the user has stopped touching the screen and we were unsure whether he was deselecting
        // an object or moving an object, make a decision based on whether he moved his finger.        
        if (touches.Length == 1 && this.checkForMovement && touches[0].phase == TouchPhase.Ended) {
            // Move object
            if (!this.hasMoved) {
                SelectObject(null);
            }
            
            this.lastTwoFingerPosition = null;
            this.checkForMovement = this.hasMoved = false;
        }

        // Check for rotation if an object is selected.
		if (touches.Length == 2 && touches[0].phase == TouchPhase.Moved && touches[1].phase == TouchPhase.Moved && this.selectedObject) {
            
            currTwoFingerPosition = new Vector2[] {touches[0].position, touches[1].position};
            
            if (this.lastTwoFingerPosition != null) {
                
                float slope_1 = CalcSlope(this.lastTwoFingerPosition[0], this.lastTwoFingerPosition[1]);
                float slope_2 = CalcSlope(currTwoFingerPosition[0], currTwoFingerPosition[1]);
                
                float angle = (slope_2 - slope_1) / (1 + slope_1 * slope_2);            
                float inv = (float) Math.Atan(angle);
                float degrees = (float) ((inv * 180) / Math.PI);

                this.selectedObject.transform.Rotate(0.0f, -1.0f * degrees, 0.0f, Space.Self);
            }

            this.lastTwoFingerPosition = currTwoFingerPosition;
		}

        
        

        if (touches.Length == 3 
            && touches[0].phase == TouchPhase.Began 
            && touches[1].phase == TouchPhase.Began
            && touches[2].phase == TouchPhase.Began
            && this.selectedObject)
        {
            Debug.Log("3 fingers!");
            Renderer renderer = this.selectedObject.GetComponent<Renderer>();
            TextMesh textMesh = this.selectedObject.GetComponentInChildren<TextMesh>();
            Material mat = renderer.material;

            int index = Catalogue.materials.FindIndex(material => {
                return mat.name.Contains(material.name);
            });

            if (index != -1)
            {
                int new_index = (index + 1) % Catalogue.materials.Count;
                renderer.material = Catalogue.materials[new_index];
                Debug.Log(renderer.material.mainTexture.name);
                // FIX THIS
                /* textMesh.text = renderer.material.mainTexture.name; */
                SelectObject(this.selectedObject);
            }
        }
    }
    
    public void OnImageTrackerUpdate(ARTrackedImagesChangedEventArgs eventArgs) {
        foreach (var newImage in eventArgs.added)
        {
            Instantiate(lampPrefab, new Vector3(0, 0, 0), Quaternion.Euler(-90, 0, 0), newImage.transform);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
        }

        foreach (var removedImage in eventArgs.removed)
        {
        }
    }

    public void toggleGlossy()
    {
        Material mat = this.selectedObject.GetComponent<Renderer>().material;
        float currentGlossiness = mat.GetFloat("_Glossiness");

        if (currentGlossiness == 0.0)
        {
            mat.SetFloat("_Glossiness", 1);
            mat.SetFloat("_Metallic", 1);
        }
        else
        {
            mat.SetFloat("_Glossiness", 0);
            mat.SetFloat("_Metallic", 0);
        }
        
    }

    public void toggleAnimate()
    {
        this.selectedObject.GetComponent<ChangeAppearance>().ToggleAnimated();
    }
    
    // Start is called before the first frame update
    void Start() {
        this.camera = Camera.main;
        this.currentMaterial = -1;
        this.showingPlaceholder = false;
        this.placeholderRenderer.enabled = false;
        
        this.lastTwoFingerPosition = null;
        
        // Buttons
        DisableButton(this.placeItemButton);
        DisableButton(this.deleteItemButton);
        DisableButton(this.deleteAllButton);
        
        // Tracked images
        trackedImageManager.trackedImagesChanged += OnImageTrackerUpdate;

    }

    // Update is called once per frame
    void Update() {
        UpdatePlaceholder();
        CheckUserInput();
    }
}
