using GameLib.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Core.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteUrl = "https://localhost:44330/api/platforms";

        public PlatformService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Game>> GetGameByPlatformsAsync(int id)
        {
            var responseString = await _httpClient.GetStringAsync($"{_remoteUrl}/{id}/games");
            var result = JsonConvert.DeserializeObject<List<Game>>(responseString);

            return result;
        }

        public async Task<Platform> GetPlatformByIdAsync(int id)
        {
            var responseString = await _httpClient.GetStringAsync($"{_remoteUrl}/{id}");
            var result = JsonConvert.DeserializeObject<Platform>(responseString);

            return result;
        }

        public async Task<IEnumerable<Platform>> GetPlatformsAsync()
        {
            /*var response = await _httpClient.GetAsync(_remoteUrl);
            var responseString = await response.Content.ReadAsStringAsync();*/

            var responseString = await _httpClient.GetStringAsync(_remoteUrl);
            var result = JsonConvert.DeserializeObject<List<Platform>>(responseString);

            return result;
        }
    }
}
