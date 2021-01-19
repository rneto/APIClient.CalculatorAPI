using APIClient.CalculatorAPI.Common;
using APIClient.CalculatorAPI.v2.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace APIClient.CalculatorAPI.v2
{
    public class Client : IDisposable
    {
        private string url;

        private HttpClient httpClient;

        private HttpClientHandler httpClientHandler;

        public Client(string url)
        {
            this.Initilize(url);
        }

        public Client(string url, int timeout)
        {
            this.Initilize(url, timeout);
        }

        public void Dispose()
        {
            this.httpClientHandler?.Dispose();
            this.httpClient?.Dispose();
        }

        public OperationResult Send(OperationRequest operationRequest)
        {
            return this.Send(operationRequest.Parse());
        }

        public OperationResult Send(string operation)
        {
            string endpoint = $"{this.url}?expr={HttpUtility.UrlEncode(operation)}";

            var responseMessage = this.httpClient.GetAsync(endpoint).Result;
            string response = responseMessage.Content.ReadAsStringAsync().Result;
            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                this.ThrowException(responseMessage.StatusCode, response);
            }

            return new OperationResult(int.Parse(response));
        }

        private void Initilize(string url, int timeout = 90)
        {
            this.url = url;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            this.httpClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            this.httpClient = new HttpClient(this.httpClientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
        }

        private void ThrowException(HttpStatusCode statusCode, string response)
        {
            throw new Exception($"Error {statusCode}: {response}");
        }
    }
}
