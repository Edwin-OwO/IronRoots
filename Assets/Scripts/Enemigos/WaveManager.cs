using System;
using System.Collections;
using System.Collections.Generic;
using Economia;
using Nucleo;
using UnityEngine;

namespace Enemigos
{
    [System.Serializable]
    public class WaveData
    {
        public Enemy enemyPrefab;
        public int amount;
        public float timeBetweenSpawns = 0.5f;
    }
    public class  WaveManager: MonoBehaviour
    {
        public static event Action OnWaveEnd;
        
        [SerializeField] private List<WaveData> waves;
        [SerializeField] private Path principalPath;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private FarmBase farmBase;

        private EnemyFactory enemyFactory;
        private int actualWave = -1;
        private int remainEnemies;

        public bool waveOnGoing => remainEnemies > 0; 
        public int ActualWave => actualWave;

        private void Awake()
        {
            enemyFactory = new EnemyFactory();
        }

        private void OnEnable()
        {
            GameManager.OnStartGame += OnStartNextWave;
            Enemy.OnEnemyDied += OnDie;
        }

        private void OnDisable()
        {
            GameManager.OnStartGame -= OnStartNextWave;
            Enemy.OnEnemyDied -= OnDie;
        }

        public void OnStartNextWave()
        {
            actualWave++;
            if (actualWave >= waves.Count)
            {
                Debug.Log("No quedan mas oleadas definidas.");
                return;
            }

            StartCoroutine(SpawnWave(waves[actualWave]));
        }

        private IEnumerator SpawnWave(WaveData data)
        {
            for (int i = 0; i < data.amount; i++)
            {
                SpawnEnemy(data.enemyPrefab);
                yield return new WaitForSeconds(data.timeBetweenSpawns);
            }
        }

        private void SpawnEnemy(Enemy prefab)
        {
            Enemy enemy = enemyFactory.CreateEnemy(prefab, spawnPoint.position, principalPath);
            if (enemy == null) return;
            enemy.OnEnemyReachedEnd += OnFinalStep;
            remainEnemies++;
        }

        private void OnDie()
        {
            remainEnemies--;
            ChekWaveEnd();
        }

        private void OnFinalStep(Enemy enemy)
        {
            farmBase.TakeDamage();
            remainEnemies--;
            ChekWaveEnd();
            enemy.OnEnemyReachedEnd -= OnFinalStep;
        }

        private void ChekWaveEnd()
        {
            if (remainEnemies <= 0)
            {
                Debug.Log($"Oleada {actualWave + 1} completada.");
                OnWaveEnd?.Invoke();
            }
        }
    }
}