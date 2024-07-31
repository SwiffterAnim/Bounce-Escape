using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("SHOOTER")]
    [SerializeField]
    private float timeSpawnShooter = 10f;

    [SerializeField]
    private GameObject shooterPrefab;

    public bool isSpawningShooter;

    [Header("ELECTRIC WALL")]
    [SerializeField]
    private float timeSpawnElectricWall = 10f;

    [SerializeField]
    private GameObject electricWallPrefab;

    [SerializeField]
    private float spawnRadiusOffScreen;

    public bool isSpawningElectricWall;

    private Coroutine shooterCoroutine;
    private Coroutine electricWallCoroutine;

    private void OnEnable()
    {
        PlayerLifeController.OnPlayerDiedEvent += OnPlayerDiedEvent;
    }

    private void OnDisable()
    {
        PlayerLifeController.OnPlayerDiedEvent -= OnPlayerDiedEvent;
    }

    private void Start()
    {
        shooterCoroutine = StartCoroutine(InstantiateShooterCoroutine());
        electricWallCoroutine = StartCoroutine(InstantiateElectricWallCoroutine());
    }

    private void Update()
    {
        if (PlayerManager.Instance.isAlive)
        {
            if (!isSpawningShooter || !isSpawningElectricWall)
            {
                StopSpawning();
            }
            if (isSpawningShooter || isSpawningElectricWall)
            {
                StartSpawning();
            }
        }
    }

    private IEnumerator InstantiateShooterCoroutine()
    {
        while (true)
        {
            if (isSpawningShooter)
            {
                yield return new WaitForSeconds(timeSpawnShooter);
                Instantiate(shooterPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator InstantiateElectricWallCoroutine()
    {
        while (true)
        {
            if (isSpawningElectricWall)
            {
                float randomZRotation = Random.Range(0, 180);

                Instantiate(
                    electricWallPrefab,
                    GetRandomPointOnCircle(spawnRadiusOffScreen),
                    Quaternion.Euler(0, 0, randomZRotation)
                );
                yield return new WaitForSeconds(timeSpawnElectricWall);
            }
            else
            {
                yield return null;
            }
        }
    }

    private Vector3 GetRandomPointOnCircle(float radius)
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, Mathf.PI * 2); // Mathf.PI * 2 = 360

        // Calculate the x and y coordinates using the angle and radius
        float x = Mathf.Cos(randomAngle) * radius;
        float y = Mathf.Sin(randomAngle) * radius;

        // Return the point as a Vector3 (assuming z = 0 for 2D)
        return new Vector3(x, y, 0);
    }

    private void StopSpawning()
    {
        if (!isSpawningShooter && shooterCoroutine != null)
        {
            StopCoroutine(InstantiateShooterCoroutine());
        }
        if (!isSpawningElectricWall && electricWallCoroutine != null)
        {
            StopCoroutine(InstantiateElectricWallCoroutine());
        }
    }

    private void StartSpawning()
    {
        if (isSpawningShooter && shooterCoroutine == null)
        {
            shooterCoroutine = StartCoroutine(InstantiateShooterCoroutine());
        }
        if (isSpawningElectricWall && electricWallCoroutine == null)
        {
            electricWallCoroutine = StartCoroutine(InstantiateElectricWallCoroutine());
        }
    }

    private void OnPlayerDiedEvent()
    {
        isSpawningElectricWall = false;
        isSpawningShooter = false;
        StopAllCoroutines();
    }
}
