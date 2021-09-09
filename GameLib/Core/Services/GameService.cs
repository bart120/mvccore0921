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
    public class GameService : IGameService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteUrl = "https://localhost:44330/api/games";

        public GameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            var content = new StringContent(JsonConvert.SerializeObject(game), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_remoteUrl, content);
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task<Game> EditGameAsync(Game game)
        {
            var content = new StringContent(JsonConvert.SerializeObject(game), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_remoteUrl}/{game.ID}", content);
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<Game>(await response.Content.ReadAsStringAsync());
            return result;
        }

        public async Task EditPlatformAsync(int[] platforms, int id)
        {
            var content = new StringContent(JsonConvert.SerializeObject(platforms), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_remoteUrl}/{id}/editplatform", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            var responseString = await _httpClient.GetStringAsync($"{_remoteUrl}/{id}");
            var result = JsonConvert.DeserializeObject<Game>(responseString);

            return result;
        }

        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            var responseString = await _httpClient.GetStringAsync(_remoteUrl);
            var result = JsonConvert.DeserializeObject<List<Game>>(responseString);

            return result;
        }

        public async Task<IEnumerable<Platform>> GetPlatformsByGameIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_remoteUrl}/{id}/platforms");
            //response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<Platform>>(await response.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }
    }
}
