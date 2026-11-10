using System;
using UnityEngine;
using Enemigos;
    
namespace Nucleo
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static event Action OnStartGame;
        public static event Action OnGameOver;

        public bool GameActive { get; private set; } = true;

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
            WaveManager.OnWaveEnd += GameActiveToFalse;
            WaveManager.OnWavesFinished += GameActiveToFalse;
            WaveManager.OnWaveStart += GameActiveToTrue;
        }

        private void OnDisable()
        {
            FarmBase.OnBaseDestroyed -= GameOver;
            WaveManager.OnWaveEnd -= GameActiveToFalse;
            WaveManager.OnWavesFinished -= GameActiveToFalse;
            WaveManager.OnWaveStart -= GameActiveToTrue;
        }

        private void Start()
        {
            OnStartGame?.Invoke();
        }

        private void GameActiveToFalse()
        {
            GameActive = false;

        }
        private void GameActiveToTrue()
        {
            GameActive = true;
        }

        private void GameOver()
        {
            GameActive = false;
            OnGameOver?.Invoke();
        }
    }
}