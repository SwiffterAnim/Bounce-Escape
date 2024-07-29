using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    public Sprite[] crackStages;

    [SerializeField]
    private GameObject brokenPlayer;

    [SerializeField]
    private SpriteRenderer SR;

    private void OnEnable()
    {
        PlayerLifeController.OnPlayerDiedEvent += OnPlayerDieEvent;
    }

    private void OnDisable()
    {
        PlayerLifeController.OnPlayerDiedEvent -= OnPlayerDieEvent;
    }

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = crackStages[0];
    }

    public void CrackCristalBall(int crackIndex)
    {
        sr.sprite = crackStages[crackIndex];
    }

    public void RecoverCristalBall(int crackIndex)
    {
        sr.sprite = crackStages[crackIndex];
    }

    private void OnPlayerDieEvent()
    {
        Instantiate(brokenPlayer, transform.position, Quaternion.identity);
        SR.enabled = false;
    }
}
