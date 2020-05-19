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
            string url = "https://api-v2.ermeo.com/oauth/v2/token";
            AccessObject accessObject = ServiceErmeo.PostToken(url, token);

            if (string.IsNullOrEmpty(accessObject.exceptionMessage))
            {
                Console.WriteLine("Authentification ok - access_token: " + accessObject.access_token);
            }
            else
            {
                Console.WriteLine("Error: " + accessObject.exceptionMessage);
            }

            WriteTitleConsole("Get : Right");
            url = "https://api-v2.ermeo.com/api/v1/access_rights?limit=10&page=1&sort=created_at:desc";
            var response = ServiceErmeo.GetApi(url, accessObject);
            Console.WriteLine("response.Content: ");
            Console.WriteLine(response.Content);


            WriteTitleConsole("Get : Asset");
            url = "https://api-v2.ermeo.com/api/v1/assets?limit=10&page=1&sort=created_at:desc";
            response = ServiceErmeo.GetApi(url, accessObject);
            Console.WriteLine("response.Content: ");
            Console.WriteLine(response.Content);


            WriteTitleConsole("Get : Reports");
            url = "https://api-v2.ermeo.com/api/v1/reports?limit=10&page=1&sort=created_at:desc";
            response = ServiceErmeo.GetApi(url, accessObject);
            Console.WriteLine("response.Content: ");
            Console.WriteLine(response.Content);


            WriteTitleConsole("Get : Dashboards");
            url = "https://api-v2.ermeo.com/api/v1/dashboards?limit=10";
            response = ServiceErmeo.GetApi(url, accessObject);
            Console.WriteLine("response.Content: ");
            Console.WriteLine(response.Content);


            // Generate File
            WriteTitleConsole("Generate file token.csv");
            File.WriteAllText("token.csv", ServiceErmeo.CSVFormat(token));
            var path = System.Reflection.Assembly.GetEntryAssembly().Location.Substring(0, System.Reflection.Assembly.GetEntryAssembly().Location.LastIndexOf('\\') + 1);
            Console.WriteLine("File generate in " + path);

            Console.ReadKey();
        }

        private static void WriteTitleConsole(string title)
        {
            Console.WriteLine("");
            Console.WriteLine("#################");
            Console.WriteLine("#" + title);
            Console.WriteLine("#################");
        }
    }
}
