using UnityEngine;
using Torretas; 

namespace Granja
{
    public class BalisticPeaCrop : CropBase
    {
        [SerializeField] private float damageBonusToTurrets = 10f;
        [SerializeField] private float searchRadius = 100f;
        [SerializeField] private LayerMask turretLayer;
        protected override void PlantAbility()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRadius, turretLayer);

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out Turret turret))
                {
                    turret.AddDamage(damageBonusToTurrets);
                }
            }
        }
    }
}