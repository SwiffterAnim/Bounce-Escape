using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    public event Action<Collision2D> OnCollisionEnter2DEvent;
    public event Action<Collider2D> OnTriggerEnter2DEvent;

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnCollisionEnter2DEvent?.Invoke(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter2DEvent?.Invoke(other);
    }
}
