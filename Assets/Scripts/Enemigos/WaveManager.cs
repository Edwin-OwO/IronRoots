
using System;
using System.Collections;
using System.Collections.Generic;
using Economia;
using Nucleo;
using UnityEngine;

namespace Enemigos
{
    public class WaveManager : MonoBehaviour
    {
        public static event Action OnWaveEnd;
        public static event Action OnWavesFinished;
        
        [SerializeField] private EnemyDatabase enemyDatabase;
        [SerializeField] private List<WaveData> waves;
        [SerializeField] private Enemy enemyPrefab;
        
        [SerializeField] private Path principalPath;
        [SerializeField] private Transform spawnPoint;
        
        [SerializeField] private FarmBase farmBase;

        private EnemyFactory enemyFactory;

        private int actualWave = -1;
        private int remainEnemies;

        private bool spawning;
        private bool waveRunning;

        public bool waveOnGoing => waveRunning;
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
            if (waveRunning) return;

            if (actualWave + 1 >= waves.Count)
            {
                Debug.Log("No quedan mas oleadas definidas.");
                
                return;
            }

            actualWave++;
            waveRunning = true;
            spawning = true;
            remainEnemies = 0;

            StartCoroutine(SpawnWave(waves[actualWave]));
        }

        
        private IEnumerator SpawnWave(WaveData data)
        {
            bool firstSpawn = true;

            foreach (EnemySpawnEntry entry in data.enemies)
            {
                for (int i = 0; i < entry.amount; i++)
                {
                    if (!firstSpawn)
                        yield return new WaitForSeconds(
                            data.timeBetweenSpawns
                        );

                    firstSpawn = false;
                    SpawnEnemy(entry.enemyId);
                }
            }

            spawning = false;
        }
        
        private void SpawnEnemy(string enemyId)
        {
            EnemyStrategy strategy =
                enemyDatabase.GetStrategy(enemyId);

            if (strategy == null)
                return;

            Enemy enemy = enemyFactory.CreateEnemy(
                enemyPrefab,
                spawnPoint.position,
                principalPath,
                strategy
            );

            if (enemy == null)
                return;

            enemy.OnEnemyReachedEnd += OnFinalStep;
            remainEnemies++;
        }


        private void OnDie()
        {
            if (!waveRunning) return;

            remainEnemies--;
            ChekWaveEnd();
        }

        private void OnFinalStep(Enemy enemy)
        {
            enemy.OnEnemyReachedEnd -= OnFinalStep;

            farmBase.TakeDamage();
            remainEnemies--;

            ChekWaveEnd();
        }

        private void ChekWaveEnd()
        {
            if (spawning || !waveRunning)
                return;
            
            if (remainEnemies <= 0)
            {
                waveRunning = false;
                if (actualWave + 1 >= waves.Count)
                {
                    OnWavesFinished?.Invoke();
                }
                else
                {
                    Debug.Log(
                        $"Oleada {actualWave + 1} completada.");
                    OnWaveEnd?.Invoke();
                }
            }
        }
    }
}



