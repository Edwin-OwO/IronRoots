using UnityEngine;
using Nucleo.Abilities;

namespace Nucleo
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ExplosionTool explosiveTool;
        public void UseExplosionTool(Vector2 cursorPosition)
        {
            explosiveTool.ActiveOnPosition(cursorPosition);
        }
    }
}