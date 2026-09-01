using UnityEngine;

namespace Economia
{
    public class EconomyManager : MonoBehaviour
    {
        public static EconomyManager Instance { get; private set; }

        [SerializeField] private int money = 100;
        private int passiveEntryPerTick;

        public int Money => money;
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
        
        public void RegisterCrop(IEntrySource entry)
        {
            entry.OnEntryGenerated += AgregarDinero;
        }
        
        public void UnregisterCrop(IEntrySource entry)
        {
            entry.OnEntryGenerated -= AgregarDinero;
        }

        public void RegisterPassiveMoney(int monto)
        {
            passiveEntryPerTick += monto;
        }

        public void ReducePassiveMoney(int monto)
        {
            passiveEntryPerTick = Mathf.Max(0, passiveEntryPerTick - monto);
        }
        
        public void PassiveEntryOnTick()
        {
            if (passiveEntryPerTick > 0)
                AgregarDinero(passiveEntryPerTick);
        }

        public void AgregarDinero(int monto)
        {
            money += monto;
        }

        public bool CanBuy(int costo) => money >= costo;

        public bool PayCost(int cost)
        {
            if (!CanBuy(cost)) return false;
            money -= cost;
            return true;
        }
    }
    }
