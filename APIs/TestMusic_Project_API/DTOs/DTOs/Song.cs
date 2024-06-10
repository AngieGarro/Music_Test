using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class Song : BaseDTO
    {

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The name of the artist of the song is required.")]
        public string ArtistName { get; set; }

        public string Album { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan Duration { get; set; }
    }
}
