using Enemigos;
using UnityEngine;

namespace Torretas
{
    public class Proyectile : MonoBehaviour
    {
        [SerializeField] private float velocity = 8f;
        [SerializeField] private float distanceBetweenImpact = 0.1f;
        [SerializeField] private float lifetimeAfterLosingTarget = 1.5f;

        private Enemy objective;
        private float damage;

        private Vector2 lastDirection;
        private bool objectiveLost;
        private float lifetimeTimer;

        public void Initialize(Enemy objectiveAssigned, float damageAssigned)
        {
            objective = objectiveAssigned;
            damage = damageAssigned;
        }

        private void Update()
        {
            if (objective == null)
            {
                MoveStraightAndExpire();
                return;
            }

            Vector2 actualPosition = transform.position;
            Vector2 objectivePosition = objective.transform.position;
            lastDirection = (objectivePosition - actualPosition).normalized;

            Vector2 newPosition = Vector2.MoveTowards(actualPosition, objectivePosition, velocity * Time.deltaTime);
            transform.position = newPosition;

            if (Vector2.Distance(newPosition, objectivePosition) <= distanceBetweenImpact)
            {
                ObjectiveImpact();
            }
        }
        
        private void MoveStraightAndExpire()
        {
            if (!objectiveLost)
            {
                objectiveLost = true;
                lifetimeTimer = lifetimeAfterLosingTarget;
            }

            Vector2 avance = lastDirection * velocity * Time.deltaTime;
            Vector2 nuevaPosicion = (Vector2)transform.position + avance;
            transform.position = nuevaPosicion;

            lifetimeTimer -= Time.deltaTime;
            if (lifetimeTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }

        private void ObjectiveImpact()
        {
            objective.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}