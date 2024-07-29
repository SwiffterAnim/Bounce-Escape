using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingCollider2D : MonoBehaviour
{
    [SerializeField]
    private int numSegments = 16;

    [SerializeField]
    private float outterRadius = 5f; //not sure what these radius are.

    [SerializeField]
    private float colliderThickness = 3f; //again not sure what these are.

    private void Start()
    {
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[(numSegments + 1)];

        for (int i = 0; i <= numSegments; i++)
        {
            float angle = i * Mathf.PI * 2f / numSegments;
            points[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * outterRadius;
        }

        edgeCollider.points = points;
        edgeCollider.edgeRadius += colliderThickness;
    }
}
