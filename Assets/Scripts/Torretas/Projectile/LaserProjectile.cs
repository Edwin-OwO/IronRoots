using System.Collections.Generic;
using Enemigos;
using UnityEngine;

namespace Torretas
{
    public class LaserProjectile : Proyectile
    {
        [SerializeField] private Collider2D laserArea;
        [SerializeField] private SpriteRenderer laserSprite;
        [SerializeField] private float laserDuration = 0.2f;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private Transform laserBeam;
        [SerializeField] private float forwardOffset = 5f;

        private readonly HashSet<Enemy> alreadyHit = new();
        private readonly List<Collider2D> overlaps = new();

        private void Awake()
        {
            laserArea.isTrigger = true;
            laserArea.enabled = false;
            laserSprite.enabled = false;
        }

        public override void Initialize(Enemy objectiveAssigned, float damageAssigned)
        {
            base.Initialize(objectiveAssigned, damageAssigned);

            Vector2 direction =
                targetPosition - (Vector2)transform.position;

            if (direction.sqrMagnitude > 0.0001f)
                transform.right = direction.normalized;
            
            laserBeam.localPosition = new Vector3(forwardOffset, 0f, 0f);
        }

        protected override bool Move()
        {
            return true;
        }

        protected override void OnImpact()
        {
            laserSprite.enabled = true;
            laserArea.enabled = true;

            Physics2D.SyncTransforms();

            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(enemyLayer);
            filter.useTriggers = true;

            overlaps.Clear();
            laserArea.Overlap(filter, overlaps);

            foreach (Collider2D other in overlaps)
            {
                
                if (other.TryGetComponent(out Enemy enemy) && alreadyHit.Add(enemy))
                {
                    enemy.TakeDamage(damage);
                }
                // Enemy enemy =
                //     other.GetComponentInParent<Enemy>();
                //
                // if (enemy != null && alreadyHit.Add(enemy))
                //     enemy.TakeDamage(damage);
            }
        }

        protected override void FinishAfterImpact()
        {
            Destroy(gameObject, laserDuration);
        }
    }
}