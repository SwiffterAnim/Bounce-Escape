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

    //TODO: Add a prefab of the explosion (empty gameobject with particleSystem) for it to be independent in the scene and not attatched to this.
    
    [SerializeField] private float sparkSpeed = 0.2f;
    private float timer = 0;


    void Update()
    {
        DrawnLine();
        if(enemyEntity.isDead)
        {
            Vector3 temp = PlayerManager.Instance.GetPlayerPosition();
            timer += Time.deltaTime;
            SparkAnimation(); //this function is being called every frame... and I only need it once.
            float t = timer / sparkSpeed;
            destroyer.transform.position = Vector3.Lerp(temp, shooter.transform.position, t);
            if(timer >= sparkSpeed)
            {
                Instantiate(explosion, destroyer.gameObject.transform.position, Quaternion.identity);
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
        Debug.Log(sparks);
        sparks.Play();
    }
}
