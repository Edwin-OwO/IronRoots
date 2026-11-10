using System.Collections.Generic;
using Economia;
using UnityEngine;

namespace UI
{
    public class NotificationManager : MonoBehaviour
    {
        [Header("Referencias")]
        [SerializeField] private IncomeNotification notificationPrefab;
        [Tooltip("El objeto vacío en el Canvas donde aparecerán")]
        [SerializeField] private RectTransform container; 
        
        [Header("Configuración")]
        [SerializeField] private float spacing = 70f;
        [SerializeField] private Vector2 startSpawnPosition = new Vector2(0, 50f); // Desde dónde cae

        private List<IncomeNotification> activeNotifications = new List<IncomeNotification>();

        private void OnEnable()
        {
            EconomyManager.OnIncomeNotification += CreateNotification;
        }

        private void OnDisable()
        {
            EconomyManager.OnIncomeNotification -= CreateNotification;
        }

        private void CreateNotification(float amount, SpriteRenderer icon)
        {
            activeNotifications.RemoveAll(n => n == null);
            
            IncomeNotification newNoti = Instantiate(notificationPrefab, container);
            newNoti.Setup(amount, icon);
            
            RectTransform rect = newNoti.GetComponent<RectTransform>();
            rect.anchoredPosition = startSpawnPosition;
            
            activeNotifications.Insert(0, newNoti);
            
            UpdatePositions();
        }

        private void UpdatePositions()
        {
            for (int i = 0; i < activeNotifications.Count; i++)
            {
                if (activeNotifications[i] != null)
                {
                    float targetY = -i * spacing;
                    activeNotifications[i].SetTargetPosition(new Vector2(0, targetY));
                }
            }
        }
    }
}