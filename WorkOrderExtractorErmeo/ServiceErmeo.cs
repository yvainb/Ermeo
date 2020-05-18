using RestSharp;
using System.Text;
using System.Text.Json;
using System.IO;

namespace WorkOrderExtractorErmeo
{
    public class ServiceErmeo
    {

        public static IRestResponse PostToken(string url, Token token)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonSerializer.Serialize(token), ParameterType.RequestBody);
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
