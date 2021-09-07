using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameApi.Models
{
    public class Game
    {
        //[Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Champ obligatoire")]
        [StringLength(50, ErrorMessage = "Maximun {1} caractères")]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public ICollection<PlatformGame> PlatformGames { get; set; }


    }
}
