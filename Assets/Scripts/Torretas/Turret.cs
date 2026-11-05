using System;
using System.Collections;
using System.Collections.Generic;
using Economia;
using UnityEngine;
using Enemigos;
using JetBrains.Annotations;

namespace Torretas
{
    [RequireComponent(typeof(Collider2D))] 
    public class Turret : MonoBehaviour
    {
        public static event Action<Turret> OnTurretCreated;
     
        [Header("Stats")]
        [SerializeField] protected int level = 1;
        [SerializeField] protected float range = 3f;
        [SerializeField] protected float damage = 5f;
        [SerializeField] protected float baseFireRate = 1f;
        [SerializeField] protected int baseUpgradeCost = 30;
        
        [Header("FindEnemy")]
        [SerializeField] protected LayerMask enemyLayer;
        [SerializeField] private float targetSearchInterval = 0.2f;
        [SerializeField] private float idlePollInterval = 0.2f;
        private readonly List<Enemy> enemiesByEntry = new();
        private readonly HashSet<Enemy> detectedEnemies = new();
        
        [Header("Aim")]
        [SerializeField] private Transform firePoint;
        [SerializeField] protected Transform rotatingPart;
        [SerializeField] private float rotationSpeed = 360f;
        [SerializeField] private float spriteAngleOffset = -90f;
        
        [SerializeField] private ScriptableObject turretStrategyAsset;
        
        [Header("Particles System")]
        [SerializeField] private ParticleSystem buffedDamageParticles;
        
        private ITurretStrategy attackStrategy;
        
        private static readonly Collider2D[] overlapBuffer = new Collider2D[16];

        private Enemy currentTarget;
        private Coroutine targetSearchCoroutine;
        private Coroutine shootCoroutine;
        private Coroutine aimCoroutine;

        public Vector3 FirePosition => firePoint != null ? firePoint.position : transform.position;
        public int Level => level;
        public float Range => range;
        public float Damage => damage;
        public float FireRateMultiplier { get; set; } = 1f;
        public float ActualFireRate => baseFireRate*FireRateMultiplier;

        protected int ActualUpgradeCost => Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(1.5f, level - 1));

        protected virtual void Awake()
        {
            if (rotatingPart == null) rotatingPart = transform;
            attackStrategy = turretStrategyAsset as ITurretStrategy;
        }

        private void Start()
        {
            OnTurretCreated?.Invoke(this);
        }

        protected virtual void OnEnable()
        {
            targetSearchCoroutine = StartCoroutine(TargetSearchRoutine());
            shootCoroutine = StartCoroutine(ShootRoutine());
            aimCoroutine = StartCoroutine(AimRoutine());
        }

        protected virtual void OnDisable()
        {
            if (targetSearchCoroutine != null) StopCoroutine(targetSearchCoroutine);
            if (shootCoroutine != null) StopCoroutine(shootCoroutine);
            if (aimCoroutine != null) StopCoroutine(aimCoroutine);
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
                currentTarget = FindObjective();

                if (currentTarget == null || !IsInRange(currentTarget))
                {
                    currentTarget = null;
                    yield return idleWait;
                    continue;
                }

                attackStrategy.OnTargetAcquired(this, currentTarget);
                attackStrategy.Shoot(this, currentTarget);
                attackStrategy.OnShotFired(this, currentTarget);
                yield return new WaitForSeconds(1f / ActualFireRate);
                
            }
        }
        
        private IEnumerator AimRoutine()
        {
            var idleWait = new WaitForSeconds(idlePollInterval);

            while (true)
            {
                if (currentTarget == null)
                {
                    yield return idleWait;
                    continue;
                }

                RotateTowards(currentTarget.transform.position);
                yield return null;
            }
        }
        
        protected virtual void RotateTowards(Vector2 targetPosition)
        {
            Vector2 direction = targetPosition - (Vector2)rotatingPart.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + spriteAngleOffset;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

            rotatingPart.rotation = rotationSpeed <= 0f  ? targetRotation  : Quaternion.RotateTowards(rotatingPart.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private bool IsInRange(Enemy enemy)
        {
            float sqrDistance = ((Vector2)enemy.transform.position - (Vector2)transform.position).sqrMagnitude;
            return sqrDistance <= range * range;
        }
        
        
      
        protected virtual Enemy FindObjective()
        {
            int count = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                range,
                overlapBuffer,
                enemyLayer
            );

            detectedEnemies.Clear();

            for (int i = 0; i < count; i++)
            {
                if (overlapBuffer[i] == null ||
                    !overlapBuffer[i].TryGetComponent(out Enemy enemy))
                    continue;

                if (!enemy.isActiveAndEnabled)
                    continue;

                detectedEnemies.Add(enemy);

                if (!enemiesByEntry.Contains(enemy))
                    enemiesByEntry.Add(enemy);
            }

            for (int i = enemiesByEntry.Count - 1; i >= 0; i--)
            {
                Enemy enemy = enemiesByEntry[i];

                if (enemy == null || !detectedEnemies.Contains(enemy))
                    enemiesByEntry.RemoveAt(i);
            }

            return attackStrategy.SelectTarget(
                enemiesByEntry,
                transform.position
            );
        }

        

        public void Upgrade()
        {
            int costo = ActualUpgradeCost;
            if (!EconomyManager.Instance.PayCost(costo))
            {
                attackStrategy.OnUpgradeFailed(this);
                return;
            }

            level++;
            range *= 1.15f;
            damage *= 1.25f;
            baseFireRate *= 0.9f;

            attackStrategy.OnUpgraded(this, level);
        }
        public void AddDamage(float extraDamage)
        {
            damage += extraDamage;  
            buffedDamageParticles.Play();
        }

        public void RemoveDamage(float extraDamage)
        {
            damage -= extraDamage;  
            buffedDamageParticles.Stop();
        }
        
        public void UpdateFireRate(float newFireRate)
        {
            FireRateMultiplier = newFireRate;
        }
    }
}


