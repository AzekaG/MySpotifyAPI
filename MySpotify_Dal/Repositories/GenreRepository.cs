using Microsoft.EntityFrameworkCore;
using MySpotify.DAL.Interfaces;
using MySpotify.DAL.EF;
using MySpotify.DAL.Entities;
using MySpotify.Repository;


namespace MySpotify.DAL.Repositories
{
    internal class GenreRepository : IRepositoryGenres<Genre>
    {
        MediaUserContext mediaUserContext;
        public GenreRepository(MediaUserContext context) => mediaUserContext = context;

        public async Task<IEnumerable<Genre>> GetGenresList() => await mediaUserContext.Genres.ToListAsync();

        public async Task<Genre> Get(int id)
        {
            var genrColl = await mediaUserContext.Genres.Where(x=> x.Id == id).ToListAsync();
            Genre? genre = genrColl.FirstOrDefault();
            return genre;
        }
        public async Task<Genre> Get(string name)
        {
            var genrColl = await mediaUserContext.Genres.Where(x => x.Name == name).ToListAsync();
            Genre? genre = genrColl.FirstOrDefault();
            return genre;
        }

        public async Task Create(Genre genre) => await mediaUserContext.AddAsync(genre);

        public void Update(Genre genre)
        {
            mediaUserContext.Entry(genre).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
          Genre? gen = await mediaUserContext.Genres.FindAsync(id);
            if(gen !=null) 
            {
                mediaUserContext.Remove(gen);
            }
        }

        public async Task<bool> GenreExist(int id) => await mediaUserContext.Genres.AnyAsync(x => x.Id == id);
    }
}
