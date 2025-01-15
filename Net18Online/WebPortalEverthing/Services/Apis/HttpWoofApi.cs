using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace WebPortalEverthing.Services.Apis
{
    public class HttpWoofApi
    {
        private HttpClient _httpClient;

        public HttpWoofApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRandomDogImage()
        {
            var response = await _httpClient.GetAsync("woof.json");
            var dto = await response.Content.ReadFromJsonAsync<Dto>();
            if (dto == null)
            {
                throw new Exception("Api is broken");
            }

            return dto.Url;
        }

        private class Dto
        {
            public string Url { get; set; }
            public int FileSizeBytes { get; set; }
        }
    }
}