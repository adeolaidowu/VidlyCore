using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VidlyCore.Models
{
    public class Genre
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
