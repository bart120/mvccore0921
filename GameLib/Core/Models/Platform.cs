using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Core.Models
{
    public class Platform
    {
        public int ID { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }
}
