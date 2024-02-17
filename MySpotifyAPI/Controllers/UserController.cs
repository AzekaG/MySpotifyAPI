using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotifyAPI.Models;

namespace MySpotifyAPI.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        readonly IMediaService _mediaService;
        readonly IGenreService _genreService;
        readonly IUserService _userService;
        public UserController(IMediaService mediaService, IGenreService genreService, IUserService userService)
        {
            _mediaService = mediaService;
            _genreService = genreService;
            _userService = userService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserModel>().ForMember("UserStatus" , opt=>opt.MapFrom(c=>c.Status.Value.ToString())));
            var mapper = new Mapper(config);
            var el = mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserModel>>((await _userService.GetUserList())).ToList();
           
            return el;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
        
            var user = await _userService.GetUser(id);

            UserModel usModel = new UserModel()
            {
                Active = user.Active,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Id = user.Id,
                UserStatus = user.Status.ToString(),
                Password = user.Password,
                Salt = user.Salt

            };
            if (usModel == null)
            {
                return NotFound();
            }
            return new ObjectResult(usModel);
        }


        [HttpPost]

        public async Task<ActionResult<UserModel>> PostStudent(UserModel user)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var us = new UserDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Status = Status.user,
                Active = false,
                Password = user.Password,
                Id = 0
            };



            await _userService.CreateUser(us);

            return Ok(us);
        }



        [HttpPut]
        public async Task<ActionResult<UserDTO>> PutUser(UserModel user)
        {

            UserDTO us = new UserDTO();
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (us == null)
            {
                return NotFound();
            }
            us.Id = user.Id;
            us.FirstName = user.FirstName;
            us.LastName = user.LastName;
            us.Status = (Status)(Enum.Parse(typeof(Status), user.UserStatus));
            us.Active = user.Active;
            us.Salt = user.Salt;
            us.Email = user.Email;
            us.Password = user.Password;

            try { await _userService.UpdateUser(us); }
            catch (Exception ex) {

               string exe =  ex.Message;
            }
           
            return Ok(user);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
           
            await _userService.DeleteUser(id);

            return Ok();
        }











    }
}
