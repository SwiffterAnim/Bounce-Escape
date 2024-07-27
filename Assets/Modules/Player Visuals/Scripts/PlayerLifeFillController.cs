using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeFillController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer rend;

    [SerializeField]
    private ParticleSystem bloodDrops;

    private void Start()
    {
        SetBloodDropEmissionRate(0);
    }

    public void SetFillValue(float amount)
    {
        rend.material.SetFloat("_Fill", amount);
    }

    public void SetBloodDropEmissionRate(int crackIndex)
    {
        var myBloodEmission = bloodDrops.emission;
        myBloodEmission.rateOverTime = crackIndex * 4;
    }
}
