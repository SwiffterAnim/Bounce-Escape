using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeFillController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer rend;

    public void SetFillValue(float amount)
    {
        rend.material.SetFloat("_Fill", amount);
    }
}
