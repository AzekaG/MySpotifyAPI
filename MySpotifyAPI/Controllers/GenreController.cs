using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Interfaces;
using MySpotify.BLL.Services;
using MySpotify.DAL.Entities;
using MySpotifyAPI.Models;

namespace MySpotifyAPI.Controllers
{
    [ApiController]
    [Route("api/Genre")]
    //возможно сейчас нужно будет дополнительные модели
    public class GenreController : ControllerBase
    {
        
        readonly IGenreService _genreService;
        readonly IMediaService _mediaService;
        public GenreController(IGenreService genreService, IMediaService mediaService)
        {
            _genreService = genreService;
            _mediaService = mediaService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()
        {
            return (await _genreService.GetGenres()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> GetGenre(int id)
        {
            var genre = await _genreService.Get(id);
            if (genre == null)
            {
                return NotFound();
            }
            return new ObjectResult(genre);
        }

        [HttpPost]
        public async Task<ActionResult<GenreDTO>> PostGenre(GenreDTO genre)
        {

            genre.Id = 0;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
   
            await _genreService.CreateGenre(genre);

            return Ok(genre);
        }



        [HttpPut]
        public async Task<ActionResult<GenreDTO>> PutGenre(GenreDTO genre)
        {

            if (!ModelState.IsValid || String.IsNullOrEmpty(genre.Name))
            {
                return BadRequest();
            }
          
            await _genreService.UpdateGenre(genre);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<GenreDTO>> DeleteGenre(int id)
        {
            var genr = await _genreService.Get(id);
            var col = _mediaService.GetMediaList().Result.Where(x => x.Genre == genr.Name);
            foreach(var item in col) 
            {
                item.Genre = null;
                await _mediaService.UpdateMedia(item, null, null);
            }
            
            if (!ModelState.IsValid)
            {   
                return NotFound();
            }
            try
            {
                await _genreService.DeleteGenre(id);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            return Ok();
        }

    }
}
