using System;
using UnityEngine;

namespace Modules.Obstacles.Scripts
{
    public class ObstacleHookController: MonoBehaviour
    {
        [SerializeField] private ObstacleEntity obstacleEntity;
        
        private float timerForHookReactivation = 0;
        private SpriteRenderer sr;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!obstacleEntity.hooknessActivated)
            {
                timerForHookReactivation += Time.deltaTime;
                // if (timerForHookReactivation >= reactivateHookAfter)
                // {
                //     ReactivateHook();
                // }
            }
        
        }
        
        public void DeactivateHook()
        {
            Color tmp = sr.color;
            tmp.a = 0f;
            sr.color = tmp;
            obstacleEntity.hooknessActivated = false;
            // Destroy(GetComponent<CircleCollider2D>());
        }

        public void ReactivateHook()
        {
            timerForHookReactivation = 0;
            // Color tmp = sr.color;
            // tmp.a = 255f;
            // sr.color = tmp;
            obstacleEntity.hooknessActivated = true;
            // gameObject.AddComponent<CircleCollider2D>();
            // CircleCollider2D myCollider = gameObject.GetComponent<CircleCollider2D>();
            // myCollider.isTrigger = true;
        }
    }
}