using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHookController : MonoBehaviour
{
    [SerializeField] private ObstacleEntity obstacleEntity;
    [SerializeField] float reactivateHookAfter = 5f;

    private SpriteRenderer sr;
    private float timerForHookReactivation = 0;

    private void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!obstacleEntity.hooknessActivated)
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
        obstacleEntity.hooknessActivated = false;
        Destroy(GetComponent<CircleCollider2D>());
    }

    public void ReactivateHook()
    {
        timerForHookReactivation = 0;
        Color tmp = sr.color;
        tmp.a = 255f;
        sr.color = tmp;
        obstacleEntity.hooknessActivated = true;
        gameObject.AddComponent<CircleCollider2D>();
        CircleCollider2D myCollider = gameObject.GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
    }
}
