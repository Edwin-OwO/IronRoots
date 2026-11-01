using System.Collections.Generic;
using UnityEngine;

namespace Enemigos
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "Enemies/Wave Data")]
    public class WaveData : ScriptableObject
    {
        public float timeBetweenSpawns = 1f;
        public List<EnemySpawnEntry> enemies = new();
    }
}