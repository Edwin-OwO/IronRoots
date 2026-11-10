using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class OptionsMenuController : MonoBehaviour
    {
        [Header("Sonido")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private string volumeParameter = "MasterVolume"; // debe estar Exposed en el mixer
        
        private void Start()
        {
            CargarVolumenGuardado();
        }

        // ---------- Sonido ----------

        /// <summary>Conectar al OnValueChanged() de un Slider (rango sugerido: -80 a 0, escala logaritmica).</summary>
        public void SetVolume(float valorEnDb)
        {
            audioMixer.SetFloat(volumeParameter, valorEnDb);
            PlayerPrefs.SetFloat(volumeParameter, valorEnDb);
        }

        private void CargarVolumenGuardado()
        {
            if (PlayerPrefs.HasKey(volumeParameter))
            {
                float valorGuardado = PlayerPrefs.GetFloat(volumeParameter);
                audioMixer.SetFloat(volumeParameter, valorGuardado);
            }
        }
        
        public void PlayMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
