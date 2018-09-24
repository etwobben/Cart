using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CartWebApplication.ApplicationSettings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CartWebApplication.Clients
{
    public abstract class BaseApiClient<T>
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

        protected async Task<List<T>> GetAsync(Uri requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(data);
        }

        protected async Task<T> PostAsync<TR>(Uri requestUrl, TR content)
        {
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        protected async Task<T> PutAsync<TR>(Uri requestUrl, TR content)
        {
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }


        protected async Task DeleteAsync(Uri requestUrl)
        {
            var response = await _httpClient.DeleteAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            await response.Content.ReadAsStringAsync();
        }

        private HttpContent CreateHttpContent<TR>(TR content)
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
