using System;

namespace Gentrack_Assessment
{
    class Program
    {
        public const string FILENAME = "/Users/lianatime/Desktop/testfile.xml";         // update file path

        static void Main(string[] args)
        {
            CreateCSV.LoadXml(FILENAME);
        }
    }
}
