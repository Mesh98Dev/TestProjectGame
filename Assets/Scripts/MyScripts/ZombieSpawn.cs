using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    
    [Header("ZombieSpawn var")]
    
    public GameObject zombiePrefab;
    public Transform zombieSpawnPosition;

   
   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            Invoke("EnemySpawner", 1f);
            Destroy(gameObject, 10f);
        }
    }
    void EnemySpawner()
    {

        Instantiate (zombiePrefab, zombieSpawnPosition.position, zombieSpawnPosition.rotation);
    }
    
}
