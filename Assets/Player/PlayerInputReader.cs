using UnityEngine;
using IronRoots.Core;

namespace IronRoots.Player
{
    // Única responsabilidad: leer el input crudo del jugador local.
    // No es un NetworkBehaviour porque no necesita replicarse: cada cliente
    // ejecuta esto solo para SU propio jugador, nunca para los demás.
    public class PlayerInputReader : MonoBehaviour, IMovementInput
    {
        public Vector2 GetMovementInput()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            return new Vector2(h, v).normalized;
        }
    }
}
