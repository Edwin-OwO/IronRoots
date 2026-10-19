using UnityEngine;
using Enemigos;

namespace Torretas
{
    public class ProjectileTurret : Turret
    {
        [Header("Shoot")]
        [SerializeField] private GameObject proyectilePrefab;
        [SerializeField] private Transform shootPoint;
        protected override void Shoot(Enemy objective)
        {
            Vector3 origin = shootPoint.position;
            GameObject projectileGO = Instantiate(proyectilePrefab, origin, Quaternion.identity);
            Proyectile proyectile = projectileGO.GetComponent<Proyectile>();
            proyectile.Initialize(objective, damage);
        }
    }
}