
using MySpotify.DAL.Repositories;

namespace MySpotify.DAL.Interfaces
{
    public interface IRepositoryUser<User>
    {
        
        Task<IEnumerable<User>> GetUserList();
        Task<User> Get(int id);

        Task<User> Get(string email);
        Task Create(User user);

        void Update(User user);
     
        Task Delete(int id);
        
        Task<bool> UserExist(int id);

        

    }
}
