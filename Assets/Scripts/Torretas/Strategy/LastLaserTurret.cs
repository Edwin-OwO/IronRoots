using System.Collections.Generic;
using Enemigos;
using UnityEngine;

namespace Torretas
{
    [CreateAssetMenu(
        fileName = "LastLaserStrategy",
        menuName = "Scriptable Objects/Last" + "TurretStrategy")]
    public class LastLaserTurret : TurretStrategyParent
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
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i] != null)
                    return enemies[i];
            }

            return null;
        }




    }
}