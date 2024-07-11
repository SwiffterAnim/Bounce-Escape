using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float timeToShoot = 3;
    // private ProjectileEntity projectileEntity;

    
    private GameObject player;
    private float timer = 0;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //want to find a better way to get player.
    }

   
   private void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeToShoot)
        {
            timer = 0;
            Shoot();
        }
    }


    private void Shoot()
    {
        GameObject instantiatedProjectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        Rigidbody2D projectileRB = instantiatedProjectile.GetComponent<Rigidbody2D>();
        ProjectileEntity projectileEntity = instantiatedProjectile.GetComponent<ProjectileEntity>();
        
        Vector3 direction = player.transform.position - transform.position;
        Vector2 directionProjectile = new Vector2 (direction.x, direction.y).normalized * projectileEntity.projectileSpeed;
        projectileRB.velocity = directionProjectile;
    }

}
