using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float detectingRange;
    public float moveSpeed;

    private Animator anim;

    [SerializeField] private float attackRange = 2f;
    private bool isAttacking = false;
    public float timeBetweenAttacks = 0.5f;

    public int currentHealth;
    public int maxHealth = 100;
    public int healthDamageAmount;

    private Rigidbody rb; // Added Rigidbody component

[SerializeField] HealthBar healthBar;

    private void Awake()
    {
        
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

    void ResetVelocity()
    {
        var v = rb.velocity;
        v.x = 0;
        v.z = 0;
        rb.velocity = v;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            ResetVelocity();
            return;
        }
    
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);
        if (distance < detectingRange)
        {
            //anim.SetBool("inRange", true); // enemy see player

            if (distance <= attackRange && !isAttacking)
            {
                ResetVelocity();
                StartCoroutine(AttackForDelay());
                anim.SetBool("running", false);
                anim.SetBool("attack", true);
            }
            else if (!isAttacking)
            {
                MoveTowards();
            }
            else
                ResetVelocity();
        }
        else
        {
            ResetVelocity();
            anim.SetBool("running", false);
            anim.SetBool("attack", false);
        }
    }

    void MoveTowards()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        Vector3 direction = (playerPosition - transform.position).normalized; // Fixed typo and used transform.position instead of transform.forward

        // Use Rigidbody to move the enemy
        //rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        var d = direction * moveSpeed;
        var v = rb.velocity;
        v.x = d.x;
        v.z = d.z;
        rb.velocity = v;

        // Rotate the enemy to face the player
        Vector3 lookDirection = playerPosition - transform.position;
        lookDirection.y = 0;
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
        if (gameObject.TryGetComponent<animationaudio>(out var trigger))
            trigger.AttackSound();
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
        anim.SetBool("attack", false);
    }

    public void TakeDamage()
    {
        if (currentHealth <= 0)
            return;

//anim enem getting shot
        currentHealth -= healthDamageAmount;

 healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            //anim enemy death death
            anim.SetTrigger("die");
            if (gameObject.TryGetComponent<animationaudio>(out var trigger))
                trigger.DeathSound();
            Destroy(gameObject, 5);
        }
    }
}
