using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameApi.Models
{
    public class Platform
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        //public ICollection<PlatformGame> PlatformGames { get; set; }
    }
}
