using System.Collections.Generic;
using Buffs;
using Enemigos;
using UnityEngine;

namespace Granja
{
    public class IceCrop : CropBase
    {
        [SerializeField] private float slowAmount;
        [SerializeField] private float durationDebuff;
        [SerializeField] private float range;
        [SerializeField] private LayerMask enemiesLayer;
        [SerializeField] private ParticleSystem particleSystem;
        
        private HashSet<Enemy> slowedEnemies = new HashSet<Enemy>();
        
        protected override void PlantAbility()
        {

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, enemiesLayer);
            foreach (Collider2D hit in hits)
            {
                var enemy = hit.GetComponent<Enemy>();
                ApplyIceDebuff(enemy);
            }
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponentInChildren<Enemy>();
                ApplyIceDebuff(enemy);
            }
        }

        private void ApplyIceDebuff(Enemy enemy)
        {
            slowedEnemies.RemoveWhere(e => e == null);
            //if(!slowedEnemies.Add(enemy)) return;
            if (slowedEnemies.Contains(enemy)) return;
            slowedEnemies.Add(enemy);
            IceDebuff iceDebuff = enemy.gameObject.AddComponent<IceDebuff>();
            iceDebuff.Initialize(durationDebuff,slowAmount);
        }
    }
}