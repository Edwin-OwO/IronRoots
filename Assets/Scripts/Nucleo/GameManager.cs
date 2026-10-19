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

        public static event Action OnStartGame;
        public static event Action OnGameOver; 
         
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
            WaveManager.OnWaveEnd += UpdateGameActive;
        }

        private void OnDisable()
        {
            FarmBase.OnBaseDestroyed -= GameOver;
            WaveManager.OnWaveEnd -= UpdateGameActive;
        }

        private void Start()
        {
            OnStartGame?.Invoke();
        }

        private void UpdateGameActive()
        {
            GameActive = !GameActive;
        }

        private void GameOver()
        {
            GameActive = false;
            OnGameOver?.Invoke();
        }
    }
}