using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Torretas;

namespace Granja
{
    public class BalisticPeaCrop : CropBase
    {
        [SerializeField] private float damageBonusToTurrets = 10f;
        [SerializeField] private float searchRadius = 100f;
        [SerializeField] private LayerMask turretLayer;
        
        private HashSet<Turret> buffedTurrets = new HashSet<Turret>();

        private void OnEnable()
        {
            Turret.OnTurretCreated += CheckNewTurret;
        }

        private void OnDisable()
        {
            Turret.OnTurretCreated -= CheckNewTurret;
            RemoveAllBuffs();
        }

        protected override void PlantAbility()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRadius, turretLayer);
            var turretsInRange = hits
                .Select(hit => hit.GetComponent<Turret>())
                .Where(t => t != null && !buffedTurrets.Contains(t));

            foreach (var turret in turretsInRange)
            { 
                ApplyBuff(turret);
            }
        }

        private void CheckNewTurret(Turret turret)
        {
            float distance = Vector2.Distance(transform.position, turret.transform.position);

            if (distance <= searchRadius && !buffedTurrets.Contains(turret))
            {
                ApplyBuff(turret);
            }
        }

        private void ApplyBuff(Turret turret)
        {
            turret.AddBuffDamage(damageBonusToTurrets);
            buffedTurrets.Add(turret);
        }
        
        private void RemoveAllBuffs()
        {
            buffedTurrets.RemoveWhere(t => t == null);

            foreach (var turret in buffedTurrets)
            {
                turret.RemoveBuffDamage(damageBonusToTurrets);
            }
            
            buffedTurrets.Clear();
        }
    }
}