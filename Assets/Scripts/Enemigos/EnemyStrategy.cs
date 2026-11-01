using UnityEngine;

namespace Enemigos
{
    
    public abstract class EnemyStrategy : ScriptableObject
    {
        [SerializeField] private float maxHealth = 10f;
        [SerializeField] private float velocity = 2f;
        [SerializeField] private int bounty = 5;
        [SerializeField] private float Armour = 0f;
        public float VelocityMultiplier { get; set; } = 1f;
        private float CurrentVelocity => velocity * VelocityMultiplier;

        public float MaxHealth => maxHealth;
        public float Velocity => velocity;
        public int Bounty => bounty;

        public virtual bool Move(Enemy enemy, Path path, ref int waypointIndex)
        {
            Vector2 actualPosition = enemy.transform.position;
            Vector2 objective = path.GetWaypoint(waypointIndex);

            Vector2 newPosition = Vector2.MoveTowards(actualPosition, objective, CurrentVelocity * Time.deltaTime);

            enemy.transform.position = new Vector3(newPosition.x, newPosition.y, enemy.transform.position.z);

            if (Vector2.Distance(newPosition, objective) < 0.05f)
            {
                waypointIndex++;
                return waypointIndex >= path.AmountOfWaypoints;
            }

            return false;
        }

        public virtual float CheckResistance(Enemy enemy, float amount)
        {
            return amount - Armour;
        }

        public virtual void OnDeath(Enemy enemy)
        {
            enemy.GenerateReward(Bounty);
        }
    }
}