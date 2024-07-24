using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyedPiecesController : MonoBehaviour
{
    [SerializeField] private Vector2 randomForceDirection;
    [SerializeField] private float force;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 playerVelocity;

    

    void Start()
    {
        playerVelocity = PlayerManager.Instance.GetPlayerVelocity().normalized;
        
        rb.AddForce(playerVelocity * force + randomForceDirection * force);
    }

}
