using AutoMapper;
using MySpotify.BLL.DTO;
using MySpotify.BLL.Infrastructure;
using MySpotify.BLL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotify.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MySpotify.BLL.Services
{
    public class GenreService : IGenreService
    {

       IUnitOfWork Database {  get; set; }

       public GenreService(IUnitOfWork database) => Database = database;


       public async Task CreateGenre(GenreDTO genreDto)
        {
            var genre = new Genre
            { 
                Id = genreDto.Id,
                Name = genreDto.Name,
            };
           await Database.Genres.Create(genre);
           await Database.Save();

        }

        public async Task UpdateGenre(GenreDTO genreDto)
        {
            var genre = new Genre
            {
                Id = genreDto.Id,
                Name = genreDto.Name,
            };
            Database.Genres.Update(genre);
            await Database.Save();
        }
        public async Task DeleteGenre(int id)
        {
           await Database.Genres.Delete(id);
           await Database.Save();
        }
        public async Task<GenreDTO> Get(int id)
        {
            var genre = await Database.Genres.Get(id);
            if (genre == null)
                throw new ValidationException("Неверный жанр!", "");
            return new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public async Task<GenreDTO> Get(string name)
        {
            var genre = await Database.Genres.Get(name);
            if (genre == null)
                throw new ValidationException("Неверный жанр!", "");
            return new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
        public async Task<IEnumerable<GenreDTO>> GetGenres()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(await Database.Genres.GetGenresList());

        }
        public async Task<bool> GenreExist(int id)
        {
            return await Database.Genres.GenreExist(id);
        }
    }
}
