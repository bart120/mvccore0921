using GameLib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Core.Services
{
    public interface IPlatformService
    {

        Task<IEnumerable<Platform>> GetPlatformsAsync();

        Task<Platform> GetPlatformByIdAsync(int id);

        Task<IEnumerable<Game>> GetGameByPlatformsAsync(int id);
    }
}
