using System;
using UnityEngine;

namespace Granja
{
    public class BasicCrop : CropBase
    {
        private void Awake()
        {
            moneyPerCycle = 2;
            timePerCycle = 4f;
        }
    }
}