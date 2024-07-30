using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEntity : MonoBehaviour
{
    public bool hooknessActivated;
    public bool isHook;
    public float attractionSpeed = 3f;
    public bool doesDamage;
    public bool isDestroyableAfterImpact;
    public bool isProjectile;
    public bool isWall;
    public bool isVerticalWall;
    public bool isHorizontalWall;

    Collider2D thisCollider;

    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out ObstacleEntity obstacleEntity))
        {
            Collider2D colliderToIgnore = other.gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(thisCollider, colliderToIgnore);
        }
    }
}
