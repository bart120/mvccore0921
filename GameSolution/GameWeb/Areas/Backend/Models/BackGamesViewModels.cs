using GameLib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Areas.Backend.Models
{
    public class BackGamesCreateViewModel : Game
    {
        public int[] Platforms { get; set; }
    }

    public class BackGamesEditViewModel : Game
    {
        public int[] Platforms { get; set; }

        public static BackGamesEditViewModel Convert(Game game)
        {
            return new BackGamesEditViewModel
            {
                Description = game.Description,
                ID = game.ID,
                Name = game.Name
            };
        }
    }
}
