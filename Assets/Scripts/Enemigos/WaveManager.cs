using System.Collections;
using System.Collections.Generic;
using Nucleo;
using UnityEngine;

namespace Enemigos
{
    [System.Serializable]
    public class WaveData
    {
        public GameObject enemyPrefab;
        public int amount;
        public float timeBetweenSpawns = 0.5f;
    }
    public class  WaveManager: MonoBehaviour, IEnemyObserver
    {
        [SerializeField] private List<WaveData> waves;
        [SerializeField] private Path principalPath;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private FarmBase farmBase;

        private int actualWave = -1;
        private int remainEnemies;

        public bool waveOnGoing => remainEnemies > 0;
        public int ActualWave => actualWave;

        public void StartNextWave()
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

        private void SpawnEnemy(GameObject prefab)
        {
            GameObject instance = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            Enemy enemy = instance.GetComponent<Enemy>();
            enemy.Initialize(principalPath);
            enemy.Subscribe(this);
            remainEnemies++;
        }

        public void OnDie(Enemy enemigo)
        {
            remainEnemies--;
            ChekWaveEnd();
        }

        public void OnFinalStep(Enemy enemy)
        {
            farmBase.TakeDamage(enemy.Damage);
            remainEnemies--;
            ChekWaveEnd();
        }

        private void ChekWaveEnd()
        {
            if (remainEnemies <= 0)
            {
                Debug.Log($"Oleada {actualWave + 1} completada.");
            }
        }
    }
}