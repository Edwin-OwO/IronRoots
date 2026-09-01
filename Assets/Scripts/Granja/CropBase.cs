using System;
using Economia;
using UnityEngine;

namespace Granja
{
    public abstract class CropBase : MonoBehaviour, ICrop, IEntrySource
    {
       [SerializeField] protected int moneyPerCycle = 2;
        [SerializeField] protected float timePerCycle = 5f;

        public int MoneyPerCycle => moneyPerCycle;
        public event Action<int> OnEntryGenerated;
        public virtual void StartCycle()
        {
            InvokeRepeating(nameof(Harvest), timePerCycle, timePerCycle);
        }

        public virtual void Harvest()
        {
            OnEntryGenerated?.Invoke(moneyPerCycle);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(Harvest));
        }
    }
    }