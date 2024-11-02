using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintGunUIManager : MonoBehaviour
{
    public List<Image> colorUIElements;    // UI elements representing each color
    public Renderer playerRenderer;        // The player's Renderer to swap colors on

    private void Start()
    {
        // Hide all UI elements at start
        foreach (Image uiElement in colorUIElements)
        {
            uiElement.enabled = false;
        }
    }

    private void Update()
    {
        // Check for number keys 1-9 (KeyCode.Alpha1 to KeyCode.Alpha9)
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + (i - 1)))
            {
                // Check if the UI element is enabled before changing color
                if (colorUIElements[i - 1].enabled)
                {
                    // Set the player's color to the color of the UI element
                    playerRenderer.material.color = colorUIElements[i - 1].color;
                }
            }
        }
    }
}
