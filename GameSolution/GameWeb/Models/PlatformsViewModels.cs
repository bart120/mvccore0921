using GameLib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Models
{
    public class PlatformsIndexViewModel
    {
        public Platform Platform { get; set; }

        public IEnumerable<Game> Games { get; set; }
    }
}
