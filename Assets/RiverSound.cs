// code from https://www.youtube.com/watch?v=5lEzchlQYKk

using UnityEngine;
using UnityEngine.Serialization;

// ============== INSTRUCTION ==============
// Create empty game object and add "Cinemachine Path" component
// Add waypoints and draw line
// Create another empty game object and add this script
// Select "Path" as well as "Player" in the inspector
// Add sound to the object

namespace Cinemachine
{
    public class RiverSound : MonoBehaviour
    {
        [Tooltip("Cinemachine Path to follow")]
        public CinemachinePathBase m_Path;
        [Tooltip("Character to track")]
        public GameObject Player;

        float m_Position;       // The position along the path to set the cart to in path units
        private CinemachinePathBase.PositionUnits m_PositionUnits = CinemachinePathBase.PositionUnits.PathUnits;

        void Update()
        {
            // Find closest point to the Player along the path
            SetCartPosition(m_Path.FindClosestPoint(Player.transform.position, 0, -1, 10));
        }

        // Set cart's position to closest point
        void SetCartPosition(float distanceAlongPath)
        {
            m_Position = m_Path.StandardizeUnit(distanceAlongPath, m_PositionUnits);
            transform.position = m_Path.EvaluatePositionAtUnit(m_Position, m_PositionUnits);
            transform.rotation = m_Path.EvaluateOrientationAtUnit(m_Position, m_PositionUnits);
        }
    }
}