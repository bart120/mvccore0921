using GameLib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Core.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetGamesAsync();

        Task<Game> GetGameByIdAsync(int id);

        Task<Game> CreateGameAsync(Game game);

        Task<Game> EditGameAsync(Game game);

        Task EditPlatformAsync(int[] platforms, int id);

        Task<IEnumerable<Platform>> GetPlatformsByGameIdAsync(int id);
    }
}
