using System;
using UnityEngine;

[Serializable]
public class EnemySpawnEntry
{
    public string enemyId;
    public int amount = 1;

    [Min(0f)]
    public float timeBetweenSpawns = 1f;
}