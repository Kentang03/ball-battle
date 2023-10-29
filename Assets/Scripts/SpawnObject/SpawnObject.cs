using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnObject : MonoBehaviour
{
    public GameObject character;
    private Camera cam = null;

    void Start() 
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Instantiate(character, hit.point, Quaternion.identity);
            }
        }
    }
}
