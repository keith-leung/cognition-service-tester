using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;

namespace CognitionServiceTester
{
    class OCR
    {
        const string subscriptionKey = "3c72f0a6a8104e3286c2c779fe16e135";

        public static void Run()
        {
            MakeRequest();
        }

        static async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters
            queryString["language"] = "unk";
            queryString["detectOrientation "] = "true";
            var uri = "https://api.cognitive.azure.cn/vision/v1.0/ocr?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = ComputerVisionAnalyze.GetImageAsByteArray(
                @"C:\Users\liangdw\Desktop\微信截图_20171026164146.png");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                
                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();

                // Display the JSON response.
                Console.WriteLine("\nResponse:\n");
                Console.WriteLine(
                    ComputerVisionAnalyze.JsonPrettyPrint(contentString));
            } 
        }
    }
}
