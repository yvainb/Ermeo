using RestSharp;
using System;
using System.IO;

namespace WorkOrderExtractorErmeo
{
    class Program
    {
        static void Main(string[] args)
        {
            var token = new Token()
            {
                grant_type = "",
                client_id = "",
                client_secret = "",
                username = "",
                password = ""
            };
            
            // CALL API
            IRestResponse response = ServiceErmeo.PostToken("https://api-v2.ermeo.com/oauth/v2/token", token);
            Console.WriteLine("response.Content: ");
            Console.WriteLine(response.Content);

            // Generate File
            Console.WriteLine("Generate file token.csv");
            File.WriteAllText("token.csv", ServiceErmeo.CSVFormat(token));
            var path = System.Reflection.Assembly.GetEntryAssembly().Location.Substring(0, System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf('\\') + 1);
            Console.WriteLine("File generate in " + path);

            Console.ReadKey();
        }

       
    }
}
