using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField]private EnemyEntity enemyEntity;

    //For SHOOTER
    private Animator animator;
    private float randomOffset;
    readonly string ANIMATION = "Animation";
    readonly string CLONE = "(Clone)";

    //For SHOOTER DESTROYER
    private Rigidbody2D rb;
    private float randomSignX;
    private float randomSignY;
    
    void Start()
    {
            if(enemyEntity.isShooter)
            {
                StartShooter();
            }
            if(enemyEntity.isShooterDestroyer)
            {
                StartShooterDestroyer();
            }        
    }


    private void StartShooter()
    {
        string enemyName = gameObject.name.Replace(CLONE, "").Trim();
        
        string animationName = enemyName + ANIMATION;
        
        animator = GetComponent<Animator>();
        randomOffset = Random.Range(0f, 1f);

        animator.Play(animationName, 0, randomOffset);
    }

    private void StartShooterDestroyer()
    {
        float randomX = Random.Range(-7f, 7f);
        float randomY = Random.Range(-4f, 4f);
        transform.position = new Vector3(randomX, randomY, 0);

        rb = GetComponent<Rigidbody2D>();
        randomSignX = RandomSign();
        randomSignY = RandomSign();

        rb.velocity = new Vector2 (randomSignX * enemyEntity.shooterDestroyerSpeed, randomSignY * enemyEntity.shooterDestroyerSpeed);
    }

    // Method to generate a random sign (-1 or 1)
    private float RandomSign()
    {
        // Return either -1 or 1 using Unity's Random class.
        return Random.value < 0.5 ? -1f : 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(enemyEntity.isShooterDestroyer && !enemyEntity.isDead)
        {
            if(other.gameObject.TryGetComponent(out ObstacleEntity obstacleEntity))
            {
                if(obstacleEntity.isWall)
                {
                    if(obstacleEntity.isHorizontalWall)
                    {
                        randomSignY *= -1;
                    }
                    if(obstacleEntity.isVerticalWall)
                    {
                        randomSignX *= -1;
                    }
                    rb.velocity =  new Vector2 (randomSignX * enemyEntity.shooterDestroyerSpeed, randomSignY * enemyEntity.shooterDestroyerSpeed);
                }
                
            }
            if(other.gameObject.TryGetComponent(out PlayerManager playerManager)) //Just checking if it's the player.
            {
                enemyEntity.isDead = true;
            }
        }
            
       
    }

}
