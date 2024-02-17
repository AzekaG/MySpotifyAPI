using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySpotify.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpotify.BLL.Infrastructure
{ // регистрация сервиса подключения к бд
    public static class MediaUserContextExtensions
    {
        public static void AddMediaUserContext(this IServiceCollection services , string connection) => services.AddDbContext<MediaUserContext>(options=> options.UseSqlServer(connection));
    }
}
