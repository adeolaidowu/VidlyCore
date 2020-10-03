using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VidlyCore.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }
        public DateTime DateAdded { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public byte NumberInStock { get; set; }
    }
}
