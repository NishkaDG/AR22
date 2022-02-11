using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedObject : MonoBehaviour
{
    
    [SerializeField]
    ARTrackedImageManager trackedImageManager;
    
    [SerializeField]
    GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        trackedImageManager.trackedImagesChanged += OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, newImage.transform);
            
            // THIS CONDITIONAL SHOULD BE USED IN CASE OF INSERTING DIFFERENT PREFABS  
            /* if (newImage.referenceImage.name == "refimage-1") { */
            /*     Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, newImage.transform); */
            /* } else if (newImage.referenceImage.name == "refimage-2") { */
            /*     Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, newImage.transform); */
            /* } */
        }

        foreach (var updatedImage in eventArgs.updated)
        {
        }

        foreach (var removedImage in eventArgs.removed)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
