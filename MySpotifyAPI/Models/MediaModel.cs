using MySpotify.BLL.DTO;
using System.ComponentModel.DataAnnotations;

namespace MySpotifyAPI.Models
{
    public class MediaModel
    {
        public int? Id { get; set; }
       
        public string? Name { get; set; }

        public string? Artist { get; set; }

        public string? FileAdress { get; set; }

        public string? Poster { get; set; }

        public string? Genre { get; set; }

        public int? UserId { get; set; }

        public string? rootPath { get; set; }

        public IFormFile? mediaFile { get; set; }

        public IFormFile? posterFile { get; set; }






    }
}
