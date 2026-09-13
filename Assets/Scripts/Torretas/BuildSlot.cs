using Economia;
using UnityEngine;
using TMPro;

namespace Torretas
{
    /// <summary>
    /// Punto de construccion. Ademas de construir/mejorar torretas via Strategy,
    /// maneja un boton en world-space que aparece al clickear el slot: si esta
    /// vacio construye la torreta con la estrategia por defecto, si ya tiene una
    /// la mejora. El mismo boton cambia de texto segun el estado.
    ///
    /// BuildSlot solo decide QUE accion corresponde (construir vs mejorar); no
    /// conoce el detalle de como se ve el boton mas alla de su texto, y no
    /// conoce el detalle de como se construye o se mejora una torreta (eso sigue
    /// delegado en IConstructionStrategy y en Turret.Upgrade()).
    /// </summary>
    public class BuildSlot : MonoBehaviour
    {
        [SerializeField] private bool taked;

        [Header("Estrategia usada al construir desde este slot")]
        [Tooltip("Debe ser un ScriptableObject que implemente IConstructionStrategy.")]
        [SerializeField] private ScriptableObject defaultStrategyAsset;

        [Header("UI del boton contextual")]
        [SerializeField] private GameObject actionButton;
        [SerializeField] private TMP_Text actionButtonLabel;

        private Turret actualTurret;
        private IConstructionStrategy DefaultStrategy => defaultStrategyAsset as IConstructionStrategy;

        public bool Taked => taked;
        public Turret ActualTurret => actualTurret;

        private void Awake()
        {
            if (actionButton != null) actionButton.SetActive(false);
        }

        /// <summary>Requiere un Collider2D en el mismo GameObject para detectar el click.</summary>
        private void OnMouseDown()
        {
            
        }

        private void ToggleActionButton()
        {
            if (actionButton == null) return;

            bool mostrar = !actionButton.activeSelf;
            actionButton.SetActive(mostrar);

            if (mostrar) ActualizarTextoBoton();
        }

        private void ActualizarTextoBoton()
        {
            if (actionButtonLabel == null) return;
            actionButtonLabel.text = taked ? "Mejorar" : "Construir";
        }

        /// <summary>Conectar esto al OnClick() del boton en el Inspector.</summary>
        public void OnActionButtonPressed()
        {
            if (!taked)
            {
                BuildTurret(DefaultStrategy);
            }
            else
            {
                actualTurret.Upgrade();
            }

            ActualizarTextoBoton();
        }

        public void ShowActionButton()
        {
            ToggleActionButton();
        }

        public bool BuildTurret(IConstructionStrategy strategy)
        {
            if (taked) return false;
            if (strategy == null) return false;
           // if (!EconomyManager.Instance.CanBuy(strategy.Cost)) return false;

            // EconomyManager.Instance.PayCost(strategy.Cost);
            actualTurret = strategy.Build(transform.position, transform);
            taked = true;
            return true;
        }
    }
}