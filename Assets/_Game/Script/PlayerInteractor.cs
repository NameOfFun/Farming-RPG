using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    PlayerController controller;
    Land selectedLand = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get acces to our PlayerController component
        controller = transform.parent.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * 1f, Color.red);
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            OnInteractableHit(hit);
        }
    }

    // Handle what happens when the interaction raycast hits something interatable
    private void OnInteractableHit(RaycastHit hit)
    {
        Collider collider = hit.collider;
        if(collider.CompareTag("Land"))
        {
            // Get Land component
            Land land = collider.GetComponent<Land>();
            SelectLand(land);
            return;
        }else

        // Deselect the land if the player is not standing on any land at the moment
        if (selectedLand != null)
        {
            selectedLand.Select(false);
            selectedLand = null;
        }
    }

    // handles the selection prosess of the land
    void SelectLand(Land land)
    {
        // Set the previously selected land to false (If any)
        if(selectedLand != null)
        {
            selectedLand.Select(false);
        }

        // Set the new selected land to the land we're selecting now
        selectedLand = land;
        land.Select(true);
    }


    // Triggered when the player presses the tool button
    public void Interact()
    {
        // Cehck if the player is selectiing any land
        if(selectedLand != null)
        {
            selectedLand.Interact();
            return;
        }
        Debug.Log("Not on any land");
    }
}
