using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Added to use UI components

public class PlayerShooting : MonoBehaviour
{
    public Transform firePos;
    public GameObject bullet;
    public float timeBetweenShoots = 0.5f; // Corrected the default value
    public bool canShoot = true; // Corrected the naming convention
    public ParticleSystem shootParticles;

    public Image crosshair; // Added reference to the crosshair image

    void Start()
    {
        if (crosshair != null)
        {
            // Ensure the crosshair is visible at the start
            crosshair.enabled = true;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            // shoot
            Shoot();
        }
    }

    void Shoot()
    {
        var bulletInstance = Instantiate(bullet, firePos.position, firePos.rotation); //change it from firepoos to  croos hair
        if (shootParticles != null)
        {
            shootParticles.Play();
        }
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShoots);
        canShoot = true;
    }
}
