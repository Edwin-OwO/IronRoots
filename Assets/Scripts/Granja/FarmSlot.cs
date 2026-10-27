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
        private CropDataSO cropData;
        public bool Taked => actualCrop != null;
        public CropDataSO CropData => cropData;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnSlotClicked?.Invoke(this);
        }
        
        public void SetCrop(CropDataSO cropDataSO)
        {
            if (cropDataSO == null || cropDataSO.Prefab == null) return;
            if (!EconomyManager.Instance.PayCost(cropDataSO.Cost)) return;

            if (Taked)
            {
                DestroyCrop();
            }
            cropData = cropDataSO;
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
