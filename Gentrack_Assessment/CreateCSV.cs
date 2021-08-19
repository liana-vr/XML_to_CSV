using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using CsvHelper;

namespace Gentrack_Assessment
{
    public class CreateCSV
    {
        //////////////////////////////// READ XML DOCUMENT /////////////////////////////

        public static string LoadXml(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();                             
            xmlDoc.Load(fileName);                                                              // Load XML File

            string csvData = xmlDoc.SelectSingleNode("//CSVIntervalData").InnerText.Trim();     // Isolate CSVIntervalData Inner text
            ReadCSVInterval(csvData);                                                           // Call ReadCSVInterval method
            return csvData;
        }

        ////////////////////////////// READ CSVINTERVAL DATA ///////////////////////////

        public static List<string[]> ReadCSVInterval(string csvData)
        {
            var data = csvData.Split(new string[] {"\n"}, StringSplitOptions.None);             // CSVInterval string is turned into an array of strings by splitting every line
            var dataBlock = new List<string[]>();                                               // Create empty list of string arrays called dataBlock
            foreach(var line in data)                                                           
            {
                string[] entries = line.Split(",");                                             // Each line is turned into an array of strings by splitting every comma
                dataBlock.Add(entries);                                                         // This string array is then added to the datablock List 
            }

            for(var index = 0; index < dataBlock.Count; index++)                                // Traverse through the dataBlock list to see which entries start with "200"
            {
                if(dataBlock[index][0] == "200")                                                
                {
                    MakeCsv(index, dataBlock);                                                  // if an entry starts with "200" call the MakeCsv method
                } 
            }
            return dataBlock;                                                                   // return the dataBlock list
        }

        /////////////////////////////////// MAKE CSV /////////////////////////////////

        public static List<string[]> MakeCsv(int index, List<string[]> dataBlock)
        {                                          
            var name = dataBlock[index][1];                                                     // Isolate the value to be used as csv file name
            var csvBlock = new List<string[]>();                                                // Create another empty list of string arrays called csvBlock

            csvBlock.Add(dataBlock[0]);                                                         // Add the line that starts with "100" to the start of the csvBlock list
            csvBlock.Add(dataBlock[index]);                                                     // Add the line that starts with "200" to the csvBlock list
            for(var i = index+1; i<dataBlock.Count; i++)                                        // Traverse throught the datablock list 
            {
                if(dataBlock[i][0] == "300")                                                    // If a line starts with "300" add it to the csvBlock list
                {
                    csvBlock.Add(dataBlock[i]);
                } else break;                                                                   // Otherwise break
            }
            csvBlock.Add(dataBlock[dataBlock.Count-1]);                                         // Add the line that starts with "900" to the end of the list 

            WriteCsv(csvBlock, name);                                                           // Call WriteCsv method
            return csvBlock;
        }

        /////////////////////////////////// WRITE CSV /////////////////////////////////

        public static void WriteCsv(List<string[]> csvBlock, string name)                       
        {
            var csvPath = Path.Combine(Environment.CurrentDirectory, $"{name}.csv");            // Csv files will be written to this folder

            using (var streamWriter = new StreamWriter(csvPath))
            {
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    foreach(var field in csvBlock)                                              // for each item in the csvBlock list, write item as a csv field
                    {
                        csvWriter.WriteField(field);
                        csvWriter.NextRecord();
                    }
                }
            }
        }
    }
}