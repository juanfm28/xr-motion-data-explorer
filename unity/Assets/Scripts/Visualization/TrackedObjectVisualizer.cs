using UnityEngine;

namespace XrMotionDataExplorer.Visualization
{
    public class TrackedObjectVisualizer : MonoBehaviour
    {
        private LineRenderer _trajectory;
        public void ShowObject(bool show)
        {
            gameObject.SetActive(show);
        }

        public void SetInPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void CreateTrajectory(Vector3[] positions)
        {
            GameObject trajectoryObject= new GameObject();
            trajectoryObject.transform.SetParent(transform);
            trajectoryObject.name = $"{gameObject.name}_trajectory";
            _trajectory = trajectoryObject.AddComponent<LineRenderer>();
            _trajectory.useWorldSpace = true;
            _trajectory.material = new Material(Shader.Find("Sprites/Default"));
            _trajectory.widthMultiplier = 0.01f;

            _trajectory.positionCount = positions.Length;
            _trajectory.SetPositions(positions);
        }

    }
}
