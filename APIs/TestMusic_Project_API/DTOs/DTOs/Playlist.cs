using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class Playlist : BaseDTO
    {

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public List<Song> Songs { get; set; } = new List<Song>();

    }
}