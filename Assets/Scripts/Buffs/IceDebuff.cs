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
            originalVelocity = enemy.Velocity;
            enemy.UpdateVelocity(amountDebuff);
            Invoke(nameof(RemoveEffect), durationDebuff);
        }

        protected override void RemoveEffect()
        {
            enemy.UpdateVelocity(originalVelocity);
            Destroy(this);
        }
    }
}