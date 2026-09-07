using UnityEngine;
using Enemigos;
using Economia;
using UI;
    
namespace Nucleo
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instancia { get; private set; }

        [SerializeField] private int playerLives = 3;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private EconomyManager economyManager;

        public bool GameActive { get; private set; }

        private void Awake()
        {
            if (Instancia != null && Instancia != this)
            {
                Destroy(gameObject);
                return;
            }
            Instancia = this;
        }

        public void StartGame()
        {
            GameActive = true;
            uiManager.UpdateLifes(playerLives);
            uiManager.UpdateMoney(economyManager.Money);
            waveManager.StartNextWave();
        }
        
        public void LoseLife(int amount)
        {
            if (!GameActive) return;

            playerLives -= amount;
            uiManager.UpdateLifes(playerLives);

            if (playerLives <= 0)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            GameActive = false;
            uiManager.ShowGameOver();
        }
    }
}