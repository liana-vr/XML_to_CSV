using NUnit.Framework;
using System.Collections.Generic;

namespace Gentrack_Assessment_Tests
{
    public class UnitTests
    {
        public const string FILENAME = "/Users/lianatime/Desktop/testfile.xml";                             // update file path

        [Test]
        public void CSVIntervalData_Is_Found_In_XML_File()
        {
            // arrange
            string xmlFilePath = FILENAME;

            // act
            var csvInterval = Gentrack_Assessment.CreateCSV.LoadXml(xmlFilePath);                           // Testing the LoadXml Function

            // assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(csvInterval));
        }

        [Test]
        public void CSVIntervalData_Is_Turned_Into_A_List_Of_String_Arrays()
        {
            // arrange
            string csvIntervalData = "100, NEM12, 201801211010, MYENRGY\n200, 98765432109, E1, E1, N1, HGLMET502, KWH, 30\n300, 20180115, 1.000, 1.000, 1.000, 1.008, 1.000, 1.96";

            // act
            var CsvIntervalDataBlock = Gentrack_Assessment.CreateCSV.ReadCSVInterval(csvIntervalData);      // Testing the ReadCSVInterval method

            // assert
            Assert.IsTrue(CsvIntervalDataBlock[1][0] == "200");

        }

        [Test]
        public void DataBlock_Is_Shaped_Into_Correct_CsvBlock()
        {
            // arrange
            List<string[]> csvIntervalDataBlock = new List<string[]>();
            csvIntervalDataBlock.Add(new string[]{"100", "NEM12", "201801211010", "MYENRGY"});
            csvIntervalDataBlock.Add(new string[]{"200", "98765432109", "E1", "E1", "N1", "HGLMET502", "KWH", "30"});
            csvIntervalDataBlock.Add(new string[]{"300", "20180115", "1.000", "1.000", "1.000", "1.008", "1.000", "1.96"});
            csvIntervalDataBlock.Add(new string[]{"300", "20180115", "1.000", "1.000", "1.000", "1.008", "1.000", "1.96"});
            csvIntervalDataBlock.Add(new string[]{"200", "58765432109", "E1", "E1", "N1", "HGLMET502", "KWH", "30"});
            csvIntervalDataBlock.Add(new string[]{"300", "10180115", "1.000", "1.000", "1.000", "1.008", "1.000", "1.96"});
            csvIntervalDataBlock.Add(new string[]{"300", "10180115", "1.000", "1.000", "1.000", "1.008", "1.000", "1.96"});
            csvIntervalDataBlock.Add(new string[]{"900"});

            int index = 4;

            // act
            var csvTestBlock = Gentrack_Assessment.CreateCSV.MakeCsv(index, csvIntervalDataBlock);         // Testing the MakeCsv method

            // assert
            Assert.IsTrue(csvTestBlock[0][1] == "NEM12");
            Assert.IsTrue(csvTestBlock[1][1] == "58765432109");
            Assert.IsTrue(csvTestBlock[csvTestBlock.Count-1][0] == "900");

        }
    }
}

