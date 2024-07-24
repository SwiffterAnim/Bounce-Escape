using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillController : MonoBehaviour
{

    public void DestroyThisEnemy()
    {
        Destroy(gameObject);
    }
}
