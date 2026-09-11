using UnityEngine;
using Economia;
using TMPro;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI MoneyText;
        [SerializeField] private TextMeshProUGUI LivesText;
        [SerializeField] private GameObject panelGameOver;

        private void Start()
        {
            EconomyManager.Instance.OnMoneyChanged += UpdateMoney;
            UpdateMoney(EconomyManager.Instance.Money);
        }

        private void OnDestroy()
        {
            if (EconomyManager.Instance != null)
                EconomyManager.Instance.OnMoneyChanged -= UpdateMoney;
        }

        public void UpdateMoney(int amount)
        {
            MoneyText.text = $"$ {amount}";
        }

        public void UpdateLifes(int amount)
        {
            LivesText.text = $"Vidas: {amount}";
        }

        public void ShowGameOver()
        {
            panelGameOver.SetActive(true);
        }
    }

}
