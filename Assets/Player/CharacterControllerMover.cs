using UnityEngine;
using IronRoots.Core;

namespace IronRoots.Player
{
    // Única responsabilidad: traducir una dirección en movimiento físico real,
    // usando CharacterController. Tampoco es un NetworkBehaviour: es pura física
    // local, se ejecuta igual haya red o no.
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerMover : MonoBehaviour, IMovable
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float gravity = -9.81f;

        private CharacterController _controller;
        private float _verticalVelocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        public void Move(Vector2 direction, float deltaTime)
        {
            Vector3 motion = new Vector3(direction.x, 0f, direction.y) * speed;

            if (_controller.isGrounded) _verticalVelocity = -1f;
            else _verticalVelocity += gravity * deltaTime;

            motion.y = _verticalVelocity;
            _controller.Move(motion * deltaTime);
        }
    }
}
