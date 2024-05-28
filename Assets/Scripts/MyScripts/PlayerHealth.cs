using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private float currentHealth;
    public float DamageAmount;

    public GameObject PlayerObject;
   
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DealDamage()
    {
        
        currentHealth -= DamageAmount;
        Debug.Log($"Player damage: {currentHealth} {DamageAmount}");
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            GetComponentInChildren<Camera>().transform.SetParent(null);
            PlayerObject.SetActive(false);
            //anim palyer die
            StartCoroutine(Wait3Seconds());
        }
    }

    IEnumerator Wait3Seconds()
    {
        yield return new WaitForSeconds(3);
        // show UI
    }
}
