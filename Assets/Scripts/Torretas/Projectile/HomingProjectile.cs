using Enemigos;
using UnityEngine;

namespace Torretas
{
    public class HomingProjectile : Proyectile
    {
        [SerializeField] private float distanceBetweenImpact = 0.1f;
        [SerializeField] private float lifetimeAfterLosingTarget = 1.5f;
 
        private Vector2 lastDirection;
        private bool objectiveLost;
        private float lifetimeTimer;
 
        public override void Initialize(Enemy objectiveAssigned, float damageAssigned)
        {
            base.Initialize(objectiveAssigned, damageAssigned);
            lastDirection = (targetPosition - (Vector2)transform.position).normalized;
        }
 
        protected override bool Move()
        {
            if (objective == null)
            {
                MoveStraightAndExpire();
                return false;
            }
 
            Vector2 actualPosition = transform.position;
            Vector2 objectivePosition = objective.transform.position;
            lastDirection = (objectivePosition - actualPosition).normalized;
 
            Vector2 newPosition = Vector2.MoveTowards(actualPosition, objectivePosition, velocity * Time.deltaTime);
            transform.position = newPosition;
 
            return Vector2.Distance(newPosition, objectivePosition) <= distanceBetweenImpact;
        }
 
        private void MoveStraightAndExpire()
        {
            if (!objectiveLost)
            {
                objectiveLost = true;
                lifetimeTimer = lifetimeAfterLosingTarget;
            }
 
            transform.position += (Vector3)(lastDirection * velocity * Time.deltaTime);
 
            lifetimeTimer -= Time.deltaTime;
            if (lifetimeTimer <= 0f)
            {
                Destroy(gameObject);
            }
        }
 
        protected override void OnImpact()
        {
            objective.TakeDamage(damage);
        }
    }
}


