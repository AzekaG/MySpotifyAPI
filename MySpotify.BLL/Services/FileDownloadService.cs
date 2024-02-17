using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MySpotify.BLL.Services
{
    public static class FileDownloadService //сюда скинуть нуэно будет полный путь к папке файла!. Т.е. _environment.WebRootPath + MediaPath
    {
      public static async Task UpLoadMedia(IFormFile? formFile, string MediaPath)
        {
            using (var fileStream = new FileStream(MediaPath, FileMode.Create))
            {

                await formFile.CopyToAsync(fileStream); 
            };


        }
    }
}
