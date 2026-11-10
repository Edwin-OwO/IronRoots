using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class IncomeNotification : MonoBehaviour
    {
        [SerializeField] private TMP_Text amountText;
        [SerializeField] private Image entityIcon;
        
        [Header("Animación y Tiempo")]
        [SerializeField] private float lifetime = 10f;
        [SerializeField] private float slideSpeed = 8f;

        private RectTransform rectTransform;
        private Vector2 targetPosition;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void Setup(float amount, SpriteRenderer icon)
        {
            amountText.text = $"+{amount}";
            entityIcon.sprite = icon.sprite;
            
            Destroy(gameObject, lifetime);
        }
        
        public void SetTargetPosition(Vector2 newTarget)
        {
            targetPosition = newTarget;
        }

        private void Update()
        {
            rectTransform.anchoredPosition = Vector2.Lerp(
                rectTransform.anchoredPosition, 
                targetPosition, 
                Time.deltaTime * slideSpeed
            );
        }
    }
}