using System.Collections.Generic;
using Enemigos;
using UnityEngine;

namespace Torretas
{
    [CreateAssetMenu(
        fileName = "FirstMortarStrategy",
        menuName = "Scriptable Objects/First" + "TurretStrategy")]
    public class FirstMortarTurret : TurretStrategyParent
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
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                    return enemies[i];
            }

            return null;
        }



    }
}