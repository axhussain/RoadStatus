using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatus
{
    public class ApiClient : IApiClient
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly AppSettings _appSettings;

        public ApiClient(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<List<RoadCorridor>> GetRoadStatus(string roadId)
        {
            var uri = new StringBuilder();
            uri.Append(_appSettings.Url)
               .Append(roadId)
               .Append($"?app_id={_appSettings.AppId}&app_key={_appSettings.AppKey}");

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _client.GetAsync(uri.ToString(), HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var serializer = new DataContractJsonSerializer(typeof(List<RoadCorridor>));
                var stream = await response.Content.ReadAsStreamAsync();
                var roadCorridors = serializer.ReadObject(stream) as List<RoadCorridor>;
                return roadCorridors;
            }
        }

    }
}
