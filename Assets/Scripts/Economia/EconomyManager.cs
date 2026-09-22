using System;
using UnityEngine;

namespace Economia
{
    public class EconomyManager : MonoBehaviour
    {
        public static event Action<float> OnMoneyChanged; 
        
        public static EconomyManager Instance { get; private set; }

        [SerializeField] private float money = 100;

        public float Money => money;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void RegisterEntity(IEntrySource entry)
        {
            entry.OnEntryGenerated += AddMoney;
        }
        
        public void UnregisterEntity(IEntrySource entry)
        {
            entry.OnEntryGenerated -= AddMoney;
        }

        public void AddMoney(float amount)
        {
            money += amount;
            OnMoneyChanged?.Invoke(money);
        }

        private bool CanBuy(float cost) => money >= cost;

        public bool PayCost(float cost)
        {
            if (!CanBuy(cost)) return false;
            money -= cost;
            OnMoneyChanged?.Invoke(money);
            return true;
        }
    }
    }
