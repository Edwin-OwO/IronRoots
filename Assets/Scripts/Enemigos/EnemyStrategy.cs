using UnityEngine;

namespace Enemigos
{
    
    public abstract class EnemyStrategy : ScriptableObject
    {
        [SerializeField] private float maxHealth = 10f;
        [SerializeField] private float velocity = 2f;
        [SerializeField] private int bounty = 5;
        [SerializeField] private float Armour = 0f;
        [SerializeField] private Sprite enemySprite;

        public Sprite EnemySprite => enemySprite;
        public float MaxHealth => maxHealth;
        public float Velocity => velocity; 
        public int Bounty => bounty;

        public virtual bool Move(Enemy enemy, Path path, ref int waypointIndex, float velocityMultiplier)
        {
            Vector2 actualPosition = enemy.transform.position;
            Vector2 objective = path.GetWaypoint(waypointIndex);

            float currentVelocity = velocity * velocityMultiplier;
            
            Vector2 newPosition = Vector2.MoveTowards(actualPosition, objective, currentVelocity * Time.deltaTime);

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
            return Mathf.Max(0f, amount - Armour);
        }

        public virtual void OnDeath(Enemy enemy)
        {
            enemy.GenerateReward(Bounty);
        }
    }
}