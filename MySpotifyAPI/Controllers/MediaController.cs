using Microsoft.AspNetCore.Mvc;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;
using MySpotifyAPI.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace MySpotifyAPI.Controllers
{
    [ApiController]
    [Route("api/Media")]
    public class MediaController : ControllerBase
    {
        readonly IMediaService _mediaService;
        readonly IWebHostEnvironment _webRoothPAth;
        

        //добавить загрузку медиа ! Посмотреть последний урок , в самом конце. 
        //Начать делать визуал.

        public MediaController(IMediaService mediaService, IWebHostEnvironment hostingEnvironment)
        {
            _mediaService = mediaService;
            _webRoothPAth = hostingEnvironment;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<MediaDTO>>> GetMedias()
        {

            return (await _mediaService.GetMediaList()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MediaDTO>> GetMedia(int id)
        {
            var media = await _mediaService.GetMedia(id);
            if (media == null)
            {
                return NotFound();
            }
            return new ObjectResult(media);
        }

        [HttpPut]

        public async Task<ActionResult<MediaDTO>> PutMedia(MediaModel media)
        {
            if (media.Id == 0 || media == null)
            {
                return new ObjectResult(media);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            MediaDTO mediaDTO = new MediaDTO();
            mediaDTO.Artist = media.Artist;
            mediaDTO.Name = media.Name;
            mediaDTO.Genre = media.Genre;
            mediaDTO.UserId = (int)media.UserId;
            mediaDTO.Id = (int)media.Id;
            if (media.mediaFile != null)
            {
                mediaDTO.FileAdress = media.mediaFile.FileName;
            }
            else mediaDTO.FileAdress = media.FileAdress;
            if (media.posterFile != null)
            {
                mediaDTO.Poster = media.posterFile.FileName;
            }
            else mediaDTO.Poster = media.Poster;

            mediaDTO.rootPath = _webRoothPAth.WebRootPath;


            await _mediaService.UpdateMedia(mediaDTO, media.posterFile, media.mediaFile);
            return Ok(media);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MediaDTO>> DeleteMedia(int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            var media = await _mediaService.GetMedia(id);
            await _mediaService.DeleteMedia(id);

            return Ok(media);
        }

        /*MediaDTO mediaDto , IFormFile upPoster , IFormFile upMedia)*/

        [HttpPost]
        public  async Task<ActionResult<MediaDTO>> Create([FromForm]MediaModel media )
        {



            if (HttpContext.Session.GetString("Id") != null)
            {
                media.UserId = int.Parse(HttpContext.Session.GetString("Id"));
            }
            else media.UserId = 1;
            MediaDTO mediaDTO = new MediaDTO();


            mediaDTO.Artist = media.Artist;
            mediaDTO.Name = media.Name;
            mediaDTO.Genre = media.Genre;
            mediaDTO.UserId = (int)media.UserId;
            mediaDTO.FileAdress = media.mediaFile.FileName;
            mediaDTO.Poster = media.posterFile.FileName;
            mediaDTO.rootPath = _webRoothPAth.WebRootPath;



            if (ModelState.IsValid)
            {

                await _mediaService.CreateMedia(mediaDTO , media.posterFile , media.mediaFile);

                return Ok(mediaDTO);

            }
            return Ok(media);
        }





    }
}
