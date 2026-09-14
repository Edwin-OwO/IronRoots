using UnityEngine;
using Economia;
using Enemigos;
using Nucleo;
using TMPro;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI MoneyText;
        [SerializeField] private TextMeshProUGUI LivesText;
        [SerializeField] private GameObject waveButton;
        [SerializeField] private GameObject panelGameOver;
        
        private void OnEnable()
        {
            EconomyManager.OnMoneyChanged += UpdateMoney;
            WaveManager.OnWaveEnd += ShowWaveButton;
            FarmBase.OnEnemieEnter += UpdateLife;
        }

        private void OnDisable()
        {
                EconomyManager.OnMoneyChanged -= UpdateMoney;
                WaveManager.OnWaveEnd -= ShowWaveButton;
                FarmBase.OnEnemieEnter -= UpdateLife;
        }

        public void UpdateMoney(float amount)
        {
            MoneyText.text = $"$ {amount}";
        }

        public void UpdateLife(int amount)
        {
            LivesText.text = $"Remaining mistakes: {amount}";
        }

        private void ShowWaveButton()
        {
            waveButton.SetActive(true);
        }
        
        public void ShowGameOver()
        {
            panelGameOver.SetActive(true);
        }
    }

}
