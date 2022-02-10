using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class IndicationMarker : MonoBehaviour
{
    
    [SerializeField] 
    private GameObject marker;
    
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    
    private bool markerEnabled = false;
    
    [SerializeField] 
    private Transform parent;
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        var markerRenderer = marker.GetComponent<Renderer>();
        markerRenderer.enabled = false;
        if (!markerEnabled)
        {
            return;
        }
        var centerOfScreen = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
        if (m_RaycastManager.Raycast(centerOfScreen, m_Hits))
        {
            var hit = m_Hits[0];
            if (hit.trackable is ARPlane plane)
            {
                Debug.Log("hit plane");
                markerRenderer.enabled = true;
                Debug.Log("marker pos ");
                marker.transform.localPosition = hit.pose.position;
                marker.transform.localRotation = hit.pose.rotation;
            }
        }
    }

    public void enable()
    {
        markerEnabled = true;
    }

    public void disable()
    {
        markerEnabled = false;
    }
    
}
