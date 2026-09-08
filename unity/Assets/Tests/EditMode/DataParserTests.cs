using System.IO;
using NUnit.Framework;
using XrMotionDataExplorer.Data;

namespace XrMotionDataExplorer.Tests.Data
{
    public class DataParserTests
    {
        [Test]
        public void ParseMotionData_ValidCsv_ParsesAllFields()
        {
            const string csv =
                "timestamp,object_id,x,y,z\r\n" +
                "0.0,alpha,-1.0,0.5,0.0\r\n" +
                "1.0,beta,2.5,-3.0,4.25";

            var samples = DataParser.ParseMotionData(csv);

            Assert.That(samples, Has.Count.EqualTo(2));

            Assert.That(samples[0].Timestamp, Is.EqualTo(0.0));
            Assert.That(samples[0].ObjectId, Is.EqualTo("alpha"));
            Assert.That(samples[0].X, Is.EqualTo(-1.0f));
            Assert.That(samples[0].Y, Is.EqualTo(0.5f));
            Assert.That(samples[0].Z, Is.EqualTo(0.0f));

            Assert.That(samples[1].Timestamp, Is.EqualTo(1.0));
            Assert.That(samples[1].ObjectId, Is.EqualTo("beta"));
            Assert.That(samples[1].X, Is.EqualTo(2.5f));
            Assert.That(samples[1].Y, Is.EqualTo(-3.0f));
            Assert.That(samples[1].Z, Is.EqualTo(4.25f));
        }

        [Test]
        public void ParseMotionData_InvalidYCoordinate_ReportsLineAndField()
        {
            const string csv =
                "timestamp,object_id,x,y,z\n" +
                "0.0,alpha,-1.0,0.5,0.0\n" +
                "1.0,beta,2.5,invalid,4.25";

            InvalidDataException exception = Assert.Throws<InvalidDataException>(
                () => DataParser.ParseMotionData(csv));

            Assert.That(exception.Message, Does.Contain("Line 3"));
            Assert.That(exception.Message, Does.Contain("y"));
        }

        [Test]
        public void ParseMotionData_MalformedHeader_ReportsHeaderError()
        {
            const string csv =
                "time,id,x,y,z\n" +
                "0.0,alpha,-1.0,0.5,0.0";

            InvalidDataException exception = Assert.Throws<InvalidDataException>(
                () => DataParser.ParseMotionData(csv));

            Assert.That(exception.Message, Does.Contain("Line 1"));
            Assert.That(exception.Message, Does.Contain("Malformed header"));
        }
    }
}
