using System;
using UnityEngine;
using Enemigos;
using Economia;
using UI;
    
namespace Nucleo
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private EconomyManager economyManager;

        public bool GameActive { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnEnable()
        {
            FarmBase.OnBaseDestroyed +=  GameOver;
        }

        private void OnDisable()
        {
            FarmBase.OnBaseDestroyed -= GameOver;
        }

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            GameActive = true;
            uiManager.UpdateMoney(economyManager.Money);
            uiManager.UpdateLife(FarmBase.Instance.Health);
            waveManager.StartNextWave();
        }

        private void GameOver()
        {
            GameActive = false;
            uiManager.ShowGameOver();
        }
    }
}