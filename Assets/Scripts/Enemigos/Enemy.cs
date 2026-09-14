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
        [SerializeField] private int damage = 1;

        private Path path;
        private int actualIndexWeapon;
        private float zposition;

        public int Damage => damage;
        public event Action<float> OnEntryGenerated;
        public event Action<Enemy> OnEnemyDied;
        public event Action<Enemy> OnEnemyReachedEnd;
        
        public void Initialize(Path path)
        {
            this.path = path;
            actualIndexWeapon = 0;
            zposition = transform.position.z;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (path == null) return;
            Vector2 actualPosition = transform.position;
            Vector2 objetive = path.GetWaypoint(actualIndexWeapon);
            Vector2 newPosition = Vector2.MoveTowards(actualPosition,objetive, velocity * Time.deltaTime); 
            transform.position = new Vector3(newPosition.x, newPosition.y, zposition);
            
            if (Vector2.Distance(transform.position, objetive) < 0.05f)
            {
                actualIndexWeapon++;
                if (actualIndexWeapon >= path.AmountOfWaypoints)
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