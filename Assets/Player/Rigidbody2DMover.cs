using UnityEngine;
using IronRoots.Core;

namespace IronRoots.Player
{
    // Única responsabilidad: aplicar el movimiento en un mundo 2D top-down
    // usando Rigidbody2D. No hay eje de altura ni gravedad real: "arriba" y
    // "abajo" en pantalla son simplemente el eje Y del mundo.
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rigidbody2DMover : MonoBehaviour, IMovable
    {
        [SerializeField] private float speed = 5f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0f; // top-down: no hay nada de qué "caer"
        }

        public void Move(Vector2 direction, float deltaTime)
        {
            // deltaTime no se usa acá: la velocidad ya es una tasa
            // (unidades por segundo), no un desplazamiento a sumar.
            _rb.linearVelocity = direction * speed;
        }
    }
}
