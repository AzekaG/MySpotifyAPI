using MySpotify.DAL.Entities;
using System.ComponentModel.DataAnnotations;






namespace MySpotify.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
      
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]

        public string? LastName { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public string? Email { get; set; }  

        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public string? Password { get; set; }

        public string? Salt { get; set; }


        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public Status? Status { get; set; } 

       
    }
}
