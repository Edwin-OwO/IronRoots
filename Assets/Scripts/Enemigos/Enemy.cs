 
using System;
using Economia;
using UnityEngine;

namespace Enemigos
{
    public class Enemy : MonoBehaviour, IEntrySource
    {
        [SerializeField] private ParticleSystem attackParticles;
        

        private EnemyStrategy strategy;
        private Path path;

        private float health;
        private int actualIndexWaypoint;
        private bool finished;
        private bool initialized;
        private SpriteRenderer spriteRenderer;
        
        public float VelocityMultiplier { get; set; } = 1f;
        
        public event Action<float> OnEntryGenerated;
        public static event Action OnEnemyDied;
        public event Action<Enemy> OnEnemyReachedEnd;

        public void Initialize(Path assignedPath, EnemyStrategy assignedStrategy)
        {
            path = assignedPath;
            strategy = assignedStrategy;
            spriteRenderer = GetComponent<SpriteRenderer>();

            health = strategy.MaxHealth;
            actualIndexWaypoint = 0;
            finished = false;
            initialized = true;

            EconomyManager.Instance.RegisterEntity(this);
            
            if (spriteRenderer != null && strategy.EnemySprite != null)
            {
                spriteRenderer.sprite = strategy.EnemySprite;
                Debug.Log("qa");
            }
        }

        private void Update()
        {
            if (!initialized || finished || path == null)
                return;

            if (strategy.Move(this, path, ref actualIndexWaypoint, VelocityMultiplier))
                OnEndStep();
        }

        public void CheckResistance(float amount)
        {
            
            if (!initialized || finished) return;

            amount = strategy.CheckResistance(this, amount);
            ApplyDamage(amount);
        }

        public void ApplyDamage(float amount)
        {
            if (!initialized || finished) return;

            health -= amount;

            if (attackParticles != null)
            {
                attackParticles.Play();
            }
            if (health <= 0f)
                Die();
        }

        private void Die()
        {
            if (finished) return;
            finished = true;

            strategy.OnDeath(this);

            OnEnemyDied?.Invoke();

            Unregister();
            Destroy(gameObject);
        }

        private void OnEndStep()
        {
            if (finished) return;
            finished = true;

            OnEnemyReachedEnd?.Invoke(this);

            Unregister();
            Destroy(gameObject);
        }

        public void GenerateReward(float amount)
        {
            OnEntryGenerated?.Invoke(amount);
        }

        private void Unregister()
        {
            EconomyManager.Instance.UnregisterEntity(this);
        }
    }
}

