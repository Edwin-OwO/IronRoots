using System;
using Economia;
using UnityEngine;
using Enemigos;

namespace Torretas
{
    public class Turret : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private int level = 1;
        [SerializeField] private float range = 3f;
        [SerializeField] private float damage = 5f;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private int baseUpgradeCost = 30;

        [Header("Shoot")]
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private GameObject proyectilePrefab;
        [SerializeField] private Transform shootPoint;
        
        private float timeBetweenShots;
        
        public int Level => level;
        public float Range => range;
        public float Damage => damage;
        public float FireRate => fireRate;

        public int ActualUpgradeCost => Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(1.5f, level - 1));

        private void Update()
        {
            if (Time.time < timeBetweenShots) return;
            Enemy objective = FindObjective();
            if (objective != null)
            {
                Shoot(objective);
                timeBetweenShots = Time.time + 1f / fireRate;
            }
        }

        private Enemy FindObjective()
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
            Enemy closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (var objective in enemiesInRange)
            {
                if(!objective.TryGetComponent(out Enemy enemy)) continue;
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;

        }
        
        private void Shoot(Enemy objective)
        {
            Vector3 origin = shootPoint != null? shootPoint.position : transform.position;
            GameObject ProjectileGO = Instantiate(proyectilePrefab, origin, Quaternion.identity);
            Proyectile proyectile = ProjectileGO.GetComponent<Proyectile>();
            proyectile.Initialize(objective, damage);
        }
        
        public bool Upgrade()
        {
            int costo = ActualUpgradeCost;
            if (!EconomyManager.Instance.PayCost(costo)) return false;

            level++;
            range *= 1.15f;
            damage *= 1.25f;
            fireRate *= 0.9f; 

            return true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}