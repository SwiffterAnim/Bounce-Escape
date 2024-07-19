using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeFillController : MonoBehaviour
{
    [SerializeField] Material material;
    private float fillValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetFillValue(1/10 * Time.deltaTime);
    }

    public void SetFillValue(float amount)
    {
        material.SetFloat("_Fill", fillValue - amount);
    }
}
