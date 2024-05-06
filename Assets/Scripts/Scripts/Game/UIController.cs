using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public Slider healthSlider; // Reference to the health slider UI element
    public Text pointsText; // Reference to the points text UI element

    // Method to update the health bar UI
    public void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    // Method to update the points text UI
    public void UpdatePoints(int points)
    {
        pointsText.text = "Points: " + points.ToString();
    }
}

