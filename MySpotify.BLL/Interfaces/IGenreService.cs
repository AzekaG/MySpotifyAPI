using MySpotify.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpotify.BLL.Interfaces
{
    public interface IGenreService
    {
        Task CreateGenre(GenreDTO mediaDto);

        Task UpdateGenre(GenreDTO genreDto);
        Task DeleteGenre(int id);
        Task<GenreDTO> Get(int id);

        Task<GenreDTO> Get(string name);
        Task<IEnumerable<GenreDTO>> GetGenres();
        Task<bool> GenreExist(int id);

    }
}
