using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLifeController : MonoBehaviour
{
    [SerializeField] private PlayerCollisionController playerCollisionController;
    [SerializeField] private PlayerVisualController playerVisualController;
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] float timeToRecoverShield = 3f;

    private Rigidbody2D rb;
    private ObstacleEntity obstacleEntity;

    public bool isAlive;

    private float recoveryTimer = 0;
    private int crackIndex;
    private int numberOfCrackedSprites;

    private void Awake()
    {
        playerCollisionController.OnCollisionEnter2DEvent += OnCollisionEnter2DEvent;

        if(!playerMovementController.enabled)
        {
            playerMovementController.EnableInput();
            playerMovementController.enabled = true;
        }
    }
    private void OnDestroy()
    {
        playerCollisionController.OnCollisionEnter2DEvent -= OnCollisionEnter2DEvent;    
    }

    
    void Start()
    {
        crackIndex = 0;
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        numberOfCrackedSprites = playerVisualController.crackStages.Length - 1;
    }

    
    void Update()
    {
        if(isAlive)
        {
            RecoverShield();
        }
    }

    private void OnCollisionEnter2DEvent(Collision2D other)
    {

        if (other.gameObject.TryGetComponent<ObstacleEntity>(out obstacleEntity))
        {
            if (obstacleEntity.doesDamage && isAlive)
            {
                TakeDamage();
            }
            if (obstacleEntity.isDestroyableAfterImpact)
            {
                Destroy(other.gameObject); //I think I should destroy this object on the ProjectileDestroyController... But I don't know how hehe
            }
        }

    }

    private void TakeDamage()
    {
        TimeManager.Instance.DoSlowMotion();
        ps.Play();
        recoveryTimer = 0;
        if (crackIndex < numberOfCrackedSprites)
        {
            crackIndex++;
            playerVisualController.CrackCristalBall(crackIndex);
        }

        //TO CHANGE: For now I'll do if you take another damage after being on minimum shield, you die.

        if (crackIndex >= numberOfCrackedSprites)
        {
            Die();
        }
    }

    private void RecoverShield()
    {
        if (crackIndex > 0)
        {
            recoveryTimer += Time.deltaTime;
            if (recoveryTimer >= timeToRecoverShield)
            {
                crackIndex--;
                playerVisualController.RecoverCristalBall(crackIndex);
                recoveryTimer = 0;
            }
        }
    }

    private void Die()
    {
        playerMovementController.DisableInput();
        playerMovementController.enabled = false;
        isAlive = false;
        rb.WakeUp();
        rb.isKinematic = false;
        
    }
}
