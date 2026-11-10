using System;
using Economia;
using Nucleo;
using UnityEngine;

namespace Granja
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class CropBase : MonoBehaviour, ICrop, IEntrySource
    { 
        [SerializeField] protected CropData data; 
        
        [Header("GameObject")]
        [SerializeField] protected SpriteRenderer cropSprite;
        
        [Header("Effects")]
        [SerializeField] protected AudioSource audioSource;
        [SerializeField] protected AudioClip harvestSound;
        
        protected float moneyPerCycle;
        protected float currentMoneyPerCycle;
        public event Action<float,SpriteRenderer> OnEntryGenerated;

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
            if (!GameManager.Instance.GameActive || FarmBase.Instance.IsDestroyed) return;
            OnEntryGenerated?.Invoke(currentMoneyPerCycle, cropSprite);
            audioSource.PlayOneShot(harvestSound);
        }
        protected abstract void PlantAbility();
    }
}