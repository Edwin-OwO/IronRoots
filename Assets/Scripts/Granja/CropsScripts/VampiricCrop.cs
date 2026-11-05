using UnityEngine;
using Enemigos;
using Economia;

namespace Granja
{
    public class VampiricCrop : CropBase
    {
        [SerializeField] private AudioClip vampiricCropSound;
        [Header("Vampiric Crop")]
        [SerializeField] private float moneyPerKillBonus = 0.5f;
        [SerializeField] private ParticleSystem particleSystem;    
        
        private float accumulatedBonus = 0f;
        private void OnEnable()
        {
            Enemy.OnEnemyDied += PlantAbility;
        }

        private void OnDisable()
        {
            Enemy.OnEnemyDied -= PlantAbility;
        }
        protected override void PlantAbility()
        {
            accumulatedBonus += moneyPerKillBonus;
            currentMoneyPerCycle = accumulatedBonus + moneyPerCycle;
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
            audioSource.PlayOneShot(vampiricCropSound);
        }
    }
}