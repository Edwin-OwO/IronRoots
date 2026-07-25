using UnityEngine;
using Unity.Netcode;
using IronRoots.Core;

namespace IronRoots.Player
{
    // Esta es la ÚNICA clase de las tres que sabe que existe una red.
    // No implementa IMovementInput ni IMovable, ni hereda de las otras dos:
    // las busca con GetComponent y las usa a través de sus interfaces.
    [RequireComponent(typeof(PlayerInputReader))]
    [RequireComponent(typeof(Rigidbody2DMover))]
    public class PlayerMovementController : NetworkBehaviour
    {
        private IMovementInput _input;
        private IMovable _mover;
        private Vector2 _cachedDirection;

        private void Awake()
        {
            _input = GetComponent<PlayerInputReader>();
            _mover = GetComponent<Rigidbody2DMover>();
        }

        private void Update()
        {
            if (!IsOwner) return;

            // El input se lee en Update: es el momento en que Unity
            // efectivamente registra las teclas presionadas ese frame.
            _cachedDirection = _input.GetMovementInput();
        }

        private void FixedUpdate()
        {
            if (!IsOwner) return;

            // El movimiento físico se aplica en FixedUpdate: es cuando
            // corre el motor de física de Rigidbody2D, a paso fijo,
            // independiente de la tasa de frames de Update.
            _mover.Move(_cachedDirection, Time.fixedDeltaTime);
        }
    }
}