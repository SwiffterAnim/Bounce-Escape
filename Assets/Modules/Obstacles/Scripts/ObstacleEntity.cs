using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEntity : MonoBehaviour
{
    [SerializeField] float reactivateHookAfter = 5f;
    
    public bool hooknessActivated;
    public float attractionSpeed = 3f;

    private SpriteRenderer sr;
    private float timerForHookReactivation = 0;

    //Not sure if any of this Activation and Deactivation of the hooks should be here on this script.
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!hooknessActivated)
        {
            timerForHookReactivation += Time.deltaTime;
            if (timerForHookReactivation >= reactivateHookAfter)
            {
                ReactivateHook();
            }
        }
        
    }

    public void DeactivateHook()
    {
        Color tmp = sr.color;
        tmp.a = 0f;
        sr.color = tmp;
        hooknessActivated = false;
        Destroy(GetComponent<CircleCollider2D>());
    }

    public void ReactivateHook()
    {
        timerForHookReactivation = 0;
        Color tmp = sr.color;
        tmp.a = 255f;
        sr.color = tmp;
        hooknessActivated = true;
        gameObject.AddComponent<CircleCollider2D>();
        CircleCollider2D myCollider = gameObject.GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
    }
}
