using System.Collections.Generic;
using Enemigos;
using UnityEngine;
using UnityEngine.Serialization;

namespace Torretas
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class MortarProjectile : Proyectile
    {
        [SerializeField] private Collider2D explosionArea;
        [SerializeField] private float explosionDuration = 0.1f;
        [SerializeField] private SpriteRenderer spriteExplosion;
        [SerializeField] private SpriteRenderer spriteProjectile;
        [SerializeField] private float travelDuration = 1f;
        private Vector2 startPosition;
        private float elapsedTime;

        private readonly HashSet<Enemy> alreadyHit = new HashSet<Enemy>();

        private void Awake()
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            explosionArea.isTrigger = true;
            explosionArea.enabled = false;
            spriteExplosion.enabled = false;
            startPosition = transform.position;
            elapsedTime = 0f;
        }

        protected override bool Move()
        {
            elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(
                elapsedTime / travelDuration
            );

            transform.position = Vector2.Lerp(
                startPosition,
                targetPosition,
                progress
            );

            return progress >= 1f;
        }

        protected override void OnImpact()
        {
            if (spriteExplosion != null) spriteExplosion.enabled = false;
            explosionArea.enabled = true;
            spriteExplosion.enabled = true;
            spriteProjectile.enabled = false;
            
        }
        
        protected override void FinishAfterImpact()
        {
            Destroy(gameObject, explosionDuration);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!explosionArea.enabled) return;

            if (other.TryGetComponent(out Enemy enemy) && alreadyHit.Add(enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}