using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLifeController : MonoBehaviour
{
    public static event Action OnPlayerDiedEvent;

    [Header("References")]
    [SerializeField]
    private PlayerCollisionController playerCollisionController;

    [SerializeField]
    private PlayerVisualController playerVisualController;

    [SerializeField]
    private PlayerMovementController playerMovementController;

    [SerializeField]
    private ParticleSystem ps;

    [SerializeField]
    private GameObject protectiveshield;

    [SerializeField]
    private PlayerLifeFillController fill;

    [SerializeField]
    private Rigidbody2D rb;

    [Header("Variables")]
    [SerializeField]
    private float timeToRecoverShield = 3f;

    [SerializeField]
    private float playerHealth = 100;

    [SerializeField]
    private float damagePerSec1Crack = 1;

    [SerializeField]
    private float damagePerSec2Cracks = 3;

    [SerializeField]
    private float damagePerSec3Cracks = 6;

    [SerializeField]
    private float damageFullCrack = 20;

    private float healthAmount;
    private float recoveryTimer = 0;
    private int crackIndex;
    private int numberOfCrackedSprites;

    public bool isAlive;

    private void Awake()
    {
        playerCollisionController.OnCollisionEnter2DEvent += OnCollisionEnter2DEvent;

        if (!playerMovementController.enabled)
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
        healthAmount = playerHealth;
        crackIndex = 0;
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
        numberOfCrackedSprites = playerVisualController.crackStages.Length - 1;
    }

    void Update()
    {
        if (isAlive)
        {
            if (healthAmount <= 0)
            {
                Die();
            }

            RecoverShield();
            TakeDamagePerSecond();
        }
    }

    private void OnCollisionEnter2DEvent(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out ObstacleEntity obstacleEntity))
        {
            if (obstacleEntity.doesDamage && isAlive)
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

        if (crackIndex + 1 > numberOfCrackedSprites)
        {
            healthAmount -= damageFullCrack;
        }

        if (crackIndex < numberOfCrackedSprites)
        {
            crackIndex++;
            playerVisualController.CrackCristalBall(crackIndex);
        }
    }

    public void TakeDamagePerSecond()
    {
        if (crackIndex == 0)
        {
            return;
        }
        else if (crackIndex == 1)
        {
            healthAmount -= damagePerSec1Crack * Time.deltaTime;
        }
        else if (crackIndex == 2)
        {
            healthAmount -= damagePerSec2Cracks * Time.deltaTime;
        }
        else if (crackIndex == 3)
        {
            healthAmount -= damagePerSec3Cracks * Time.deltaTime;
        }

        fill.SetFillValue(healthAmount / playerHealth);
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
        TimeManager.Instance.DoSlowMotion();
        CameraManager.Instance.Shake();
        playerMovementController.DisableInput();
        playerMovementController.enabled = false;
        isAlive = false;
        rb.WakeUp();
        rb.isKinematic = false;
    }
}
