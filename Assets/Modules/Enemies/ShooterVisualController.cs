using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterVisualController : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject shooter;
    [SerializeField] private GameObject destroyer;
    [SerializeField] private EnemyKillController enemyKillController;
    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private GameObject explosion;
    
    [SerializeField] private float sparkSpeed;
    private float sparkLerpSpeed;
    private float timer = 0;
    private Vector3 destroyerPosition;

    private void OnEnable()
    {
        EnemyMovementController.OnShooterIsDeadEvent += OnShooterIsDeadEvent;
    }

    private void OnDisable()
    {
        EnemyMovementController.OnShooterIsDeadEvent -= OnShooterIsDeadEvent;
    }

    void Update()
    {
        DrawnLine();
        if(enemyEntity.isDead)
        {
            timer += Time.deltaTime;
            float t = timer / sparkLerpSpeed;
            destroyer.transform.position = Vector3.Lerp(destroyerPosition, shooter.transform.position, t);
            if(timer >= sparkLerpSpeed)
            {
                Instantiate(explosion, destroyer.gameObject.transform.position, Quaternion.identity);
                CameraManager.Instance.Shake();
                enemyKillController.DestroyThisEnemy();
            }
        }
    }

    private void DrawnLine()
    {
        lineRenderer.SetPosition(0, shooter.transform.position);
        lineRenderer.SetPosition(1, destroyer.transform.position);
    }

    private void SparkAnimation()
    {
        SpriteRenderer destroyerSpriteRenderer = destroyer.GetComponent<SpriteRenderer>();
        destroyerSpriteRenderer.enabled = false;
        ParticleSystem sparks = destroyer.gameObject.GetComponent<ParticleSystem>();
        sparks.Play();
    }

    private void OnShooterIsDeadEvent()
    {
        destroyerPosition = destroyer.transform.position;
        float distance = Vector3.Distance(destroyerPosition, shooter.transform.position);
        sparkLerpSpeed = distance / sparkSpeed;
        SparkAnimation();
    }

    
}
