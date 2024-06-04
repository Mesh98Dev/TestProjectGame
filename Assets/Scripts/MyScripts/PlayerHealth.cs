using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private float currentHealth;
    public float DamageAmount;
    public GameObject loseCanvas; // Reference to the "You Lose" UI canvas
    public GameObject crosshair;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

    }

    
       public void DealDamage()
    {
        if (currentHealth > 0)
            currentHealth -= DamageAmount;
        Debug.Log($"Player damage: {currentHealth} {DamageAmount}");
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<PlayerShooting>().enabled = false;
            crosshair.SetActive(false);
            // var camera = GetComponentInChildren<Camera>();
            // if (camera != null)
            //     camera.transform.SetParent(null);
            //anim palyer die
            StartCoroutine(Wait3Seconds());
           // Debug.Log("Player Died");
        }
    }

    IEnumerator Wait3Seconds()
    {
        // show UI
        if (loseCanvas != null)
        {
            loseCanvas.SetActive(true); // Show the "You Lose" UI
        }
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu"); // Load the MainMenu scene
    }
}

