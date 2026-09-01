using Economia;
using UnityEngine;

namespace Torretas
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private int level = 1;
        [SerializeField] private float range = 3f;
        [SerializeField] private float damage = 5f;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private int baseUpgradeCost = 30;

        public int Level => level;
        public float Range => range;
        public float Damage => damage;
        public float FireRate => fireRate;

        public int ActualUpgradeCost => Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(1.5f, level - 1));

        public bool Upgrade()
        {
            int costo = ActualUpgradeCost;
            if (!EconomyManager.Instance.PayCost(costo)) return false;

            level++;
            range *= 1.15f;
            damage *= 1.25f;
            fireRate *= 0.9f; 

            return true;
        }

    }
}