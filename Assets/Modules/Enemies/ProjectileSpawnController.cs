using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float timeToShoot = 3;
        
    private Vector3 playerPosition;
    private float timer = 0;

   
   private void Update()
    {
        if(PlayerManager.Instance.isAlive)
        {
            timer += Time.deltaTime;

            if (timer > timeToShoot)
            {
                timer = 0;
                Shoot();
            }
        }
        
    }


    private void Shoot()
    {
        GameObject instantiatedProjectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        ProjectileEntity projectileEntity = instantiatedProjectile.GetComponent<ProjectileEntity>();
        Rigidbody2D projectileRB = projectileEntity.rb;
        
        playerPosition = PlayerManager.Instance.GetPlayerPosition();
        Vector3 direction = playerPosition - transform.position;
        Vector2 directionProjectile = new Vector2 (direction.x, direction.y).normalized * projectileEntity.projectileSpeed;
        projectileRB.velocity = directionProjectile;
    }

}
