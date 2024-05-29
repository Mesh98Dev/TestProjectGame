using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float detectingRange;
    public float moveSpeed;

    private Animator anim;

    private float attackRange = 3f;
    private bool isAttacking = false;
    public float timeBetweenAttacks = 0.5f;

    public int currentHealth;
    public int maxHealth = 100;
    public int healthDamageAmount;

    public static EnemyController instance;

    private Rigidbody rb; // Added Rigidbody component

[SerializeField] HealthBar healthBar;

    private void Awake()
    {
        instance = this;
        
        healthBar = GetComponentInChildren<HealthBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        
 //healthBar.UpdateHealthBar(currentHealth, maxHealth);

        rb = GetComponent<Rigidbody>(); // Get Rigidbody component
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
            return;
    
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);
        if (distance < detectingRange)
        {
//anim.SetBool("inRange", true); // enemy see player

            if (distance <= attackRange && !isAttacking)
            {
                StartCoroutine(AttackForDelay());
                anim.SetBool("running", false);
                anim.SetBool("attack", true);
            }
            else if (!isAttacking)
            {
                MoveTowards();
            }
        }
        else
        {
            anim.SetBool("running", false);
            anim.SetBool("attack", false);
        }
    }

    void MoveTowards()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        Vector3 direction = (playerPosition - transform.position).normalized; // Fixed typo and used transform.position instead of transform.forward

        // Use Rigidbody to move the enemy
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        // Rotate the enemy to face the player
        Vector3 lookDirection = playerPosition - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        rb.rotation = Quaternion.Slerp(rb.rotation, lookRotation, Time.deltaTime * moveSpeed);
        //enemy moving anim
        anim.SetBool("running", true);
    }

    IEnumerator AttackForDelay()
    {
        isAttacking = true;
//anim.SetTrigger("Attack");
        player.GetComponent<PlayerHealth>().DealDamage();
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    public void TakeDamage()
    {
//anim enem getting shot
        currentHealth -= healthDamageAmount;

 healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            //anim enemy death death
            anim.SetTrigger("die");
            Destroy(gameObject, 5);
        }
    }
}
