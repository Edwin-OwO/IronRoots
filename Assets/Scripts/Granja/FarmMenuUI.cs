using UI;
using UnityEngine;

namespace Granja
{
    public class FarmMenuUI : MonoBehaviour

    {
        [Header("Referencias UI")] [SerializeField]
        private RectTransform menuRectTransform;

        [SerializeField] private Vector2 offset = new Vector2(0, 100f);

        [Header("Contenedor Dinamico")] [SerializeField]
        private Transform optionsContainer;

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
            menuRectTransform.position = screenPos;

            ClearOptions();

            if (!slot.Taked)
            {
                CreateOptionButton(basicCropData, () =>
                {
                    selectedSlot.SetCrop(basicCropData);
                    gameObject.SetActive(false);
                });
            }
            else
            {
                CropDataSO currentData = slot.CropData;

                if (currentData != null && currentData.Upgrades != null)
                {
                    foreach (var upgradeData in currentData.Upgrades)
                    {
                        CropDataSO targetUpgrade = upgradeData;
                        CreateOptionButton(targetUpgrade, () =>
                        {
                            selectedSlot.SetCrop(targetUpgrade);
                            gameObject.SetActive(false);
                        });
                    }
                }
            }

            gameObject.SetActive(true);
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