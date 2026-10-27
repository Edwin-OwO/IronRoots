using System;
using Economia;
using Nucleo;
using UnityEngine;

namespace Granja
{
    public abstract class CropBase : MonoBehaviour, ICrop, IEntrySource
    { 
        [SerializeField] protected CropData data; 
        protected float moneyPerCycle;
        protected float currentMoneyPerCycle;
        public event Action<float> OnEntryGenerated;

        private void OnDisable()
        {
            CancelInvoke(nameof(Harvest));
        }
        
        private void Start()
        {
            moneyPerCycle = data.MoneyPerCycle;
            currentMoneyPerCycle = moneyPerCycle;
            PlantAbility();
        }

        public void StartCycle()
        {
            InvokeRepeating(nameof(Harvest), data.TimePerCycle, data.TimePerCycle);
        }

        public void Harvest()
        {
            if(!GameManager.Instance.GameActive) return;
            OnEntryGenerated?.Invoke(currentMoneyPerCycle);
        }
        protected abstract void PlantAbility();
    }
}