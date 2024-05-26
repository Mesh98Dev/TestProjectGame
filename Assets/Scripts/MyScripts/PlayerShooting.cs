using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public Transform firePos;
    public GameObject bullet;
    public float timeBetweenShoots;
    public bool canShoots = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoots)
        {
            //shoot
            Shoot();
        }

    }
        void Shoot() 
        {
            Instantiate(bullet,firePos.position, firePos.rotation);
             StartCoroutine(ShootDelay());
        }

    IEnumerator ShootDelay()
    {
        canShoots = false;
        yield return new WaitForSeconds(timeBetweenShoots);
        canShoots = true;
    }
}
