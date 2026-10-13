using UnityEngine;

namespace Enemigos
{
    public class EnemyFactory
    {
        public Enemy CreateEnemy(GameObject prefab, Vector3 position, Path path)
        {
            GameObject instance = Object.Instantiate(prefab, position, Quaternion.identity);
            Enemy enemy = instance.GetComponent<Enemy>();

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