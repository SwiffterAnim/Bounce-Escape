using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeController : MonoBehaviour
{
    public event Action OnShooterGetsDestroyedEvent;

    [SerializeField]
    private EnemyMovementController destroyerMovementController;

    [SerializeField]
    private EnemyEntity destroyerEnemyEntity;

    private void OnEnable()
    {
        destroyerMovementController.OnDestroyerGetsCaughtEvent += OnDestroyerGetsCaughtEvent;
    }

    private void OnDisable()
    {
        destroyerMovementController.OnDestroyerGetsCaughtEvent -= OnDestroyerGetsCaughtEvent;
    }

    public void DestroyThisEnemy() //Not sure I need this. Was thinking that maybe other types of enemies do but...
    {
        Destroy(gameObject);
    }

    private void OnDestroyerGetsCaughtEvent()
    {
        OnShooterGetsDestroyedEvent?.Invoke();
        destroyerEnemyEntity.isDead = true;
    }
}
