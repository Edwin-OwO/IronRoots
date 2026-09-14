using UnityEngine;

namespace Granja
{
    public class FarmMenuUI : MonoBehaviour

    { 
        [Header("Referencias UI")]
        [SerializeField] private RectTransform menuRectTransform;
        [SerializeField] private Vector2 offset = new Vector2(0, 100f); 
        
        [Header("Paneles de Botones")]
        [SerializeField] private GameObject plantMenuPanel; 
        [SerializeField] private GameObject actionMenuPanel;
        
       [Header("Configuración de Cultivos")]
       [SerializeField] private CropDataSO cropDataSO;

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
            
            plantMenuPanel.SetActive(!slot.Taked);
            actionMenuPanel.SetActive(slot.Taked);
        }
        
        public void OnPlantPressed()
        {
            if(selectedSlot != null && cropDataSO != null) 
            {
                selectedSlot.PlantCrop(cropDataSO);
            }
        
            plantMenuPanel.SetActive(false); 
        }
        
        public void OnDestroyCropPressed()
        {
            if (selectedSlot != null)
            {
                selectedSlot.DestroyCrop(); 
            }
            
            actionMenuPanel.SetActive(false); 
        }
        }
}