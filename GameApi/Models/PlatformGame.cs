using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameApi.Models
{
    public class PlatformGame
    {
     
        public int PlatformID { get; set; }
        [ForeignKey("PlatformID")]
        public Platform Platform { get; set; }


     
        public int GameID { get; set; }
        [ForeignKey("GameID")]
        public Game Game { get; set; }

    }
}
