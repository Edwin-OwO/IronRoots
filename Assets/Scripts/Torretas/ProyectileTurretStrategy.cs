using UnityEngine;

namespace Torretas
{
    [CreateAssetMenu(fileName = "ProyectileTurretStrategy", menuName = "Scriptable Objects/ProyectileTurretStrategy")]
    public class ProyectileTurretStrategy : ScriptableObject
    {
        [SerializeField] private GameObject turretPrefab;
        [SerializeField] private int cost = 50;

        public int Cost => cost;
        
        public Turret Build(Vector3 position, Transform parent)
        {
            GameObject instance = Object.Instantiate(turretPrefab, position, Quaternion.identity, parent);
            return instance.GetComponent<Turret>();
        }
    }
}


