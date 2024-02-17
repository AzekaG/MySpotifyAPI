using Microsoft.EntityFrameworkCore;
using MySpotify.DAL.Entities;

namespace MySpotify.DAL.EF
{
    public class MediaUserContext : DbContext
    {   

        public DbSet<User> Users {  get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public MediaUserContext(DbContextOptions<MediaUserContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                /*login == azekaggg@gmail.com */
                /*pass == 1111*/
                

                Users?.Add(new User { FirstName = "Sergio", LastName = "Adminos", Email = "azekaggg@gmail.com", Status = Status.admin, Active = true , Password = "938E3782FCDEC612B9A5BB5FD9EB94E9", Salt = "4BFEA494ACEE92BB6ADBD53348F5CD52" });
                SaveChanges();

                Genres?.AddRange(new Genre { Name = "Pop" }, new Genre { Name = "Rock" }, new Genre { Name = "Rap" },
                    new Genre { Name = "Metal" }, new Genre { Name = "House" }, new Genre { Name = "Tehno" },
                    new Genre { Name = "Indi" }, new Genre { Name = "Lo-fi" }, new Genre { Name = "Reggae" });
                SaveChanges();

                Medias?.Add(new Media { Name = "Слышишь", Artist = "Dante", Genre = Genres?.First(x => x.Name == "Pop"), Poster = "/Poster/Hear.jpg", FileAdress = "/Media/Vladimir Dantes-Чуєш.mp3", User = Users.FirstOrDefault(x=>x.Status == Status.admin)});
                Medias?.Add(new Media { Name = "Ohne dich", Artist = "Rammstein", Genre = Genres?.First(x => x.Name == "Rock"), Poster = "/Poster/Rammstein.jpg", FileAdress = "Media/ramshtajn_rammstein_-_ohne_dich_(z3.fm).mp3" , User = Users.FirstOrDefault(x => x.Status == Status.admin) });
                Medias?.Add(new Media { Name = "Слышишь", Artist = "ДзиДзьо", Genre = Genres?.First(x => x.Name == "Pop"), Poster = "/Poster/DziDzio.jpg", FileAdress = "/Media/DziDzio-pavuk.mp3", User = Users.FirstOrDefault(x => x.Status == Status.admin) });

                SaveChanges();
            };
             
            
        }

    }
}
