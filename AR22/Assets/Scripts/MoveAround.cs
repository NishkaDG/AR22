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
        Transform parentTransform = this.GetComponentInParent<Transform>();
        if (Input.GetKey(KeyCode.W))
        {
            parentTransform.Translate(parentTransform.forward * 0.1f);
        } else if (Input.GetKey(KeyCode.S))
        {
            parentTransform.Translate(parentTransform.forward * -0.1f);
        } else if (Input.GetKey(KeyCode.D))
        {
            parentTransform.Translate(parentTransform.right * 0.1f);
        } else if (Input.GetKey(KeyCode.A))
        {
            parentTransform.Translate(parentTransform.right * -0.1f);
        } else if (Input.GetKey(KeyCode.E))
        {
            parentTransform.Rotate(parentTransform.up, 5);
        } else if (Input.GetKey(KeyCode.Q))
        {
            parentTransform.Rotate(parentTransform.up, -5);
        }
    }
}
