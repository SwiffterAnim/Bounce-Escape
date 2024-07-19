using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimationController : MonoBehaviour
{
    private Animator animator;
    private float randomOffset;

    readonly string ANIMATION = "Animation";
    readonly string CLONE = "(Clone)";
    
    void Start()
    {
        string enemyName = gameObject.name.Replace(CLONE, "").Trim();
        
        string animationName = enemyName + ANIMATION;
        
        animator = GetComponent<Animator>();
        randomOffset = Random.Range(0f, 1f);

        animator.Play(animationName, 0, randomOffset);
    }
}
