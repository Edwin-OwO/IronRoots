using System.Collections.Generic;
using UnityEngine;

namespace Enemigos
{
    [CreateAssetMenu(
        fileName = "EnemyDatabase",
        menuName = "Enemies/Enemy Database"
    )]
    public class EnemyDatabase : ScriptableObject
    {
        [SerializeField]
        private List<EnemyDatabaseEntry> enemies = new();

        public EnemyStrategy GetStrategy(string id)
        {
            foreach (EnemyDatabaseEntry entry in enemies)
            {
                if (entry.id == id)
                    return entry.strategy;
            }

            Debug.LogWarning(
                $"No existe el enemigo '{id}' en la base de datos."
            );

            return null;
        }
    }
}