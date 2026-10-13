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

        [Header("Resolucion")]
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private Toggle fullscreenToggle;

        private Resolution[] resolucionesDisponibles;

        private void Start()
        {
            CargarVolumenGuardado();
            PoblarDropdownDeResoluciones();
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

        // ---------- Resolucion ----------

        private void PoblarDropdownDeResoluciones()
        {
            resolucionesDisponibles = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> opciones = new List<string>();
            int indiceActual = 0;

            for (int i = 0; i < resolucionesDisponibles.Length; i++)
            {
                Resolution res = resolucionesDisponibles[i];
                opciones.Add($"{res.width} x {res.height}");

                bool esLaActual = res.width == Screen.currentResolution.width
                                && res.height == Screen.currentResolution.height;
                if (esLaActual) indiceActual = i;
            }

            resolutionDropdown.AddOptions(opciones);
            resolutionDropdown.value = indiceActual;
            resolutionDropdown.RefreshShownValue();
        }

        /// <summary>Conectar al OnValueChanged() del dropdown de resoluciones.</summary>
        public void SetResolution(int indice)
        {
            Resolution res = resolucionesDisponibles[indice];
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        }

        /// <summary>Conectar al OnValueChanged() de un Toggle de pantalla completa.</summary>
        public void SetFullscreen(bool esPantallaCompleta)
        {
            Screen.fullScreen = esPantallaCompleta;
        }
        
        public void PlayMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
