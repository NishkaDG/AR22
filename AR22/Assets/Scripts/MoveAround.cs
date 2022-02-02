using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x, y, z;
        x = transform.rotation.x;
        y = transform.rotation.y;
        z = transform.rotation.z;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 0.1f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(0.1f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0, 0, 0.1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.1f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(transform.position, -transform.up, Time.deltaTime * 90f);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        }
    }
}
