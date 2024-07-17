using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] Rigidbody2D rb;

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


    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public Vector2 GetPlayerVelocity()
    {
        return rb.velocity;
    }
}
