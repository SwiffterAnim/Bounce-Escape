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

    private float timer;

    private void Update()
    {
        if (PlayerManager.Instance.isAlive)
        {
            //if Difficulty or Level = x enum whatever: Because it depends on the level which enemies spawn.
            timer += Time.deltaTime;
            if (timer >= timeSpawnShooter && isSpawningShooter)
            {
                timer = 0;
                InstantiateShooter();
            }
            else if (timer >= timeSpawnElectricWall && isSpawningElectricWall)
            {
                timer = 0;
                InstantiateElectricWall();
            }
        }
    }

    private void InstantiateShooter()
    {
        Instantiate(shooterPrefab, transform.position, Quaternion.identity);
    }

    private void InstantiateElectricWall()
    {
        float randomZRotation = Random.Range(0, 180);

        Instantiate(
            electricWallPrefab,
            GetRandomPointOnCircle(spawnRadiusOffScreen),
            Quaternion.Euler(0, 0, randomZRotation)
        );
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
}
