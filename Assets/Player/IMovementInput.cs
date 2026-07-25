using UnityEngine;

namespace IronRoots.Core
{
    // Abstrae DE DÓNDE viene el input: teclado, IA, replay grabado, etc.
    // Quien use esta interfaz no sabe (ni le importa) el origen del Vector2.
    public interface IMovementInput
    {
        Vector2 GetMovementInput();
    }
}
