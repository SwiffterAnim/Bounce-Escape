using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerLifeController : MonoBehaviour
{
    public static event Action OnPlayerDiedEvent;

    [SerializeField] private PlayerCollisionController playerCollisionController;
    [SerializeField] private PlayerVisualController playerVisualController;
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private GameObject protectiveshield;
    
    //TESTING the broken player thing.--------------------------------------------------------------------------------
    [SerializeField] private GameObject brokenPlayer;
    [SerializeField] private SpriteRenderer SR;

    [SerializeField] float timeToRecoverShield = 3f;

    private Rigidbody2D rb;
    private ObstacleEntity obstacleEntity;

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
        PlayerManager.Instance.isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        numberOfCrackedSprites = playerVisualController.crackStages.Length - 1;
    }

    
    void Update()
    {
        if(PlayerManager.Instance.isAlive)
        {
            RecoverShield();
        }
    }

    private void OnCollisionEnter2DEvent(Collision2D other)
    {

        if (other.gameObject.TryGetComponent<ObstacleEntity>(out obstacleEntity))
        {
            if (obstacleEntity.doesDamage && PlayerManager.Instance.isAlive)
            {
                TakeDamage();
            }
            if (obstacleEntity.isDestroyableAfterImpact)
            {
                Destroy(other.gameObject); //I think I should destroy this object on the ProjectileDestroyController... But I don't know how hehe --------------------------------------------------------------------------------
            }
        }

    }

    private void TakeDamage()
    {
        TimeManager.Instance.DoSlowMotion();
        Instantiate(protectiveshield, transform.position, Quaternion.identity);
        ps.Play();
        recoveryTimer = 0;


        //TO CHANGE: For now I'll do if you take another damage after being on minimum shield, you die. --------------------------------------------------------------------------------

        if (crackIndex + 1 > numberOfCrackedSprites)
        {
            Die();
        }



        if (crackIndex < numberOfCrackedSprites)
        {
            crackIndex++;
            playerVisualController.CrackCristalBall(crackIndex);
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
        OnPlayerDiedEvent?.Invoke();
        playerMovementController.DisableInput();
        playerMovementController.enabled = false;
        PlayerManager.Instance.isAlive = false;
        rb.WakeUp();
        rb.isKinematic = false;

        //TESTING the broken player thing.--------------------------------------------------------------------------------
        GameObject destroyed = Instantiate(brokenPlayer);
        destroyed.transform.position = transform.position;
        SR.enabled = false;
        
    }
}
