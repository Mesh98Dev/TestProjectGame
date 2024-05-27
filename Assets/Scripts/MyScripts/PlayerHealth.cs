using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private float currentHealth;
    public float DamageAmount;
   
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DealDamage(int amount)
    {
        currentHealth -= DamageAmount;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
