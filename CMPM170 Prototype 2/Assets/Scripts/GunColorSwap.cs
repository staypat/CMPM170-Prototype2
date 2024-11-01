using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunColorSwap : MonoBehaviour
{
    public Renderer playerRenderer;         // Reference to the player's Renderer component
    public Material emptyMaterial;          // Default "empty" material for objects when material is taken
    private void Start()
    {
        if (playerRenderer == null)
        {
            Debug.LogError("Player Renderer is not assigned in the inspector.");
        }
        if (emptyMaterial == null)
        {
            Debug.LogError("Empty Material is not assigned in the inspector.");
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Perform a raycast to detect the clicked object
            if (Physics.Raycast(ray, out hit))
            {
                Renderer objectRenderer = hit.collider.GetComponent<Renderer>();
                // Check if the clicked object has a Renderer
                if (objectRenderer != null)
                {
                    // Ensure the clicked object has a valid material, assign emptyMaterial if it doesn't
                    Material objectMaterial = objectRenderer.material != null ? objectRenderer.material : emptyMaterial;
                    // Apply the player's current material to the clicked object
                    objectRenderer.material = playerRenderer.material != null ? playerRenderer.material : emptyMaterial;
                    // Apply the clicked object's material to the player
                    playerRenderer.material = objectMaterial;
                }
            }
        }
    }
}