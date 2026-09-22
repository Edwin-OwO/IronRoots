 using System;
using System.Collections.Generic;
using Economia;
using UnityEngine;

namespace Enemigos
{
    public class Enemy : MonoBehaviour, IEntrySource
    { 
        [SerializeField] private float health = 10f;
        [SerializeField] private float velocity = 2f;
        [SerializeField] private int bounty = 5;

        private Path path;
        private int actualIndexWaypoint;
        
        public event Action<float> OnEntryGenerated;
        public event Action<Enemy> OnEnemyDied;
        public event Action<Enemy> OnEnemyReachedEnd;
        
        public void Initialize(Path path)
        {
            this.path = path;
            actualIndexWaypoint = 0;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (path == null) return;
            Vector2 actualPosition = transform.position;
            Vector2 objetive = path.GetWaypoint(actualIndexWaypoint);
            Vector2 newPosition = Vector2.MoveTowards(actualPosition,objetive, velocity * Time.deltaTime); 
            
            if (Vector2.Distance(newPosition, objetive) < 0.05f)
            {
                actualIndexWaypoint++;
                if (actualIndexWaypoint >= path.AmountOfWaypoints)
                {
                    OnEndStep();
                }
            }
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
            if (health <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            OnEntryGenerated?.Invoke(bounty);
            OnEnemyDied?.Invoke(this);
            Destroy(gameObject);
        }

        private void OnEndStep()
        {
            OnEnemyReachedEnd?.Invoke(this);
            Destroy(gameObject);
        }
    }
}