using Enemigos;
using UnityEngine;

namespace Torretas
{
    public class Proyectile : MonoBehaviour
    {
        [SerializeField] private float velocity = 8f;
        [SerializeField] private float distanceBetweenImpact = 0.1f;

        private Enemy Objective;
        private float damage;
        private float zPosition;
        
        public void Initialize( Enemy ObjectiveAssigned, float damageAssigned)
        {
            Objective = ObjectiveAssigned;
            damage = damageAssigned;
            zPosition = transform.position.z;
        }

        private void Update()
        {
            if (Objective == null) return;
            
            Vector2 actualPosition = transform.position;
            Vector2 objectivePosition = Objective.transform.position;
            Vector2 newPosition = Vector2.MoveTowards(actualPosition, objectivePosition, velocity*Time.deltaTime);
            
            transform.position = new Vector3(newPosition.x, newPosition.y, zPosition);

            if (Vector2.Distance(newPosition, objectivePosition) <= distanceBetweenImpact)
            {
                ObjectiveImpact();
            }
        }

        private void ObjectiveImpact()
        {
            Objective.TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }
}