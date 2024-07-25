using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private float timeSpawnShooter = 10f;

    [SerializeField]
    private GameObject shooter;

    private float timer;

    private void Start()
    {
        InstantiateShooter();
    }

    private void Update()
    {
        if (PlayerManager.Instance.isAlive)
        {
            //if Difficulty or Level = x enum whatever: Because it depends on the level which enemies spawn.
            timer += Time.deltaTime;
            if (timer > timeSpawnShooter)
            {
                timer = 0;
                InstantiateShooter();
            }
        }
    }

    private void InstantiateShooter()
    {
        Instantiate(shooter, transform.position, Quaternion.identity);
    }
}
