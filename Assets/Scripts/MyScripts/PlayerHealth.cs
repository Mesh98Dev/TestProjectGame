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
        if (currentHealth > 0)
            currentHealth -= DamageAmount;
        Debug.Log($"Player damage: {currentHealth} {DamageAmount}");
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            var camera = GetComponentInChildren<Camera>();
            if (camera != null)
                camera.transform.SetParent(null);
            PlayerObject.SetActive(false);
            //anim palyer die
            StartCoroutine(Wait3Seconds());
           // Debug.Log("Player Died");
        }
    }

    IEnumerator Wait3Seconds()
    {
        yield return new WaitForSeconds(3);
        // show UI
    }
}
