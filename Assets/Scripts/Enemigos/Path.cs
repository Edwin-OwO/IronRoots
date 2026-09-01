using UnityEngine;

namespace Enemigos
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private Transform[] waypoints;

        public int AmountOfWaypoints => waypoints.Length;

        public Vector2 GetWaypoint(int index)
        {
            index = Mathf.Clamp(index, 0, waypoints.Length - 1);
            return waypoints[index].position;
        }
    }
}