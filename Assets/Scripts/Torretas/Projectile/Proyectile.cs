using Enemigos;
using UnityEngine;

namespace Torretas
{
    public abstract class Proyectile : MonoBehaviour
    {
        [SerializeField] protected float velocity = 8f;

        protected Enemy objective;
        protected float damage;
        
        protected Vector2 targetPosition;

        public virtual void Initialize(Enemy objectiveAssigned, float damageAssigned)
        {
            objective = objectiveAssigned;
            damage = damageAssigned;
            targetPosition = objective.transform.position;
        }

        private bool impacted;

        protected virtual void Update()
        {
            if (impacted) return;

            if (Move())
            {
                impacted = true;
                OnImpact();
                FinishAfterImpact();
            }
        }

        protected virtual void FinishAfterImpact()
        {
            Destroy(gameObject);
        }
        
        protected abstract bool Move();
        
        protected abstract void OnImpact();
    }
}