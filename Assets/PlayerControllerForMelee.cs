using UnityEngine;

public class PlayerControllerForMelee : MonoBehaviour
{
    public float meleeRange = 2f;               // Range of the melee attack
    public int meleeDamage = 20;                // Damage dealt by melee attack
    public float pushbackForce = 5f;            // Force applied to zombies when hit by melee attack

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        // Perform a sphere cast to detect zombies within melee range
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, meleeRange, transform.forward, 0f);
        foreach (RaycastHit hit in hits)
        {
            //// Check if the hit object is a zombie
            //ZombieController zombie = hit.collider.GetComponent<ZombieController>();
            //if (zombie != null)
            //{
            //    // Apply damage to the zombie
            //    zombie.TakeDamage(meleeDamage);

            //    // Calculate pushback direction from the player to the zombie
            //    Vector3 pushbackDirection = (zombie.transform.position - transform.position).normalized;

            //    // Apply force to push back the zombie
            //    Rigidbody zombieRigidbody = zombie.GetComponent<Rigidbody>();
            //    if (zombieRigidbody != null)
            //    {
            //        zombieRigidbody.AddForce(pushbackDirection * pushbackForce, ForceMode.Impulse);
            //    }
            //}
        }
    }
}
