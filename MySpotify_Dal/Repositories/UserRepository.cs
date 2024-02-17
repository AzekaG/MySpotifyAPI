using Microsoft.EntityFrameworkCore;
using MySpotify.DAL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotify.DAL.EF;

namespace MySpotify.Repository
{
    public class UserRepository : IRepositoryUser<User>
    {
        readonly MediaUserContext _mediauserContext;
        public UserRepository(MediaUserContext mediaUserContext) => _mediauserContext = mediaUserContext;

        public async Task<IEnumerable<User>> GetUserList() => await _mediauserContext.Users.Include(x=>x.MediaFiles).ToListAsync();

        public async Task<User> Get(int id)
        {
            var UsCollection = await _mediauserContext.Users.Where(x=>x.id == id).ToListAsync();
            User? user = UsCollection.FirstOrDefault();
            return user;
        }
        public async Task<User> Get(string email)
        {
            var UsCollection = await _mediauserContext.Users.Where(x => x.Email == email).ToListAsync();
            User? user = UsCollection.FirstOrDefault();
            return user;
        }


        public async Task Create(User user) => await _mediauserContext.AddAsync(user);

        public void Update(User user) => _mediauserContext.Entry(user).State = EntityState.Modified;

        public async Task Delete(int id)
        {
            var user = await _mediauserContext.Users.FindAsync(id);
            if (user != null)
                _mediauserContext.Users.Remove(user);
        }

        public async Task<bool> UserExist(int id) => await _mediauserContext.Users.AnyAsync(x=>x.id == id);


    


    }
}
