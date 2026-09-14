using System;
using UnityEngine;

namespace Economia
{
    public class EconomyManager : MonoBehaviour
    {
        public static event Action<float> OnMoneyChanged; 
        
        public static EconomyManager Instance { get; private set; }

        [SerializeField] private float money = 100;
        
        private int passiveEntryPerTick;

        public float Money => money;
        public int PassiveEntryPerTick => passiveEntryPerTick;
        
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

        public void RegisterPassiveMoney(int amount)
        {
            passiveEntryPerTick += amount;
        }

        public void ReducePassiveMoney(int amount)
        {
            passiveEntryPerTick = Mathf.Max(0, passiveEntryPerTick - amount);
        }
        
        public void PassiveEntryOnTick()
        {
            if (passiveEntryPerTick > 0)
                AddMoney(passiveEntryPerTick);
        }

        public void AddMoney(float amount)
        {
            money += amount;
            OnMoneyChanged?.Invoke(money);
        }

        public bool CanBuy(float cost) => money >= cost;

        public bool PayCost(float cost)
        {
            if (!CanBuy(cost)) return false;
            money -= cost;
            OnMoneyChanged?.Invoke(money);
            return true;
        }
    }
    }
