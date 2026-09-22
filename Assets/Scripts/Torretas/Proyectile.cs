using Enemigos;
using UnityEngine;

namespace Torretas
{
    public class Proyectile : MonoBehaviour
    {
        [SerializeField] private float velocity = 8f;
        [SerializeField] private float distanceBetweenImpact = 0.1f;

        private Enemy objective;
        private float damage;
        
        public void Initialize( Enemy ObjectiveAssigned, float damageAssigned)
        {
            objective = ObjectiveAssigned;
            damage = damageAssigned;
        }

        private void Update()
        {
            if (objective == null) return;
            
            Vector2 actualPosition = transform.position;
            Vector2 objectivePosition = objective.transform.position;
            Vector2 newPosition = Vector2.MoveTowards(actualPosition, objectivePosition, velocity*Time.deltaTime);
            transform.position = new Vector3(newPosition.x, newPosition.y);
            if (Vector2.Distance(newPosition, objectivePosition) <= distanceBetweenImpact)
            {
                ObjectiveImpact();
            }
        }

        private void ObjectiveImpact()
        {
            objective.TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }
}