using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterVisualController : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private GameObject shooter;

    [SerializeField]
    private GameObject destroyer;

    [SerializeField]
    private EnemyLifeController enemyLifeController;

    [SerializeField]
    private EnemyEntity destroyerEnemyEntity;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private EnemyMovementController enemyMovementController;

    [SerializeField]
    private float sparkSpeed;
    private float sparkLerpSpeed;
    private float timer = 0;
    private Vector3 destroyerPosition;

    private void OnEnable()
    {
        enemyLifeController.OnShooterGetsDestroyedEvent += OnShooterGetsDestroyedEvent;
    }

    private void OnDisable()
    {
        enemyLifeController.OnShooterGetsDestroyedEvent -= OnShooterGetsDestroyedEvent;
    }

    void Update()
    {
        DrawnLine();
        if (destroyerEnemyEntity.isDead)
        {
            timer += Time.deltaTime;
            float t = timer / sparkLerpSpeed;
            destroyer.transform.position = Vector3.Lerp(
                destroyerPosition,
                shooter.transform.position,
                t
            );
            if (timer >= sparkLerpSpeed)
            {
                Instantiate(
                    explosion,
                    destroyer.gameObject.transform.position,
                    Quaternion.identity
                );
                CameraManager.Instance.Shake();
                Destroy(enemyLifeController.gameObject);
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

    private void OnShooterGetsDestroyedEvent()
    {
        destroyerPosition = destroyer.transform.position;
        float distance = Vector3.Distance(destroyerPosition, shooter.transform.position);
        sparkLerpSpeed = distance / sparkSpeed;
        SparkAnimation();
    }
}
