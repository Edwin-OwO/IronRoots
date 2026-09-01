using System;

namespace Granja
{
    public class PremiumCroop : CropBase
    {
        private void Awake()
        {
            moneyPerCycle = 4;
            timePerCycle = 6f;
        }
    }
}