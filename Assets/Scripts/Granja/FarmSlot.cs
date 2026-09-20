using UnityEngine;
using Economia;
using System;
using UnityEngine.EventSystems;

namespace Granja
{
    [RequireComponent(typeof(Collider2D))]
    public class FarmSlot : MonoBehaviour, IPointerClickHandler
    {
        public static event  Action<FarmSlot> OnSlotClicked;
        
        private CropBase actualCrop;
        public bool Taked => actualCrop != null;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSlotClicked?.Invoke(this);
        }
        
        public void PlantCrop(CropDataSO cropDataSO)
        {
            if (Taked) return;
            if (!EconomyManager.Instance.PayCost(cropDataSO.Cost)) return;

            actualCrop = Instantiate(cropDataSO.Prefab, transform.position, Quaternion.identity, transform);
            EconomyManager.Instance.RegisterEntity(actualCrop);
            actualCrop.StartCycle();
        }
        
        public void DestroyCrop()
        {
            if (!Taked) return;
            
            EconomyManager.Instance.UnregisterEntity(actualCrop);

            Destroy(actualCrop.gameObject);
            actualCrop = null;
        }
    }
    }
