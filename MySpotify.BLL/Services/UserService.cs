using AutoMapper;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotify.DAL.Interfaces;
using Status = MySpotify.DAL.Entities.Status;

namespace MySpotify.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork unitOfWork) => Database = unitOfWork;


        public async Task<IEnumerable<UserDTO>> GetUserList()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetUserList());
        }
        public async Task<UserDTO> GetUser(int id)
        {
            var user = await Database.Users.Get(id);
            return new UserDTO
            {
                Active = user.Active,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.id,
                LastName = user.LastName,
                Status = user.Status,
                Password = user.Password,
                Salt = user.Salt
            };
        }

        public async Task<UserDTO> GetUser(string email)
        {
            var user = await Database.Users.Get(email);
            return new UserDTO
            {
                Active = user.Active,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.id,
                LastName = user.LastName,
                Status = user.Status,
                Password = user.Password,
                Salt = user.Salt
            };
        }
        public async Task CreateUser(UserDTO user)  //вернуться к етому методу , сделать генерацию Соли и пассворда
        {

           var pass =  PasswordService.CreatePass(user.Password);

            User us = new User
            {
                Active = false,
                Email = user.Email,
                FirstName = user.FirstName,
                id = user.Id,
                LastName = user.LastName,
                Status = (Status)user.Status,
                Password = pass["Password"],
                Salt = pass["Salt"]
            };
           await Database.Users.Create(us);
           await Database.Save();
        }

        public async Task UpdateUser(UserDTO user)  //обновить обработку соли
        {

            User us = new User
            {
                Active = user.Active,
                Email = user.Email,
                FirstName = user.FirstName,
                id = user.Id,
                LastName = user.LastName,
                Status = (Status)user.Status,
                Password = user.Password,
                Salt = user.Salt
            };
            Database.Users.Update(us);
            await Database.Save();
        }

        public async Task DeleteUser(int id)
        {
            await Database.Users.Delete(id);
            await Database.Save();
        }

        public async Task<bool> UserExist(int id) => await Database.Users.UserExist(id);

        public async Task<bool> isLogged(UserDTO user)
        {
           var us = await Database.Users.Get(user.Id);
           if (us == null)
                return false;

           var res =  PasswordService.isLogged(user.Password, us);
           return res;
        }

      
    }
}
