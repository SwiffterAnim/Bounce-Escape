using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;


    private Camera mainCam;
    [SerializeField] private float shakeAmmount= 0.02f;
    [SerializeField] private float shakeFrequency = 0.003f;
    [SerializeField] private float shakeLength = 0.3f;


    private void Awake() {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Shake()
    {
        InvokeRepeating("DoShake", 0, shakeFrequency);
        Invoke("StopShake", shakeLength);
    }

    void DoShake()
    {
        if(shakeAmmount > 0)
        {
            Vector3 camPos = mainCam.transform.position;
            
            float offsetX = Random.value * shakeAmmount * 2 - shakeAmmount;
            float offsetY = Random.value * shakeAmmount * 2 - shakeAmmount;
            camPos.x = offsetX;
            camPos.y = offsetY;

            mainCam.transform.position = camPos;
        }
    }

    void StopShake()
    {
        CancelInvoke("DoShake");
        mainCam.transform.localPosition = new Vector3(0, 0, -10);
    }
}
