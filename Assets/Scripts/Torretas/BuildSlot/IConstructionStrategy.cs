using UnityEngine;

namespace Torretas
{
    public interface IConstructionStrategy
    {
        int Cost { get; }
        Turret Build(Vector3 posicion, Transform padre);
    }
}