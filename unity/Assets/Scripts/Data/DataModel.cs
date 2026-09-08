namespace XrMotionDataExplorer.Data
{
    public class SpatialSample
    {
        public double Timestamp { get; }
        public string ObjectId { get; }
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public SpatialSample(double timestamp, string objectId, float x, float y, float z)
        {
            this.Timestamp = timestamp;
            this.ObjectId = objectId;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
