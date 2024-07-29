using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    PlayerLifeController playerLifeController;

    public bool isAlive => playerLifeController.isAlive;

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

    private void Start() { }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public Vector2 GetPlayerVelocity()
    {
        return rb.velocity;
    }
}
