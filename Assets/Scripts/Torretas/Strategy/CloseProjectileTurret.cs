using System.Collections.Generic;
using UnityEngine;
using Enemigos;

namespace Torretas
{
    [CreateAssetMenu(
        fileName = "CloseProjectileTurretStrategy",
        menuName = "Scriptable Objects/CloseProjectileTurretStrategy")]
    public class CloseProjectileTurret : TurretStrategyParent
    {
        [SerializeField] private GameObject proyectilePrefab;

        public override void Shoot(Turret turret, Enemy objective)
        {
            Vector3 origin = turret.FirePosition;

            GameObject projectileGO =
                Instantiate(proyectilePrefab, origin, Quaternion.identity);

            Proyectile proyectile =
                projectileGO.GetComponent<Proyectile>();

            proyectile.Initialize(objective, turret.Damage);
        }

        
        public override Enemy SelectTarget(
            IReadOnlyList<Enemy> enemies,
            Vector2 turretPosition)
        {
            Enemy closestEnemy = null;
            float closestSqrDistance = float.MaxValue;

            foreach (Enemy enemy in enemies)
            {
                if (enemy == null)
                    continue;

                Vector2 enemyPosition = enemy.transform.position;

                float sqrDistance =
                    (enemyPosition - turretPosition).sqrMagnitude;

                if (sqrDistance < closestSqrDistance)
                {
                    closestSqrDistance = sqrDistance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }

    }
}