using RestSharp;
using System.Web;

namespace BrowserSim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting BrowserSim");
            var token = GetToken();
            Console.WriteLine($"Token Returned: {token}");
            SendSubmit(token);

            Console.ReadKey();
        }

        private static string GetToken()
        {
            var options = new RestClientOptions("http://localhost:5000")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/token", Method.Get);
            RestResponse response = client.Execute(request);
            return response.Content;
        }

        private static void SendSubmit(string tokens)
        {
            var options = new RestClientOptions("http://localhost:5000")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/submit", Method.Post);
            request.AddHeader("NCSRF", $"{tokens}");
            request.AddHeader("Cookie", $"NCSRF={HttpUtility.UrlEncode(tokens)}");
            request.AlwaysMultipartFormData = true;
            RestResponse response = client.Execute(request);

            Console.WriteLine($"StatusCode: {response.StatusCode.ToString()}");
            Console.WriteLine($"StatusDescription: {response.StatusDescription.ToString()}");
        }
    }
}