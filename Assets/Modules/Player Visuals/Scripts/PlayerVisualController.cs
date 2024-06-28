using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    [SerializeField] Sprite[] crackStages;
    [SerializeField] float timeToRecoverShield = 3f;

    private float recoveryTimer = 0;
    private SpriteRenderer sr;
    // private Sprite currentSprite;
    private int crackIndex;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        crackIndex = 0;
        sr.sprite = crackStages[crackIndex];
    }

    void Update()
    {
        RecoverCristalBall();
    }

    private void CrackCristalBall()
    {
        recoveryTimer = 0;
        if (crackIndex < 4)
        {
            crackIndex++;
            sr.sprite = crackStages[crackIndex];
        }
        
    }

    private void RecoverCristalBall()
    {
        if (crackIndex > 0)
        {
            recoveryTimer += Time.deltaTime;
            if (recoveryTimer >= timeToRecoverShield)
            {
                crackIndex--;
                sr.sprite = crackStages[crackIndex];
                recoveryTimer = 0;
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CrackCristalBall();
    }
}
