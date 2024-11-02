using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorAbsorbUIManager : MonoBehaviour
{
    public Renderer cubeRenderer;        // Reference to the cube with the beige color
    public Image CounterUI;         // Reference to the UI Image for the beige counter

    private void Start()
    {
        if (cubeRenderer == null || CounterUI == null)
        {
            Debug.LogError("Please assign all references in the inspector.");
            return;
        }

        // Hide the beige counter UI at the start
        CounterUI.enabled = false;
    }

    private void Update()
    {
        // Check if left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked object is the cube with the beige color
                if (hit.collider.GetComponent<Renderer>() == cubeRenderer)
                {

                    // Enable the beige counter UI
                    CounterUI.enabled = true;
                }
            }
        }
    }
}
