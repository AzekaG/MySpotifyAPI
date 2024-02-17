
using System.ComponentModel.DataAnnotations;


namespace MySpotify.BLL.DTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено.")]
        public string? Name { get; set; }

    }
}
