using UnityEngine;

namespace Enemigos
{
    
    public abstract class EnemyStrategy : ScriptableObject
    {
        [SerializeField] private float maxHealth = 10f;
        [SerializeField] private float velocity = 2f;
        [SerializeField] private int bounty = 5;
        public float velocityMultiplier { get; set; } = 1f;
        private float currentVelocity => velocity * velocityMultiplier;

        public float MaxHealth => maxHealth;
        public float Velocity => velocity;
        public int Bounty => bounty;

        public virtual bool Move(Enemy enemy, Path path, ref int waypointIndex)
        {
            Vector2 actualPosition = enemy.transform.position;
            Vector2 objective = path.GetWaypoint(waypointIndex);

            Vector2 newPosition = Vector2.MoveTowards(actualPosition, objective, currentVelocity * Time.deltaTime);

            enemy.transform.position = new Vector3(newPosition.x, newPosition.y, enemy.transform.position.z);

            if (Vector2.Distance(newPosition, objective) < 0.05f)
            {
                waypointIndex++;
                return waypointIndex >= path.AmountOfWaypoints;
            }

            return false;
        }

        public virtual void TakeDamage(Enemy enemy, float amount)
        {
            enemy.ApplyDamage(amount);
        }

        public virtual void OnDeath(Enemy enemy)
        {
            enemy.GenerateReward(Bounty);
        }
    }
}