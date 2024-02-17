using MySpotify.BLL.DTO;
using MySpotify.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpotify.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUserList();
        Task<UserDTO> GetUser(int id);

        Task<UserDTO> GetUser(string email);
        Task CreateUser(UserDTO user);

        Task UpdateUser(UserDTO user);

        Task DeleteUser(int id);

        Task<bool> UserExist(int id);

        Task<bool> isLogged(UserDTO user);

    }
}
