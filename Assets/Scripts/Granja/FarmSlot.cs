using UnityEngine;
using Economia;

namespace Granja
{
    public class FarmSlot : MonoBehaviour
    {
        private CropBase actualCrop;

        public bool Taked => actualCrop != null;

        public bool PlantCrop<T>(int cost) where T : CropBase
        {
            if (Taked) return false;
            if (!EconomyManager.Instance.PayCost(cost)) return false;

            actualCrop = gameObject.AddComponent<T>();
            EconomyManager.Instance.RegisterCrop(actualCrop);
            EconomyManager.Instance.RegisterPassiveMoney(actualCrop.MoneyPerCycle);
            actualCrop.StartCycle();
            return true;
        }
        
        public void DestroyCrop()
        {
            if (!Taked) return;

            EconomyManager.Instance.ReducePassiveMoney(actualCrop.MoneyPerCycle);
            EconomyManager.Instance.UnregisterCrop(actualCrop);

            Destroy(actualCrop);
            actualCrop = null;
        }
    }
    }
