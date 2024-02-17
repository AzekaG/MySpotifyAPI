using MySpotify.DAL.EF;
using MySpotify.DAL.Interfaces;
using MySpotify.DAL.Entities;
using MySpotify.Repository;
using Microsoft.EntityFrameworkCore;


namespace MySpotify.Repository
{
    public class MediaRepository : IRepositoryMedia<Media>
    {
        readonly MediaUserContext _mediauserContext;
        public MediaRepository(MediaUserContext mediaUserContext) => _mediauserContext = mediaUserContext;


        public async Task<IEnumerable<Media>> GetMediaList() => await _mediauserContext.Medias.Include(x => x.Genre).Include(p => p.User).ToListAsync();

        public async Task<Media> Get(int id)
        {
            var media =  await _mediauserContext.Medias.Include(x => x.Genre).Include(p => p.User).Where(x => x.Id == id).ToListAsync();
            Media? result = media.FirstOrDefault();
            return result;
        }

        public async Task<Media> Get(string name)
        {
            var media = await _mediauserContext.Medias.Where(x => x.Name == name).ToListAsync();
            Media? result = media.FirstOrDefault();
            return result;
        }



        public async Task Create(Media media) => await _mediauserContext.Medias.AddAsync(media);

        public async Task Delete(int id)
        {
            var media = await _mediauserContext.Medias.FindAsync(id);
            if (media != null)
                _mediauserContext.Medias.Remove(media);
            await _mediauserContext.SaveChangesAsync();
        }

        public void UpdateMedia(Media media)
        {
            _mediauserContext.Entry(media).State = EntityState.Modified;

            //в прошлом проекте были глюки , помогло решение : 
            /*try
        //    {
        //       await Task.Run(() => {
        //            var MediaSave = _mediauserContext.Medias.FirstOrDefault(x => x.Id == media.Id);
        //            MediaSave.Poster = media.Poster;
        //            MediaSave.FileAdress = media.FileAdress;
        //            MediaSave.Name = media.Name;
        //            MediaSave.Genre = media.Genre;
        //            MediaSave.Artist = media.Artist;
        //            MediaSave.TypeMedia = media.TypeMedia;
        //        });
        //      await _mediauserContext.SaveChangesAsync();

        //    }
        //    catch(Exception ex) 
        //    {
        //             Console.WriteLine("_____________________________" + ex.Message + "!!!");
        //    }
*/
        }



  

     

    }
}
