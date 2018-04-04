using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CognitionServiceTester
{
    class Program
    { 
        static void Main()
        {
            ComputerVisionAnalyze.Run();
            //OCR.Run();

            System.Console.Read();
        }
    } 
}
