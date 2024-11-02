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
        if (gunRenderer == null)
        {
            Debug.LogError("Gun Renderer is not assigned in the inspector.");
        }
        else
        {
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
            if (Physics.Raycast(ray, out hit))
            {
                Renderer objectRenderer = hit.collider.GetComponent<Renderer>();

                // Check if the clicked object has a Renderer and a material with color
                if (objectRenderer != null && objectRenderer.material != null)
                {
                    Color objectColor = objectRenderer.material.color;
                    Debug.Log("Object Color: " + objectColor);

                    // Add the object's color to the list of absorbed colors
                    absorbedColors.Add(objectColor);

                    // Calculate weighted average color
                    Color weightedColor = Color.black;
                    float totalWeight = 0f;

                    for (int i = 0; i < absorbedColors.Count; i++)
                    {
                        float weight = i + 1; // Increasing weight for each absorbed color
                        weightedColor += absorbedColors[i] * weight;
                        totalWeight += weight;
                    }

                    currentGunColor = weightedColor / totalWeight; // Calculate weighted average

                    // Update the gun's Albedo color directly using "_Color"
                    gunRenderer.material.SetColor("_Color", currentGunColor);
                    Debug.Log("Updated Gun Color: " + gunRenderer.material.GetColor("_Color"));

                    // Set the object's color to white after absorption
                    objectRenderer.material.color = Color.black;
                    Debug.Log("Object Color after Absorption: " + objectRenderer.material.color);
                }
            }
        }
    }
}
