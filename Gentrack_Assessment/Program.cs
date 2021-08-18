using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using CsvHelper;

namespace GentrackAssessment
{
    class Program
    {
        public const string FILENAME = "/Users/lianatime/Desktop/testfile.xml";         // update file path
        static void Main(string[] args)
        {
            LoadXml(FILENAME);
        }

        static void LoadXml(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            string csvData = xmlDoc.SelectSingleNode("//CSVIntervalData").InnerText.Trim();
            var data = csvData.Split(new string[] {"\n"}, StringSplitOptions.None);
            var dataBlock = new List<string[]>();

            foreach(var line in data)
            {
                string[] entries = line.Split(",");
                dataBlock.Add(entries);
            }

            for(var index = 0; index < dataBlock.Count; index++)
            {
                if(dataBlock[index][0] == "200")
                {
                    MakeCsv(index, data, dataBlock);
                } 
            }
        }

        static void MakeCsv(int index, string[] data, List<string[]> dataBlock)
        {
            var csv200Row = data[index].Split(",");
            var name = csv200Row[1];
            var csvBlock = new List<string[]>();

            csvBlock.Add(dataBlock[0]);
            csvBlock.Add(dataBlock[index]);
            for(var i = index+1; i<data.Length; i++)
            {
                if(dataBlock[i][0] == "300")
                {
                    csvBlock.Add(dataBlock[i]);
                } else break;
            }
            csvBlock.Add(dataBlock[dataBlock.Count-1]);

            WriteCsv(csvBlock, name);
        }

        static void WriteCsv(List<string[]> csvBlock, string name)
        {
            var csvPath = Path.Combine(Environment.CurrentDirectory, $"{name}.csv");

            using (var streamWriter = new StreamWriter(csvPath))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    foreach(var field in csvBlock)
                    {
                        csvWriter.WriteField(field);
                        csvWriter.NextRecord();
                    }
                }
            }
        }
    }
}
