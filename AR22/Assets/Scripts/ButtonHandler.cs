using System;
using UnityEngine;


public class ButtonHandler : MonoBehaviour
{
    
    [SerializeField]
    RealObjectAdder adder;

    [SerializeField]
    RealObjectInteractor interactor;
    

    private void Start()
    {
        interactor.enabled = true;
        adder.enabled = false;
    }

    public void DeleteButton() {
        GameObject[] go = GameObject.FindGameObjectsWithTag("art");
        for (int i = 0; i < go.Length; i++)
        {
            Destroy(go[i]);
        }
    }

    public void FinishedAdding()
    {
        adder.enabled = false;
        interactor.enabled = true;
    }

    public void AddObjectButton()
    {
        adder.enabled = true;
        interactor.enabled = false;
    }
}
