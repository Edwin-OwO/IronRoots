using System;

namespace Enemigos
{
    [Serializable]
    public class EnemyDatabaseEntry
    {
        public string id;
        public EnemyStrategy strategy;
    }
}