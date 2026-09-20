using System;
using Economia;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Torretas
{
    [RequireComponent(typeof(Collider2D))]
    public class BuildSlot : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<BuildSlot> OnSlotClicked;

        [SerializeField] private bool taked;
    
        private Turret actualTurret;
        private int actualTurretCost;
        
        public bool Taked => taked;
        public Turret ActualTurret => actualTurret;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSlotClicked?.Invoke(this);
            Debug.Log("uwu");
        }

        public void BuildTurret(TurretStrategy strategy)
        {
            if (taked) return;
            if (strategy == null) return;
            if (!EconomyManager.Instance.PayCost(strategy.Cost)) return;
            actualTurretCost = strategy.Cost;
            actualTurret = strategy.Build(transform.position, transform);
            taked = true;
        }
        public void SellTurret(float porcentageToRefund)
        {
            if (!taked) return;
            float refundMoney = actualTurretCost * porcentageToRefund;
            EconomyManager.Instance.AddMoney(refundMoney);
            Destroy(actualTurret.gameObject);
            actualTurret = null;
            taked = false;
        }
    }
}