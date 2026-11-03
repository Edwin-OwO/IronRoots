
using System.Collections.Generic;
using UnityEngine;

namespace Torretas
{
    public class TurretMenuUI : MonoBehaviour
    {
        [Header("Referencias UI")]
        [SerializeField] private GameObject menu;
        [SerializeField] private RectTransform menuRectTransform;
        [SerializeField] private Vector2 offset = new Vector2(0, 100f);

        [Header("Paneles de Botones")]
        [SerializeField] private GameObject buildMenuPanel;
        [SerializeField] private GameObject actionMenuPanel;

        [Header("Contenedor Dinamico")]
        [SerializeField] private Transform optionsContainer;
        [SerializeField] private TurretOptionButton optionButtonPrefab;

        [Header("Torretas Disponibles")]
        [SerializeField] private List<TurretSO> availableTurrets;

        [Header("Venta")]
        [SerializeField] private float sellPorcentage = 0.50f;

        private BuildSlot selectedSlot;
        private Camera mainCam;

        private void Awake()
        {
            mainCam = Camera.main;
        }

        private void OnEnable() =>
            BuildSlot.OnSlotClicked += ShowMenu;

        private void OnDisable() =>
            BuildSlot.OnSlotClicked -= ShowMenu;

        private void ShowMenu(BuildSlot slot)
        {
            if (mainCam == null || slot == null)
                return;

            selectedSlot = slot;

            Vector3 screenPos =
                mainCam.WorldToScreenPoint(slot.transform.position);

            screenPos.x += offset.x;
            screenPos.y += offset.y;

            menuRectTransform.position = screenPos;

            ClearOptions();

            if (!slot.Taked)
            {
                foreach (var turretData in availableTurrets)
                {
                    if (turretData == null)
                        continue;

                    CreateOptionButton(turretData, () =>
                    {
                        if (selectedSlot == null || selectedSlot.Taked)
                            return;

                        selectedSlot.BuildTurret(turretData);
                        menu.SetActive(false);
                    });
                }
            }

            buildMenuPanel.SetActive(!slot.Taked);
            actionMenuPanel.SetActive(slot.Taked);

            menu.SetActive(true);
        }

        private void CreateOptionButton(
            TurretSO turretData,
            System.Action onClickAction)
        {
            if (turretData == null || optionButtonPrefab == null)
                return;

            TurretOptionButton btn =
                Instantiate(optionButtonPrefab, optionsContainer);

            btn.Setup(turretData, onClickAction);
        }

        private void ClearOptions()
        {
            foreach (Transform child in optionsContainer)
            {
                Destroy(child.gameObject);
            }
        }

        public void OnUpgradePressed()
        {
            if (selectedSlot != null && selectedSlot.Taked)
                selectedSlot.ActualTurret.Upgrade();

            menu.SetActive(false);
        }

        public void OnSellPressed()
        {
            if (selectedSlot != null && selectedSlot.Taked)
                selectedSlot.SellTurret(sellPorcentage);

            menu.SetActive(false);
        }
    }
}