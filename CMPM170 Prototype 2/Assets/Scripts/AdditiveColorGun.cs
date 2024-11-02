using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveColorGun : MonoBehaviour
{
    public Renderer gunRenderer;   // Reference to the gun's Renderer component
    private Color currentGunColor = Color.white; // Start with white color for the gun
    private List<Color> absorbedColors = new List<Color>(); // List to store absorbed colors

    private void Start()
    {
        if (gunRenderer == null) {
            Debug.LogError("Gun Renderer is not assigned in the inspector.");
        }
        else {
            // Ensure the gun has its own unique material instance
            gunRenderer.material = new Material(gunRenderer.material);
            gunRenderer.material.SetColor("_Color", currentGunColor); // Directly setting Albedo color
            Debug.Log("Starting Gun Color: " + currentGunColor);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform a raycast to detect the clicked object
            if (Physics.Raycast(ray, out hit)) {
                Renderer objectRenderer = hit.collider.GetComponent<Renderer>();

                // Check if the clicked object has a Renderer and a material with color
                if (objectRenderer != null && objectRenderer.material != null) {
                    Color objectColor = objectRenderer.material.color;
                    Debug.Log("Object Color: " + objectColor);

                    // Add the object's color to the list of absorbed colors
                    absorbedColors.Add(objectColor);

                    // Sum all absorbed colors (to reduce gun color)
                    currentGunColor = Color.white; // Start from white before subtracting
                    foreach (Color color in absorbedColors) {
                        currentGunColor -= color; // Reduce the color values
                    }

                    // Ensure color values don't go below black
                    currentGunColor = new Color(
                        Mathf.Clamp(currentGunColor.r, 0, 1),
                        Mathf.Clamp(currentGunColor.g, 0, 1),
                        Mathf.Clamp(currentGunColor.b, 0, 1)
                    );

                    // Update the gun's Albedo color directly using "_Color"
                    gunRenderer.material.SetColor("_Color", currentGunColor);
                    Debug.Log("Updated Gun Color: " + gunRenderer.material.GetColor("_Color"));

                    // Set the object's color to white after absorption
                    objectRenderer.material.color = Color.white;
                    Debug.Log("Object Color after Absorption: " + objectRenderer.material.color);
                }
            }
        }
    }
}
