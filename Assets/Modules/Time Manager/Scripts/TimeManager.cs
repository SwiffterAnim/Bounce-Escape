using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [SerializeField]
    private float slowDownFactor = 0.05f;

    [SerializeField]
    private float slowDownLength = 2f;

    [SerializeField]
    private float timeToNormalSpeed = 1f;

    [SerializeField]
    private float tempTimer = 0;

    private bool isSlowedDown = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isSlowedDown)
        {
            tempTimer += Time.deltaTime / slowDownFactor;
            if (tempTimer > slowDownLength)
            {
                UndoSlowMotion();
            }
        }
    }

    public void DoSlowMotion()
    {
        // normal timescale runs at 1. 0.05 means it's running 20 times slower.
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        isSlowedDown = true;
    }

    private void UndoSlowMotion()
    {
        Time.timeScale += 1f / timeToNormalSpeed * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        if (Time.timeScale == 1)
        {
            Time.fixedDeltaTime = 0.02f;
            tempTimer = 0;
            isSlowedDown = false;
        }
    }

    public void CancelSlowMotion()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        tempTimer = 0;
        isSlowedDown = false;
    }
}
