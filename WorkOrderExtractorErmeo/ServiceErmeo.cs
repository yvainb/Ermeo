using RestSharp;
using System.Text;
using System.Text.Json;
using System.IO;
using System;

namespace WorkOrderExtractorErmeo
{
    public class ServiceErmeo
    {

        public static AccessObject PostToken(string url, Token token)
        {
            var accessObject = new AccessObject();

            try
            {
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonSerializer.Serialize(token), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    accessObject = JsonSerializer.Deserialize<AccessObject>(response.Content);
                }
                else
                {
                    accessObject.exceptionMessage = "Authentication fail :" + response.Content;
                }
            }
            catch (Exception ex)
            {
                accessObject.exceptionMessage = ex.ToString();
            }

            return accessObject;
        }

        public static IRestResponse GetApi(string url, AccessObject accessObject)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + accessObject.access_token);


            IRestResponse response = client.Execute(request);

            return response;
        }

        public static string CSVFormat(Token token)
        {
            var builder = new StringBuilder();
            builder.AppendLine("grant_type;client_id;client_secret;username;password");
            builder.AppendLine($"{token.grant_type};{token.client_id};{token.client_secret};{token.username};{token.password}");
            return builder.ToString();
        }
    }
}
