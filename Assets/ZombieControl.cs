using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public float moveSpeed = 3f;                // Movement speed of the zombie
    public float attackRange = 1.5f;            // Attack range of the zombie
    public int maxHealth = 100;                 // Maximum health of the zombie

    private int currentHealth;                   // Current health of the zombie
    private Transform player;                    // Reference to the player's transform
    private bool isDead = false;                 // Flag to track if the zombie is dead

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isDead)
        {
            // Move towards the player
            MoveTowardsPlayer();

            // Check if the player is within attack range
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                // Attack the player
                AttackPlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move the zombie towards the player
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        // Placeholder for the attack logic
        Debug.Log("Zombie is attacking the player!");
    }

    public void TakeDamage(int damage)
    {
        // Reduce the zombie's health
        currentHealth -= damage;

        // Check if the zombie is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Set the zombie as dead
        isDead = true;

        // Placeholder for death effects or animations
        Debug.Log("Zombie died!");

        // Destroy the zombie GameObject
        Destroy(gameObject);
    }
}
