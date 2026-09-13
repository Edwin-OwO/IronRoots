using System;
using Economia;
using UnityEngine;

namespace Granja
{
    public class CropBase : MonoBehaviour, ICrop, IEntrySource
    { 
        [SerializeField] protected CropData data; 
       
        public int MoneyPerCycle => data.MoneyPerCycle;
        public event Action<int> OnEntryGenerated;
        public virtual void StartCycle()
        {
            InvokeRepeating(nameof(Harvest), data.TimePerCycle, data.TimePerCycle);
        }

        public virtual void Harvest()
        {
            OnEntryGenerated?.Invoke(data.MoneyPerCycle);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(Harvest));
        }
    }
    }