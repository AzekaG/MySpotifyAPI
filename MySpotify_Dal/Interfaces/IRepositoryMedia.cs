
namespace MySpotify.DAL.Interfaces
{
    public interface IRepositoryMedia<Media> where Media : Entities.Media
    {
        Task<IEnumerable<Media>> GetMediaList();

        Task<Media> Get(int id);
        Task<Media> Get(string name);
        Task Create(Media media);
        Task Delete(int id);
        void UpdateMedia(Media media);

      
    }
}
