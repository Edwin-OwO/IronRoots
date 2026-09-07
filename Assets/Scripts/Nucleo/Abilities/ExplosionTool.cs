using Enemigos;
using UnityEngine;

namespace Nucleo.Abilities
{
    public class ExplosionTool : MonoBehaviour
    {
        [SerializeField] private float explosionRadius = 1.5f;
        [SerializeField] private float damage = 15f;
        [SerializeField] private float cooldown = 2f;
        [SerializeField] private LayerMask enemyLayer;

        private float nextUseAvailable;

        public bool ReadyToUse => Time.time >= nextUseAvailable;
        
        public void ActiveOnPosition(Vector2 cursorPosition)
        {
            if (!ReadyToUse) return;

            ApplyDamageOnRadius(cursorPosition);
            nextUseAvailable = Time.time + cooldown;
        }

        private void ApplyDamageOnRadius(Vector2 position)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(position, explosionRadius, enemyLayer);

            foreach (Collider2D target in targets)
            {
                if (target.TryGetComponent(out Enemy enemy))
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}