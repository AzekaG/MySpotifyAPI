using MySpotify.DAL.EF;
using MySpotify.DAL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotify.Repository;



namespace MySpotify.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        MediaUserContext mediaUserContext;
        UserRepository userRepository;
        MediaRepository mediaRepository;
        GenreRepository genreRepository;

        public EFUnitOfWork(MediaUserContext context)
        {
            mediaUserContext = context;
        }

        public IRepositoryUser<User> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(mediaUserContext);
                }
                return userRepository;
            }
        }
        public IRepositoryGenres<Genre> Genres
        {
            get
            {
                if(genreRepository == null)
                {
                    genreRepository = new GenreRepository(mediaUserContext);
                }
                return genreRepository;
            }
        }


        public IRepositoryMedia<Media> Medias
        {
            get
            {
                if(mediaRepository == null)
                {
                    mediaRepository = new MediaRepository(mediaUserContext);
                }
                return mediaRepository;
            }
        }


        public async Task Save()
        {
            await mediaUserContext.SaveChangesAsync();
        }


    }
}
