using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHookController : MonoBehaviour
{
    [SerializeField]
    private ObstacleEntity obstacleEntity;

    [SerializeField]
    float reactivateHookAfter = 5f;

    [SerializeField]
    Animator hookAnimator;

    readonly string animationTransitions = "AnimStages";

    private SpriteRenderer sr;
    private float timerForHookReactivation = 0;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

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
        // Color tmp = sr.color;
        // tmp.a = 0f;
        // sr.color = tmp;
        obstacleEntity.hooknessActivated = false;
        Destroy(GetComponent<CircleCollider2D>());

        //Testing Animator ________________________________________
        hookAnimator.SetInteger(animationTransitions, 2);
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
        myCollider.radius = 0.5f;

        //Testing Animator ________________________________________
        hookAnimator.SetInteger(animationTransitions, 0);
    }

    public void PlayHookingAnimation()
    {
        //Testing Animator ________________________________________
        hookAnimator.SetInteger(animationTransitions, 1);
    }
}
