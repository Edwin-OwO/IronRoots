using Torretas;

namespace Buffs
{
    public class FireRateBuff : BuffBase
    {
        private float originalFireRate;
        private Turret turret;
        
        public override void Initialize(float durationBuff, float amountBuff)
        {
            turret = GetComponent<Turret>();
            originalFireRate = turret.ActualFireRate;
            turret.UpdateFireRate(amountBuff);
        }

        protected override void RemoveEffect()
        {
            //uwu
        }
    }
}