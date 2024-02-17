using MySpotify.DAL.Entities;


namespace MySpotify.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryMedia<Media> Medias { get; }
        IRepositoryUser<User> Users { get;  }
        IRepositoryGenres<Genre> Genres { get; }

        Task Save();
    
    } 
}
