using Enemigos;
using UnityEngine;

namespace Buffs
{
    public class IceDebuff : BuffBase
    {
        private float originalVelocity;
        private Enemy enemy;
        
        public override void Initialize(float durationDebuff, float amountDebuff)
        {
            enemy = GetComponent<Enemy>();
            originalVelocity = enemy.VelocityMultiplier;
            enemy.VelocityMultiplier = amountDebuff;
            Invoke(nameof(RemoveEffect), durationDebuff);
        }

        protected override void RemoveEffect()
        {
            enemy.VelocityMultiplier = originalVelocity;
            Destroy(this);
        }
    }
}