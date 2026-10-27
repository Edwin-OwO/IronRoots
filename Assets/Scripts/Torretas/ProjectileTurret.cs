using UnityEngine;
using Enemigos;

namespace Torretas
{
    [CreateAssetMenu(fileName = "ProjectileTurretStrategy", menuName = "Scriptable Objects/ProjectileTurretStrategy")]
    public class ProjectileTurret : ScriptableObject, ITurretStrategy
    {
        [SerializeField] private GameObject proyectilePrefab;

        public void Shoot(Turret turret, Enemy objective)
        {
            Vector3 origin = turret.FirePosition;
            GameObject projectileGO = Instantiate(proyectilePrefab, origin, Quaternion.identity);
            Proyectile proyectile = projectileGO.GetComponent<Proyectile>();
            proyectile.Initialize(objective, turret.Damage);
        }

        public void OnTargetAcquired(Turret turret, Enemy objective)
        {
            
        }

        public void OnShotFired(Turret turret, Enemy objective)
        {
           
        }

        public void OnUpgraded(Turret turret, int newLevel)
        {
          
        }

        public void OnUpgradeFailed(Turret turret)
        {
            
        }
    }
}