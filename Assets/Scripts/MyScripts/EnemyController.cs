using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float detetctingRange;
    public float movSpeed;

    private Animator anim;

    private float attackRange = 3f;
    private bool isAttacking= false;
    public float timeBetweenAttacks = 0.5f;

    public int CurrentHealth;
    public int maxHealth=100;
    public int healthDamageAmount;

    public static EnemyController instance;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player");
        CurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(distance);
        if (distance < detetctingRange) 
        {
            anim.SetBool("inRange", true);

            if (distance <= attackRange && !isAttacking ) 
            {
                StartCoroutine(AttackForDealy());
            }
            else if (!isAttacking) 
            {
                MoveTowards();
            }
        }
            else { anim.SetBool("inRange",false); }
    }

    void MoveTowards() 
    {
        Vector3 playerPosition = new Vector3 (player.transform.position.x, transform.position.y,player.transform.position.z );
        Vector3 dirction = (playerPosition - transform.forward).normalized;

        transform.position = Vector3.MoveTowards(transform.position, playerPosition, movSpeed * Time.deltaTime);
        transform.LookAt(playerPosition);
    }

    IEnumerator AttackForDealy() 
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    public void TakeDamage() 
    {
        CurrentHealth -= healthDamageAmount;
        if (CurrentHealth <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
