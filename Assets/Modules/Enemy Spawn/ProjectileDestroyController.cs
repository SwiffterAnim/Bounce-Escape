using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyController : MonoBehaviour
{

    private float timer;

    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
