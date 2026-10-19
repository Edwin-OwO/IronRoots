using UnityEngine;

namespace Enemigos
{
    public class EnemyFactory
    {
        public Enemy CreateEnemy(Enemy prefab, Vector3 position, Path path)
        {
            Enemy instance = Object.Instantiate(prefab, position, Quaternion.identity);
            var enemy = instance.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Initialize(path);
            }
            else
            {
                Debug.LogError($"El prefab {prefab.name} no contiene el componente Enemy.");
            }

            return enemy;
        }
    }
}