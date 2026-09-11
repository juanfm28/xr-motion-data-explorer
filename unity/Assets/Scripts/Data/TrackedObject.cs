using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace XrMotionDataExplorer.Data
{
    public class TrackedObject
    {
        public string ObjectId { get; }
        IReadOnlyList<SpatialSample> _objectSamples;

        private SpatialSample First => _objectSamples[0];
        private SpatialSample Last => _objectSamples[^1];

        public TrackedObject(string objectID, IReadOnlyList<SpatialSample> objectSamples)
        {
            ObjectId = objectID;
            _objectSamples = objectSamples;

            var timestamps = new HashSet<double>();
            bool hasDuplicates = _objectSamples.Any(sample => !timestamps.Add(sample.Timestamp));

            if (hasDuplicates)
            {
                throw new InvalidDataException("Duplicated timestamps in object. Object rejected");
            }
        }

        public Vector3 EvaluatePosition(double time)
        {
            if(time <= First.Timestamp || _objectSamples.Count == 1)
                return First.Position;
            if(time >= Last.Timestamp)
                return Last.Position;

            SpatialSample previous = _objectSamples.Last(x=>x.Timestamp <= time);
            SpatialSample next = _objectSamples.First(x=>x.Timestamp > time);

            double duration = next.Timestamp - previous.Timestamp;
            double elapsed = time - previous.Timestamp;
            float t = (float)(elapsed / duration);

            var interpolatedPosition = Vector3.Lerp(previous.Position, next.Position, t);
            return interpolatedPosition;

        }
    }
}
