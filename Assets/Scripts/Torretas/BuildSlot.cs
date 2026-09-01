using Economia;
using UnityEngine;

namespace Torretas
{
    public class BuildSlot : MonoBehaviour
    {
        [SerializeField] private bool taked;
        private Turret actualTurret;

        public bool Taked => taked;
        public Turret ActualTurret => actualTurret;

        public bool BuildTurret(IConstructionStrategy strategy)
        {
            if (taked) return false;
            if (!EconomyManager.Instance.CanBuy(strategy.Cost)) return false;

            EconomyManager.Instance.PayCost(strategy.Cost);
            actualTurret = strategy.Build(transform.position, transform);
            taked = true;
            return true;
        }
    }
}