using UnityEngine;

namespace Buffs
{
    public abstract class BuffBase : MonoBehaviour
    {
        public abstract void Initialize(float durationBuff, float amountBuff);
        protected abstract void RemoveEffect();
    }
}