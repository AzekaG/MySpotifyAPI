using MySpotify.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpotify.BLL.DTO
{
    public class MediaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public string? Artist { get; set; }
        
        public string? FileAdress {get; set; }

        public string? Poster { get; set; }

        public string? Genre { get; set; }
       
       
        public int UserId { get; set; }

        public string? rootPath { get; set; }
    }
}
