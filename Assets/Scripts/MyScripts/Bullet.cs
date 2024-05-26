using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buellt : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody rb;
    // Start is called before the first frame update
    Vector3 velocity;

    private void Awake()
    {
        Destroy(gameObject, 2f);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = transform.forward * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OncCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Enemy" )
        {
            EnemyController.instance.TakeDamage();
            Destroy(gameObject);
            
        }

    }
}
