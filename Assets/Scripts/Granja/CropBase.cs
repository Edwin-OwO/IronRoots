using System;
using Economia;
using UnityEngine;

namespace Granja
{
    public class CropBase : MonoBehaviour, ICrop, IEntrySource
    { 
        [SerializeField] protected CropData data; 
        
        public event Action<float> OnEntryGenerated;
        public void StartCycle()
        {
            InvokeRepeating(nameof(Harvest), data.TimePerCycle, data.TimePerCycle);
        }

        public void Harvest()
        {
            OnEntryGenerated?.Invoke(data.MoneyPerCycle);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(Harvest));
        }
    }
    }