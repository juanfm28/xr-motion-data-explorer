using System;
using System.IO;
using UnityEngine;
using System.Linq;
using XrMotionDataExplorer.Data;
using XrMotionDataExplorer.Visualization;
using XrMotionDataExplorer.Playback;

namespace XrMotionDataExplorer.Controllers
{
    public class TrackedObjectController : MonoBehaviour
    {
        [SerializeField] private MotionDataLoader dataLoader;
        [SerializeField] private PlaybackController playbackController;
        [SerializeField] private string objectId;
        private TrackedObject _trackedObject;
        private TrackedObjectVisualizer _view;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _view = gameObject.AddComponent<TrackedObjectVisualizer>();
            var objectSamples = dataLoader.Dataset.Where(x=>x.ObjectId == objectId).OrderBy(x=>x.Timestamp).ToList();
            if (objectSamples.Count == 0)
            {
                _view.ShowObject(false);
                enabled = false;
                return;
            }

            try
            {
                _trackedObject = new TrackedObject(objectId, objectSamples);
            }
            catch (InvalidDataException ide)
            {
                Debug.LogError(ide.Message);
                enabled = false;
                return;
            }
            gameObject.name = objectId;
            if (objectSamples.Count == 1)
            {
                enabled = false;
                _view.SetInPosition(objectSamples[0].Position);
                return;
            }
            Vector3[] positions = objectSamples
                .Select(x => x.Position)
                .ToArray();
            _view.CreateTrajectory(positions);
        }

        private void LateUpdate()
        {
            Vector3 position = _trackedObject.EvaluatePosition(playbackController.CurrentTime);
            _view.SetInPosition(position);
        }
    }
}
