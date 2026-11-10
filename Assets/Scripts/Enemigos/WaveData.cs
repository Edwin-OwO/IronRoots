
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemigos
{
    [Serializable]
    public class EnemySpawnEntry
    {
        public string enemyId;
        public int amount = 1;

        [Min(0f)]
        public float timeBetweenSpawns = 1f;
    }

    [CreateAssetMenu(
        fileName = "WaveData",
        menuName = "Enemies/Wave Data"
    )]
    public class WaveData : ScriptableObject
    {
        public List<EnemySpawnEntry> enemies = new();
    }
}
