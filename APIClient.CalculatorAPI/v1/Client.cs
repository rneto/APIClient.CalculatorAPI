using APIClient.CalculatorAPI.Common;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;

namespace APIClient.CalculatorAPI.v1
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

        public T Send<T>(object request)
        {
            XElement operation = Serializer.ToXElement(request);
            XElement response = this.Send(operation);

            return Serializer.Deserialize<T>(response);
        }

        public XElement Send(XElement request)
        {
            string soapRequest = this.CreateSOAPRequest(request).ToString();
            string soapResponse = this.SOAPSend(soapRequest);

            return this.ReadSOAPResponse(soapResponse);
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

        private XElement CreateSOAPRequest(XElement request)
        {
            XNamespace soapEnv = XNamespace.Get("http://schemas.xmlsoap.org/soap/envelope/");

            // Empaquetar
            return new XElement(soapEnv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "SOAP-ENV", soapEnv),
                new XElement(soapEnv + "Body", request));
        }

        private string SOAPSend(string request)
        {
            var content = new StringContent(request, Encoding.UTF8, "text/xml");

            string endpoint = this.url;

            var responseMessage = this.httpClient.PostAsync(endpoint, content).Result;
            string response = responseMessage.Content.ReadAsStringAsync().Result;
            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                this.ThrowException(responseMessage.StatusCode, response);
            }

            return response;
        }

        private XElement ReadSOAPResponse(string s)
        {
            XElement element = XElement.Parse(s);

            XNamespace soap = XNamespace.Get("http://schemas.xmlsoap.org/soap/envelope/");

            XElement body = element.Element(soap + "Body");
            if (body == null && !body.HasElements)
            {
                return null;
            }

            return body.Elements().FirstOrDefault();
        }

        private void ThrowException(HttpStatusCode statusCode, string response)
        {
            throw new Exception($"Error {statusCode}: {response}");
        }
    }
}
