using UnityEngine;

namespace Enemigos
{
    public class EnemyFactory
    {
        public Enemy CreateEnemy(
            Enemy prefab,
            Vector3 position,
            Path path,
            EnemyStrategy strategy)
        {
            Enemy enemy = Object.Instantiate(
                prefab,
                position,
                Quaternion.identity
            );

            enemy.Initialize(path, strategy);

            return enemy;
        }
    }
}