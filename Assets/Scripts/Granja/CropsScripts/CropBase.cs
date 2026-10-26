using System;
using Economia;
using Nucleo;
using UnityEngine;

namespace Granja
{
    public abstract class CropBase : MonoBehaviour, ICrop, IEntrySource
    { 
        [SerializeField] protected CropData data; 
        private float moneyPerCycle;
        
        public event Action<float> OnEntryGenerated;

        private void Start()
        {
            moneyPerCycle = data.MoneyPerCycle;
            PlantAbility();
        }

        public void StartCycle()
        {
            InvokeRepeating(nameof(Harvest), data.TimePerCycle, data.TimePerCycle);
        }

        public void Harvest()
        {
            if(!GameManager.Instance.GameActive) return;
            
            OnEntryGenerated?.Invoke(moneyPerCycle);
        }

        protected abstract void PlantAbility();

        private void OnDisable()
        {
            CancelInvoke(nameof(Harvest));
        }
        
    }
}