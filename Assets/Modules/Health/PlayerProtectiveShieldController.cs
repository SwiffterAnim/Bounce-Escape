using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProtectiveShieldController : MonoBehaviour
{
    [SerializeField] private Vector3 initialScale;
    [SerializeField] private Vector3 finalScale;
    [SerializeField] private float shieldTime;
    [SerializeField] private float decelerationExponent = 20;
    [SerializeField] private float retractionSpeed = 0.5f;

    private float expansionProgress = 0;
    private float retractionProgress = 0;

    private void Start()
    {
        transform.localScale = initialScale;
    }

    private void Update()
    {
        transform.position = PlayerManager.Instance.GetPlayerPosition();
        expansionProgress += Time.deltaTime;
        float expansionT = expansionProgress / shieldTime;
        expansionT = 1 - Mathf.Pow(1 - expansionT, decelerationExponent);
        expansionT = Mathf.Clamp01(expansionT);

        transform.localScale = Vector3.Lerp(initialScale, finalScale, expansionT);


        if (expansionProgress >= shieldTime)
        {
            retractionProgress += Time.deltaTime;
            float retractionT = retractionProgress / retractionSpeed;
            retractionT = retractionT * retractionT;
            retractionT = Mathf.Clamp01(retractionT);

            transform.localScale = Vector3.Lerp(finalScale, initialScale, retractionT);
            Destroy(gameObject, retractionSpeed);
            

        }

        // Destroy(gameObject, shieldTime);

        if(!PlayerManager.Instance.isAlive)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.TryGetComponent(out ObstacleEntity obstacleEntity))
        {
            if(obstacleEntity.isDestroyableAfterImpact)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
