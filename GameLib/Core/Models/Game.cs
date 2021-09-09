using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib.Core.Models
{
    public class Game
    {
        
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} obligatoire")]
        [StringLength(50, ErrorMessage = "Maximun {1} caractères")]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
