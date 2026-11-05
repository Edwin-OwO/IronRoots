using System;
using Economia;
using Nucleo;
using Torretas;
using UnityEngine;

namespace Granja
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class CropBase : MonoBehaviour, ICrop, IEntrySource
    { 
        [SerializeField] protected CropData data; 
        
        [Header("Effects")]
        [SerializeField] protected AudioSource audioSource;
        [SerializeField] protected AudioClip harvestSound;
        
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
            OnEntryGenerated?.Invoke(currentMoneyPerCycle);
            audioSource.PlayOneShot(harvestSound);
        }
        protected abstract void PlantAbility();
    }
}