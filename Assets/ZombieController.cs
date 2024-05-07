using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieController : MonoBehaviour
{
    // Reference to the player
    public Transform player;

    // Variables for zombie movement
    private NavMeshAgent navAgent;
    public float chaseRange = 10f;
    public float attackRange = 2f;

    // Variables for zombie health
    public int maxHealth = 100;
    private int currentHealth;

    // Variables for zombie attack
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    void Start()
    {
        // Initialize components
        navAgent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Check if player is within chase range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= chaseRange)
        {
            // Chase the player
            navAgent.SetDestination(player.position);

            // Check if player is within attack range
            if (distanceToPlayer <= attackRange)
            {
                // Attack the player if enough time has passed since the last attack
                if (Time.time >= nextAttackTime)
                {
                    AttackPlayer();
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }

    // Method to damage the zombie
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method for zombie to attack the player
    private void AttackPlayer()
    {
        // Deal damage to the player
        // Implement your damage logic here
    }

    // Method to handle zombie's death
    private void Die()
    {
        // Handle zombie's death, such as playing death animation, giving rewards, etc.
        Destroy(gameObject);
    }
}
