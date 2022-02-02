using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAdder : MonoBehaviour
{

    [SerializeField]
    public Transform prefab;

    [SerializeField]
    public Transform midairPrefab;

    [SerializeField]
    public Transform raycastPrefab;

    [SerializeField]
    public Camera camera;
    
    private Vector3 midairPos = new Vector3(0.0f, 5.0f, 3.0f);
    private Vector3 raycastPos, raycastSize;

    // Start is called before the first frame update
    void Start()
    {
        float planeY;
        GameObject plane;
        Vector3 cubeSize, cubePos, planePos;

        // A crappy Unity script is born...
        Debug.Log("Hello, World");

        // Retrieve the plane from the scene
        plane = GameObject.Find("Plane");
        // Get the plane's Y value
        planeY = plane.GetComponent<Collider>().bounds.size.y;
        // Get the plane's position
        planePos = plane.transform.position;
        // Get the prefab's size
        cubeSize = prefab.Find("Cube").GetComponent<MeshRenderer>().bounds.size;
        
        raycastSize = raycastPrefab.Find("Sphere").GetComponent<MeshRenderer>().bounds.size;
        
        // The cube is on top of the plane now...
        cubePos = new Vector3(0, planeY, 0);
        // But we still need to add an offset (half of the cube's Y value)
        cubePos += new Vector3(0, cubeSize.y / 2, 0);
        
        // We create the prefab :D
        Instantiate(prefab, cubePos, Quaternion.identity);
        GameObject.Find("Cube").AddComponent<MoveAround>();        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray;
        RaycastHit hit;
        
        if (Input.GetKeyDown(KeyCode.K)) {
            Instantiate(midairPrefab, midairPos, Quaternion.identity);
        } else if (Input.GetKeyDown(KeyCode.Mouse0)) {
            ray = camera.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit)) {
                raycastPos = ray.GetPoint(hit.distance);
                raycastPos.y += raycastSize.y / 2;
                Instantiate(raycastPrefab, raycastPos, Quaternion.identity);
            }
        }
    }
}
