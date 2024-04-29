using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class ZombieController : MonoBehaviour
{
    public float moveSpeed = 3f;                // Movement speed of the zombie
    public float attackRange = 1.5f;            // Attack range of the zombie
    public int maxHealth = 100;                 // Maximum health of the zombie
    public float minDistanceToPlayer = 2f;      // Minimum distance to the player
    public float minSeparationDistance = 2f;    // Minimum separation distance between zombies
    public LayerMask obstacleMask;              // Layer mask for obstacles

    private int currentHealth;                   // Current health of the zombie
    private Transform player;                    // Reference to the player's transform
    private NavMeshAgent navMeshAgent;           // Reference to the NavMeshAgent component
    private bool isDead = false;                 // Flag to track if the zombie is dead
    private List<Transform> zombies;             // List of all zombie transforms in the scene

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Set the initial destination to the player's position
        navMeshAgent.SetDestination(player.position);

        // Find all active zombies in the scene
        zombies = new List<Transform>();
        GameObject[] zombieObjects = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject zombieObject in zombieObjects)
        {
            // Add zombie's transform to the list
            if (zombieObject != gameObject) // Exclude this zombie from the list
                zombies.Add(zombieObject.transform);
        }
    }

    void Update()
    {
        if (!isDead)
        {
            MoveTowardsPlayer();
            AvoidOtherZombies();
            // Add other logic here, such as attacking the player
        }
    }

    void AvoidOtherZombies()
    {
        foreach (Transform zombie in zombies)
        {
            if (zombie != transform)
            {
                float distance = Vector3.Distance(transform.position, zombie.position);
                if (distance < minSeparationDistance)
                {
                    // Calculate a direction away from the nearby zombie
                    Vector3 avoidanceDirection = (transform.position - zombie.position).normalized;

                    // Calculate a destination point to move towards while avoiding other zombies
                    Vector3 newDestination = transform.position + avoidanceDirection * minSeparationDistance;

                    // Check if the calculated destination is valid on the NavMesh
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(newDestination, out hit, minSeparationDistance, NavMesh.AllAreas))
                    {
                        // Move towards the calculated destination
                        navMeshAgent.SetDestination(hit.position);
                    }
                }
            }
        }
    }

    void MoveTowardsPlayer()
    {
        if (player == null)
            return;

        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            // Implement attack logic here
        }
    }
}
