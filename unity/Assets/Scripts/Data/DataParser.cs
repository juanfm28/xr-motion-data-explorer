using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace XrMotionDataExplorer.Data
{
    public static class DataParser
    {
        private const char Separator = ',';

        public static IReadOnlyList<SpatialSample> ParseMotionData(string fullText)
        {
            List<SpatialSample> dataset = new();
            if (string.IsNullOrWhiteSpace(fullText))
                throw new InvalidDataException("Empty data");
            string[] lines = fullText.Split(separator: '\n');
            if(lines.Length <= 1)
                throw new InvalidDataException("Header-only file");
            if(lines[0].TrimEnd('\r') != "timestamp,object_id,x,y,z")
                throw new InvalidDataException("Line 1: Malformed header");
            for (int i = 1; i < lines.Length; i++)
            {
                if(string.IsNullOrWhiteSpace(lines[i]))
                   continue;
                string[] components = lines[i].TrimEnd('\r').Split(Separator);
                if (components.Length != 5)
                    throw new InvalidDataException($"Line {i+1}: Unexpected number of columns");
                if (!double.TryParse(components[0],NumberStyles.Float,CultureInfo.InvariantCulture, out double time))
                    throw new InvalidDataException($"Line {i+1}: Invalid timestamp format");
                if(double.IsInfinity(time) || double.IsNaN(time) || time < 0)
                    throw new InvalidDataException($"Line {i+1}: Invalid timestamp value: {time}");
                if(string.IsNullOrWhiteSpace(components[1]))
                    throw new InvalidDataException($"Line {i+1}: Invalid object id");
                string id = components[1];
                if(!float.TryParse(components[2],NumberStyles.Float,CultureInfo.InvariantCulture, out float x))
                    throw new InvalidDataException($"Line {i+1}: Invalid position => x");//TODO: Validate xyz for NaN and Infinity
                if(!float.TryParse(components[3],NumberStyles.Float,CultureInfo.InvariantCulture, out float y))
                    throw new InvalidDataException($"Line {i+1}: Invalid position => y");
                if(!float.TryParse(components[4],NumberStyles.Float,CultureInfo.InvariantCulture, out float z))
                    throw new InvalidDataException($"Line {i+1}: Invalid position => z");

                dataset.Add(new SpatialSample(time,id,x,y,z));
            }

            return dataset.AsReadOnly();
        }
    }
}
