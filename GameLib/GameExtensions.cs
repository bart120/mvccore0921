using GameLib.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public static class GameExtensions
    {
        public static void AddGameServices(this IServiceCollection services)
        {
            services.AddTransient<IPlatformService, PlatformService>();
            services.AddTransient<IGameService, GameService>();
        }

    }
}
