using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;



public class ZombieController : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed at which the zombie moves
    public float attackRange = 1.5f; // Range within which the zombie can attack the player
    public int attackDamage = 10; // Damage dealt to the player per attack
    public float attackCooldown = 1f; // Time between attacks

    private Transform playerTransform; // Reference to the player's transform
    private bool isAttacking = false; // Flag to prevent multiple attacks during cooldown

    private void Start()
    {
        // Find the player in the scene (assuming the player has the tag "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the tag 'Player'.");
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // Move towards the player
            MoveTowardsPlayer();

            // Check if within attack range and attack the player
            if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
            {
                if (!isAttacking)
                {
                    StartCoroutine(AttackPlayer());
                }
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        
        // Play attack animation here (if any)
        
        // Deal damage to the player
        FinalPlayerController playerController = playerTransform.GetComponent<FinalPlayerController>();
        if (playerController != null)
        {
            playerController.TakeDamage(attackDamage);
        }

        // Wait for the attack cooldown
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}

