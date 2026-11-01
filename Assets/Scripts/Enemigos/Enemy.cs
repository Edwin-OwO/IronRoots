 
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
        
        public float Velocity => strategy.VelocityMultiplier;
        
        public event Action<float> OnEntryGenerated;
        public static event Action OnEnemyDied;
        public event Action<Enemy> OnEnemyReachedEnd;

        public void Initialize(Path assignedPath, EnemyStrategy assignedStrategy)
        {
            path = assignedPath;
            strategy = assignedStrategy;

            health = strategy.MaxHealth;
            actualIndexWaypoint = 0;
            finished = false;
            initialized = true;

            EconomyManager.Instance.RegisterEntity(this);
        }

        private void Update()
        {
            if (!initialized || finished || path == null)
                return;

            if (strategy.Move(this, path, ref actualIndexWaypoint))
                OnEndStep();
        }

        public void TakeDamage(float amount)
        {
            if (!initialized || finished) return;

            strategy.TakeDamage(this, amount);
        }

        public void ApplyDamage(float amount)
        {
            if (!initialized || finished) return;

            health -= amount;

            if (attackParticles != null)
                attackParticles.Play();

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
        
        public void UpdateVelocity(float newVelocity)
        {
            strategy.VelocityMultiplier = newVelocity;      
        }
    }
}

