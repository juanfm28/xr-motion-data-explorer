using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XrMotionDataExplorer.Data;
using XrMotionDataExplorer.Playback;

namespace XrMotionDataExplorer.Visualization
{
    public class TrackedObjectVisualizer : MonoBehaviour
    {
        [SerializeField] private MotionDataLoader dataLoader;
        [SerializeField] private PlaybackController playbackController;
        [SerializeField] private string objectId;
        public string ObjectId => objectId;

        private List<SpatialSample> _objectSamples;

        private SpatialSample _activeSample;


        private void Start()
        {
            _objectSamples = dataLoader.Dataset.Where(x=>x.ObjectId == ObjectId).OrderBy(x=>x.Timestamp).ToList();
            if (_objectSamples.Count == 0) return;
            _activeSample = _objectSamples[0];
            transform.position = new Vector3( _activeSample.X, _activeSample.Y, _activeSample.Z);
        }

        private void LateUpdate()
        {
            SpatialSample resolved = ResolveSample(playbackController.CurrentTime);
            if (resolved == null || resolved == _activeSample) return;
            _activeSample = resolved;
            transform.position = new Vector3( _activeSample.X, _activeSample.Y, _activeSample.Z);
        }

        public SpatialSample ResolveSample(double timestamp)
        {
            return _objectSamples.FindLast(x => x.Timestamp <= timestamp);
        }
    }
}
