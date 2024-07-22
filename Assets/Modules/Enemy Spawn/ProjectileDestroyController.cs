using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent(out ObstacleEntity obstacleEntity))
        {
            if (obstacleEntity.isDestroyableAfterImpact)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }    
    }

}
