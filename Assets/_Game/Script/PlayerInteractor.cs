using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    PlayerController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get acces to our PlayerCOntroller component
        controller = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * 2f, Color.red);
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            OnInteractableHit(hit);
        }
    }

    private void OnInteractableHit(RaycastHit hit)
    {
        Collider collider = hit.collider;
        if(collider.CompareTag("Land"))
        {
            Debug.Log("Land");
        }
        else
        {
            Debug.Log("Not Land");
        }
    }
}
