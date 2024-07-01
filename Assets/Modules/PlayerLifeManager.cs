using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField] private PlayerCollisionController playerCollisionController;
    [SerializeField] private PlayerVisualController playerVisualController;
    [SerializeField] float timeToRecoverShield = 3f;

    private float recoveryTimer = 0;
    private int crackIndex;

    private void Awake()
    {
        playerCollisionController.OnCollisionEnter2DEvent += OnCollisionEnter2DEvent;
    }
    private void OnDestroy()
    {
        playerCollisionController.OnCollisionEnter2DEvent -= OnCollisionEnter2DEvent;    
    }

    // Start is called before the first frame update
    void Start()
    {
        crackIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RecoverShield();
    }

    private void OnCollisionEnter2DEvent(Collision2D other)
    {
        recoveryTimer = 0;
        if (crackIndex < 4)
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
}
