using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Granja
{
    public class FarmMenuUI : MonoBehaviour

    {
        [Header("Referencias UI")] 
        [SerializeField] private GameObject menu;

        [SerializeField] private Vector2 offset = new Vector2(0, 100f);

        [Header("Contenedor Dinamico")] 
        [SerializeField] private Transform optionsContainer;

        [SerializeField] private CropOptionButton optionButtonPrefab;

        [Header("Cultivo Inicial")] [SerializeField]
        private CropDataSO basicCropData;

        private FarmSlot selectedSlot;
        private Camera mainCam;

        private void Awake()
        {
            mainCam = Camera.main;
        }

        private void OnEnable() => FarmSlot.OnSlotClicked += ShowMenu;
        private void OnDisable() => FarmSlot.OnSlotClicked -= ShowMenu;

        private void ShowMenu(FarmSlot slot)
        {
            selectedSlot = slot;

            if (mainCam == null) return;

            Vector3 screenPos = mainCam.WorldToScreenPoint(slot.transform.position);
            screenPos.x += offset.x;
            screenPos.y += offset.y;
            menu.transform.position = screenPos;

            ClearOptions();
            
            List<CropDataSO> optionsToDisplay = new List<CropDataSO>();

            if (!slot.Taked)
            {
                // Si el slot está vacío, ofrecemos la planta básica inicial
                if (basicCropData != null)
                    optionsToDisplay.Add(basicCropData);
            }
            else
            {
                if (slot.CropData != null && slot.CropData.Upgrades != null)
                {
                    optionsToDisplay = slot.CropData.Upgrades;
                }
            }
            
            foreach (var optionData in optionsToDisplay)
            {
                CropDataSO targetData = optionData;
                CreateOptionButton(targetData, () => 
                {
                    selectedSlot.SetCrop(targetData);
                    menu.SetActive(false); 
                });
            }

            menu.SetActive(true);
        }

        private void CreateOptionButton(CropDataSO cropData, System.Action onClickAction)
        {
            if (cropData == null || optionButtonPrefab == null) return;

            CropOptionButton btn = Instantiate(optionButtonPrefab, optionsContainer);
            btn.Setup(cropData, onClickAction);
        }

        private void ClearOptions()
        {
            foreach (Transform child in optionsContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}