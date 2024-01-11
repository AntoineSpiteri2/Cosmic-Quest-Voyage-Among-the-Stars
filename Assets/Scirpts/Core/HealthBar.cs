using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform healthBarRect; // Assign this in the inspector
    private Vector3 originalScale;

    public static HealthBar Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep this object alive across scenes.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Store the original local scale of the health bar for later use
        originalScale = healthBarRect.localScale;
    }

    // Call this method to update the health bar based on the player's current health
    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (currentHealth <= 0)
        {
            // If health is 0, set scale to 0
            healthBarRect.localScale = new Vector3(0, originalScale.y, originalScale.z);
        }
        else
        {
            // Calculate the scale based on current health
            float scaleX = (currentHealth / maxHealth) * originalScale.x;
            healthBarRect.localScale = new Vector3(scaleX, originalScale.y, originalScale.z);
        }
    }
}
