using System.ComponentModel.DataAnnotations;

namespace MySpotify.DAL.Entities
{



public class Media
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }
        
        public string? Artist {  get; set; }
       
        public string? FileAdress { get; set; }
        
        public string? Poster { get; set; }

        
        public Genre? Genre { get; set; }
        

        public User User { get; set; }
        public Media() { }
    }
}
