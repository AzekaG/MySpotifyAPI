
using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace MySpotifyAPI.Models
{
    public class UsersModel
    {
        public int? Id { get; set; }
       

        public string? FirstName { get; set; }
        

        public string? LastName { get; set; }

      
        public string? Email { get; set; }

     
        public bool? Active { get; set; }

   
        public string? Password { get; set; }

 
        public string? Status { get; set; }
       
     
        public List<string>? Statuses { get; set; }

    }
}
