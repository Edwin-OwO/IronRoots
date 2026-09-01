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
        private readonly List<IEnemyObserver> observers = new List<IEnemyObserver>();

        public int Damage => damage;
        public event Action<int> OnEntryGenerated;
        
        public void Initialize(Path path)
        {
            this.path = path;
            actualIndexWeapon = 0;
        }

        public void Subscribe(IEnemyObserver observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
        }

        public void Unsubscribe(IEnemyObserver observer)
        {
            observers.Remove(observer);
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (path == null) return;

            Vector2 objetive = path.GetWaypoint(actualIndexWeapon);
            transform.position = Vector2.MoveTowards(transform.position, objetive, velocity * Time.deltaTime);

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

            foreach (var observador in observers)
            {
                observador.OnDie(this);
            }

            Destroy(gameObject);
        }

        private void OnEndStep()
        {
            foreach (var observador in observers)
            {
                observador.OnFinalStep(this);
            }

            Destroy(gameObject);
        }
    }
}