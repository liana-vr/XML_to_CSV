# **XML to CSV**
## **My Solution Contains The Following:**
### A CreateCSV class containig four functions.

        1. LoadXml - This function takes the file path for the XML file as a string input and loads it using xmlDoc.
                     It isolates the CSVIntervalData element with xmlDoc.SelectSingleNode("//CSVIntervalData").InnerText.Trim()
                     And returns it as a string. It then calls the ReadCSVInterval function. 

        2. ReadCSVInterval - This function splits the string from the XML into an array of strings based on every new line.
                             It then takes each line and splits in into another array of strings based on the commas.
                             Once a line has been split, it is added to a List of type string array.
                             Then it traverses that list to find the lines starting with "200", in order to create the CSV based on that entry.
                             If it finds a line starting with "200", it will call the MakeCsv function.
        
        3. MakeCsv - This function determines the name of the CSV file to be created based on the "200" entry found in the previous function.
                     It creates a new List of type string array and adds the "100" line to the beginning of the list,
                     followed by the "200" and all the "300" entries if there are any, and then adds the "900" to the end. 
                     It then calls the WriteCsv function and passes this list as well the name found. 
        
        4. WriteCsv - This function uses the List created in the MakeCsv function and writes the CSV file using CsvWriter. 
                      The CSV file will be written to the current Gentrack_Assessment/Gentrack_Assessment folder.

### UnitTests class containing tests for the functions above.

        1. CSVIntervalData_Is_Found_In_XML_File
        2. CSVIntervalData_Is_Turned_Into_A_List_Of_String_Arrays
        3. DataBlock_Is_Shaped_Into_Correct_CsvBlock

### If I had more time I would add more tests

## **Instructions on building and running the code:**

        1. You have to install CsvHelper by running 'dotnet add package CsvHelper' in the .NET CLI console.
        2. Make sure to update the file path for the FILENAME const in the Program class as well as the UnitTests class.
        3. Make sure you have NUnit installed using NuGet Package Manager.
        4. To build, make sure you are in the top level /Gentrack_Assessment and run 'dotnet build'.
        5. To run the code and create the CSV files, cd down into /Gentrack_Assessment/Gentrack_Assessment and run 'dotnet run'.
        6. To run the tests, make sure you are at the top level /Gentrack_Assessment and run 
            'dotnet test Gentrack_Assessment_Tests/Gentrack_Assessment_Tests.csproj'.  
