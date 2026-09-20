using System;
using UnityEngine;
namespace Torretas
{
    public class TurretMenuUI : MonoBehaviour
    {
        [Header("Referencias UI")]
        [SerializeField] private RectTransform menuRectTransform;
        [SerializeField] private Vector2 offset = new Vector2(0, 100f);
        
        [Header("Paneles de Botones")]
        [SerializeField] private GameObject buildMenuPanel; 
        [SerializeField] private GameObject actionMenuPanel; 
        
        [Header("Turrets")]
        [SerializeField] private TurretStrategy defaultStrategyAsset; 
        [SerializeField] private float sellPorcentage = 0.50f;
        
        private BuildSlot selectedSlot;
        private Camera mainCam;

        private void Awake()
        {
            mainCam = Camera.main;
        }

        private void OnEnable() => BuildSlot.OnSlotClicked += ShowMenu;
        private void OnDisable() => BuildSlot.OnSlotClicked -= ShowMenu;

        private void ShowMenu(BuildSlot slot)
        {
            selectedSlot = slot;
            
            Vector3 screenPos = mainCam.WorldToScreenPoint(slot.transform.position);
            
            screenPos.x += offset.x;
            screenPos.y += offset.y; 
            
            menuRectTransform.position = screenPos;

            buildMenuPanel.SetActive(!slot.Taked);
            actionMenuPanel.SetActive(slot.Taked);
        }
        
        public void OnBuildBasicTurretPressed()
        {
            if(selectedSlot != null) 
                selectedSlot.BuildTurret(defaultStrategyAsset);
        
            buildMenuPanel.SetActive(false);
        }
        
        public void OnUpgradePressed()
        {
            if (selectedSlot != null && selectedSlot.Taked)
                selectedSlot.ActualTurret.Upgrade();
            
            actionMenuPanel.SetActive(false);
        }
        
        public void OnSellPressed()
        {
            if (selectedSlot != null)
                selectedSlot.SellTurret(sellPorcentage); 
            
            actionMenuPanel.SetActive(false);
        }
    }
}