using System.Collections;
using Economia;
using UnityEngine;
using Enemigos;

namespace Torretas
{
    public abstract class Turret : MonoBehaviour
    {

        [SerializeField] protected int level = 1;
        [SerializeField] protected float range = 3f;
        [SerializeField] protected float damage = 5f;
        [SerializeField] protected float fireRate = 1f;
        [SerializeField] protected int baseUpgradeCost = 30;
        [SerializeField] protected LayerMask enemyLayer;
        [SerializeField] private float targetSearchInterval = 0.2f;
        [SerializeField] private float idlePollInterval = 0.2f;
        
        private static readonly Collider2D[] overlapBuffer = new Collider2D[16];

        private Enemy currentTarget;
        private Coroutine targetSearchCoroutine;
        private Coroutine shootCoroutine;

        public int Level => level;
        public float Range => range;
        public float Damage => damage;
        public float FireRate => fireRate;

        protected int ActualUpgradeCost => Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(1.5f, level - 1));

        protected virtual void OnEnable()
        {
            targetSearchCoroutine = StartCoroutine(TargetSearchRoutine());
            shootCoroutine = StartCoroutine(ShootRoutine());
        }

        protected virtual void OnDisable()
        {
            if (targetSearchCoroutine != null) StopCoroutine(targetSearchCoroutine);
            if (shootCoroutine != null) StopCoroutine(shootCoroutine);
            currentTarget = null;
        }
        
        private IEnumerator TargetSearchRoutine()
        {
            var wait = new WaitForSeconds(targetSearchInterval);
            while (true)
            {
                currentTarget = FindObjective();
                yield return wait;
            }
        }
        
        private IEnumerator ShootRoutine()
        {
            var idleWait = new WaitForSeconds(idlePollInterval);

            while (true)
            {
                if (currentTarget == null || !IsInRange(currentTarget))
                {
                    currentTarget = null;
                    yield return idleWait;
                    continue;
                }

                OnTargetAcquired(currentTarget);
                Shoot(currentTarget);
                OnShotFired(currentTarget);

                yield return new WaitForSeconds(1f / fireRate);
            }
        }

        private bool IsInRange(Enemy enemy)
        {
            float sqrDistance = ((Vector2)enemy.transform.position - (Vector2)transform.position).sqrMagnitude;
            return sqrDistance <= range * range;
        }

        protected virtual Enemy FindObjective()
        {
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, range, overlapBuffer, enemyLayer);

            Enemy closestEnemy = null;
            float closestSqrDistance = float.MaxValue;

            for (int i = 0; i < count; i++)
            {
                if (!overlapBuffer[i].TryGetComponent(out Enemy enemy)) continue;

                float sqrDistance = ((Vector2)enemy.transform.position - (Vector2)transform.position).sqrMagnitude;
                if (sqrDistance < closestSqrDistance)
                {
                    closestSqrDistance = sqrDistance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }
        
        protected abstract void Shoot(Enemy objective);
        protected virtual void OnTargetAcquired(Enemy objective) { }
        
        protected virtual void OnShotFired(Enemy objective) { }

        public void Upgrade()
        {
            int costo = ActualUpgradeCost;
            if (!EconomyManager.Instance.PayCost(costo))
            {
                OnUpgradeFailed();
                return;
            }

            level++;
            range *= 1.15f;
            damage *= 1.25f;
            fireRate *= 0.9f;

            OnUpgraded(level);
        }
        protected virtual void OnUpgraded(int newLevel) { }
        protected virtual void OnUpgradeFailed() { }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}