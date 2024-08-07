using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEntity : MonoBehaviour
{
    public float projectileSpeed = 7;
    public Rigidbody2D rb;

    private float timer = 0;
    public Vector2 velocity;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.01)
        {
            velocity = GetComponent<Rigidbody2D>().velocity;
        }
    }
}
