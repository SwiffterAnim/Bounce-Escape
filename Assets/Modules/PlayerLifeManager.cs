using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private PlayerCollisionController playerCollisionController;
    [SerializeField] private PlayerVisualController playerVisualController;
    [SerializeField] float timeToRecoverShield = 3f;

    private Rigidbody2D rb;

    public bool isAlive;

    private float recoveryTimer = 0;
    private int crackIndex;

    private void Awake()
    {
        playerCollisionController.OnCollisionEnter2DEvent += OnCollisionEnter2DEvent;

        if(!gameObject.GetComponent<PlayerMovementController>().enabled)
        {
            gameObject.GetComponent<PlayerMovementController>().enabled = true;
        }
        
        /*
        if(!gameObject.TryGetComponent(out PlayerMovementController playerMovement))
        {
            gameObject.AddComponent<PlayerMovementController>();
        }
        */
    }
    private void OnDestroy()
    {
        playerCollisionController.OnCollisionEnter2DEvent -= OnCollisionEnter2DEvent;    
    }

    // Start is called before the first frame update
    void Start()
    {
        crackIndex = 0;
        isAlive = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            RecoverShield();
        }
    }

    private void OnCollisionEnter2DEvent(Collision2D other)
    {
        recoveryTimer = 0;
        if (crackIndex < 4 && isAlive)
        {
            crackIndex++;
            playerVisualController.CrackCristalBall(crackIndex);
        }

        //TO CHANGE: For now I'll do if you take another damage after being on minimum shield, you die.

        if (crackIndex >= 4 && isAlive)
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
        gameObject.GetComponent<PlayerMovementController>().enabled = false;
        //Somehow this (below) works...
        // Destroy(GetComponent<PlayerMovementController>());
        isAlive = false;
        rb.WakeUp();
        rb.isKinematic = false;
        
    }
}
