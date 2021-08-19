using NUnit.Framework;

namespace Gentrack_Assessment_Tests
{
    public class Tests
    {
        [Test]
        public void CSVIntervalData_IsFoundInXML_File()
        {
            // arrange
            string xmlFile = "/Users/lianatime/Desktop/testfile.xml"

            // act
            var csvInterval = Gentrack_Assessment.CreateCSV.LoadXml(xmlFile);

            // assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(csvInterval));
        }

        [Test]
        public void CSVIntervalData_IsTurnedIntoA_ListOfStringArrays()
        {
            // arrange
            List<string[]> CsvIntervalDataBlock = new List<string[]>();
            
        }

    }
}