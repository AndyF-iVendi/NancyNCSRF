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

            SendSubmit1(token);

            Console.ReadKey();
        }

        private static string GetToken()
        {
            var options = new RestClientOptions("http://localhost:6000")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/token", Method.Get);
            RestResponse response = client.Execute(request);

            Console.WriteLine("Generated token 6000");
            return response.Content;
        }

        private static void SendSubmit(string tokens)
        {
            var options = new RestClientOptions("http://localhost:6000")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/submit", Method.Post);
            request.AddHeader("NCSRF", $"{tokens}");
            request.AddHeader("Cookie", $"NCSRF={HttpUtility.UrlEncode(tokens)}");
            request.AlwaysMultipartFormData = true;
            RestResponse response = client.Execute(request);

            Console.WriteLine($"StatusCode 6000: {response.StatusCode.ToString()}");
            Console.WriteLine($"StatusDescription 6000: {response.StatusDescription.ToString()}");
        }

        private static void SendSubmit1(string tokens)
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

            Console.WriteLine($"StatusCode 5000: {response.StatusCode.ToString()}");
            Console.WriteLine($"StatusDescription 5000: {response.StatusDescription.ToString()}");
        }
    }
}