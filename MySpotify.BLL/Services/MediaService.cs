using AutoMapper;
using Microsoft.AspNetCore.Http;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Infrastructure;
using MySpotify.BLL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotify.DAL.Interfaces;



namespace MySpotify.BLL.Services
{
    public class MediaService : IMediaService

    {
       
        IUnitOfWork Database { get; set; }
        IGenreService GenreService { get; set; }
        public MediaService(IUnitOfWork unitOfWork, IGenreService genreService)
        {
             Database = unitOfWork;
             GenreService = genreService;
        }
        
        
        public async Task CreateMedia(MediaDTO mediaDto , IFormFile upPoster , IFormFile upMedia)
        {
            Genre? gnr = await Database.Genres.Get(mediaDto.Genre);
            if (gnr == null)
            {
                await GenreService.CreateGenre(new GenreDTO() { Name = mediaDto.Genre });
            }
            var Media = new Media
            {
                
                Id = mediaDto.Id,
                Name = mediaDto.Name,
                Artist = mediaDto.Artist,
                FileAdress = "/Media/" + mediaDto.FileAdress,
                Genre = await Database.Genres.Get(mediaDto.Genre),
                Poster = "/Poster/" + mediaDto.Poster,
                User = Database.Users.Get(mediaDto.UserId).Result,


            };
            await UpLoadMedia(upPoster, mediaDto.rootPath  + "/Poster/" + mediaDto.Poster);
            await UpLoadMedia(upMedia, mediaDto.rootPath + "/Media/" + mediaDto.FileAdress);
            await Database.Medias.Create(Media);
            await Database.Save();
        }

        async Task UpLoadMedia(IFormFile? formFile, string FullMediaPath)
        {
            
             await FileDownloadService.UpLoadMedia(formFile, FullMediaPath);
        }

        public async Task<IEnumerable<MediaDTO>> GetMediaList()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Media , MediaDTO>()
                                 .ForMember("UserId" , opt => opt.MapFrom(c=>c.User.id)));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Media>, IEnumerable<MediaDTO>>(await Database.Medias.GetMediaList());

        }

        public async Task<MediaDTO> GetMedia(int id)
        {
           var media = await Database.Medias.Get(id);
            if (media == null)
                throw new ValidationException("Неверный медиаФайл", "");
            return new MediaDTO()
            {
                Artist = media.Artist,
                Genre = media.Genre?.Name,
                FileAdress = media.FileAdress,
                Id = media.Id,
                Name = media.Name,
                Poster = media.Poster,
                UserId = media.User.id
                
            };
        }

        public async Task<MediaDTO> GetMedia(string name)
        {
            var media = await Database.Medias.Get(name);
            if (media == null) throw new ValidationException("Неверный медиаФайл", "");
            return new MediaDTO
            {
              
                Artist = media.Artist,
                Genre = media.Genre.Name,
                FileAdress = media.FileAdress,
                Id = media.Id,
                Name = media.Name,
                Poster = media.Poster,
                UserId = media.User.id
            };

        }
        public async Task DeleteMedia(int id)
        {
           await Database.Medias.Delete(id);
        }
        public async Task UpdateMedia(MediaDTO mediaDto, IFormFile upPoster, IFormFile upMedia)
        {

            if (upPoster != null) { await UpLoadMedia(upPoster, mediaDto.rootPath + mediaDto.Poster); }
            if (upMedia != null) { await UpLoadMedia(upMedia, mediaDto.rootPath + mediaDto.FileAdress); }
            Genre? gnr = await Database.Genres.Get(mediaDto.Genre);
            if(gnr == null)
            {
                await GenreService.CreateGenre(new GenreDTO() { Name = mediaDto.Genre });
            }
            var Media = new Media
            {
                Id = mediaDto.Id,
                Name = mediaDto.Name,
                Artist = mediaDto.Artist,
                FileAdress = mediaDto.FileAdress,
                Genre =  await Database.Genres.Get(mediaDto.Genre),
                Poster = mediaDto.Poster,
                User = Database.Users.Get(mediaDto.UserId).Result

            };
            Database.Medias.UpdateMedia(Media);
            await Database.Save();
        }
   

    }
}
