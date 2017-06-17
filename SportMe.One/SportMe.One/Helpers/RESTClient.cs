using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace SportMe.One.Helpers
{
    public class RestClient
    {
        public enum HTTPMethod { GET, POST, PUT, DELETE }

        private HttpClient httpClient;
        private string uri;

        public RestClient(string uri, string user, string pass)
        {
            var authData = string.Format("{0}:{1}", user, pass);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }


        public RestClient()
        {
            //this.uri = uri;
            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
        }

        public RestClient(string uri)
        {
            this.uri = uri;
            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
        }


        public Task<HttpResponseMessage> Request(string uri, HTTPMethod httpMethod)
        {
            return Request(uri, httpMethod, null);
        }

        public async Task<HttpResponseMessage> Request(string uri, HTTPMethod httpMethod, object entity)
        {

            HttpResponseMessage response = null;

            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            switch (httpMethod)
            {
                case HTTPMethod.GET:
                    response = await httpClient.GetAsync(uri);
                    break;
                case HTTPMethod.POST:
                    response = await httpClient.PostAsync(uri, content);
                    break;
                case HTTPMethod.PUT:
                    response = await httpClient.PutAsync(uri, content);
                    break;
                case HTTPMethod.DELETE:
                    response = await httpClient.DeleteAsync(uri);
                    break;
                default:
                    return new HttpResponseMessage();
            }
            return response;
        }
    }

}
