using Microsoft.AspNetCore.Http;
using MySpotify.BLL.DTO;
using MySpotify.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpotify.BLL.Interfaces
{
    public interface IMediaService
    {
        Task<IEnumerable<MediaDTO>> GetMediaList();

        Task<MediaDTO> GetMedia(int id);
        Task<MediaDTO> GetMedia(string name);
        Task CreateMedia(MediaDTO media, IFormFile upPoster, IFormFile upMedia);
        Task DeleteMedia(int id);
        Task UpdateMedia(MediaDTO media, IFormFile upPoster, IFormFile upMedia);



    }
}
