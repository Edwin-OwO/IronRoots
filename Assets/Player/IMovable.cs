using UnityEngine;

namespace IronRoots.Core
{
    // Abstrae CÓMO se mueve el objeto en el mundo: CharacterController, Rigidbody, NavMesh...
    // Permite cambiar la física de movimiento sin tocar quien la usa.
    public interface IMovable
    {
        void Move(Vector2 direction, float deltaTime);
    }
}
