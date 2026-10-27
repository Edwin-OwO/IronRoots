using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CropOptionButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text labelText;
        [SerializeField] private Image iconImage;
        
        public void Setup(CropDataSO cropData, Action onClickAction)
        {
            if (labelText != null)
                labelText.text = $"{cropData.CropName}\n(${cropData.Cost})";

            if (iconImage != null && cropData.CropIcon != null)
                iconImage.sprite = cropData.CropIcon;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClickAction?.Invoke());
        }
    }
}