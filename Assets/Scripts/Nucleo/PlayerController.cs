using Granja;
using UnityEngine;
using Torretas;
using Nucleo.Abilities;

namespace Nucleo
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ExplosionTool explosiveTool;

        private IConstructionStrategy selectedStrategy;

        /// <summary>
        /// Llamado desde la UI (por ejemplo, al tocar el boton de "Torreta
        /// Proyectil") para definir que se va a construir en el proximo click
        /// sobre un BuildSlot. Gracias al patron Strategy, este metodo funciona
        /// igual sin importar cuantos tipos de torreta existan a futuro.
        /// </summary>
        
        public void SelectTurretStrategy(IConstructionStrategy strategy)
        {
            selectedStrategy = strategy;
        }

        public void BuildOnSlot(BuildSlot slot)
        {
            if (selectedStrategy == null)
            {
                Debug.LogWarning("No hay ninguna torreta seleccionada para construir.");
                return;
            }

            slot.BuildTurret(selectedStrategy);
        }

        public void UpgradeTurret(Turret turret)
        {
            turret.Upgrade();
        }

        /// <summary>T es el tipo de cultivo elegido en la UI (CultivoBasico, CultivoPremium, ...).</summary>
        public void PlantarEnFarmSlot<T>(FarmSlot slot, int cost) where T : CropBase
        {
            slot.PlantCrop<T>(cost);
        }

        /// <summary>Se llama con la posicion en mundo correspondiente al click del cursor.</summary>
        public void UseExplosionTool(Vector2 cursorPosition)
        {
            explosiveTool.ActiveOnPosition(cursorPosition);
        }
    }
}