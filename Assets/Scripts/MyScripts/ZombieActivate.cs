using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieActivate : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();
    bool activated;

    void Awake()
    {
        foreach (Transform tr in transform) {
            enemies.Add(tr.gameObject);
            tr.gameObject.SetActive(false);
        }
    }



   private void OnTriggerEnter(Collider other)
    {
        if (activated)
            return;

        if (other.gameObject.tag == "Player") 
        {
            activated = true;
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                    enemy.SetActive(true);
            }
        }
    }

    
}
