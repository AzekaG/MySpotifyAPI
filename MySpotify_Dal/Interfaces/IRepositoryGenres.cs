
namespace MySpotify.DAL.Interfaces
{
    public interface IRepositoryGenres<Genre> where Genre : Entities.Genre
    {
        
        Task<IEnumerable<Genre>> GetGenresList();

        Task<Genre> Get(int id);
        Task<Genre> Get(string name);

        Task Create(Genre genre);
        void Update(Genre genre);

        Task Delete(int id);

        Task<bool> GenreExist(int id);
      
    }
}
