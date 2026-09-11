using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using XrMotionDataExplorer.Data;

namespace XrMotionDataExplorer.Tests.Data
{
    public class TrackedObjectTests
    {
        private const float PositionTolerance = 0.0001f;

        [Test]
        public void EvaluatePosition_ExactTimestamp_ReturnsRecordedPosition()
        {
            TrackedObject trackedObject = CreateTrackedObject(
                new SpatialSample(0.0, "alpha", 1.0f, 2.0f, 3.0f),
                new SpatialSample(1.0, "alpha", 4.0f, 5.0f, 6.0f),
                new SpatialSample(2.0, "alpha", 7.0f, 8.0f, 9.0f));

            Vector3 position = trackedObject.EvaluatePosition(1.0);

            AssertPosition(position, 4.0f, 5.0f, 6.0f);
        }

        [Test]
        public void EvaluatePosition_BetweenSamples_InterpolatesUsingTimestamps()
        {
            TrackedObject trackedObject = CreateTrackedObject(
                new SpatialSample(2.0, "alpha", 10.0f, 0.0f, -4.0f),
                new SpatialSample(4.0, "alpha", 20.0f, 8.0f, 4.0f));

            Vector3 position = trackedObject.EvaluatePosition(2.5);

            AssertPosition(position, 12.5f, 2.0f, -2.0f);
        }

        [Test]
        public void EvaluatePosition_BeforeFirstSample_ReturnsFirstPosition()
        {
            TrackedObject trackedObject = CreateTrackedObject(
                new SpatialSample(2.0, "alpha", 1.0f, 2.0f, 3.0f),
                new SpatialSample(4.0, "alpha", 4.0f, 5.0f, 6.0f));

            Vector3 position = trackedObject.EvaluatePosition(1.0);

            AssertPosition(position, 1.0f, 2.0f, 3.0f);
        }

        [Test]
        public void EvaluatePosition_AfterLastSample_ReturnsLastPosition()
        {
            TrackedObject trackedObject = CreateTrackedObject(
                new SpatialSample(2.0, "alpha", 1.0f, 2.0f, 3.0f),
                new SpatialSample(4.0, "alpha", 4.0f, 5.0f, 6.0f));

            Vector3 position = trackedObject.EvaluatePosition(5.0);

            AssertPosition(position, 4.0f, 5.0f, 6.0f);
        }

        [Test]
        public void EvaluatePosition_SingleSample_ReturnsOnlyPosition()
        {
            TrackedObject trackedObject = CreateTrackedObject(
                new SpatialSample(3.0, "alpha", 7.0f, 8.0f, 9.0f));

            Vector3 position = trackedObject.EvaluatePosition(100.0);

            AssertPosition(position, 7.0f, 8.0f, 9.0f);
        }

        [Test]
        public void Constructor_DuplicateTimestamps_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => CreateTrackedObject(
                new SpatialSample(0.4, "alpha", 1.0f, 2.0f, 3.0f),
                new SpatialSample(0.4, "alpha", 4.0f, 5.0f, 6.0f)));
        }

        private static TrackedObject CreateTrackedObject(params SpatialSample[] samples)
        {
            return new TrackedObject("alpha", new List<SpatialSample>(samples).AsReadOnly());
        }

        private static void AssertPosition(Vector3 actual, float x, float y, float z)
        {
            Assert.That(actual.x, Is.EqualTo(x).Within(PositionTolerance));
            Assert.That(actual.y, Is.EqualTo(y).Within(PositionTolerance));
            Assert.That(actual.z, Is.EqualTo(z).Within(PositionTolerance));
        }
    }
}
