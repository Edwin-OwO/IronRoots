using Torretas;

namespace Buffs
{
    public class DamageBuff : BuffBase
    {
        private float amountBuffed;
        private Turret turret;
        public override void Initialize(float durationBuff, float amountBuff)
        {
            turret = GetComponent<Turret>();
            amountBuffed = amountBuff;
            turret.AddDamage(amountBuffed);
            Invoke(nameof(RemoveEffect), durationBuff);
        }

        protected override void RemoveEffect()
        {
            turret.RemoveDamage(amountBuffed);
            Destroy(this);
        }
    }
}