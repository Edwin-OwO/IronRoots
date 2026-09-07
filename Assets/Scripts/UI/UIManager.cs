using UnityEngine;
using UnityEngine.UI;
using Economia;
namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text textoDinero;
        [SerializeField] private Text textoVidas;
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
            textoDinero.text = $"$ {amount}";
        }

        public void UpdateLifes(int amount)
        {
            textoVidas.text = $"Vidas: {amount}";
        }

        public void ShowGameOver()
        {
            panelGameOver.SetActive(true);
        }
    }

}
