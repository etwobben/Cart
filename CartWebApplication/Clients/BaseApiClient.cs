using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CartWebApplication.ApplicationSettings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CartWebApplication.Clients
{
    public abstract class BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _resourcePath;
        private readonly Uri _baseUri;

        protected BaseApiClient(IOptions<ApiSettingsModel> settings, string resourcePath)
        {
            _httpClient = new HttpClient();
            _baseUri = new Uri(settings.Value.BaseUrl);
            _resourcePath = resourcePath;
        }

        protected async Task<T> GetAsync<T>(Uri requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        protected async Task<TR> PostAsync<T, TR>(Uri requestUrl, T content)
        {
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TR>(data);
        }

        protected async Task<TR> PutAsync<T, TR>(Uri requestUrl, T content)
        {
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TR>(data);
        }


        protected async Task DeleteAsync(Uri requestUrl)
        {
            var response = await _httpClient.DeleteAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            await response.Content.ReadAsStringAsync();
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            return new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        }

        protected Uri CreateItemRequestUri(int id, string queryString = "")
        {
            return BuildUri(_resourcePath + "/" + id);
        }

        protected Uri CreateRequestUri()
        {
            return BuildUri(_resourcePath);
        }

        private Uri BuildUri(string path)
        {
            var endpoint = new Uri(_baseUri, path);
            var uriBuilder = new UriBuilder(endpoint);
            return uriBuilder.Uri;
        }
    }
}
